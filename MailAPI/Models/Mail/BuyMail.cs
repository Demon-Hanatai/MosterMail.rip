using System.ComponentModel.DataAnnotations;

namespace MailAPI.Models.Mail
{
    public class BuyMail
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public double Price { get; set; }
        public DateTime DateTime { get; set; }
        public string Domain { get; set; }        
        public int MailID { get; set; }
        public int? OwnerId { get; set; }
    }
}
