namespace Puppet.Generator;

public static class TargetClassSources
{
    public const string PROPERTIES = @"
    
    public bool IsWorking { get; private set; } = true;

";
    
    //TODO: Create specific libs for xamarin.forms and maui
    
    public const string CONNECT_METHOD = @"

    public virtual async System.Threading.Tasks.Task PUPPET_ESTABLISH_CONNECTION()
    {
        try 
        {
            var progress = new System.Progress<string>(OnConnectionResult);

            var processor = new Puppet.Http.ConnectionManager();

            await processor.Run(progress); 
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
";

    public const string ON_CONNECTION_RESULT_METHOD = @"

    public virtual void OnConnectionResult(string res)
    {
       Console.WriteLine(res);
    }
";

}