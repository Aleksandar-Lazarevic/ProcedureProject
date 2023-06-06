using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using Android.Provider;
namespace iProcedure;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        RequestedOrientation = ScreenOrientation.Portrait;

        setAndroidDeviceAirplaneMode(Android.App.Application.Context, false);
        isAirplaneModeOn(Android.App.Application.Context);
    }

    private static bool isAirplaneModeOn(Context context)
    {
        Settings.System.GetInt(context.ContentResolver, Settings.Global.AirplaneModeOn, 0);
        bool flag = Settings.System.GetInt(context.ContentResolver, Settings.Global.AirplaneModeOn, 0) != 0;
        return flag;
    }

    public void setAndroidDeviceAirplaneMode(Context context, bool status)
    {
        /*
        // Toggle airplane mode.
        Settings.System.PutInt(context.ContentResolver,
              Settings.Global.AirplaneModeOn, status ? 0 : 1);

        // Post an intent to reload.
        Intent intent = new Intent(Intent.ActionAirplaneModeChanged);
        intent.PutExtra("state", !status);
        SendBroadcast(intent);
        */
        try
        {

            String airplaneModeStatus = "";
            if (status)
            {
                airplaneModeStatus = "1";
            }
            else
            {
                airplaneModeStatus = "0";
            }
            String sdkPath = Java.Lang.JavaSystem.Getenv("ANDROID_HOME") + "/platform-tools/";
            Java.Lang.Runtime.GetRuntime().Exec(/*sdkPath + */"adb shell settings put global airplane_mode_on " + airplaneModeStatus);
            Thread.Sleep(1000);
            Java.Lang.Runtime.GetRuntime().Exec(/*sdkPath + */"adb shell am broadcast -a android.intent.action.AIRPLANE_MODE");
            //process .waitFor();
            Thread.Sleep(4000);
            if (status)
            {
                //logger.info("Android device Airplane mode status is set to ON");
            }
            else
            {
                //logger.info("Android device Airplane mode status is set to OFF");
            }
        }
        catch (Exception e)
        {
            //System.out.println(e.getMessage());
            //logger.error("Unable to set android device Airplane mode.");
        }
    }
}
