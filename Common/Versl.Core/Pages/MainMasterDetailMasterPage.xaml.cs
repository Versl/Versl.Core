using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master, WrapInNavigationPage = false, Title = "Menu", Icon = "hamburger.png", NoHistory = true)]
    public partial class MainMasterDetailMasterPage :  ContentPageBase
    {
        public MainMasterDetailMasterPage()
        {
            InitializeComponent();
        }
    }
}
