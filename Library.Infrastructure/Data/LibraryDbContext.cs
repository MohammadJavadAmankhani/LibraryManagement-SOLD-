using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Loan>().HasKey(l => l.Id);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.BookId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId);
    }

}
