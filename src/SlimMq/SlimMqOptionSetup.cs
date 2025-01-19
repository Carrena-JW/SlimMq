using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SlimMq;

public class SlimMqOptionSetup(IConfiguration config) : IConfigureOptions<SlimMqOptions>
{
    private const string SECTION_NAME = "SlimMq";
    private readonly IConfiguration _configuration = config;

    void IConfigureOptions<SlimMqOptions>.Configure(SlimMqOptions options)
    {
        _configuration.GetSection(SECTION_NAME).Bind(options);
    }
}
 