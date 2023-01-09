using System.Diagnostics;

namespace Puppet.Client.Network;

internal class PersistentJob<T>
{
    public static PersistentJob<T> Configure(Func<CancellationToken, Task<T>> job)
    {
        return new PersistentJob<T>(job);
    }

    
    private readonly Func<CancellationToken, Task<T>> _taskJob;

    private bool _isRunning;
    
    private PersistentJob(Func<CancellationToken, Task<T>> job)
    {
        _taskJob = job;
    }

    public async Task Start(IProgress<T> progress, CancellationToken cancellationToken)
    {
        _isRunning = true;

        try
        {
            while (_isRunning)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _taskJob(cancellationToken);

                progress.Report(result);
            }
        }
        catch (Exception e) when (e is TaskCanceledException)
        {
            //TODO: refactoring, add logger
            Debug.WriteLine(e.Message);
        }
        finally
        {
            _isRunning = false;
        }
    }
}