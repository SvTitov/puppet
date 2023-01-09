namespace Puppet.Shared.Commands;

public class CommandParameter
{
    public string Meaning { get; set; }

    public IList<string> Synonyms { get; set; } = new List<string>();
}