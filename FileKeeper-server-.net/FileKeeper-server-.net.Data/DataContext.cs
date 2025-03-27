using Microsoft.EntityFrameworkCore;
using FileKeeper_server_.net.Core.Entities;

namespace FileKeeper_server_.net.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FileItem> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // הגדרות קיימות ל-User
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .UseIdentityColumn();

            // הגדרות אינדקסים לטבלאות החדשות
            modelBuilder.Entity<Folder>()
                .HasIndex(f => new { f.UserId, f.ParentFolderId });

            modelBuilder.Entity<FileItem>()
                .HasIndex(f => new { f.UserId, f.FolderId });

            // הגדרת קשרים
            modelBuilder.Entity<Folder>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Folder>()
                .HasOne<Folder>()
                .WithMany(f => f.SubFolders)
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FileItem>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<FileItem>()
                .HasOne<Folder>()
                .WithMany(f => f.Files)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}