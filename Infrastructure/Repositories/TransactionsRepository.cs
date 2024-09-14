using System.Globalization;
using Application.Abstractions.Repisitories;
using Application.Contracts.Exceptions;
using Application.Models.Transactions;
using Npgsql;

namespace Infrastructure.Repositories;

public class TransactionsRepository : ITransactionRepository
{
    public async Task<IList<TransactionMy>?> FindTransactionsByUser(string username)
    {
        const string sql = """
                           
                                   SELECT *
                                   FROM Transactions
                                   WHERE Username = :username;
                               
                           """;

        const string connectionString =
            "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("username", username);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        IList<TransactionMy> answer = new List<TransactionMy>();
        while (await reader.ReadAsync())
        {
            answer.Add(
                new TransactionMy(
                    Convert.ToString(reader["username"], CultureInfo.InvariantCulture) ??
                    throw new InvalidOperationException(),
                    Convert.ToString(reader["type"], CultureInfo.InvariantCulture) == "Put" ? TransactionType.Put : TransactionType.Take,
                    Convert.ToInt32(reader["sum"], CultureInfo.InvariantCulture)));
        }

        if (answer.Count == 0)
        {
            throw new WrongUserNameException("Cant found operations");
        }

        return answer;
    }
}