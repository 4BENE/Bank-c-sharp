using Application.Models.Transactions;

namespace Application.Abstractions.Repisitories;

public interface ITransactionRepository
{
    Task<IList<TransactionMy>?> FindTransactionsByUser(string username);
}