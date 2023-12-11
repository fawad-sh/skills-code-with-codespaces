using System;
using System.IO;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace CommandAPIClient;

public class AuthConfig
{

    public string Instance {get; set;} = "https://login.microsoft.com/{0}";
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string Authority { 
        get
        {
            return string.Format(CultureInfo.InvariantCulture, Instance, TenantId);
        }
    }
    public string ClientSecret { get; set; }
    public string BaseAddress { get; set; }
    public string ResourceID { get; set; }

    public static AuthConfig ReadFromJsonFile(string path)
    {
        IConfiguration configuration;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path);

        configuration = builder.Build();

        return configuration.Get<AuthConfig>();
    }
}