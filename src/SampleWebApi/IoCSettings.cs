using System;
using System.Net.Http;
using System.Reflection;
using Drum;
using HttpEx;
using Library.DomainModel;
using Library.DomainModel.Storage;
using Library.DomainServices;
using Library.Storage;
using Library.Storage.Providers;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

namespace Library.WebApi
{
    public static class IoCSettings
    {
        //internal static void Configure(IApplicationBuilder app)
        //{
        //    //IContainer container = null;
        //    //var builder = new ContainerBuilder();

        //    //builder.RegisterApiControllers( Assembly.GetExecutingAssembly() ).InstancePerRequest();
        //    //builder.RegisterWebApiFilterProvider( config );

        //    //RegisterTypes( builder );
        //    //RegisterDrum( config, builder );

        //    //container = builder.Build();
        //    //config.DependencyResolver = new AutofacWebApiDependencyResolver( container );

        //    //app.UseAutofacMiddleware( container );
        //    //app.UseAutofacWebApi( config );
        //    //app.UseWebApi( config );

        //}

        //private static void RegisterDrum( HttpConfiguration config, ContainerBuilder builder )
        //{
        //    // Web API routes
        //    UriMakerContext uriMakerContext = config.MapHttpAttributeRoutesAndUseUriMaker();
        //    builder.RegisterInstance( uriMakerContext ).ExternallyOwned();
        //    builder.RegisterHttpRequestMessage( config );
        //    builder.RegisterGeneric( typeof( UriMaker<> ) ).AsSelf().InstancePerRequest();

        //    builder.RegisterType<DrumUrlProvider>().As<IUrlProvider>();
        //}

        public static void RegisterTypes( IServiceCollection services)
        {
			services.TryAddSingleton<IBookService, BookService>();
			services.TryAddSingleton<IAuthorService, AuthorService>();
			services.TryAddSingleton<ILendingService, LendingService>();

			//builder.RegisterType<BookService>().As<IBookService>();
			//builder.RegisterType<AuthorService>().As<IAuthorService>();
			//builder.RegisterType<LendingService>().As<ILendingService>();

			services.TryAddSingleton<BookResourceAssembler>();
			services.TryAddSingleton<AuthorResourceAssembler>();
			services.TryAddSingleton<LendingRecordResourceAssembler>();
			
			//builder.RegisterType<BookResourceAssembler>().AsSelf().PropertiesAutowired( PropertyWiringOptions.AllowCircularDependencies );
			//builder.RegisterType<AuthorResourceAssembler>().AsSelf().PropertiesAutowired( PropertyWiringOptions.AllowCircularDependencies );
			//builder.RegisterType<LendingRecordResourceAssembler>().AsSelf();

			services.TryAddSingleton<IAuthorStore, AuthorDocumentStore>();
			services.TryAddSingleton<IBookStorage, BookDocumentStore>();
			services.TryAddSingleton<ILendingRecordStore, LendingRecordDocumentStore>();

			//builder.RegisterType<AuthorDocumentStore>().As<IAuthorStore>();
			//builder.RegisterType<BookDocumentStore>().As<IBookStorage>();
			//builder.RegisterType<LendingRecordDocumentStore>().As<ILendingRecordStore>();

			services.TryAddSingleton<IAuthorRepository, AuthorRepository>();
			services.TryAddSingleton<IISBNLookupService,ISBNDBLookupService>();

			//builder.RegisterType<AuthorRepository>().As<IAuthorRepository>();
			//builder.RegisterType<ISBNDBLookupService>().As<IISBNLookupService>();

			services.AddSingleton((sp) => DocumentStoreIndex.BookStore);
			services.AddSingleton((sp) => DocumentStoreIndex.AuthorStore);
			services.AddSingleton((sp) => DocumentStoreIndex.LendingRecordStore);

			//builder.RegisterInstance( DocumentStoreIndex.BookStore ).As<IDocumentStore<Book>>();
			//builder.RegisterInstance( DocumentStoreIndex.AuthorStore ).As<IDocumentStore<Author>>();
			//builder.RegisterInstance( DocumentStoreIndex.LendingRecordStore ).As<IDocumentStore<LendingRecord>>();
		}
    }
}
