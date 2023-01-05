using System.ComponentModel.DataAnnotations;

namespace MailAPI.Models.Mail
{
    public class SendMail
    {

        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? APIKey { get; set; }
        public string? Header { get; set; }
        public string? Subject { get; set; }
        public string? To { get; set; }
        public string? Body { get; set; }
   

    }
}
