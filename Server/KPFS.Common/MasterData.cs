using System.Xml.Serialization;

namespace KPFS.Common
{
    [XmlRoot("MasterData")]
    public class MasterData
    {
        [XmlArray("TransactionTypes")]
        public List<TransactionType> TransactionTypes { get; set; }

        [XmlArray("ModeOfHoldings")]
        public List<ModeOfHolding> ModeOfHoldings { get; set; }

        [XmlArray("BankAccountTypes")]
        public List<BankAccountType> BankAccountTypes { get; set; }

        [XmlArray("TaxOptions")]
        public List<TaxOption> TaxOptions { get; set; }

        [XmlArray("KpfsRecordStatuses")]
        public List<KpfsRecordStatus> KpfsRecordStatuses { get; set; }
    }

    public class TransactionType : EntryBase
    {
        [XmlElement("TransactionSubType")]
        public List<TransactionSubType> TransactionSubTypes { get; set; }
    }

    public class TransactionSubType : EntryBase
    {

        [XmlAttribute("IsCredit")]
        public bool IsCredit { get; set; }

        [XmlElement("IncomeType")]
        public List<IncomeType> IncomeTypes { get; set; }
    }

    public class ModeOfHolding : EntryBase { }
    public class BankAccountType : EntryBase { }
    public class TaxOption : EntryBase { }
    public class KpfsRecordStatus : EntryBase { }
    public class IncomeType : EntryBase { }

    public class EntryBase 
    {
        [XmlAttribute("Value")]
        public string Value { get; set; }

        [XmlAttribute("IsActive")]
        public bool IsActive { get; set; }
    }
}
