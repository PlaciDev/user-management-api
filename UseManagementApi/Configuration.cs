namespace UseManagementApi;

public class Configuration
{
    public static SmtpConfiguration Smtp = new();
    
    public class SmtpConfiguration  
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string Username { get; set; }
        public string Password { get; set; }
    }
}