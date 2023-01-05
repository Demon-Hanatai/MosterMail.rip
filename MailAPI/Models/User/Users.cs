using MailAPI.Data;
using MailAPI.Models.Mail;
using MailAPI.Models.MailHelper;
using System.ComponentModel.DataAnnotations;

namespace MailAPI.Models.User
{
    public class Users
    {

        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; }
        public DateTime CreationDate { get; set; }
        public double Credit { get; set; } = 20;
        public string IP { get; set; }
        public string Roles { get; set; } = nameof(Users);
        public int UserID { get; set; }
        public Users()
        {
         
           

        }

    }

}
