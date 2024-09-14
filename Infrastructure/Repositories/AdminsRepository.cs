using System.Globalization;
using Application.Abstractions.Repisitories;
using Application.Models.Admins;
using Npgsql;

namespace Infrastructure.Repositories;

public class AdminsRepository : IAdminRepository
{
    public async Task<Admin?> FindAdminByAdminId(string adminId, string password)
    {
        const string sql = """
                           
                                   SELECT AdminId, Password
                                   FROM admins
                                   WHERE AdminId = :adminId and Password = :password;
                               
                           """;

        const string connectionString =
            "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("adminid", adminId);
        command.Parameters.AddWithValue("password", password);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Admin(
                Convert.ToString(reader["adminid"], CultureInfo.InvariantCulture) ?? throw new InvalidOperationException(),
                Convert.ToString(reader["password"], CultureInfo.InvariantCulture) ?? throw new InvalidOperationException());
        }

        return null;
    }

    public async Task CreateNewAdmin(string adminId, string password)
    {
        const string searchSql = @"
                    SELECT COUNT(*)
                    FROM admins
                    WHERE adminId = :adminId;
                   ";

        const string insertSql = @"
                    INSERT INTO admins (Username, Password, Balance)
                    VALUES (:username, :password);
                   ";

        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=ivan1234;Database=postgres;";

        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        using var searchCommand = new NpgsqlCommand(searchSql, connection);
        searchCommand.Parameters.AddWithValue("adminid", adminId);
        bool userExists = (int)(await searchCommand.ExecuteScalarAsync() ?? 0) > 0;

        if (!userExists)
        {
            using var insertCommand = new NpgsqlCommand(insertSql, connection);
            insertCommand.Parameters.AddWithValue("adminid", adminId);
            insertCommand.Parameters.AddWithValue("password", password);

            await insertCommand.ExecuteNonQueryAsync();
        }
    }
}