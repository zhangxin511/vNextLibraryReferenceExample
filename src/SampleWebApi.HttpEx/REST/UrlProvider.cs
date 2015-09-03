using HttpEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SampleWebApi.HttpEx.REST
{
	public class UrlProvider : IUrlProvider
	{
		public string GetRequestUri()
		{
			throw new NotImplementedException();
		}

		public Uri UriFor<TController>(Expression<Action<TController>> action)
		{
			throw new NotImplementedException();
		}

		public string UriStringFor<TController>(Expression<Action<TController>> action)
		{
			throw new NotImplementedException();
		}
	}
}
