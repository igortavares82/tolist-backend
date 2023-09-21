﻿using System;

namespace Standard.ToList.Model.Aggregates.Users
{
	public class User : Entity
	{
		public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
		public DateTime? LastAccess { get; set; }
		public RoleType Role { get; set; }
		public bool IsActive { get; set; }
		public string ActivationToken { get; set; }

		public User(string name,
					string password,
					string email,
					DateTime createDate,
					RoleType role = RoleType.Free)
		{
			Name = name;
			Password = password;
			Email = email;
			CreateDate = createDate;
            Role = role;
			IsActive = role == RoleType.Admin;
		}

		public void SetLastAccess() => LastAccess = DateTime.UtcNow;

		public void SetActivationToke(string token) => ActivationToken = token;

		public void SetAsActive()
		{
			IsActive = true;
			ActivationToken = null;
		}

		public void Update(string name, string password)
		{
			Name = name ?? Name;
			Password = password ?? Password;
			LastUpdate = DateTime.UtcNow;
		}
	}
}
