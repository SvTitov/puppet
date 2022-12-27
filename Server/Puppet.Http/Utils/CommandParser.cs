using System;
using System.Linq;
using Puppet.Http.Commands;

namespace Puppet.Http.Utils
{
    public class CommandParser
    {
        public ICommand Parse(string command, string[] args)
        {
            if (command == "start")
            {
                return ParseStartCommand(args);
            }
            if (command == "stop")
            {
                return new StopCommand();
            }
            if (command == "log")
            {
                return ParseLogCommand(args);
            }
            if (command == "set")
            {
                return ParseSetCommand(args);
            }
            if (command == "info")
            {
                return new InfoCommand();
            }

            if (command == "restart")
            {
                return new RestartCommand();
            }

            return null;
        }

        private ICommand ParseSetCommand(string[] args)
        {
            if (args.Length < 2)
            {
                return new LogCommand { Message = "Please check set command options" };
            }

            var property = args[0];
            var value = args[1];

            return new SetCommand { PropertyName = property, PropertyValue = value };
        }

        private static ICommand ParseLogCommand(string[] args)
        {
            var message = string.Join(' ', args);

            return new LogCommand { Message = message };
        }

        private static ICommand ParseStartCommand(string[] args)
        {
            var port = 5432;

            var portArgument = args.ElementAtOrDefault(0);
            int.TryParse(portArgument, out var result);

            if (result != default)
            {
                port = result;
            }

            return new StartCommand { Port = port };
        }
    }
}