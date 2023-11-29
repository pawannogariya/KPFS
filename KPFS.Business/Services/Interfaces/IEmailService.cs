using KPFS.Business.Models;

namespace KPFS.Business.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(MessageDto message);
    }
}
