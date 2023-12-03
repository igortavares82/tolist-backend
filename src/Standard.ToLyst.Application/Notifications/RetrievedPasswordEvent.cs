using MediatR;
using Standard.ToLyst.Model.Aggregates.Users;

namespace Standard.ToLyst.Application
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
