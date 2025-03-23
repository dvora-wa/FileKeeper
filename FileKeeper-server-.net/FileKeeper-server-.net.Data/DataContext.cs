using Microsoft.EntityFrameworkCore;
using FileKeeper_server_.net.Core.Entities;
//using FileKeeper_server_.net.Core.Interfaces;

namespace FileKeeper_server_.net.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ��� ���� ������ ������ ������ �� ����
        }
    }
}


