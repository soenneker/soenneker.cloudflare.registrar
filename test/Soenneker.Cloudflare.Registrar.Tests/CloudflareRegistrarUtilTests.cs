using Soenneker.Cloudflare.Registrar.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Cloudflare.Registrar.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class CloudflareRegistrarUtilTests : HostedUnitTest
{
    private readonly ICloudflareRegistrarUtil _util;

    public CloudflareRegistrarUtilTests(Host host) : base(host)
    {
        _util = Resolve<ICloudflareRegistrarUtil>(true);
    }

    [Test]
    [Skip("Manual")]
    public void Default()
    {

    }
}
