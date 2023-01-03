using System.Runtime.CompilerServices;

namespace Puppet.Core;

public class ConnectionProcessor
{
    private bool IsWorking { get; set; }
    
    public Task Run(System.IProgress<string> progress)
    {
        IsWorking = true;
        return Task.Run(() => StartWork(progress));
    }

    public Task Stop()
    {
        IsWorking = false;
        
        //TODO: Add cancellation token to Start Work -> Connector
        
        return Task.CompletedTask;
    }

    private async Task StartWork(IProgress<string> progress)
    {
        var connector = new ServerConnector();
        
        while (IsWorking)
        {
            var res = await connector.Connect();
            progress?.Report(res);
        }
    }
}