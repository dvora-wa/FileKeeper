namespace FileKeeper_server_.net.Core.DTOs
{
	public class CreateFolderDto
	{
		public required string Name { get; set; }
		public Guid? ParentFolderId { get; set; }
	}

	public class FolderDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public Guid? ParentFolderId { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public List<FolderDto> SubFolders { get; set; } = new();
		public List<FileItemDto> Files { get; set; } = new();
	}

	public class FileItemDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public string ContentType { get; set; } = string.Empty;
		public long Size { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}