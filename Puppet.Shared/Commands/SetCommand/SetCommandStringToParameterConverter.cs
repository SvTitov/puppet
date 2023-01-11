namespace Puppet.Shared.Commands.SetCommand;

public class SetCommandStringToParameterConverter : IStringToParameterConverter
{
    public string Convert(string input) => input switch
    {
        "-p" or "-property" => "property",
        _ => throw new ArgumentException("Can't convert command")
    };

}