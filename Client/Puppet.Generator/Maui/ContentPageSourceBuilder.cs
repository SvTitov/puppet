using System.Text;

namespace Puppet.Generator.Maui;

internal static class ContentPageSourceBuilder
{
    internal static void AssignPlatformCode(StringBuilder stringBuilder)
    {
        stringBuilder.Append(@"
        
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
 

");
    }
}