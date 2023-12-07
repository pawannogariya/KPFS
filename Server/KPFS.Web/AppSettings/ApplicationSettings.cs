namespace KPFS.Web.AppSettings
{
    public class ApplicationSettings
    {
        public string BasePath { get; set; }
        public AdminCredentials AdminCredentials { get; set; }
    }

    public class AdminCredentials
    {
        public string Email { get; set;}
        public string Password { get; set;}
    }
}
