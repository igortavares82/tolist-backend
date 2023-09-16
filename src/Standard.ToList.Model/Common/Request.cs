namespace Standard.ToList.Model.Common
{
    public class Request
	{
		public string ResourceId { get; set; }
		public string UserId { get; set; } = "abc123";
		public Page Page { get; set; } = new Page();
		public Order Order { get; set; }
    }

	public class Page
	{
		public int Size { get; set; } = 10;
		public int Index { get; set; } = 1;
		public int Skip { get { return Size * (Index - 1); } }
	}

	public class Order
	{
		public string Field { get; set; }
		public int Direction { get; set; } 
	}
}
