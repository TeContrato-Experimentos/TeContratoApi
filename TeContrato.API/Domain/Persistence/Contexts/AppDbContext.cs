using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeContrato.API.Extensions;
using TeContrato.API.Domain.Models;

namespace TeContrato.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } //Set = Conjunto. Por eso recomienda Categories, porque las tablas (categories), representa un conjunto
        public DbSet<Client> Clients { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectControl> ProjectControls { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Status> Status { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>().ToTable("User");
            builder.Entity<Client>().ToTable("Clients").HasBaseType<User>();
            builder.Entity<Technician>().ToTable("Technicians").HasBaseType<User>();
            builder.Entity<City>().ToTable("Cities");
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Posts>().ToTable("Posts");
            builder.Entity<ProjectControl>().ToTable("ProjectControls");
            builder.Entity<Status>().ToTable("Status");
            builder.Entity<Budget>().ToTable("Budget");

            builder.Entity<Status>().HasKey(p => p.CStatus);
            builder.Entity<Status>().Property(p => p.NStatus);
            builder.Entity<Status>()
                .HasMany(p => p.CProjectControls)
                .WithOne(p => p.CStatus)
                .HasForeignKey(p => p.CStatusId);

            builder.Entity<Budget>().HasKey(p => p.CBudget);
            builder.Entity<Budget>().Property(p => p.DFecha);
            builder.Entity<Budget>().Property(p => p.MMonto);
            builder.Entity<Budget>().Property(p => p.TDescription);

            builder.Entity<Budget>()
                .HasMany(p => p.CProject)
                .WithOne(p => p.CBudget)
                .HasForeignKey(p => p.BudgetId);

            builder.Entity<User>().HasKey(p => p.Cuser);
            builder.Entity<User>().Property(p => p.Cuser).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Cpassword).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Temail).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Cdni).IsRequired().HasMaxLength(8);
            builder.Entity<User>().Property(p => p.Nname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Nlastname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.is_admin).IsRequired();

            builder.Entity<User>()
                .HasMany(p => p.Posts)
                .WithOne(q => q.CUser)
                .HasForeignKey(post => post.UserId);

            builder.Entity<Client>().Property(p => p.TBio).IsRequired().HasMaxLength(50);
            builder.Entity<Client>().Property(p => p.TAddress).IsRequired().HasMaxLength(50);
            builder.Entity<Client>().Property(p => p.Nlastname);
            builder.Entity<Client>()
                .HasOne(p => p.CCity)
                .WithMany(q => q.CClients)
                .HasForeignKey(key => key.CityId);



            builder.Entity<City>().HasKey(p => p.CCity);
            builder.Entity<City>().Property(p => p.NCity).IsRequired().HasMaxLength(30);

            builder.Entity<City>().HasData
            (new City
                {
                    CCity = 1,
                    NCity = "Lima"
                }
            );

            builder.Entity<Technician>().Property(p => p.TBio);
            builder.Entity<Technician>().Property(p => p.NEducation).HasMaxLength(50);
            builder.Entity<Technician>().Property(p => p.Numphone).HasMaxLength(50);

            builder.Entity<Project>().HasKey(p => p.Cproject);
            builder.Entity<Project>().Property(p => p.Nproject);
            builder.Entity<Project>().Property(p => p.Created_at);
            builder.Entity<Project>().Property(p => p.Tdescription);
            builder.Entity<Project>().Property(p => p.Mbudget);
            builder.Entity<Project>()
                .HasOne(p => p.CTechnician)
                .WithMany(q => q.CProjects)
                .HasForeignKey(id => id.TechnicianId);
            builder.Entity<Project>()
                .HasOne(p => p.CClient)
                .WithMany(q => q.CProjects)
                .HasForeignKey(id => id.ClientId);
            builder.Entity<Project>()
                .HasOne(p => p.CBudget)
                .WithMany(p => p.CProject)
                .HasForeignKey(p => p.BudgetId);

            builder.Entity<Posts>().HasKey(p => p.Cposts);
            builder.Entity<Posts>().Property(p => p.Ntitle);
            builder.Entity<Posts>().Property(p => p.Tbody);
            builder.Entity<Posts>().Property(p => p.Created_at);
            builder.Entity<Posts>().Property(p => p.Mbudget);
            builder.Entity<Posts>().Property(p => p.Views);
            builder.Entity<Posts>().Property(p => p.Pic);
            
            builder.Entity<ProjectControl>().HasKey(p => p.Ccontrol);
            builder.Entity<ProjectControl>().Property(p => p.Nproject);
            builder.Entity<ProjectControl>().Property(p => p.Dlastedited);
            builder.Entity<ProjectControl>().Property(p => p.Mbudget);
            builder.Entity<ProjectControl>().Property(p => p.Qprogress);

            builder.Entity<ProjectControl>()
                .HasOne(p => p.CProject)
                .WithOne(p => p.CProjectControl);
            
        }
    }
}
