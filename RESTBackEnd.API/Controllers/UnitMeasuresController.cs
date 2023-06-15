using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models.UnitMeasure;

namespace RESTBackEnd.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UnitMeasuresController : ControllerBase
   {
		private readonly IMapper _mapper;
		private readonly IUnitMeasureRepository _unitMeasureRepository;

		public UnitMeasuresController(IMapper mapper, IUnitMeasureRepository unitMeasureRepository)
      {
			this._mapper = mapper;
			this._unitMeasureRepository = unitMeasureRepository;
		}

      // GET: api/UnitMeasures
      [HttpGet]
      public async Task<ActionResult<IEnumerable<UnitMeasureDto>>> GetUnitMeasures()
      {
         var unitMeasures = await _unitMeasureRepository.GetAllAsync();
			var dtoUnitMeasures = _mapper.Map<IEnumerable<UnitMeasureDto>>(unitMeasures);


			return Ok(dtoUnitMeasures);
      }

   }
}
