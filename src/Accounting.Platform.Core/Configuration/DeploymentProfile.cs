namespace Accounting.Platform.Core.Configuration;

/// <summary>
/// Represents the deployment characteristics that downstream services can use
/// to tailor configuration, infrastructure provisioning and onboarding flows.
/// </summary>
public sealed record DeploymentProfile(
    DeploymentMode Mode,
    string DisplayName,
    string Description,
    IReadOnlyCollection<string> KeyCapabilities);
