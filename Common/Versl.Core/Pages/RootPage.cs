using System;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Versl.Services.Theme;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Root")]
    public class RootPage : MvxMasterDetailPage
    {

    }
}
