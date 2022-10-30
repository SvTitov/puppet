using System.Collections;
using System.Collections.Generic;

namespace Puppet.Http.Commands
{
    public class InfoCommand : ICommand
    {
        public IEnumerable<string> Messages { get; set; }
    }
}