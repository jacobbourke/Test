namespace Accounting.Platform.Core.Configuration;

/// <summary>
/// Provides canonical deployment profiles used throughout the solution.
/// </summary>
public static class DeploymentProfileCatalog
{
    private static readonly IReadOnlyDictionary<DeploymentMode, DeploymentProfile> Profiles =
        new Dictionary<DeploymentMode, DeploymentProfile>
        {
            [DeploymentMode.Local] = new(
                DeploymentMode.Local,
                "Local/On-Premise",
                "Provision the accounting platform into infrastructure that the customer operates. \n" +
                "Ideal for organisations with strict residency or isolation requirements.",
                new[]
                {
                    "Self-hosted identity provider integration",
                    "Bring-your-own database clusters",
                    "Offline maintenance windows",
                    "Direct control over backup and retention policies"
                }),
            [DeploymentMode.Cloud] = new(
                DeploymentMode.Cloud,
                "Vendor-Managed Cloud",
                "Operate the accounting platform as a SaaS solution in vendor-managed cloud.",
                new[]
                {
                    "Automatic upgrades",
                    "High-availability multi-region hosting",
                    "Managed identity provider",
                    "Zero-downtime maintenance"
                })
        };

    /// <summary>
    /// Gets the canonical profile for a deployment mode.
    /// </summary>
    public static DeploymentProfile GetProfile(DeploymentMode mode)
    {
        if (!Profiles.TryGetValue(mode, out var profile))
        {
            throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unsupported deployment mode");
        }

        return profile;
    }
}
