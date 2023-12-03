using System;
using System.Collections.Generic;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Standard.ToLyst.Model.Aggregates
{
    public class Entity
	{
		[BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
		public bool? IsEnabled { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? LastUpdate { get; set; }
		public DateTime? ExpireAt { get; set; }

		[BsonIgnore]
		public List<INotification> Notifications { get; set; } 

		public Entity()
		{
			IsEnabled = true;
			CreateDate = DateTime.Now;
			Notifications = new List<INotification>();
		}

		public Entity(string id) : this()
		{
			Id = id;
		}

		public void SetExpireAt(double addSeconds = 1)
		{
			ExpireAt = CreateDate.AddSeconds(addSeconds);
		}

		public void AddNotification(params INotification[] notifications)
		{
			if (notifications == null || notifications.Length == 0)
				return;

			if (Notifications == null)
				Notifications = new List<INotification>();

			foreach(var notification in notifications)
				Notifications?.Add(notification);
		}
	}
}

