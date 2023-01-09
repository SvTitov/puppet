using System.Threading;
using System.Threading.Tasks;
using Puppet.Http.Commands;

namespace Puppet.Http.Utils
{
    public class CommandProvider
    {
        private TaskCompletionSource<ICommand> _completionSource = new ();
        
        public void SetCommand(ICommand command)
        {
            _completionSource.SetResult(command);
            _completionSource = new TaskCompletionSource<ICommand>();
        }

        public async Task<ICommand> GetCommand() => await _completionSource.Task;
    }
}