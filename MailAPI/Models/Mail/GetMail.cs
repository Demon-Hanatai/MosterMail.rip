using System.ComponentModel.DataAnnotations;

namespace MailAPI.Models.Mail
{
    public class GetMail
    {
        [Key]
        public int Id { get; set; }
        public class Root
        {
           
            public List<data> data { get; set; }
        }
     
        public class Sender
        {
           
          
            public string display_name { get; set; }
            public string email { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
        public class Inbox
        {
           
            
            public string display_name { get; set; }
            public string email { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
        public class data
        {
           
          
            public string subject { get; set; }
            public string created_at { get; set; }
            public List<object> attachments { get; set; }
            public Sender sender { get; set; }
            public Inbox inbox { get; set; }
        }
    }
}
