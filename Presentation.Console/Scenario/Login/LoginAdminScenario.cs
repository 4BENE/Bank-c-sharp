using Application.Contracts.Admin;
using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenario.Login;

public class LoginAdminScenario : IScenario
{
    private readonly IAdminService _adminService;
    public LoginAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "LoginAdmin";
    public async Task Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");

        Task<LoginResult> result = _adminService.Login(username, password);

        string message = await result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.NotFound => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
    }
}