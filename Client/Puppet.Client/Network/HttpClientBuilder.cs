namespace Puppet.Client.Network;

public static class HttpClientBuilder
{
    public static HttpClient Configure(Action<HttpConfiguration>? overrides = null)
    {
        var httpConfig = new HttpConfiguration();
       
        overrides?.Invoke(httpConfig);
        
        var client = new HttpClient();
        client.BaseAddress = new UriBuilder(httpConfig.Scheme, httpConfig.Url, httpConfig.Port).Uri;
        client.Timeout = httpConfig.TimeOut;

        return client;
    }
}

public class HttpConfiguration
{
    public int Port { get; set; } = 5432;

    public string Url { get; set; } = "localhost";
    
    public string Scheme { get; set; } = "http";
    
    public TimeSpan TimeOut { get; set; } = Timeout.InfiniteTimeSpan;
}