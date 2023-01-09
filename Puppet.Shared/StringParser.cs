namespace Puppet.Shared;

public static class StringParser
{
    public static IEnumerable<string> Parse(string input)
    {
        return input.Split(" ");
    }
}