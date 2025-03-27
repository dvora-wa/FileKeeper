using FileKeeper_server_.net.Core.DTOs;

namespace FileKeeper_server_.net.Core.Interfaces.Services
{
    public interface IFileSystemService
    {
        Task<FolderDto> CreateFolderAsync(CreateFolderDto dto, int userId);
        Task<IEnumerable<FolderDto>> GetUserFoldersAsync(int userId, Guid? parentFolderId);
        Task<string> GetUploadUrlAsync(string fileName, Guid folderId, int userId);
        Task<string> GetDownloadUrlAsync(Guid fileId, int userId);
        Task DeleteFolderAsync(Guid folderId, int userId);
        Task DeleteFileAsync(Guid fileId, int userId);
    }
}