namespace Standard.ToList.Model.Options
{
    public class AppSettingOptions
	{
		public string BackendUrl { get; set; }
        public string FrontendUrl { get; set; }
        public string SecurityToken { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
		public Workers Workers { get; set; }
    }

	public class ConnectionStrings
	{
		public string DefaultConnection { get; set; }
		public MongoDbConnectionString MongoDbConnection { get; set; }
	}

	public class MongoDbConnectionString
	{
        public string DatabaseName { get; set; }
		public string ConnectionString { get; set; }
    }

	public class Workers
	{
		public MarketWorker MarketWorker { get; set; }
    }

	public class MarketWorker
	{
		public int MissingProductsDelay { get; set; }
        public int UpdateProductsDelay { get; set; }
        public string[] KeyProducts { get; set; }
	}
}



