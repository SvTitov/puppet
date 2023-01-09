using System.Text;

namespace Puppet.Generator.Utils;

public class NamespaceBuilder
{
    private readonly StringBuilder _stringBuilder = new();

    public void Append(string namespacePart)
    {
        if (_stringBuilder.Length == 0)
        {
            _stringBuilder.Append(namespacePart);
        }
        else
        {
            _stringBuilder.Append(new[] { ".", namespacePart });
        }
    }

    public string Build()
    {
        return _stringBuilder.ToString();
    }
}