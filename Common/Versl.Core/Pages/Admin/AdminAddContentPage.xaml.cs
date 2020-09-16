using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Versl.Pages.Admin
{
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class AdminAddContentPage : ContentPageBase
    {
        public AdminAddContentPage()
        {
            InitializeComponent();
        }
    }
}
