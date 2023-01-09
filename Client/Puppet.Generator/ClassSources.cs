using Puppet.Generator;

namespace Puppet.Generator;

internal class ClassSources
{
    private const string NAMESPACE = "Puppet.Http";
    
    #region ConnectionManager

    public const string CONNECTION_MANAGER_NAME = $"ConnectionManager";

    public const string CONNECTION_MANAGER_IMPL = $@"

namespace {NAMESPACE};
internal partial class {CONNECTION_MANAGER_NAME}
{{
    private bool IsWorking {{ get; set; }}

    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    
    public Task Run(IProgress<string> progress)
    {{
        IsWorking = true;
        return Task.Run(() => StartWork(progress, _cancellationTokenSource.Token));
    }}

    public Task Stop()
    {{
        IsWorking = false;
        
        _cancellationTokenSource.Cancel();
        
        return Task.CompletedTask;
    }}

    private async Task StartWork(IProgress<string> progress, CancellationToken cancellationToken)
    {{
        var connector = new ServerConnector();
        
        while (IsWorking)
        {{
            var res = await connector.Connect(cancellationToken);
            progress?.Report(res);
        }}
    }}
}}
";


    #endregion

    #region ServerConnector

    public const string SERVER_CONNECTOR_NAME = "ServerConnector";

    public const string SERVER_CONNECTOR_IMPL = $@"
namespace {NAMESPACE};
internal class {SERVER_CONNECTOR_NAME}
{{
    public async Task<string> Connect(CancellationToken cancellationToken)
    {{
        var client = HttpClientBuilder.Configure();

        var result = await client.GetAsync(string.Empty, cancellationToken);

        return await result.Content.ReadAsStringAsync();
    }}
}}
";

    #endregion

    #region HttpClientBuilder

    public const string HTTP_BUILDER_NAME = "HttpClientBuilder"; 
    
    public const string HTTP_BUILDER_IMPL = $@"
namespace {NAMESPACE};
internal static class {HTTP_BUILDER_NAME}
{{
    public static HttpClient Configure(int port = 5432, string url = ""localhost"")
    {{
        var client = new System.Net.Http.HttpClient();
        client.BaseAddress = new UriBuilder(""http"", url, port).Uri;
        client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;

        return client;
    }}
}}
";

    #endregion

}