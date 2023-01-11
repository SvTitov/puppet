namespace Puppet.Shared;

public static class StringParser
{
    public static string[] Parse(string input)
    {
        return input.Split(" ");
    }
}