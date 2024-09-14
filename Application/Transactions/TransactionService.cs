using Application.Abstractions.Repisitories;
using Application.Contracts.Exceptions;
using Application.Contracts.Transactions;
using Application.Models.Transactions;

namespace Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<TransactionMy>> GetAllTransactions(string userName)
    {
        IList<TransactionMy>? allTransactions = await _repository.FindTransactionsByUser(userName);

        if (allTransactions is null)
        {
            throw new WrongUserNameException("Cant find transaction by this username");
        }

        return allTransactions;
    }
}