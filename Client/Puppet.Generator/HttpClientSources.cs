namespace Puppet.Generator;

public class HttpClientSources
{
    public const string PROPERTIES = @"
    
    public bool IsWorking { get; private set; } = true;

";
    
    // TODO: move All classes from Core -> generator
    
    //TODO: Create specific libs for xamarin.forms and maui
    
    public const string CONNECT_METHOD = @"

    public virtual async System.Threading.Tasks.Task PUPPET_ESTABLISH_CONNECTION()
    {{
        var progress = new System.Progress<string>(OnConnectionResult);

        var processor = new Puppet.Core.ConnectionProcessor();

        await processor.Run(progress); 
    }}
";

    public const string ON_CONNECTION_RESULT_METHOD = @"

    public virtual void OnConnectionResult(string res)
    {{
       Console.WriteLine(res);
    }}
";

}