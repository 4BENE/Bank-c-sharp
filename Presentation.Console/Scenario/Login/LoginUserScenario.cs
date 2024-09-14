using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenario.Login;

public class LoginUserScenario : IScenario
{
    private readonly IUserService _userService;
    public LoginUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "LoginUser";
    public async Task Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");

        Task<LoginResult> result = _userService.Login(username, password);

        string message = await result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.NotFound => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
    }
}