namespace KPFS.Web.AppSettings
{
    public class ApplicationSettings
    {
        public string BaseAppPath { get; set; }
        public string BaseApiPath { get; set; }
        public Admin[] AdminCredentials { get; set; }
    }

    public class Admin
    {
        public string Email { get; set;}
        public string Password { get; set;}
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
    }
}
