using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenario.AdminScenarios;
using Presentation.Console.Scenario.Login;
using Presentation.Console.Scenario.SignUp;
using Presentation.Console.Scenario.UserScenarios;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, SignUpUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CheckUserTransactionsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CheckBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetMyTransactionsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, PutMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TakeMoneyScenarioProvider>();

        return collection;
    }
}