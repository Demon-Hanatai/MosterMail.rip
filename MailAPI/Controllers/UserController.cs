using MailAPI.Data;
using MailAPI.Models.User;
using MailAPI.Program_;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public ApiContext Db;
        public UserController(ApiContext us)
        {
            Db = us;
          
        }
        // GET: api/<UserController>
        
        [HttpGet]
        public IActionResult Get([FromHeader]string Token) {
           var search = Db.users.Where(x => x.Token == Token);
            if(search.Any())
                return Ok(Db.users.Where(x => x.Token == Token).First());
            else
                return Unauthorized();
        }
        public class UserForm
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }
        // POST api/<UserController>
        [HttpPost("[action]")]
        public IActionResult CreateAccount([FromBody] UserForm userForm)
        {
            Random ra = new Random();
            Users user = new Users()
            {
                IP = "0.0.0.0",
                Token = (string)Token.CreateToken(nameof(TokenTypes.Bas9)),
                Credit = 20,
                CreationDate = DateTime.Now,
                Roles = nameof(User),
                APIKey =APIKeySetter.ApiKey(),
                Username = userForm.Username,   
                Password = userForm.Password,   
                Email = userForm.Email,
                UserID = ra.Next(100000000,900000000)
            };
            if (Db.users.Where(x => x.Username == userForm.Username).Any())
                return BadRequest("Username name already exist");
            if (Db.users.Where(x => x.Email == userForm.Email).Any())
                return BadRequest("Email name already exist");

            try
            {
                Db.users.Add(user);
                Db.SaveChanges();
                return Ok(user);
            }
            catch {
                return StatusCode(404);
            }
           

        }

        // PUT api/<UserController>/5
        [HttpPut("[action]")]
        public IActionResult Edit([FromHeader]string Token, [FromBody]UserForm user)
        {
            var finduser = Db.users.Where(x=> 
                                            x.Token == Token);
            if (finduser.Count() == 0)
                return Unauthorized("Unauthorized".ToJson);
            else
            {
                var item = finduser.FirstOrDefault();
                item.Username = user.Username;
                item.Password = user.Password;
                item.Email = user.Email;
                user.Username = user.Email;
               
                try
                {

                    finduser.First().Username = item.Username;
                    finduser.First().Password = item.Password;
                    finduser.First().Email = item.Email;
                    Db.SaveChanges();
                    return StatusCode(201);
                }
                catch
                {
                    return BadRequest();
                }
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPut("[action]")]
        public IActionResult ReloadBalance(string token)
        {
            Db.users.ToList().Find(x => x.Token == token).Credit += 100;
            Db.SaveChanges();
            return Ok();
        }
    }
}
