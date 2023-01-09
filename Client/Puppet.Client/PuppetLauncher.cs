using Puppet.Client.Network;

namespace Puppet.Client;

public class PuppetLauncher
{
    private readonly NetworkClient _networkClient = new();
    
    
    public Task Launch()
    {
        return Task.CompletedTask;
    }
}