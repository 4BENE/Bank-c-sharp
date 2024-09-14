using Application.Contracts.Users;
using Application.Models.Transactions;

namespace Application.Contracts.Admin;

public interface IAdminService
{
    Task<LoginResult> Login(string userName, string password);
    Task<IList<TransactionMy>?> CheckUserTransactions(string userName);
}