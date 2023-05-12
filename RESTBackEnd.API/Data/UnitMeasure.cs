using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace RESTBackEnd.API.Data
{
    public class UnitMeasure
    {
        [Key]
        public int UnitMeasureId { get; set; }

        [Required, MaxLength(5)]
        public string? Code { get; set; }

        [Required]
        public string? LongName { get; set; }

        public IList<Ingredient>? Ingredients { get; set; }
    }
}
