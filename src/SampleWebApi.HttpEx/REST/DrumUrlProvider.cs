//using System;
//using System.Linq.Expressions;
//using Microsoft.AspNet.Mvc;
//using Drum;
//using Microsoft.AspNet.Http;

//namespace HttpEx
//{
//	public class DrumUrlProvider : IUrlProvider
//	{
//		private readonly UriMakerContext _context;
//		private readonly HttpRequest _request;

//		public DrumUrlProvider(UriMakerContext context, HttpRequest request)
//		{
//			_context = context;
//			_request = request;
//		}

//		public Uri UriFor<TController>(Expression<Action<TController>> action)
//		{
//			HttpRequest message = _request;
//			var maker = new UriMaker<TController>(_context, message.);
//			return maker.UriFor(action);
//		}

//		public string UriStringFor<TController>(Expression<Action<TController>> action)
//		{
//			return UriFor<TController>(action).ToString();
//		}

//		public string GetRequestUri()
//		{
//			return _request.RequestUri.ToString();
//		}
//	}
//}
