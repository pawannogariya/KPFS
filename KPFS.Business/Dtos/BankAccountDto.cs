namespace KPFS.Business.Dtos
{
    public class BankAccountDto
    {
        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public int FundId { get; set; }

        public FundDto Fund { get; set; }   
    }
}
