namespace Puppet.Shared.Commands;

public abstract class BaseCommand : ICommand
{
    private IParameterValidator _parameterValidator { get; }
    private IStringToParameterConverter _stringToParameterConverter { get; }
    
    public ParameterCollection Parameters { get; } = new();

    public ArgumentCollection Arguments { get; } = new();

    protected BaseCommand(IEnumerable<string> inputs, IParameterValidator parameterValidator, IStringToParameterConverter stringToParameterConverter)
    {
        _parameterValidator = parameterValidator;
        _stringToParameterConverter = stringToParameterConverter;
        ParseInputs(inputs);
    }

    private void ParseInputs(IEnumerable<string> inputs)
    {
        foreach (var input in inputs)
        {
            if (_parameterValidator.IsParameter(input))
            {
                Parameters.TryAdd(_stringToParameterConverter.Convert(input));
            }
            else
            {
                Arguments.Add(input);
            }
        }
    }
}