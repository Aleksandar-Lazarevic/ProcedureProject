namespace iProcedure;

using Microsoft.Maui.LifecycleEvents;
using CommunityToolkit.Maui;
using iProcedure.ViewModel;
using iProcedure.View;
using SkiaSharp.Views.Maui.Controls.Hosting;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        builder.UseMauiCommunityToolkit();
        builder.UseSkiaSharp();
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("fontawesome-webfont.ttf", "AwesomeWeb");
            fonts.AddFont("fa-brands-400.ttf", "AwesomeBrands");
            fonts.AddFont("fa-regular-400.ttf", "AwesomeRegular");
            fonts.AddFont("fa-solid-900.ttf", "AwesomeSolid");
            fonts.AddFont("fa-v4compatibility.ttf", "AwesomeV4compatibility");
            fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialRegular");
        })
        .ConfigureLifecycleEvents(events =>
        {
#if WINDOWS
               events.AddWindows(windows => windows
                    .OnActivated((window, args) => LogEvent("OnActivated"))
                    .OnClosed((window, args) => LogEvent("OnClosed"))
                    .OnLaunched((window, args) => LogEvent("OnLaunched"))
                    .OnLaunching((window, args) => LogEvent("OnLaunching"))
                    .OnVisibilityChanged((window, args) => LogEvent("OnVisibilityChanged"))
                    .OnPlatformMessage((window, args) =>
                    {

                    }));
#elif ANDROID
            events.AddAndroid(android => android.OnActivityResult((activity, requestCode, resultCode, data) => LogEvent("OnActivityResult", requestCode.ToString())).OnStart((activity) => LogEvent("OnStart")).OnCreate((activity, bundle) => LogEvent("OnCreate")).OnBackPressed((activity) => LogEvent1("OnBackPressed")).OnStop((activity) => LogEvent("OnStop")));
#elif IOS
                events.AddiOS(ios => ios
                    .OnActivated((app) => LogEvent("OnActivated"))
                    .OnResignActivation((app) => LogEvent("OnResignActivation"))
                    .DidEnterBackground((app) => LogEvent("DidEnterBackground"))
                    .WillTerminate((app) => LogEvent("WillTerminate")));
#endif
            static void LogEvent(string eventName, string type = null)
            {
                System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
                if (eventName.Equals("OnStop"))
                {
#if ANDROID
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
                }

            }

            static bool LogEvent1(string eventName, string type = null)
            {
                System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
                return false;
            }
        });

        return builder.Build();
    }
}