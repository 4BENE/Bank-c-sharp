using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenario.SignUp;

public class SignUpUserScenario : IScenario
{
    private readonly IUserService _userService;

    public SignUpUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Sign up User";
    public async Task Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");
        await _userService.CreateAccount(username, password);
        AnsiConsole.WriteLine("Success");
    }
}