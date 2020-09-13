
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using MediaManager;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.FirebasePushNotification;
using Versl.ViewModels;
using Xamarin.Forms;
using Versl.Droid;

namespace Versl.Droid
{
    [Activity(Label = "Versl",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    public class InitActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            MessagingCenter.Subscribe<HorizontalVideoPlayerViewModel>(this, "AllowLandscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            MessagingCenter.Subscribe<HorizontalVideoPlayerViewModel>(this, "AllowPortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Fabric.Fabric.With(this, new Crashlytics.Crashlytics());

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
          
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {                
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";             
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            //If debug you should reset the token each time.
            #if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
            #else
            FirebasePushNotificationManager.Initialize(this,false);
            #endif

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
               
            CrossMediaManager.Current.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            base.OnCreate(savedInstanceState);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}