using System;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Versl.Services.Theme;
using Xamarin.Forms;

namespace Versl
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            var customAppStart = Mvx.IoCProvider.Resolve<IMvxAppStart>();

            RegisterAppStart(customAppStart);
        }
    }
}