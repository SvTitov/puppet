namespace Puppet.Generator;

internal class Attributes
{
    public const string ATTRIBUTE_FULL_NAME = $"{ATTRIBUTE_NAME}Attribute";
    public const string ATTRIBUTE_NAME = "Puppet";
    public const string ATTRIBUTE_IMPL = $@"
using System;
namespace Puppet
{{
    /// Auto-generated attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class {ATTRIBUTE_FULL_NAME} : System.Attribute
    {{}}
}}";
}