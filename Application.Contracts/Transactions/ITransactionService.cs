using Application.Models.Transactions;

namespace Application.Contracts.Transactions;

public interface ITransactionService
{
    public Task<IList<TransactionMy>> GetAllTransactions(string userName);
}