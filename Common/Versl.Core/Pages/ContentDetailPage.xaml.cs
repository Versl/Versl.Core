using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Versl.ViewModels;

namespace Versl.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class ContentDetailPage : ContentPageBase
    {
        public ContentDetailPage()
        {
            InitializeComponent();
        }
    }
}
