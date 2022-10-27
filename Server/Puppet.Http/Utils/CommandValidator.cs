namespace Puppet.Http.Utils
{
    public class CommandValidator
    {
        public bool ValidateCommand(string command)
        {
            return !string.IsNullOrEmpty(command);
        }

        public bool ValidateArguments(string[] args)
        {
            return true;
        }
    }
}