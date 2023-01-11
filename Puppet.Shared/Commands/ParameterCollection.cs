using System.Collections;

namespace Puppet.Shared.Commands;

public class ParameterCollection : List<string>
{
    public bool TryAdd(string item)
    {
        if (!Contains(item))
        {
            Add(item);
            return true;
        }

        return false;
    }
}