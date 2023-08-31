using ExtremeSports2.Common;

namespace ExtremeSports2.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
