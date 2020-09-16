using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Versl.Pages.Admin
{
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class AdminEditContentPage : ContentPageBase
    {
        public AdminEditContentPage()
        {
            InitializeComponent();
        }

        void DatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
        }
    }
}
