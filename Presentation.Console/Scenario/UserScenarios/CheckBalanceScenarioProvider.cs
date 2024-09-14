using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Admin;
using Application.Contracts.Users;

namespace Presentation.Console.Scenario.UserScenarios;

public class CheckBalanceScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentAdminService _serviceAdmin;
    private readonly ICurrentUserService _serviceUser;

    public CheckBalanceScenarioProvider(IUserService service, ICurrentAdminService serviceAdmin, ICurrentUserService serviceUser)
    {
        _service = service;
        _serviceAdmin = serviceAdmin;
        _serviceUser = serviceUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_serviceAdmin.Admin is not null || _serviceUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new CheckBalanceScenario(_service);
        return true;
    }
}