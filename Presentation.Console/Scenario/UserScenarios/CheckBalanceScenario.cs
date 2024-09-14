using System.Globalization;
using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenario.UserScenarios;

public class CheckBalanceScenario : IScenario
{
    private readonly IUserService _service;

    public CheckBalanceScenario(IUserService service)
    {
        _service = service;
    }

    public string Name => "CheckBalance";
    public Task Run()
    {
        AnsiConsole.Write(_service.CheckYourBalance().ToString(CultureInfo.InvariantCulture));
        AnsiConsole.Console.Input.ReadKey(false);
        return Task.CompletedTask;
    }
}