using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Versl.Services
{
	public interface IUserInteractionService
	{
		Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
		Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
		Task DisplayAlert(string title, string message, string cancel);
		Task<string> DisplayPrompt(string title, string message, string accept, string cancel);
	}

	public class UserInteractionService : ServiceBase<UserInteractionService>, IUserInteractionService
	{		
		#region IUserInteractionService implementation
		public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
		{
			return await CurrentPage.DisplayActionSheet(title, cancel, destruction, buttons);
		}

		public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
		{
			return await CurrentPage.DisplayAlert(title, message, accept, cancel);
		}

		public async Task DisplayAlert(string title, string message, string cancel)
		{
			await CurrentPage.DisplayAlert(title, message, cancel);
		}

		public async Task<string> DisplayPrompt(string title, string message, string accept, string cancel)
		{
			return await CurrentPage.DisplayPromptAsync(title, message, accept, cancel);
		}

		private static Page CurrentPage
		{
			get
			{
				return Xamarin.Forms.Application.Current.MainPage;
			}
		}
		#endregion
	}
}
