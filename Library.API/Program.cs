using Library.Application.Commands;
using Library.Application.Dtos;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Add DbContext
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite("Data Source=library.db"));


// 💉 Dependency Injection
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

builder.Services.AddScoped<LoanService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

    context.Database.Migrate();

    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new Book("The Pragmatic Programmer", "Andy Hunt", 3),
            new Book("Clean Code", "Robert C. Martin", 5),
            new Book("Domain-Driven Design", "Eric Evans", 2)
        );
    }

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User("Ali Rezaei"),
            new User("Sara Ahmadi"),
            new User("Reza Mohammadi")
        );
    }

    await context.SaveChangesAsync();
}

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
