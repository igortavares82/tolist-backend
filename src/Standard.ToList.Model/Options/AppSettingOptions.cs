using System;
using System.Collections.Generic;

namespace Standard.ToList.Model.Options
{
	public class AppSettingOptions
	{
		public ConnectionStrings ConnectionStrings { get; set; }
		
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
}



