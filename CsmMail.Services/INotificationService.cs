using System.Threading.Tasks;
using CsmMail.Dto;

namespace CsmMail.Services
{
  public interface INotificationService
    { 
         Task<string> SendAsync(MailDto mail);
    }
}
