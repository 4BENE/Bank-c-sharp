using Application.Abstractions.Repisitories;
using Application.Contracts.Admin;
using Application.Contracts.Exceptions;
using Application.Contracts.Users;
using Application.Models.Admins;
using Application.Models.Transactions;

namespace Application.Admins;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminsRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly CurrentAdminManager _currentAdminManager;

    public AdminService(IAdminRepository adminsRepository, ITransactionRepository transactionRepository, CurrentAdminManager currentAdminManager)
    {
        _adminsRepository = adminsRepository;
        _transactionRepository = transactionRepository;
        _currentAdminManager = currentAdminManager;
    }

    public async Task<LoginResult> Login(string userName, string password)
    {
        Admin? admin = await _adminsRepository.FindAdminByAdminId(userName, password);

        if (admin is null)
        {
            return new LoginResult.NotFound();
        }

        _currentAdminManager.Admin = admin;
        return new LoginResult.Success();
    }

    public Task<IList<TransactionMy>?> CheckUserTransactions(string userName)
    {
        if (userName != null)
        {
            return _transactionRepository.FindTransactionsByUser(userName);
        }

        throw new WrongUserNameException("wrong username");
    }
}