using System;

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
			IsActive = false;	 
		}

		public void SetLastAccess() => LastAccess = DateTime.UtcNow;
	}
}
