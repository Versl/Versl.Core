using MvvmCross.Forms.Presenters.Attributes;

namespace Versl.Pages
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = true)]
    public partial class WebViewPage : ContentPageBase
    {
        public WebViewPage()
        {
            InitializeComponent();
        }
    }
}
