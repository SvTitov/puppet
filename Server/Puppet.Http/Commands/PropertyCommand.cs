namespace Puppet.Http.Commands
{
    public abstract class PropertyCommand : ICommand
    {
        public string PropertyName { get; set; }
    }
}