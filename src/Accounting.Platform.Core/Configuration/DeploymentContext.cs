namespace Accounting.Platform.Core.Configuration;

/// <summary>
/// Represents the deployment settings currently active for the platform instance.
/// </summary>
public sealed class DeploymentContext
{
    public DeploymentContext(DeploymentProfile profile)
    {
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
    }

    /// <summary>
    /// Gets the deployment profile describing the hosting characteristics.
    /// </summary>
    public DeploymentProfile Profile { get; }

    /// <summary>
    /// Gets the deployment mode for quick access.
    /// </summary>
    public DeploymentMode Mode => Profile.Mode;
}
