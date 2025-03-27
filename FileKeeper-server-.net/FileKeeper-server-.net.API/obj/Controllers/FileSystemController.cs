using FileKeeper_server_.net.Core.Interfaces.Services;
using FileKeeper_server_.net.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileSystemController : ControllerBase
    {
        private readonly IFileSystemService _fileSystemService;

        public FileSystemController(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        [HttpGet("folders")]
        public async Task<ActionResult<List<FolderDto>>> GetFolders([FromQuery] Guid? parentFolderId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var folders = await _fileSystemService.GetUserFoldersAsync(userId, parentFolderId);
            return Ok(folders);
        }

        [HttpPost("folders")]
        public async Task<ActionResult<FolderDto>> CreateFolder([FromBody] CreateFolderDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var folder = await _fileSystemService.CreateFolderAsync(dto, userId);
            return Ok(folder);
        }

        [HttpGet("files/upload-url")]
        public async Task<ActionResult<string>> GetUploadUrl([FromQuery] string fileName, [FromQuery] Guid folderId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = await _fileSystemService.GetUploadUrlAsync(fileName, folderId, userId);
            return Ok(url);
        }

        [HttpGet("files/{fileId}/download-url")]
        public async Task<ActionResult<string>> GetDownloadUrl(Guid fileId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = await _fileSystemService.GetDownloadUrlAsync(fileId, userId);
            return Ok(url);
        }

        [HttpDelete("folders/{folderId}")]
        public async Task<ActionResult> DeleteFolder(Guid folderId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _fileSystemService.DeleteFolderAsync(folderId, userId);
            return Ok();
        }

        [HttpDelete("files/{fileId}")]
        public async Task<ActionResult> DeleteFile(Guid fileId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _fileSystemService.DeleteFileAsync(fileId, userId);
            return Ok();
        }
    }
}