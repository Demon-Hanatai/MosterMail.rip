using MailAPI.Models.Mail;

using MailAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace MailAPI.Data
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<Users>? users { get; set; } 
        public DbSet<BuyMail> buyMail { get; set; } 
        public DbSet<GetAllMail> GetAllMail { get; set; }
        public DbSet<GetMail> GetMail { get; set; }
        public DbSet<GetMailBId> GetGetMailBIds { get; set; }
        public DbSet<Mail> Mail { get;set; }
        public DbSet<MailHistory> MailHistory { get; set; }
        public DbSet<SendMail> SendMail { get; set; }
        
    }
}
