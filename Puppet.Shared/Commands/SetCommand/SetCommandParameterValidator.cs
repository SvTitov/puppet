namespace Puppet.Shared.Commands.SetCommand;

public class SetCommandParameterValidator : IParameterValidator
{
    private string[] _patterns = 
    {
        "-p",
        "-property"
    };

    public bool IsParameter(string input)
    {
        return _patterns.Contains(input);
    }
}