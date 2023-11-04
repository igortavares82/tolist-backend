using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Standard.ToList.Model.Aggregates.Configuration
{
    public class Logger : Entity
    {
        public Logger(Dictionary<LogLevel, bool> levelConfiguration)
        {
            LevelConfiguration = levelConfiguration;
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<LogLevel, bool> LevelConfiguration { get; set; } = new Dictionary<LogLevel, bool>();
    }
}
