using System.Collections;

namespace Puppet.Shared.Commands;

public class ParameterCollection : ICollection<string>
{
    public int Count => _innerParameters.Count;
    
    public bool IsReadOnly => true;
    private List<CommandParameter> ParametersCanBeStored { get; }
    
    private readonly List<CommandParameter> _innerParameters = new();

    public ParameterCollection(params CommandParameter[] parametersCanBeStored)
    {
        ParametersCanBeStored = parametersCanBeStored.ToList();
    }
    
    public IEnumerator<string> GetEnumerator()
    {
        return _innerParameters.Select(x => x.Meaning).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _innerParameters.GetEnumerator();
    }

    public void Add(string item)
    {
        var parameter = GetParameterBySynonymous(item);

        StoreParameterIfNeeded(parameter);
    }

    public void Clear()
    {
        _innerParameters.Clear();
    }

    public bool Contains(string item)
    {
        return _innerParameters.Any(x => x.Synonyms.Contains(item));
    }

    public void CopyTo(string[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(string item)
    {
        CommandParameter? firstOrDefault = _innerParameters.FirstOrDefault(x => x.Synonyms.Contains(item));
        if (firstOrDefault != null)
        {
            return _innerParameters.Remove(firstOrDefault);
        }

        return false;
    }
    
    private void StoreParameterIfNeeded(CommandParameter? parameter)
    {
        if (_innerParameters.All(p => p.Meaning != parameter?.Meaning))
        {
            _innerParameters.Add(parameter!);
        }
    }

    private CommandParameter? GetParameterBySynonymous(string item)
    {
        return ParametersCanBeStored.FirstOrDefault
        (
            x => x.Synonyms.Contains(item)
        );
    }
}