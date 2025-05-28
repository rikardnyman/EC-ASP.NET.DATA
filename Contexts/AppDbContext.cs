using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
    {
        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<StatusEntity> Statuses { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusEntity>().HasData(
                new StatusEntity { StatusId = 1, StatusName = "Started" },
                new StatusEntity { StatusId = 2, StatusName = "Completed" }
            );
            modelBuilder.Entity<ClientEntity>().HasData(
                new ClientEntity { Id = 1, Name = "Client 1" },
                new ClientEntity { Id = 2, Name = "Client 2" }
            );


        }
    }


}
