using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FileManager.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FileManager.DAL.Contexts
{
    public class FileManagerDbContext : IdentityDbContext<User>
    {
        public DbSet<File> Files { get; set; }
        
        public FileManagerDbContext()
            : base("DefaultConnection")
        {
            
        }

        public static FileManagerDbContext Create()
        {
            return new FileManagerDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");

            modelBuilder.Entity<User>()
                .HasMany(f => f.Files);

            modelBuilder.Entity<File>().ToTable("File")
                .HasRequired<User>(u => u.User);
        }
    }
}
