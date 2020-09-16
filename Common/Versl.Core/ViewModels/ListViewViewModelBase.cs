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
using Versl.Services.Analytics;
using Versl.Services.Data;
using Versl.Services.Media;

namespace Versl.ViewModels
{
    public abstract class ListViewViewModelBase<M> : ViewModelBase where M : DataModelBase
    {       
		private ObservableCollection<M> _listItems;
		private M _selectedItem;
		private bool _isRefreshing;		

		public ListViewViewModelBase(IMvxMessenger messenger, IMvxNavigationService navigationService, IUserInteractionService userInteractionService, IMediaService mediaService, IAnalyticsService analyticsService) : base(messenger, navigationService, userInteractionService, mediaService, analyticsService)
		{			

        }

        public override async Task Initialize()
        {
            await base.Initialize();
			await GetItems();
        }

        public async virtual Task GetItems()
		{
			IsBusy = true;

			try
			{
				List<M> itemList;

				itemList = await ListService.GetListAsync();

				if (itemList == null)
				{
					IsBusy = false;
				}

				ListItems = new ObservableCollection<M>(itemList);
			}
			catch (Exception ex)
			{
                //await ErrorLoggerService.Log(ex);
                await UserInteractionService.DisplayAlert("Error", "An error occurred loading data", "Ok");
            }

			IsBusy = false;
		}

		public async virtual Task<M> GetItem(string id)
		{
			IsBusy = true;

			return await ListService.GetAsync(id);
		}

		public virtual ObservableCollection<M> ListItems
		{
			get
			{
				return _listItems;
			}
			protected set
			{
				if (_listItems != value)
				{
					_listItems = value;

					RaisePropertyChanged(() => ListItems);
				}
			}
		}

		public virtual M SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				if (_selectedItem != value)
				{
					_selectedItem = value;

					RaisePropertyChanged(() => SelectedItem);
					if (value != null)
					{
						ItemSelectedCommand.Execute(value);
					}
					SelectedItem = null;
				}
			}
		}

		public abstract ICommand ItemSelectedCommand { get; }

		protected IDataService<M> ListService
		{
			get;
			set;
		}

		public bool IsRefreshing
		{
			get
			{
				return _isRefreshing;
			}
			set
			{
				if (_isRefreshing != value)
				{
					_isRefreshing = value;

					RaisePropertyChanged(() => IsRefreshing);
				}
			}
		}

		public virtual ICommand RefreshCommand
		{
			get
			{
				return new MvxAsyncCommand(async () => {
					IsRefreshing = true;
					await GetItems();
					IsRefreshing = false;
				});
			}
		}
	}
}
