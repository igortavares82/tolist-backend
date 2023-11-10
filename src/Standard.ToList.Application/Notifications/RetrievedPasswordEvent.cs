using MediatR;
using Standard.ToList.Model.Aggregates.Users;

namespace Standard.ToList.Application
{
    public struct RetrievedPasswordEvent : INotification
    {
        public RetrievedPasswordEvent(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
