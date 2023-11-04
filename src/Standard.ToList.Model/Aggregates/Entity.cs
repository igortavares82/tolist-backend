﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Standard.ToList.Model.Aggregates
{
    public class Entity
	{
		[BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
		public bool? IsEnabled { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? LastUpdate { get; set; }
		public DateTime? ExpireAt { get; set; }

		public Entity()
		{
			IsEnabled = true;
			CreateDate = DateTime.Now;
		}

		public Entity(string id) : this()
		{
			Id = id;
		}

		public void SetExpireAt(double addSeconds = 1)
		{
			ExpireAt = CreateDate.Value.AddSeconds(addSeconds);
		}
	}
}

