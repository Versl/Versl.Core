using System;
using System.Threading.Tasks;

namespace Versl.Services
{
	public interface IServiceBase
	{

    }

	public abstract class ServiceBase<T> where T : new()
	{
		private static T instance;

		public ServiceBase()
		{
		}

		public static T DefaultInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new T();					
				}

				return instance;
			}
		}
	}
}
