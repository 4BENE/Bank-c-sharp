using System.Globalization;
using Application.Contracts.Admin;
using Application.Models.Transactions;
using Spectre.Console;

namespace Presentation.Console.Scenario.AdminScenarios;

public class CheckUserTransactionsScenario : IScenario
{
    private readonly IAdminService _adminService;

    public CheckUserTransactionsScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Check user Transactions";
    public async Task Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");

        IList<TransactionMy>? result = await _adminService.CheckUserTransactions(username);
        if (result != null)
        {
            foreach (TransactionMy x in result)
            {
                AnsiConsole.Write(x.Username);
                AnsiConsole.Write(x.Type.ToString());
                AnsiConsole.Write(x.Sum.ToString(CultureInfo.InvariantCulture));
                AnsiConsole.WriteLine();
            }

            AnsiConsole.Console.Input.ReadKey(false);
        }
    }
}