using Library.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using System;
using Xunit;

namespace Library.Tests.Services;

public class LoanServiceTests
{
    private readonly Mock<IBookRepository> _bookRepoMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<ILoanRepository> _loanRepoMock = new();

    private readonly LoanService _service;

    public LoanServiceTests()
    {
        _service = new LoanService(
            _bookRepoMock.Object,
            _userRepoMock.Object,
            _loanRepoMock.Object
        );
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldSucceed_WhenBookAndUserExist()
    {
        // Arrange
        var book = new Book("Test Book", "Author", 5);
        var user = new User("Test User");

        _bookRepoMock.Setup(x => x.GetByIdAsync(book.Id)).ReturnsAsync(book);
        _userRepoMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);

        // Act
        await _service.BorrowBookAsync(book.Id, user.Id);

        // Assert
        Assert.Equal(4, book.AvailableCopies);

        _loanRepoMock.Verify(x => x.AddAsync(It.IsAny<Loan>()), Times.Once);
        _bookRepoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _loanRepoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task BorrowBookAsync_ShouldThrow_WhenBookNotFound()
    {
        _bookRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Book?)null);

        var ex = await Assert.ThrowsAsync<Exception>(() =>
            _service.BorrowBookAsync(Guid.NewGuid(), Guid.NewGuid())
        );

        Assert.Equal("Book not found", ex.Message);
    }

    [Fact]
    public async Task ReturnBookAsync_ShouldSucceed_WhenLoanExists()
    {
        // Arrange
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid());
        var book = new Book("Test Book", "Author", 3);

        _loanRepoMock.Setup(x => x.GetByIdAsync(loan.Id)).ReturnsAsync(loan);
        _bookRepoMock.Setup(x => x.GetByIdAsync(loan.BookId)).ReturnsAsync(book);

        // Act
        await _service.ReturnBookAsync(loan.Id);

        // Assert
        Assert.True(loan.IsReturned);
        Assert.Equal(4, book.AvailableCopies);
    }
}
