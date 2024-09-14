using System.Globalization;
using Application.Abstractions.Repisitories;
using Application.Models.Users;
using Npgsql;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<User?> FindUserByUsername(string username, string password)
    {
        const string sql = """
                           
                                   SELECT Username, Password, balance
                                   FROM Users
                                   WHERE Username = :username and Password = :password;
                               
                           """;

        const string connectionString =
            "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("Username", username);
        command.Parameters.AddWithValue("Password", password);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User(
                Convert.ToString(reader["Username"], CultureInfo.InvariantCulture) ?? throw new InvalidOperationException(),
                Convert.ToString(reader["Password"], CultureInfo.InvariantCulture) ?? throw new InvalidOperationException(),
                Convert.ToInt32(reader["balance"], CultureInfo.InvariantCulture));
        }

        return null;
    }

    public async Task CreateNewUser(string username, string password)
    {
        const string searchSql = @"
                    SELECT COUNT(*)
                    FROM users
                    WHERE Username = :username;
                   ";

        const string insertSql = @"
                    INSERT INTO users (Username, Password, Balance)
                    VALUES (:username, :password, 0);
                   ";

        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var searchCommand = new NpgsqlCommand(searchSql, connection);
        searchCommand.Parameters.AddWithValue("Username", username);
        bool userExists = (long)(await searchCommand.ExecuteScalarAsync() ?? 0) > 0;

        if (!userExists)
        {
            using var insertCommand = new NpgsqlCommand(insertSql, connection);
            insertCommand.Parameters.AddWithValue("Username", username);
            insertCommand.Parameters.AddWithValue("Password", password);
            insertCommand.Parameters.AddWithValue("Balance", 0);

            await insertCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task PutMoney(string username, decimal balance, decimal money)
    {
        const string searchSql = @"
                    SELECT COUNT(*)
                    FROM users
                    WHERE Username = :username;
                   ";
        const string updateSql = @"
                    UPDATE users
                    SET Balance = :balance
                    WHERE Username = :username;
                   ";
        const string insertSql = @"
                    INSERT INTO transactions (Username, type, sum)
                    VALUES (:username,'Put', :money);
                   ";

        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var insertCommand = new NpgsqlCommand(insertSql, connection);
        using var searchCommand = new NpgsqlCommand(searchSql, connection);
        searchCommand.Parameters.AddWithValue("username", username);
        insertCommand.Parameters.AddWithValue("username", username);
        insertCommand.Parameters.AddWithValue("money", money);
        bool userExists = (long)(await searchCommand.ExecuteScalarAsync() ?? 0) > 0;

        if (userExists)
        {
            using var updateCommand = new NpgsqlCommand(updateSql, connection);
            updateCommand.Parameters.AddWithValue("username", username);
            updateCommand.Parameters.AddWithValue("balance", balance + money);

            await updateCommand.ExecuteNonQueryAsync();
            await insertCommand.ExecuteNonQueryAsync();
        }
    }

    public async Task TakeMoney(string username, decimal balance, decimal money)
    {
        const string searchSql = @"
                    SELECT COUNT(*)
                    FROM users
                    WHERE Username = :username;
                   ";

        const string updateSql = @"
                    UPDATE users
                    SET Balance = :balance
                    WHERE Username = :username;
                   ";
        const string insertSql = @"
                    INSERT INTO transactions (username, type, sum)
                    VALUES (:username,'Take', :money);
                   ";

        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var insertCommand = new NpgsqlCommand(insertSql, connection);
        using var searchCommand = new NpgsqlCommand(searchSql, connection);
        searchCommand.Parameters.AddWithValue("username", username);
        insertCommand.Parameters.AddWithValue("username", username);
        insertCommand.Parameters.AddWithValue("money", money);
        bool userExists = (long)(await searchCommand.ExecuteScalarAsync() ?? 0) > 0;

        if (userExists)
        {
            using var updateCommand = new NpgsqlCommand(updateSql, connection);
            updateCommand.Parameters.AddWithValue("username", username);
            updateCommand.Parameters.AddWithValue("balance", balance - money);

            await updateCommand.ExecuteNonQueryAsync();
            await insertCommand.ExecuteNonQueryAsync();
        }
    }
}