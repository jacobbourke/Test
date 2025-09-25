using Accounting.Platform.Core.Configuration;
using Accounting.Platform.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        { DeploymentConfigurationExtensions.DeploymentModeConfigurationKey, "Local" }
    })
    .AddJsonFile("bootstrapper.json", optional: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var mode = DeploymentConfigurationExtensions.ResolveDeploymentMode(configuration);
var profile = DeploymentProfileCatalog.GetProfile(mode);

Console.WriteLine($"Accounting Platform Bootstrapper\n===============================");
Console.WriteLine($"Deployment mode: {profile.DisplayName} ({profile.Mode})");
Console.WriteLine();
Console.WriteLine(profile.Description);
Console.WriteLine();
Console.WriteLine("Key capabilities:");
foreach (var capability in profile.KeyCapabilities)
{
    Console.WriteLine($" - {capability}");
}

Console.WriteLine();
Console.WriteLine("Next steps:");
if (mode == DeploymentMode.Local)
{
    Console.WriteLine("  1. Provision a PostgreSQL cluster or reuse existing customer-managed database.");
    Console.WriteLine("  2. Configure identity integration with the organisation's provider.");
    Console.WriteLine("  3. Populate appsettings.Local.json with secrets and networking details.");
}
else
{
    Console.WriteLine("  1. Ensure required cloud subscriptions and resource groups exist.");
    Console.WriteLine("  2. Configure managed identity provider (e.g. Azure AD B2C) tenants.");
    Console.WriteLine("  3. Populate appsettings.Cloud.json with environment specific overrides.");
}

Console.WriteLine();
Console.WriteLine("Override the deployment mode by setting DEPLOYMENT_MODE environment variable or passing --Deployment:Mode=<Local|Cloud>.");
