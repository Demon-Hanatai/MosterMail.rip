namespace MailAPI.Models.Mail
{
    public class GetMailBId:SendMail
    {

        public int Id { get; set; }
        public string From { get; set; }
    }
}
