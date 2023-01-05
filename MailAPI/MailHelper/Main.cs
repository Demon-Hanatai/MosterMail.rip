using MailAPI.Models.Mail;

namespace MailAPI.MailHelper
{
    public class Main
    {
        public object MainW(BuyMail Email)
        {
            if(Email.Domain == "@")
            return null;
        }
    }
}
