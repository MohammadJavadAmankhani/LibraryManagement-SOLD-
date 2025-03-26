using System;

namespace Library.Application.Dtos;

public class LoanDto
{
    public Guid LoanId { get; set; }
    public string BookTitle { get; set; }
    public string UserFullName { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
