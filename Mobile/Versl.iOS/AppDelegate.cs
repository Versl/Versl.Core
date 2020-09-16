using System;
using Foundation;
using MediaManager;
using MvvmCross.Core;
using MvvmCross.Platforms.Ios.Core;
using ObjCRuntime;
using Plugin.FirebasePushNotification;
using UIKit;
using Versl.ViewModels;
using Xamarin.Forms;
using XF = Firebase;

namespace Versl.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate
    {
        private UIInterfaceOrientationMask RequestedOrientation = UIInterfaceOrientationMask.Portrait;

        protected override void RegisterSetup()
        {
            this.RegisterSetupType<Setup>();

            MessagingCenter.Subscribe<HorizontalVideoPlayerViewModel>(this, "AllowLandscape", sender =>
            {
                RequestedOrientation = UIInterfaceOrientationMask.Landscape;
            });

            MessagingCenter.Subscribe<HorizontalVideoPlayerViewModel>(this, "AllowPortrait", sender =>
            {
                RequestedOrientation = UIInterfaceOrientationMask.Portrait;

                UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)RequestedOrientation), new NSString("orientation"));
            });
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            XF.Core.App.Configure();
            XF.Crashlytics.Crashlytics.Configure();

            Forms.SetFlags("AppTheme_Experimental");
            Forms.SetFlags("MediaElement_Experimental");

            Xamarin.Forms.Forms.Init();
            FirebasePushNotificationManager.Initialize(options, true);
            
            CrossMediaManager.Current.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, [Transient] UIWindow forWindow)
        {
            return RequestedOrientation;
        }
    }
}
