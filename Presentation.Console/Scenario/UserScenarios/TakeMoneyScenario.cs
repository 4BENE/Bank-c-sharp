using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenario.UserScenarios;

public class TakeMoneyScenario : IScenario
{
    private readonly IUserService _service;

    public TakeMoneyScenario(IUserService service)
    {
        _service = service;
    }

    public string Name => "Take money";
    public async Task Run()
    {
        int money = AnsiConsole.Ask<int>("Enter sum");
        await _service.TakeMoney(money);
        AnsiConsole.WriteLine("Success");
    }
}