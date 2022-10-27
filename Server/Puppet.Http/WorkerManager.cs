using Puppet.Http.Commands;
using Puppet.Http.Utils;

namespace Puppet.Http
{
    public class WorkerManager
    {
        private static WorkerManager _instance;
        public static WorkerManager Instance => _instance ??= new WorkerManager();

        private readonly CommandValidator _commandValidator = new();
        private readonly CommandParser _commandParser = new();
        private readonly HttpMessageWorker _worker = new ();
        
        public void SetCommand(string[] args)
        {
            var commandString = args[0];
            var arguments = args[1..args.Length];
            if (_commandValidator.ValidateCommand(commandString) 
                && _commandValidator.ValidateArguments(arguments))
            {
                 var command = _commandParser.Parse(commandString, arguments);

                 ProcessCommand(command);
            }
        }

        private void ProcessCommand(ICommand command)
        {
            switch (command)
            {
               case StartCommand startCommand:
                   _worker.Run(startCommand.Port).ConfigureAwait(false);
                   break;
               case StopCommand:
                   _worker.Stop().ConfigureAwait(false);
                   break;
            }
        }
    }
}