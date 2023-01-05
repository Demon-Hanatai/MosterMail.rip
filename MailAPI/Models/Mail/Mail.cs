using MailAPI.Models.User;
using System.ComponentModel.DataAnnotations;

namespace MailAPI.Models.Mail
{
    public class Mail:API
    {
       
        public DateTime TodayDate { get; set; }
        public int ID { get; set; }
        
    }
}
