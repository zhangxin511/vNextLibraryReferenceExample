using System.Collections.Generic;
using System.Threading.Tasks;
using HttpEx.REST;
using Library.DataTransferObjects;
using Library.DomainModel;
using Microsoft.AspNet.Mvc;

namespace Library.WebApi
{
	[Route("api/[controller]")]
	public class LendingRecordController : Controller
    {
        private ILendingService LendingService { get; set; }
        private LendingRecordResourceAssembler RecordAssembler { get; set; }

        public LendingRecordController( ILendingService lendingService, LendingRecordResourceAssembler assembler )
        {
            LendingService = lendingService;
            RecordAssembler = assembler;
        }

        /// <summary>
        /// Gets all the lending records.
        /// </summary>
		[HttpGet(Name = "LendingRecord_GetAll"), Produces(typeof(ResourceCollection<LendingRecordResource>))]
		public async Task<IActionResult> GetAllAsync( string expand = null )
        {
            IEnumerable<LendingRecord> records = await LendingService.GetAllRecordsAsync();
            var resourceCollection = await RecordAssembler.ConvertToResourceCollectionAsync( records, expand );
            return new ObjectResult( resourceCollection );
        }

        /// <summary>
        /// Retrieves the lending record with the specified id.
        /// </summary>
        /// <param name="id">The id of the lending record to retrieve.</param>
        [HttpGet("{id}", Name = "LendingRecord_GetById"), Produces(typeof(LendingRecordResource))]
		public async Task<IActionResult> GetByIdAsync( string id, string expand = null )
        {
            var record = await LendingService.GetByIdAsync( id );
            if( record == null ) return HttpNotFound();

            var resource = await RecordAssembler.ConvertToResourceAsync( record, expand );
            return new ObjectResult( resource );
        }
    }
}
