using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Admin;
using Application.Contracts.Users;

namespace Presentation.Console.Scenario.SignUp;

public class SignUpUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentAdminService _serviceAdmin;
    private readonly ICurrentUserService _serviceUser;

    public SignUpUserScenarioProvider(IUserService service, ICurrentAdminService serviceAdmin, ICurrentUserService serviceUser)
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

        scenario = new SignUpUserScenario(_service);
        return true;
    }
}