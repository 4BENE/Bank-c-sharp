using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Admin;
using Application.Contracts.Users;

namespace Presentation.Console.Scenario.AdminScenarios;

public class CheckUserTransactionsScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _serviceAdmin;
    private readonly ICurrentUserService _serviceUser;

    public CheckUserTransactionsScenarioProvider(IAdminService service, ICurrentAdminService serviceAdmin, ICurrentUserService serviceUser)
    {
        _service = service;
        _serviceAdmin = serviceAdmin;
        _serviceUser = serviceUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_serviceAdmin.Admin is null || _serviceUser.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new CheckUserTransactionsScenario(_service);
        return true;
    }
}