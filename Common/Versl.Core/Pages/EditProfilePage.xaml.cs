using MvvmCross.Forms.Presenters.Attributes;

namespace Versl.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = true, Title = "Edit Profile")]
    public partial class EditProfilePage : ContentPageBase
    {
        public EditProfilePage()
        {
            InitializeComponent();
        }
    }
}
