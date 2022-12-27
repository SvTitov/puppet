using System;
using Puppet.Http.Commands;
using Puppet.Http.Utils;

namespace Puppet.Http
{
    public class WorkerManager
    {
        #region Singleton

        private static WorkerManager _instance;
        public static WorkerManager Instance => _instance ??= new WorkerManager();

        #endregion

        #region Fields

        private readonly CommandValidator _commandValidator = new();
        private readonly CommandParser _commandParser = new();
        private readonly HttpMessageWorker _worker = new ();

        #endregion

        #region Private Ctor

        private WorkerManager()
        {}
        
        #endregion

        #region Public
        
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

        #endregion

        #region Private
        
        private void ProcessCommand(ICommand command)
        {
            switch (command)
            {
               case StartCommand startCommand:
                   _worker.Run(startCommand.Port);
                   break;
               case StopCommand:
                   _worker.Stop().ConfigureAwait(false);
                   break;
               case InfoCommand:
                   var info = _worker.CollectInfo();
                   foreach (var i in info) Console.WriteLine(i);
                   break;
               case RestartCommand:
                   _worker?.Restart();
                   break;
               case LogCommand:
               case Commands.SetCommand:
                   _worker.SetCommand(command);
                   break;
            }
        }

        #endregion
    }
}