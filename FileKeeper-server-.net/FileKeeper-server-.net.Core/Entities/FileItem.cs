using System;

namespace FileKeeper_server_.net.Core.Entities
{
	public class FileItem
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public required string S3Key { get; set; }
		public required string ContentType { get; set; }
		public long Size { get; set; }
		public Guid FolderId { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation Properties
		public virtual Folder? Folder { get; set; }
		public virtual User? User { get; set; }
	}
}