using System;
using System.Collections.Generic;

namespace FileKeeper_server_.net.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
        public virtual ICollection<FileItem> Files { get; set; } = new List<FileItem>();
    }
}