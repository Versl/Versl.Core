using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Auth
{   
	public interface IAuthenticationService
	{
		Task CreateUserAsync(CreateUserRequest request);
		Task LoginAsync(string email, string password);	
		Task RefreshLoginAsync(string token);
		Task LogoutAsync();
		Task SendPasswordResetEmailAsync(string email);
		User CurrentUser { get; }
		Task UpdateProfileAsync(UpdateUserRequest request);
	}
}