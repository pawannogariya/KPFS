namespace KPFS.Business.Dtos
{
    public class FundManagerDto
    {
        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string Email { get; set; }

        public int FundId { get; set; }
        public FundDto Fund { get; set; }
    }
}
