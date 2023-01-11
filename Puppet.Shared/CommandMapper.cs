using System.Collections.Immutable;
using Puppet.Shared.Commands;
using Puppet.Shared.Commands.SetCommand;

namespace Puppet.Shared;

public class CommandMapper
{
    public ICommand? Map(string[] input) => input.ElementAtOrDefault(0) switch
    {
        "set" => SetCommand.FromStrings(input.Skip(1)),
        _ => null
    };
}