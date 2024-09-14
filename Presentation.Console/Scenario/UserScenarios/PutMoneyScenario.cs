using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenario.UserScenarios;

public class PutMoneyScenario : IScenario
{
    private readonly IUserService _service;

    public PutMoneyScenario(IUserService service)
    {
        _service = service;
    }

    public string Name => "Put money";
    public async Task Run()
    {
        int money = AnsiConsole.Ask<int>("Enter sum");
        await _service.PutMoney(money);
        AnsiConsole.WriteLine("Success");
    }
}