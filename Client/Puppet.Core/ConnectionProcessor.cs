using System.Runtime.CompilerServices;

namespace Puppet.Core;

public class ConnectionProcessor
{
    public Task Run()
    {
        return Task.Run(StartWork);
    }

    private async Task StartWork()
    {
        while (true)
        {
            
        }
    }
}