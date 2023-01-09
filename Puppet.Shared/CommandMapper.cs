using System.Collections.Immutable;
using Puppet.Shared.Commands;

namespace Puppet.Shared;

public class CommandMapper
{
    
    public ICommand? Map(string[] input)
    {
        var name = input.ElementAtOrDefault(0);

        switch (name)
        {
            case SetCommand.CommandName:
                SetCommand.FromStrings(input.Skip(1));
                break;
        }

        return null;
    }
}