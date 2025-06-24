using Microsoft.EntityFrameworkCore;
using ROHForum.Data.Models;

namespace ROHForum.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

        }

        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ModModel> Mods { get; set; }
        public DbSet<PostsModel> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CommentsModel>()
                    .HasOne(a => a.UserModel)
                    .WithMany(b => b.UserComments)
                    .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<CommentsModel>()
                .HasOne(a => a.PostsModel)
                .WithMany(b => b.PostComments)
                .HasForeignKey(b => b.PostId);



            modelBuilder.Entity<PostsModel>()
                    .HasOne(a => a.UserModel)
                    .WithMany(b => b.UserPosts)
                    .HasForeignKey(b => b.UserId);

        }

        }
}
