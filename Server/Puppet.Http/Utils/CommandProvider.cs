using System.Threading;
using System.Threading.Tasks;
using Puppet.Http.Commands;

namespace Puppet.Http.Utils
{
    public class CommandProvider
    {
        private ICommand _command;
        private TaskCompletionSource<ICommand> _completionSource = new ();

        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 2);
        
        public void SetCommand(ICommand command)
        {
            _command = command;

            _completionSource.SetResult(command);
            
            _completionSource = new TaskCompletionSource<ICommand>();
        }
        
        public async Task<ICommand> GetCommand()
        {
            if (_command != null)
            {
                var temp = _command;
                _command = null;
                return temp;
            }

            return await _completionSource.Task;
        }
    }
}