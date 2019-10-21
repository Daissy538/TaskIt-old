using TaskItApi.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Handlers.Interfaces
{
    public interface IEmailHandler
    {
        void SendInviteEmail(EmailDTO email);

        EmailDTO CreateInviteEmail(User recievingUser, User sendingUser, Group group);
    }
}
