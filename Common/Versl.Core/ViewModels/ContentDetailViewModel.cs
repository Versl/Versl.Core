using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Plugin.InAppBilling;
using Versl.Enums;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Auth;
using Versl.Services.Data;
using Versl.Services.Media;
using Xamarin.Essentials;

namespace Versl.ViewModels
{
    public class ContentDetailViewModel : ViewModelBase, IContentDetailViewModel
    {
        private ContentItem _model;

        protected IContentDataService DataService;
        protected IAuthenticationService AuthenticationService;

        public ContentDetailViewModel(IMvxMessenger messenger, IAuthenticationService authenticationService, IContentDataService contentService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            DataService = contentService;
            AuthenticationService = authenticationService;
        }
        
        public override async Task Initialize()
        {
            await base.Initialize();
            await GetItem(NavigationParameter.ToString());
        }

        private async Task GetItem(string Id)
        {
            IsBusy = true;
            Model = await DataService.GetAsync(Id);            
            IsBusy = false;
        }

        public ContentItem Model
        {
            get
            {
                return _model;
            }
            protected set
            {
                if (_model != value)
                {
                    _model = value;
                    RaisePropertyChanged(() => Model);

                    PageTitle = Model.ShortTitle;
                }
            }
        }

        public virtual ICommand ShareCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    var image = Path.Combine(FileSystem.CacheDirectory, $"{Model.Title}.png");

                    var client = new WebClient();
                    client.DownloadFileAsync(new Uri(Model.Image), image);

                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = Model.Title,
                        File = new ShareFile(image, "image/png")
                    });
                });
            }
        }

        public ICommand PrimaryActionCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    bool hasSubscription = false;

                    if ((Model.IsProtected || Model.IsSubscription) && AuthenticationService.CurrentUser == null)
                    {
                         await Navigate<ILoginViewModel>();
                        return;
                    }

                    
                    if (Model.IsSubscription && !hasSubscription)
                    {
                        //Validate subscription. If invalid, return.                        
                        var response = await UserInteractionService.DisplayAlert("Subscription", "This content requires a subscription.", "Buy Now", "Cancel");

                        if (!response)
                        {
                            return;
                        }
                        
                        try
                        {
                            var confirm = await UserInteractionService.DisplayAlert("Subscription", "Purchase premium subscription for $2.99?.", "Confirm", "Cancel");

                            if (!confirm)
                            {
                                return;
                            }

                            //CrossInAppBilling.Current.InTestingMode = true;

                            //var connected = await CrossInAppBilling.Current.ConnectAsync();
                            //var products = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.InAppPurchase, "testconsumable");
                            //var purchase = await CrossInAppBilling.Current.PurchaseAsync("testsubscription", ItemType.Subscription);

                            await UserInteractionService.DisplayAlert("Subscription", "Purchase complete. Enjoy your subscription!", "Ok");
                        }
                        catch(InAppBillingPurchaseException purchaseEx)
                        {

                        }
                        catch (Exception ex)
                        {
                            await UserInteractionService.DisplayAlert("Error", ex.Message, "Ok");
                            return;
                        }
                        finally
                        {
                            await CrossInAppBilling.Current.DisconnectAsync();
                        }                        
                    }
                    

                    if (Model.CTA.Type == CTALinkType.Web)
                    {
                        await Browser.OpenAsync(Model.CTA.Url, new BrowserLaunchOptions
                        {
                            LaunchMode = BrowserLaunchMode.SystemPreferred,
                            TitleMode = BrowserTitleMode.Show
                        });
                    }
                    else
                    {
                        MediaPlaybackType playback;

                        switch(Model.CTA.Type)
                        {
                            case CTALinkType.VideoHorizontal:
                                playback = MediaPlaybackType.VideoHorizontal;
                                break;
                            case CTALinkType.VideoVertical:
                                playback = MediaPlaybackType.VideoVertical;
                                break;
                            case CTALinkType.Audio:
                            default:
                                playback = MediaPlaybackType.Audio;
                                break;
                        }

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MediaService.Play(new MediaAsset(Model.CTA.Url, Model.Title, Model.Image, playback));
                            await ShowPlayer();
                        });

                    }

                });
            }
        }        
    }
}
