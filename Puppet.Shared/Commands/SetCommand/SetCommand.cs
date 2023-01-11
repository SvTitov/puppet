namespace Puppet.Shared.Commands.SetCommand;

public class SetCommand : BaseCommand
{
    private SetCommand(IEnumerable<string> inputs)
        : base(inputs, new SetCommandParameterValidator(), new SetCommandStringToParameterConverter())
    {
    }

    public static SetCommand FromStrings(IEnumerable<string> input)
    {
        return new SetCommand(input);
    }
}