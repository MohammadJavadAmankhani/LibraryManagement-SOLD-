using System;

namespace Library.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int TotalCopies { get; private set; }
    public int AvailableCopies { get; private set; }

    private Book() { }

    public Book(string title, string author, int totalCopies)
    {
        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
    }

    public void Borrow()
    {
        if (AvailableCopies <= 0)
            throw new InvalidOperationException("No copies available.");

        AvailableCopies--;
    }

    public void Return()
    {
        AvailableCopies++;
    }
}
