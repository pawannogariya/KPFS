namespace KPFS.Business.Models
{
    public class DtoBase
    {
        public int? Id { get;set; }
        public bool IsNew => Id == null || Id == default;
    }
}
