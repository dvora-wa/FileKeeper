using System;
using System.Collections.Generic;

namespace FileKeeper_server_.net.Core.Entities
{
    public class Folder
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentFolderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public virtual Folder? ParentFolder { get; set; }
        public virtual ICollection<Folder> SubFolders { get; set; } = new List<Folder>();
        public virtual ICollection<FileItem> Files { get; set; } = new List<FileItem>();
        public virtual User? User { get; set; }

        public Folder()
        {
            SubFolders = new List<Folder>();
            Files = new List<FileItem>();
        }
    }
}