using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Models;
using Versl.Services;
using Versl.Services.Data;
using Versl.Services.Media;
using System.Linq;
using Versl.Services.Auth;
using System.IO;
using Xamarin.Essentials;
using System.Text;
using Versl.Services.Analytics;
using Versl.Enums;

namespace Versl.ViewModels.Admin
{
    public class AdminAdminUserListViewModel : AdminListViewModelBase<UserProfile>, IAdminAdminUserListViewModel
    {
        private IUserProfileDataService _userProfileDataService;
        private IAuthenticationService _authenticationService;

        public AdminAdminUserListViewModel(IAuthenticationService authService, IMvxMessenger messenger, IUserProfileDataService userProfileDataService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            _userProfileDataService = userProfileDataService;
            _authenticationService = authService;

            ListService = userProfileDataService;
        }

        public override string PageTitle { get => "Admin Users"; }

        public override async Task GetItems()
        {
            IsBusy = true;

            try
            {
                List<UserProfile> itemList;

                var list = await _userProfileDataService.GetListAsync();

                itemList = list.Where(u => u.Role == UserRole.Admin).ToList();

                if (itemList == null)
                {
                    IsBusy = false;
                }

                ListItems = new ObservableCollection<UserProfile>(itemList);
            }
            catch (Exception ex)
            {
                //await ErrorLoggerService.Log(ex);
                await UserInteractionService.DisplayAlert("Error", "An error occurred loading data", "Ok");
            }

            IsBusy = false;
        }

        public override ICommand ItemSelectedCommand
        {
            get
            {
                return new MvxAsyncCommand<UserProfile>(async (UserProfile arg) => {

                    if (arg.Id == _authenticationService.CurrentUser.Id)
                    {
                        await UserInteractionService.DisplayAlert("Error", $"You can't remove your own privledges.", "Ok");
                        return;
                    }

                    var result = await UserInteractionService.DisplayAlert("Delete", $"Remove admin privledges for '{arg.DisplayName}'?", "Yes", "Cancel");

                    if (result)
                    {
                        arg.Role = UserRole.Subscriber;
                        await ListService.UpdateAsync(arg);
                        await GetItems();
                        IsBusy = false;
                    }
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new MvxAsyncCommand<Category>(async (Category arg) => {
                    var result = await UserInteractionService.DisplayPrompt("Add Admin", "Enter email address for the new admin", "Ok", "Cancel");

                    if (!string.IsNullOrEmpty(result))
                    {                       
                        var list = await _userProfileDataService.GetListAsync();

                        var user = list.Where(u => u.Email.ToLower() == result.ToLower()).FirstOrDefault();

                        if (user == null)
                        {
                            await UserInteractionService.DisplayAlert("Add Admin", "User not found", "Ok");
                            IsBusy = false;
                            return;
                        }

                        user.Role = UserRole.Admin;
                        await ListService.UpdateAsync(user);
                        await GetItems();
                        IsBusy = false;
                    }                    
                });
            }
        }

        public ICommand ExportUsersCommand
        {
            get
            {
                return new MvxAsyncCommand<Category>(async (Category arg) => {
                    var result = await UserInteractionService.DisplayAlert("Export Users", "Export user list?", "Ok", "Cancel");

                    if (result)
                    {
                        IsBusy = true;

                        var userList = await _userProfileDataService.GetListAsync();

                        var csv = new StringBuilder();

                        csv.Append("Id,DisplayName,Email,FirstName,LastName,PhoneNumber}\n");
                        foreach (UserProfile user in userList)
                        {
                            csv.Append($"{user.Id},{user.DisplayName},{user.Email},{user.FirstName},{user.LastName},{user.PhoneNumber}\n");
                        }

                        var fn = "contacts.csv";
                        var file = Path.Combine(FileSystem.CacheDirectory, fn);
                        File.WriteAllText(file, csv.ToString());

                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "User Export",
                            File = new ShareFile(file)
                        });


                        IsBusy = false;
                    }
                });
            }
        }
    }
}
