namespace Standard.ToList.Model.Options
{
    public class AppSettingOptions
	{
		public string BackendUrl { get; set; }
        public string FrontendUrl { get; set; }
        public string SecurityToken { get; set; }
		public string[] AllowedAdmins { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
		public Workers Workers { get; set; }
		public Smtp Smtp { get; set; }
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
		public WatcherWorker WatcherWorker { get; set; }

    }

	public class MarketWorker
	{
		public int MissingProductsDelay { get; set; }
        public int UpdateProductsDelay { get; set; }
        public string[] KeyProducts { get; set; }
	}

	public class WatcherWorker
	{
		public int WatcherDelay { get; set; }
		public int MessageInterval { get; set; }
	}

	public class Smtp
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string From { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}



