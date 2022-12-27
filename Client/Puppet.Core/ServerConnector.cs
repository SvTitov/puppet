namespace Puppet.Core;

public class ServerConnector
{
    public async Task Connect()
    {
        int port = 5432;

        var client = new System.Net.Http.HttpClient();
        client.BaseAddress = new UriBuilder("http", "localhost", port).Uri;
        client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;

        var result = await client.GetAsync(String.Empty);
        return await result.Content.ReadAsStringAsync(); 

    }

    private HttpClient BuildClient()
    {
        int port = 5432;

        var client = new System.Net.Http.HttpClient();
        client.BaseAddress = new UriBuilder("http", "localhost", port).Uri;
        client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;

        return client;
    }
}