using System;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Versl.ViewModels
{
    public interface IViewModelBase : IMvxViewModel<object>
    {
        string PageTitle { get; set; }
        ImageSource PageIconImageSource { get; set; }
    }
}
