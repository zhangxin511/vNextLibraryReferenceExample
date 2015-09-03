using System;
using System.Threading.Tasks;
using Library.DataTransferObjects;
using Library.DomainModel;
using Microsoft.AspNet.Mvc;
using SampleWebApi;

namespace Library.WebApi
{
    [Route( "api/[controller]" )]
    public class BorrowController : Controller
    {
        public ILendingService LendingService { get; set; }
        private LendingRecordResourceAssembler RecordAssembler { get; set; }

        public BorrowController( ILendingService lendingService, LendingRecordResourceAssembler recordAssembler )
        {
            LendingService = lendingService;
            RecordAssembler = recordAssembler;
        }

        /// <summary>
        /// Checks out the book with the specified id.
        /// </summary>
        /// <param name="bookId">The id of the book.</param>
		[HttpPost("checkout/{bookId}", Name = "Borrow_Checkout"), Produces(typeof(LendingRecordResource))]
		public async Task<IActionResult> CheckoutAsync( string bookId )
        {
            try
            {
                var lendingRecord = await LendingService.CheckoutAsync( bookId );
                var resource = await RecordAssembler.ConvertToResourceAsync( lendingRecord );

                return CreatedAtRoute( Startup.DefaultRouteName, new { controller = "lendingRecord", id = lendingRecord.Id }, resource );
            }
            catch( DomainOperationException ex )
            {
                return HttpBadRequest( ex.Message );
            }
            catch( InvalidOperationException )
            {
                return HttpBadRequest( "that book is already checked out" );
            }
        }

        /// <summary>
        /// Checks in the book with the specified id.
        /// </summary>
        /// <param name="bookId">The id of the book.</param>
        [HttpPost("checkout/{bookId}", Name = "Borrow_Checkin"), Produces(typeof(LendingRecordResource))]
		public async Task<IActionResult> CheckinAsync( string bookId )
        {
            try
            {
                var lendingRecord = await LendingService.CheckinAsync( bookId );
                var resource = await RecordAssembler.ConvertToResourceAsync( lendingRecord );

                return new ObjectResult( resource );
            }
            catch( DomainOperationException ex )
            {
                return HttpBadRequest( ex.Message );
            }
            catch( InvalidOperationException )
            {
                return HttpBadRequest( "that book is already checked in" );
            }
        }
    }
}
