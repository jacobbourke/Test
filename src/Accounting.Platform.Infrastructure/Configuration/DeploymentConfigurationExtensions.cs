using Accounting.Platform.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Platform.Infrastructure.Configuration;

/// <summary>
/// Registers deployment specific dependencies and configuration sources.
/// </summary>
public static class DeploymentConfigurationExtensions
{
    public const string DeploymentModeConfigurationKey = "Deployment:Mode";
    public const string DeploymentModeEnvironmentVariable = "DEPLOYMENT_MODE";

    /// <summary>
    /// Configures dependency injection and configuration for the active deployment mode.
    /// </summary>
    public static WebApplicationBuilder ConfigureDeployment(this WebApplicationBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        var mode = ResolveDeploymentMode(builder.Configuration);

        builder.Configuration.AddJsonFile($"appsettings.{mode}.json", optional: true, reloadOnChange: true);

        var profile = DeploymentProfileCatalog.GetProfile(mode);
        var context = new DeploymentContext(profile);

        builder.Services.AddSingleton(profile);
        builder.Services.AddSingleton(context);
        builder.Services.AddOptions<DeploymentRuntimeOptions>()
            .Configure(options =>
            {
                options.Mode = mode;
                options.ConfigurationSource = DetermineConfigurationSource(builder.Configuration);
            });

        return builder;
    }

    /// <summary>
    /// Resolve the deployment mode by preferring environment variables, then configuration.
    /// </summary>
    public static DeploymentMode ResolveDeploymentMode(IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var candidate = Environment.GetEnvironmentVariable(DeploymentModeEnvironmentVariable);

        if (string.IsNullOrWhiteSpace(candidate))
        {
            candidate = configuration[DeploymentModeConfigurationKey];
        }

        if (!Enum.TryParse(candidate, ignoreCase: true, out DeploymentMode mode))
        {
            mode = DeploymentMode.Local;
        }

        return mode;
    }

    private static string DetermineConfigurationSource(IConfiguration configuration)
    {
        if (configuration is IConfigurationRoot root)
        {
            var providers = root.Providers
                .Select(provider => provider.GetType().Name)
                .ToArray();
            return string.Join(",", providers);
        }

        return "Unknown";
    }
}

/// <summary>
/// Provides runtime metadata about the active deployment.
/// </summary>
public sealed class DeploymentRuntimeOptions
{
    public DeploymentMode Mode { get; set; } = DeploymentMode.Local;

    public string ConfigurationSource { get; set; } = string.Empty;
}
