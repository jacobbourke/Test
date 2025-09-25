namespace Accounting.Platform.Core.Configuration;

/// <summary>
/// Defines the supported deployment targets for the platform.
/// </summary>
public enum DeploymentMode
{
    /// <summary>
    /// Install the platform within an isolated environment that a customer controls.
    /// </summary>
    Local = 0,

    /// <summary>
    /// Run the platform as a managed cloud service operated by the vendor.
    /// </summary>
    Cloud = 1
}
