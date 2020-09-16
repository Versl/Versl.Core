using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Versl.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = false)]
    public partial class HorizontalVideoPlayerPage : ContentPageBase
    {
        public HorizontalVideoPlayerPage()
        {
            InitializeComponent();
        }
    }
}
