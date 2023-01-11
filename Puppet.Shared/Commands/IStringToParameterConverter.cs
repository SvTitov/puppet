namespace Puppet.Shared.Commands;

public interface IStringToParameterConverter
{
    string Convert(string input);
}