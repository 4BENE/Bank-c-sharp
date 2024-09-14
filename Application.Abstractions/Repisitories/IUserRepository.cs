using Application.Models.Users;

namespace Application.Abstractions.Repisitories;

public interface IUserRepository
{
    Task<User?> FindUserByUsername(string username, string password);
    Task CreateNewUser(string username, string password);
    public Task PutMoney(string username, decimal balance, decimal money);
    public Task TakeMoney(string username, decimal balance, decimal money);
}