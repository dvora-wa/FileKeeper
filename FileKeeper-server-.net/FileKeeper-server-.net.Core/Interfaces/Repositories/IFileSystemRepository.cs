using FileKeeper_server_.net.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileKeeper_server_.net.Core.Interfaces.Repositories
{
    public interface IFileSystemRepository
    {
        Task<Folder> CreateFolderAsync(Folder folder);
        Task<IEnumerable<Folder>> GetUserFoldersAsync(int userId, Guid? parentFolderId);  // שינוי מ-Guid ל-int
        Task<FileItem> SaveFileInfoAsync(FileItem file);
        Task DeleteFolderAsync(Guid folderId, int userId);  // שינוי מ-Guid ל-int
        Task DeleteFileAsync(Guid fileId, int userId);  // שינוי מ-Guid ל-int
        Task<bool> ValidateUserAccessAsync(Guid resourceId, int userId);  // שינוי מ-Guid ל-int
        Task<Folder> GetFolderAsync(Guid folderId, int userId);  // שינוי מ-Guid ל-int
        Task<FileItem> GetFileAsync(Guid fileId, int userId);  // שינוי מ-Guid ל-int
    }
}