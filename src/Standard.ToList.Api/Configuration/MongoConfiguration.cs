using MongoDB.Bson.Serialization;
using Standard.ToList.Model.Aggregates;

namespace Standard.ToList.Api.Configuration;

public static class MongoConfiguration
{
    public static void ConfigureMongo(this IServiceCollection services)
    {
        BsonClassMap.RegisterClassMap<Entity>(map => 
        {
            map.AutoMap();
            map.UnmapMember(p => p.Notifications);
        });
    }
}
