namespace Puppet.Shared;

public class StringsAnalyzer
{
    public CommandTree Analyze(string[] strings)
    {
        //TODO: make more serious
        var commandTree = new CommandTree();

        var commandName = strings.ElementAtOrDefault(0);

        commandTree.Name = commandName;

        foreach (var input in strings.Skip(1))
        {
            if (input.StartsWith('-'))
            {
                commandTree.Parameters.Add(input);
            }
            else
            {
                commandTree.Arguments.Add(input);
            }
        }

        return commandTree;
    }
}

public class CommandTree
{
    public string? Name { get; set; }

    public List<string> Parameters { get; } = new();

    public List<string> Arguments { get; } = new();
}