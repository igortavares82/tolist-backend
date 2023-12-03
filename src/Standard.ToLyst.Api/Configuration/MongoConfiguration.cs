using MongoDB.Bson.Serialization;
using Standard.ToLyst.Model.Aggregates;

namespace Standard.ToLyst.Api.Configuration;

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
