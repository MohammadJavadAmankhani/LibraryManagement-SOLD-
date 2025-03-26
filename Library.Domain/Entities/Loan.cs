using System;

namespace Library.Domain.Entities;

public class Loan
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime BorrowedAt { get; private set; }
    public DateTime? ReturnedAt { get; private set; }

    public bool IsReturned => ReturnedAt.HasValue;

    public Book? Book { get; set; }
    public User? User { get; set; }

    public Loan(Guid bookId, Guid userId)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        UserId = userId;
        BorrowedAt = DateTime.UtcNow;
    }

    public void ReturnBook()
    {
        if (IsReturned)
            throw new InvalidOperationException("Book already returned.");

        ReturnedAt = DateTime.UtcNow;
    }
}
