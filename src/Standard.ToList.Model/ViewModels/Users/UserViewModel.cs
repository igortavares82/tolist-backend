using System;
using Standard.ToList.Model.Aggregates.Users;

namespace Standard.ToList.Model.ViewModels.Users
{
	public class UserViewModel
	{
		public UserViewModel(User user)
		{
			Id = user.Id;
			Name = user.Name;
			Email = user.Email;
			CreateDate = user.CreateDate.ToString("dd/MM/yyyy HH:mm");
			LastUpdate = user.LastUpdate?.ToString("dd/MM/yyyy HH:mm");
			IsEnabled = user.IsEnabled;
			IsActive = user.IsActive;
        }

        public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string CreateDate { get; set; }
		public string LastUpdate { get; set; }
		public bool? IsEnabled { get; set; }
        public bool? IsActive { get; set; }
    }
}

