using System.Globalization;
using Application.Contracts.Users;
using Application.Models.Transactions;
using Spectre.Console;

namespace Presentation.Console.Scenario.UserScenarios;

public class GetMyTransactionsScenario : IScenario
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUserService;

    public GetMyTransactionsScenario(IUserService service, ICurrentUserService currentUserService)
    {
        _service = service;
        _currentUserService = currentUserService;
    }

    public string Name => "Check Transactions";

    public async Task Run()
    {
        IList<TransactionMy>? result = await _service.CheckMyTransactions();
        if (result != null)
        {
            foreach (TransactionMy x in result)
            {
                AnsiConsole.Write(x.Username);
                AnsiConsole.Write(x.Type.ToString());
                AnsiConsole.Write(x.Sum.ToString(CultureInfo.InvariantCulture));
                AnsiConsole.WriteLine();
            }
        }

        AnsiConsole.Console.Input.ReadKey(false);
    }
}