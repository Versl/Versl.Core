using System;
using Newtonsoft.Json;

namespace Versl.Models
{
	public class User : UserProfile
    {
		[JsonIgnore]
		public string AuthToken { get; set; }

		[JsonIgnore]
		public string RefreshToken { get; set; }
	}
}
