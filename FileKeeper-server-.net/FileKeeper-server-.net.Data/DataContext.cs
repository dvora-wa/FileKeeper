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

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Folder Configuration
            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasKey(e => e.Id);

                // קשר למשתמש
                entity.HasOne(f => f.User)
                    .WithMany(u => u.Folders)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // קשר לתיקיית אב
                entity.HasOne(f => f.ParentFolder)
                    .WithMany(f => f.SubFolders)
                    .HasForeignKey(f => f.ParentFolderId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // FileItem Configuration
            modelBuilder.Entity<FileItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                // קשר למשתמש
                entity.HasOne(f => f.User)
                    .WithMany(u => u.Files)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // קשר לתיקייה
                entity.HasOne(f => f.Folder)
                    .WithMany(f => f.Files)
                    .HasForeignKey(f => f.FolderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}