namespace Puppet.Shared.Commands;

public interface IParameterValidator
{
    bool IsParameter(string input);
}