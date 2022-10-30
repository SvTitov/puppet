using System.Threading;
using System.Threading.Tasks;
using Puppet.Http.Commands;

namespace Puppet.Http.Utils
{
    public class CommandProvider
    {
        private ICommand _command;
        private TaskCompletionSource<ICommand> _completionSource;

        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        
        public void SetCommand(ICommand command)
        {
            _semaphoreSlim.Wait();
            
            _command = command;

            _completionSource?.SetResult(command);

            _semaphoreSlim.Release();
        }
        
        public async Task<ICommand> GetCommand()
        {
            await _semaphoreSlim.WaitAsync();
            
            if (_command != null)
            {
                return _command;
            }

            _completionSource = new TaskCompletionSource<ICommand>();
           
            var result = await _completionSource.Task;

            _completionSource = null;

            _semaphoreSlim.Release();
            
            return result;
        }
    }
}