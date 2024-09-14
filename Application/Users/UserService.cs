using Application.Abstractions.Repisitories;
using Application.Contracts.Exceptions;
using Application.Contracts.Users;
using Application.Models.Transactions;
using Application.Models.Users;

namespace Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserManager;
    private readonly ITransactionRepository _transactionRepository;

    public UserService(IUserRepository userRepository, ICurrentUserService currentUserManager, ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _currentUserManager = currentUserManager;
        _transactionRepository = transactionRepository;
    }

    public async Task<LoginResult> Login(string username, string password)
    {
        User? user = await _userRepository.FindUserByUsername(username, password);

        if (user is null)
        {
            return new LoginResult.NotFound();
        }

        _currentUserManager.User = user;
        return new LoginResult.Success();
    }

    public async Task TakeMoney(int money)
    {
        if (money < 0)
        {
            throw new NegativeSumException("you cant take negative sum");
        }

        if (_currentUserManager.User != null && money > _currentUserManager.User.Money)
        {
            throw new MinusBalanceException("you want take more than you have");
        }

        if (_currentUserManager.User != null) _currentUserManager.User.Money -= money;
        if (_currentUserManager.User != null)
        {
            await _userRepository.TakeMoney(_currentUserManager.User.Id, _currentUserManager.User.Money, money);
        }
    }

    public async Task PutMoney(int money)
    {
        if (money < 0)
        {
            throw new NegativeSumException("you cant put negative sum");
        }

        if (_currentUserManager.User != null) _currentUserManager.User.Money += money;
        if (_currentUserManager.User != null)
        {
            await _userRepository.PutMoney(_currentUserManager.User.Id, _currentUserManager.User.Money, money);
        }
    }

    public Task<IList<TransactionMy>?> CheckMyTransactions()
    {
        if (_currentUserManager.User != null)
        {
           return _transactionRepository.FindTransactionsByUser(_currentUserManager.User.Id);
        }

        throw new WrongUserNameException();
    }

    public async Task CreateAccount(string userName, string password)
    {
        await _userRepository.CreateNewUser(userName, password);
    }

    public int CheckYourBalance()
    {
        if (_currentUserManager.User == null)
        {
            throw new ArgumentNullException();
        }

        return _currentUserManager.User.Money;
    }
}