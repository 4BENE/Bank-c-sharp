using Application.Models.Transactions;

namespace Application.Contracts.Users;

public interface IUserService
{
    Task<LoginResult> Login(string username, string password);
    public Task TakeMoney(int money);
    public Task PutMoney(int money);
    public Task<IList<TransactionMy>?> CheckMyTransactions();
    public Task CreateAccount(string userName, string password);
    public int CheckYourBalance();
}