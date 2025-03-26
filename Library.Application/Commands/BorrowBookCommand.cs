using System;

namespace Library.Application.Commands;

public class BorrowBookCommand
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}
