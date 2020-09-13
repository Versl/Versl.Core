using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Data;
using Versl.Services.Media;

namespace Versl.ViewModels.Admin
{
    public class AdminCategoryListViewModel : AdminListViewModelBase<Category>, IAdminCategoryListViewModel
    {    
        public AdminCategoryListViewModel(IMvxMessenger messenger, ICategoryDataService categoryDataService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger,navigationService, userInteractionService, mediaService, analyticsService)
        {
            ListService = categoryDataService;
        }

        public override string PageTitle { get => "Folders"; }

        public override ICommand ItemSelectedCommand
        {
            get
            {
                return new MvxAsyncCommand<Category>(async (Category arg) => {
                    var result = await UserInteractionService.DisplayAlert("Delete", $"Delete '{arg.Name}'?", "Yes", "Cancel");

                    if (result)
                    {
                        await ListService.DeleteAsync(arg.Id);
                    }
                    await GetItems();
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new MvxAsyncCommand<Category>(async (Category arg) => {
                    var result = await UserInteractionService.DisplayPrompt("Add Folder", "Enter new folder name", "Ok", "Cancel");

                    if (!string.IsNullOrEmpty(result))
                    {
                        var newCategory = await ListService.InsertAsync(new Category()
                        {
                            Name = result
                        });
                    }

                    await GetItems();
                });
            }
        }
    }
}
