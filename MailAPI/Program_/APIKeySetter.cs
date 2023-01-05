namespace MailAPI.Program_
{
    public class APIKeySetter
    {
        public static string ApiKey() { 
            Random random= new Random();
            string api = "";
            for (int i = 0; i < 20; i++)
            {
                api += random.Next(0,9).ToString();
            }
            return api;
        }
    }
}
