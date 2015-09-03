using System.Collections.Generic;
using System.Threading.Tasks;
using HttpEx.REST;
using Library.DataTransferObjects;
using Library.DomainModel;
using Microsoft.AspNet.Mvc;

namespace Library.WebApi
{
	[Route("api/[controller]")]
	public class BookController : Controller
    {
        public IBookService BookService { get; set; }
        private BookResourceAssembler BookAssembler { get; set; }

        public BookController( IBookService bookService, BookResourceAssembler assembler )
        {
            BookService = bookService;
            BookAssembler = assembler;
        }

		/// <summary>
		/// Retrieves all books.
		/// </summary>
		[HttpGet(Name = "Book_GetAll"), Produces(typeof(IEnumerable<BookResource>))]
        public async Task<IActionResult> GetAllAsync( string expand = null )
        {
            IEnumerable<Book> books = await BookService.GetAllBooksAsync();
            var resourceCollection = await BookAssembler.ConvertToResourceCollectionAsync( books, expand );
            return new ObjectResult( resourceCollection );
        }

        /// <summary>
        /// Retrieves the book with the specified id.
        /// </summary>
        /// <param name="id">The Id of the book to retrieve.</param>
        [HttpGet("{id}",Name = "Book_GetById"), Produces(typeof(BookResource))]
        public async Task<IActionResult> GetByIdAsync( string id, string expand = null )
        {
            Book book = await BookService.GetBookByIdAsync( id );
            if( book == null ) return HttpNotFound();

            var resource = await BookAssembler.ConvertToResourceAsync( book, expand );
            return new ObjectResult( resource );
        }

        /// <summary>
        /// Retrieves all the books written by a specific author
        /// </summary>
        /// <param name="authorId">The Id of the author.</param>
        /// <param name="expand">The child resources that should be expanded</param>
        [HttpGet("ByAuthor/{authorId}", Name = "Book_GetByAuthorId"), Produces(typeof(ResourceCollection<BookResource>))]
		public async Task<IActionResult> GetByAuthorIdAsync( string authorId, string expand = null )
        {
            IEnumerable<Book> books = await BookService.GetBooksByAuthorIdAsync( authorId );
            var resourceCollection = await BookAssembler.ConvertToResourceCollectionAsync( books, expand );
			resourceCollection.Href = Request.Path.ToString();//RequestUri.ToString();

            return new ObjectResult( resourceCollection );
        }

        /// <summary>
        /// Adds a new book to the inventory.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to add.</param>
        /// <returns>The book that was added.</returns>
		[HttpPost(Name = "Book_AddByIsbn"), Produces(typeof(BookResource))]
		public async Task<IActionResult> AddBookAsync( string isbn )
        {
            Book book = await BookService.AddByISBNAsync( isbn );

            if( book == null ) return HttpBadRequest( "the provided ISBN does not resolve to a book in our backend service" );

            var resource = await BookAssembler.ConvertToResourceAsync( book );
            return CreatedAtRoute( "api", new { id = book.Id }, resource );
        }
    }
}
