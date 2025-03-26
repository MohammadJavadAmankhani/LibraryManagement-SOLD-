using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Library.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
