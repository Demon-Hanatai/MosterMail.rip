using MailAPI.Data;
using MailAPI.Models.Mail;
using MailAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Versioning;
using RestSharp;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
       
       public ApiContext Db;
        static List<BuyMail> db = new List<BuyMail>()
        {
            new BuyMail()
            {
              
                DateTime = DateTime.Now,
                Domain = "@jellyfree.com",
                Email = "demonde@jellyfree.com",
                MailID = 5434,
                Price = 4
            },
             new BuyMail()
            {
          
                DateTime = DateTime.Now,
                Domain = "@jellyfree.com",
                Email = "desemonde@jellyfree.com",
                MailID = 5444,
                Price = 4
            },
            new BuyMail()
            {
                 
                DateTime = DateTime.Now,
                Domain = "@jellyfree.com",
                Email = "emdjjede@jellyfree.com",
                MailID = 542434,
                Price = 4
            }
           };

        
        public MailController(ApiContext us)
        {
            Db = us;

         
        }
      
        // GET: api/<MailController>
        [HttpGet("[action]")]
        public IActionResult GetNewEMail([FromHeader] string token,string Domain)
        {
              
              Random random = new Random();
             if(Db.users.Any(x => x.Token == token))
             {
                var helper = Db.users.FirstOrDefault(x => x.Token == token);
                int index = random.Next(0, Db.buyMail.ToList().Where(x=> x.OwnerId == null).Count());
             
                if (!Db.buyMail.ToList().Any(x=> x.OwnerId ==null))
                    return Ok("this domain is not available  at this movement try again later");
                else if (!Db.buyMail.Any(x => x.Domain == Domain))
                    return BadRequest($"could not find domain {Domain}");
                if (helper.Credit >= Db.buyMail.ToList()[index].Price )
                {
                    Db.users.FirstOrDefault(x => x.Token == token).Credit -= Db.buyMail.ToList().Find(x=> x.Domain == Domain).Price;
                    var getfreemail = from email in Db.buyMail
                                      where email.OwnerId == null
                                      && email.Domain == Domain
                                      orderby email.Id
                                      select email;
                    foreach (var emails in getfreemail)
                    {
                        Db.Database.CloseConnection();
                       
                        Db.buyMail.ToList().Find(x=> x.Email == emails.Email).OwnerId = helper.UserID;
                        Db.buyMail.ToList().Find(x => x.Email == emails.Email).MailID = random.Next(2000, 7000);
                  
                        
                        Db.SaveChanges();
                        return Ok(Db.buyMail.ToList().Find(x => x.Email == emails.Email));
                    }
                }
                  
                else
                    return BadRequest("need more credit");
             }
             else
                return Unauthorized("invaild token");
            return NoContent();
        }

        // GET api/<MailController>/5
        [HttpGet("[action]/{token}")]
        public IActionResult GetAllemails(string  token){
            var getuser = Db.users.FirstOrDefault(x => x.Token == token);
            if (Db.users.Any(x => x.Token == token))
                return Ok( Db.buyMail.ToList().FindAll(x=> x.OwnerId == getuser.UserID));
            else
                return Unauthorized(JsonConvert.SerializeObject("Unauthorized"));

        }
        [HttpGet("[action]")]
        public IActionResult GetEmailById([FromHeader] string token,int id)
        {
            if (Db.users.Any(x => x.Token == token))
            {

                var getuser = Db.users.FirstOrDefault(x => x.Token == token);
                if (Db.buyMail.Any(x => x.MailID  == id && x.OwnerId == getuser.UserID))
                    return Ok(Db.buyMail.ToList().Find(x => x.MailID == id));

                else
                    return BadRequest("could not find this email\n reason : this email does not belong to you or does not exist");
            }
            else
                return BadRequest("invaild token");
        }
        // POST api/<MailController>
        [HttpPost("[action]")]
        public void SendMail([FromBody] SendMail info){
            if(Db.users.Any(x=> x.APIKey == info.APIKey)){
                if(Db.users.Any(x => x.Email.Contains(info.Email))){

                }
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetMails([FromHeader] string token, string Email)
        {
            if (Db.users.Any(x => x.Token == token))
            {
                var getuser = Db.users.FirstOrDefault(x => x.Token == token);
                if (Db.buyMail.Any(x=> x.OwnerId == getuser.UserID))
                {
                    RestClient  restClient = new RestClient($"https://tempmail.email/api/emails?inbox={Email}");
                    RestRequest req = new RestRequest();
                    var mails =  restClient.Get<GetMail.Root>(req);
                    if (mails == null)
                        return Ok("no Mail".ToJson(Formatting.Indented));
                    else
                        return Ok(mails.data.ToList());
                }

                else
                    return BadRequest("could not find this email reason : this email does not belong to you or does not exist".ToJson(Formatting.Indented));
            }
            else
                return BadRequest("invaild token");
        }
        [HttpGet("[action]")]
        public IActionResult GetDomains()
        {
            List<string> domainLocalDb = new List<string>();
            var get_domain = from domain in Db.buyMail
                             where domain.Domain != null
                             select domain;
            try
            {
                foreach (var domain in get_domain)
                {
                    if (domainLocalDb.Any(x => x == domain.Domain))
                        continue;
                    else
                        domainLocalDb.Add(domain.Domain.ToString());
                }
                return Ok(domainLocalDb);
            }
            catch
            {
                return BadRequest();
            }
            

        }
        // PUT api/<MailController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromHeader] string token)
        {
            return Ok("Sorry you can't edit your email.");
        }

        // DELETE api/<MailController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("Can't Delete Emails. Read ('TOU')");
        }
    }
}
