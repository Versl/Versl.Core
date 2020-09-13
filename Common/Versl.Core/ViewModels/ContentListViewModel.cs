using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Versl.Services;
using Versl.Models;
using Versl.Services.Data;
using Versl.Services.Media;
using Versl.Services.Analytics;
using System.Collections.Generic;

namespace Versl.ViewModels
{
    public class ContentListViewModel : ListViewViewModelBase<ContentItem>, IContentListViewModel            
    {
        protected ICategoryDataService CategoryDataService;

        private ObservableCollection<ContentItem> _items;

        public ContentListViewModel(IContentDataService contentService, ICategoryDataService categoryDataService, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IMvxMessenger messenger, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
        {
            ListService = contentService;
            CategoryDataService = categoryDataService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            await GetCategories();
        }

        public ObservableCollection<Category> Categories { get; set; }

        public async override Task GetItems()
        {
            IsBusy = true;

            try
            {
                List<ContentItem> itemList;

                itemList = await ListService.GetListAsync();
                itemList.RemoveAll(c => c.IsPublished == false);
                itemList.RemoveAll(c => c.CategoryId == "0");

                if (itemList == null)
                {
                    IsBusy = false;
                }

                ListItems = new ObservableCollection<ContentItem>(itemList);
            }
            catch (Exception ex)
            {               
                await UserInteractionService.DisplayAlert("Error", "An error occurred loading data", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async virtual Task GetCategories()
        {
            IsBusy = true;

            try
            {
                var list = await CategoryDataService.GetListAsync();

                if (list == null)
                {
                    IsBusy = false;
                }

                Categories = new ObservableCollection<Category>(list);
            }
            catch (Exception ex)
            {                
                await UserInteractionService.DisplayAlert("Error", "An error occurred loading data", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override ICommand ItemSelectedCommand
        {
            get
            {
                return new MvxAsyncCommand<ContentItem>(async (ContentItem arg) => {
                    await Navigate<IContentDetailViewModel>(arg.Id);
                });
            }
        }

        public ICommand ShowCategoriesCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => {

                    await GetCategories();

                    var categories = new List<string>();

                    categories.Add("All");
                    categories.AddRange(Categories.Select(c => c.Name).ToList());

                    var categoryName = await UserInteractionService.DisplayActionSheet("Select Category", "Cancel", null, categories.ToArray());

                    if (categoryName == "Cancel")
                    {
                        return;
                    }

                    if (categoryName == "All")
                    {
                        ListItems = _items;
                        return;
                    }

                    var category = Categories.First<Category>(c => c.Name == categoryName);

                    ListItems = new ObservableCollection<ContentItem>(_items.Where(c => c.CategoryId == category.Id));                    
                });
            }
        }
    }
}
