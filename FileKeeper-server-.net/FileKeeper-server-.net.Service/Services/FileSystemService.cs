using Amazon.S3;
using Amazon.S3.Model;
using FileKeeper_server_.net.Core.DTOs;
using FileKeeper_server_.net.Core.Entities;
using FileKeeper_server_.net.Core.Interfaces.Services;
using FileKeeper_server_.net.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Amazon.Runtime;

namespace FileKeeper_server_.net.Service.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public FileSystemService(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;

            var credentials = new BasicAWSCredentials(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"]
            );

            _s3Client = new AmazonS3Client(credentials, RegionEndpoint.GetBySystemName(configuration["AWS:Region"]));
            _bucketName = configuration["AWS:BucketName"];
        }

        // קבלת כל התיקיות של המשתמש בתיקייה מסוימת
        public async Task<IEnumerable<FolderDto>> GetUserFoldersAsync(int userId, Guid? parentFolderId)
        {
            var folders = await _context.Folders
                .Include(f => f.SubFolders)
                .Include(f => f.Files)
                .Where(f => f.UserId == userId && f.ParentFolderId == parentFolderId)
                .ToListAsync();

            return folders.Select(f => new FolderDto
            {
                Id = f.Id,
                Name = f.Name,
                ParentFolderId = f.ParentFolderId,
                UserId = f.UserId,
                CreatedAt = f.CreatedAt,
                SubFolders = f.SubFolders.Select(sf => new FolderDto
                {
                    Id = sf.Id,
                    Name = sf.Name,
                    ParentFolderId = sf.ParentFolderId,
                    UserId = sf.UserId,
                    CreatedAt = sf.CreatedAt
                }).ToList(),
                Files = f.Files.Select(file => new FileItemDto
                {
                    Id = file.Id,
                    Name = file.Name,
                    ContentType = file.ContentType,
                    Size = file.Size,
                    CreatedAt = file.CreatedAt
                }).ToList()
            }).ToList();
        }

        // יצירת תיקייה חדשה
        public async Task<FolderDto> CreateFolderAsync(CreateFolderDto dto, int userId)
        {
            if (dto.ParentFolderId.HasValue)
            {
                var parentExists = await _context.Folders
                    .AnyAsync(f => f.Id == dto.ParentFolderId && f.UserId == userId);
                if (!parentExists)
                {
                    throw new InvalidOperationException("תיקיית האב לא נמצאה");
                }
            }

            var folder = new Folder
            {
                Name = dto.Name,
                ParentFolderId = dto.ParentFolderId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return new FolderDto
            {
                Id = folder.Id,
                Name = folder.Name,
                ParentFolderId = folder.ParentFolderId,
                UserId = folder.UserId,
                CreatedAt = folder.CreatedAt,
                SubFolders = new List<FolderDto>(),
                Files = new List<FileItemDto>()
            };
        }

        // קבלת URL להעלאת קובץ ל-S3
        public async Task<string> GetUploadUrlAsync(string fileName, Guid folderId, int userId)
        {
            var folderExists = await _context.Folders
                .AnyAsync(f => f.Id == folderId && f.UserId == userId);
            if (!folderExists)
            {
                throw new InvalidOperationException("התיקייה לא נמצאה");
            }

            var key = $"{userId}/{folderId}/{Guid.NewGuid()}-{fileName}";

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            return await Task.FromResult(_s3Client.GetPreSignedURL(request));
        }

        // קבלת URL להורדת קובץ מ-S3
        public async Task<string> GetDownloadUrlAsync(Guid fileId, int userId)
        {
            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == userId);

            if (file == null)
            {
                throw new InvalidOperationException("הקובץ לא נמצא");
            }

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = file.S3Key,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            return await Task.FromResult(_s3Client.GetPreSignedURL(request));
        }

        // מחיקת תיקייה
        public async Task DeleteFolderAsync(Guid folderId, int userId)
        {
            var folder = await _context.Folders
                .Include(f => f.Files)
                .Include(f => f.SubFolders)
                .FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);

            if (folder == null)
            {
                throw new InvalidOperationException("התיקייה לא נמצאה");
            }

            foreach (var file in folder.Files)
            {
                await DeleteFileFromS3(file.S3Key);
            }

            foreach (var subFolder in folder.SubFolders)
            {
                await DeleteFolderAsync(subFolder.Id, userId);
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }

        // מחיקת קובץ
        public async Task DeleteFileAsync(Guid fileId, int userId)
        {
            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == userId);

            if (file == null)
            {
                throw new InvalidOperationException("הקובץ לא נמצא");
            }

            await DeleteFileFromS3(file.S3Key);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteFileFromS3(string s3Key)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = s3Key
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}