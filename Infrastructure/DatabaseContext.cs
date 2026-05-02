using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.Helper;
using Microsoft.EntityFrameworkCore;

namespace helpdesk.infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    public DbSet<FileMetaData> Files => Set<FileMetaData>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketCategory>().HasData(
            new TicketCategory { ID = 1, Title = "Complaints" },
            new TicketCategory { ID = 2, Title = "Data Support" },
            new TicketCategory { ID = 3, Title = "Outlet Request Support" }
        );
        modelBuilder.Entity<User>().HasData(
            new User { UserId = "Admin-userID", Email = "user@admin.com", Password = "adminpass", FullName = "John Doe", Role = "Admin" }
        );
    }
}