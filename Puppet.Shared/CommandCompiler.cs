using Puppet.Shared.Commands;

namespace Puppet.Shared;

public static class CommandCompiler
{
    public static ICommand? Compile(string input)
    {
        var rawInputs = StringParser.Parse(input);

        return new CommandMapper().Map(rawInputs);
    }
}