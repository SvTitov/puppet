namespace Puppet.Client.Network;

public class NetworkClient
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private readonly ServerConnector _connector = new();

    public Task Run(IProgress<string> progress)
    {
        var startTask = PersistentJob<string>
            .Configure(_connector.Get)
            .Start(progress, _cancellationTokenSource.Token);

        return Task.Run(() => startTask);
    }

    public Task Stop()
    {
        _cancellationTokenSource.Cancel();

        return Task.CompletedTask;
    }
}