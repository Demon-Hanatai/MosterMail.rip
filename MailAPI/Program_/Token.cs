
namespace MailAPI.Program_
{
    public class TokenTypes
    {
        public static Type? Bas29 { get; private set; }
        public static Type? Bas9 { get; private set; }
        public static Type? TypeBas30 { get; private set; }
    }
    public class Token
    {
       
        public static object CreateToken(string TokenType)
        {
            if(TokenType == nameof(TokenTypes.TypeBas30))
                return TypeBas30();
            if (TokenType == nameof(TokenTypes.Bas9))
                return Bas9();
            if (TokenType == nameof(TokenTypes.Bas29))
                return Bas29();
            return null;
        }
        public static object TypeBas30(){
            string types = "abcdefghijkmlopqrstuvwxyz@3@#2736@^#&@^#*@^$&*(!@)#_+}<.`.>";
            string newToken = "";
            Random random = new Random();
            for (int i = 0; i < 20 * 2 +random.Next(0,23); i++)
                newToken += types[random.Next(0,types.Length)];
            return newToken;
            
        }
        public static object Bas9()
        {
            string types = "abcdefghijkmlopqrstuvwxyz";
            string newToken = "MSF ";
            Random random = new Random();
            for (int i = 0; i < 29 ; i++)
                newToken += types[random.Next(0, types.Length)];
            return newToken;
        }
        public static object Bas29()
        {
            string types = "abcdefghijkmlopqrstuvwxyz1234567890";
            string newToken = "";
            Random random = new Random();
            for (int i = 0; i < 29; i++)
                newToken += types[random.Next(0, types.Length)];
            return newToken;
        }

    }
}
