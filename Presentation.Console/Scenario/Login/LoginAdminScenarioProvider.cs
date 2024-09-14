using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Admin;
using Application.Contracts.Users;

namespace Presentation.Console.Scenario.Login;

public class LoginAdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _serviceAdmin;
    private readonly ICurrentUserService _serviceUser;

    public LoginAdminScenarioProvider(IAdminService service, ICurrentAdminService serviceAdmin, ICurrentUserService serviceUser)
    {
        _service = service;
        _serviceAdmin = serviceAdmin;
        _serviceUser = serviceUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_serviceAdmin.Admin is not null || _serviceUser.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAdminScenario(_service);
        return true;
    }
}