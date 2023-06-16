namespace RESTBackEnd.API.Models
{
	public class PagedResults<T>
	{
		public int TotalRecord { get; set; }
		public int Page { get; set; }
		public int RecordNumber { get; set; }
		public List<T>? Items { get; set; }
	}
}