using Puppet.Client.Network;

namespace Puppet.Client;

public class PuppetLauncher
{
    private readonly NetworkClient _networkClient = new();
    
    
    public Task Launch()
    {
        return _networkClient.Run(new Progress<string>(Handler));
    }

    public Task Stop()
    {
        return _networkClient.Stop();
    }

    private void Handler(string obj)
    {
        Console.WriteLine($"Received: {obj}");
    }
}