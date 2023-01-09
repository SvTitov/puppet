using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Puppet.Shared.Commands;

public class SetCommand : ICommand
{
    private ParameterCollection Parameters { get; } = new (new []
    {
        new CommandParameter
        {
            Meaning = "Property",
            Synonyms = new List<string>
            {
                "-p",
                "-property"
            }
        }
    });

    private ArgumentCollection Arguments { get; } = new();

    public const string CommandName = "set";

    private SetCommand(IEnumerable<string> parameters)
    {
        foreach (var parameter in parameters)
        {
            if (parameter.StartsWith("-"))
            {
                Parameters.Add(parameter);
            }
            else
            {
                Arguments.Add(parameter);
            }
        }
    }
    
    public static ICommand FromStrings(IEnumerable<string> input)
    {
        return new SetCommand(input);
    }
}