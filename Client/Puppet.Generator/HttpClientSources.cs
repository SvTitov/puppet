namespace Puppet.Generator;

public class HttpClientSources
{
    public const string PROPERTIES = @"
    
    public bool IsWorking { get; private set; } = true;

";
    
    
    
    public const string CONNECT_METHOD = @"

    public virtual async System.Threading.Tasks.Task<string> PUPPET_ESTABLISH_CONNECTION()
    {{
        int port = 5432;

        var client = new System.Net.Http.HttpClient();
        client.BaseAddress = new UriBuilder(""http"", ""localhost"", port).Uri;
        client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;

        //while(IsWorking)
        //{
        //}

        var result = await client.GetAsync("""");
        return await result.Content.ReadAsStringAsync(); 
    }}

";
}