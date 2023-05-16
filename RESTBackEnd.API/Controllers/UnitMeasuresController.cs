using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Ingredient;
using RESTBackEnd.API.Models.UnitMeasure;

namespace RESTBackEnd.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UnitMeasuresController : ControllerBase
	{
		private readonly RestBackEndDbContext _context;
		private readonly IMapper _mapper;

		public UnitMeasuresController(RestBackEndDbContext context, IMapper mapper)
		{
			_context = context;
			this._mapper = mapper;
		}

		// GET: api/UnitMeasures
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetUnitMeasureDto>>> GetUnitMeasures()
		{
			var ums = await _context.UnitMeasures.ToListAsync();
			var dtoUms = _mapper.Map<IEnumerable<GetUnitMeasureDto>>(ums);

			return Ok(dtoUms);
		}

		// GET: api/UnitMeasures/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<GetUnitMeasureDto>> GetUnitMeasure(int id)
		{
			var unitMeasure = await _context.UnitMeasures.FindAsync(id);

			if (unitMeasure == null) return NotFound();

			var um = _mapper.Map<GetUnitMeasureDto>(unitMeasure);

			return Ok(um);
		}

		// PUT: api/UnitMeasures/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutUnitMeasure(int id, UpdateUnitMeasureDto updateUnitMeasureDto)
		{
			if (id != updateUnitMeasureDto.UnitMeasureId) return BadRequest();

			var unitMeasure = await _context.UnitMeasures.FindAsync(id);

			if (unitMeasure == null) return NotFound();

			_mapper.Map(updateUnitMeasureDto, unitMeasure);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UnitMeasureExists(id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// POST: api/UnitMeasures
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<CreateUnitMeasureDto>> PostUnitMeasure(CreateUnitMeasureDto createUnitMeasureDto)
		{
			var unitMeasure = _mapper.Map<UnitMeasure>(createUnitMeasureDto);

			_context.UnitMeasures.Add(unitMeasure);
			await _context.SaveChangesAsync();

			return Ok(createUnitMeasureDto);
		}

		// DELETE: api/UnitMeasures/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteUnitMeasure(int id)
		{
			var unitMeasure = await _context.UnitMeasures.FindAsync(id);
			if (unitMeasure == null) return NotFound();

			_context.UnitMeasures.Remove(unitMeasure);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool UnitMeasureExists(int id)
		{
			return (_context.UnitMeasures?.Any(e => e.UnitMeasureId == id)).GetValueOrDefault();
		}
	}
}