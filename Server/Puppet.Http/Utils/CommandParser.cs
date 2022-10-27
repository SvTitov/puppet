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
                var port = 5432;
                
                var portArgument = args.ElementAtOrDefault(0);
                int.TryParse(portArgument, out var result);

                if (result != default)
                {
                    port = result;
                }

                return new StartCommand { Port = port };
            }
            if (command == "stop")
            {
                return new StopCommand();
            }

            return null;
        }
    }
}