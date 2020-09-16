using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = true)]
    public partial class ContentListPage : ContentPageBase
    {
        public ContentListPage()
        {
            InitializeComponent();
        }
    }
}
