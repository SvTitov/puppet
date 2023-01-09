using Microsoft.Maui.LifecycleEvents;
using Puppet.Client.Network;

namespace Puppet.MAUI;

public static class BuilderExtension
{
    public static MauiAppBuilder UsePuppet(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            //TODO: add implementation of Puppet with events
            events.AddAndroid(android =>
            {
                android.OnResume(activity => Console.WriteLine("--- Native OnResume"));
                android.OnPause(activity => Console.WriteLine("--- Native OnPause"));
            });
#endif

#if IOS
            events.AddiOS(ios =>
            {
                ios.OnActivated(x => Console.WriteLine("--- OnActivated"));
                ios.OnResignActivation(x => Console.WriteLine("--- OnResignActivation"));
            });
#endif
        });
        
        return builder;
    }
 
}