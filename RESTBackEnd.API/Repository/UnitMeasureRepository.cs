using AutoMapper;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;

namespace RESTBackEnd.API.Repository
{
	public class UnitMeasureRepository : BaseRepository<UnitMeasure>, IUnitMeasureRepository
	{
		private readonly RestBackEndDbContext _context;

		public UnitMeasureRepository(RestBackEndDbContext context, IMapper mapper) : base(context, mapper)
		{
			this._context = context;
		}
	}
}