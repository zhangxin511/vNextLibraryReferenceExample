using System.Collections.Generic;
using System.Threading.Tasks;
using HttpEx.REST;
using Library.DataTransferObjects;
using Library.DomainModel;
using Microsoft.AspNet.Mvc;

namespace Library.WebApi
{
    [Route( "api/[controller]" )]
    public class AuthorController : Controller
    {
        private IAuthorService AuthorService { get; set; }
        private AuthorResourceAssembler AuthorAssembler { get; set; }

        public AuthorController( IAuthorService authorService, AuthorResourceAssembler assembler )
        {
            AuthorService = authorService;
            AuthorAssembler = assembler;
        }

		/// <summary>
		/// Retrieves all the authors in the system.
		/// </summary>
		[HttpGet(Name = "Author_GetAll"), Produces(typeof(ResourceCollection<AuthorResource>))]
		public async Task<IActionResult> GetAllAsync( string expand = null )
        {
            IEnumerable<Author> authors = await AuthorService.GetAllAuthorsAsync();

            var resourceCollection = await AuthorAssembler.ConvertToResourceCollectionAsync( authors, expand );
            return new ObjectResult( resourceCollection );
        }

		/// <summary>
		/// Retrieves the author with the specified id.
		/// </summary>
		[HttpGet("{id}", Name = "Author_GetByIdAsync"), Produces(typeof(AuthorResource))]
        public async Task<IActionResult> GetByIdAsync( string id, string expand = null )
        {
            var author = await AuthorService.GetAuthorByIdAsync( id );
            if( author == null ) return HttpNotFound();

            var resource = await AuthorAssembler.ConvertToResourceAsync( author, expand );
			return new ObjectResult( resource );
        }
    }
}
