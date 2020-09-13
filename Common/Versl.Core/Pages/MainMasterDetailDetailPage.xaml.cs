using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = false)]
    public partial class MainMasterDetailDetailPage : TabbedPageBase
    {
        public MainMasterDetailDetailPage()
        {
            InitializeComponent();
        }
    }
}
