using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Versl.Enums;
using Versl.Models;
using Versl.Services;
using Versl.Services.Analytics;
using Versl.Services.Data;
using Versl.Services.Media;
using Versl.Services.Storage;
using Xamarin.Forms;

namespace Versl.ViewModels.Admin
{
    public class AdminAddContentViewModel : AdminViewModelBase, IAdminAddContentViewModel
    {
        private ImageSource _imageSource = ImageSource.FromFile("brandlogo.png");
        private ContentItem _item;
        private Category _selectedCategory;
        private string _selectedCTAType;

        private IContentDataService ContentDataService;
        private ICategoryDataService CategoryDataService;
        private IStorageService StorageService;

        public AdminAddContentViewModel(IMvxMessenger messenger, IStorageService storageService, IContentDataService contentDataService, ICategoryDataService categoryDataService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            ContentDataService = contentDataService;
            CategoryDataService = categoryDataService;
            StorageService = storageService;

            Categories = new ObservableCollection<Category>();
            CTATypes = new ObservableCollection<string>();

            Item = new ContentItem();
        }

        public override string PageTitle { get => "Add Post" ; }

        public override async Task Initialize()
        {
            await base.Initialize();

            Item.Date = DateTime.Now;

            Categories.Add(new Category { Id = "0", Name = "None" });
            var list = await CategoryDataService.GetListAsync();
            foreach (Category category in list)
            {
                Categories.Add(category);
            }

            var ctaTypes = Enum.GetNames(typeof(CTALinkType));
            foreach (string cta in ctaTypes)
            {
                CTATypes.Add(cta);
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<String> CTATypes { get; set; }

        public ContentItem Item
        {
            get
            {
                return _item;
            }
            protected set
            {
                if (_item != value)
                {
                    _item = value;
                    RaisePropertyChanged(() => Item);
                }
            }
        }

        public string SelectedCTAType
        {
            get
            {
                return _selectedCTAType;
            }
            set
            {
                if (value != _selectedCTAType)
                {
                    _selectedCTAType = value;
                    RaisePropertyChanged(() => SelectedCTAType);
                }
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                if (value != _selectedCategory)
                {
                    _selectedCategory = value;
                    RaisePropertyChanged(() => SelectedCategory);
                }
            }
        }

        private MediaFile SelectedImage { get; set; }

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                if (value != _imageSource)
                {
                    _imageSource = value;
                    RaisePropertyChanged(() => ImageSource);
                }
            }
        }

        public ICommand SelectImageCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {
                    SelectedImage = await CrossMedia.Current.PickPhotoAsync();

                    if (SelectedImage == null)
                        return;

                    ImageSource = ImageSource.FromStream(() =>
                    {
                        var stream = SelectedImage.GetStream();
                        return stream;
                    });
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {
                    var result = await UserInteractionService.DisplayAlert("Delete", $"Delete this post?", "Yes", "Cancel");

                    if (result)
                    {
                        await ContentDataService.DeleteAsync(arg.Id);
                        await Close();
                    }
                });
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Item.Title))
                return false;

            if (string.IsNullOrEmpty(Item.ShortTitle))
                return false;

            if (string.IsNullOrEmpty(Item.Description))
                return false;

            if (string.IsNullOrEmpty(Item.ShortDescription))
                return false;

            if (Item.Date == null)
                return false;

            if (SelectedCategory == null)
                return false;

            if (SelectedImage == null)
                return false;

            return true;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {

                    if (!Validate())
                    {
                        await UserInteractionService.DisplayAlert("Error", "Missing required fields", "Ok");
                        return;
                    }

                    var result = await UserInteractionService.DisplayAlert("Add Post", "Save this post?", "Yes", "Cancel");

                    if (result)
                    {
                        Item.CategoryId = SelectedCategory.Id;
                        if (string.IsNullOrEmpty(Item.CTA.Url))
                        {
                            Item.CTA = null;
                        }

                        var item = await ContentDataService.InsertAsync(Item);

                        if (SelectedImage != null)
                        {
                            await StorageService.PutAsync(StorageFolder.Posts, item.Id, SelectedImage.GetStream());
                            var image = await StorageService.GetDownloadUrlAsync(StorageFolder.Posts, item.Id);

                            item.Image = image;

                            await ContentDataService.UpdateAsync(item);
                        }

                        await UserInteractionService.DisplayAlert("Add Post", "Your changes have been saved", "Ok");
                        await Close();
                    }
                });
            }
        }        
    }
}
