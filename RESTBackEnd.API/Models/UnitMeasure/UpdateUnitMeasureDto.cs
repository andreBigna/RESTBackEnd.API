using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.UnitMeasure
{
	public record UpdateUnitMeasureDto : BaseUnitMeasureDto
	{
		[Required] public int UnitMeasureId { get; set; }
	}
}