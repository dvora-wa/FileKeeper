using System;
using System.Collections.Generic;

namespace FileKeeper_server_.net.Core.Entities
{
    public class Folder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public Guid? ParentFolderId { get; set; }
        public required int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual User? User { get; set; }
        public virtual Folder? ParentFolder { get; set; }
        public virtual ICollection<Folder> SubFolders { get; set; } = new List<Folder>();
        public virtual ICollection<FileItem> Files { get; set; } = new List<FileItem>();
    }
}