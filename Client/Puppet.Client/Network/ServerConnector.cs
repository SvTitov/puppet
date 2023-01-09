namespace Puppet.Client.Network;

public class ServerConnector
{
    private HttpClient _httpClient;

    private HttpClient HttpClient => _httpClient;
    
    public ServerConnector()
    {
        _httpClient = HttpClientBuilder.Configure();
    }
    
    public void ConfigureHttpClient(Action<HttpConfiguration> configuration)
    {
        _httpClient = HttpClientBuilder.Configure(configuration);
    }
    
    public async Task<string> Get(CancellationToken cancellationToken)
    {
        var result = await HttpClient.GetAsync(string.Empty, cancellationToken);

        return await result.Content.ReadAsStringAsync(cancellationToken);
    }
}
