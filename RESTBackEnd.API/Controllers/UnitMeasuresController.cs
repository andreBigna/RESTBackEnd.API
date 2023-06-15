using Microsoft.AspNetCore.Mvc;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Repository;

namespace RESTBackEnd.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UnitMeasuresController : ControllerBase
   {
		private readonly UnitMeasureRepository _unitMeasureRepository;

		public UnitMeasuresController(UnitMeasureRepository unitMeasureRepository)
      {
			this._unitMeasureRepository = unitMeasureRepository;
		}

      // GET: api/UnitMeasures
      [HttpGet]
      public async Task<ActionResult<IEnumerable<UnitMeasure>>> GetUnitMeasures()
      {
         var unitMeasures = await _unitMeasureRepository.GetAllAsync();

         return Ok(unitMeasures);
      }

   }
}
