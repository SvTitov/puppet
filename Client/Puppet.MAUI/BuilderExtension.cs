using Microsoft.Maui.LifecycleEvents;
using Puppet.Client;
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
            var launcher = new PuppetLauncher();
            events.AddAndroid(android =>
            {
                android.OnResume(_ => launcher.Launch());
                android.OnPause(_ => launcher.Stop());
            });
#endif

#if IOS
            var launcher = new PuppetLauncher();
            events.AddiOS(ios =>
            {
                ios.OnActivated(_ => launcher.Launch());
                ios.OnResignActivation(_ => launcher.Stop());
            });
#endif
        });
        
        return builder;
    }
 
}