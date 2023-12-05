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

    public class TransactionType : ValueAttributeBase
    {
        [XmlElement("TransactionSubType")]
        public List<TransactionSubType> TransactionSubTypes { get; set; }
    }

    public class TransactionSubType : ValueAttributeBase
    {

        [XmlAttribute("IsCredit")]
        public bool IsCredit { get; set; }
    }

    public class ModeOfHolding : ValueAttributeBase { }
    public class BankAccountType : ValueAttributeBase { }
    public class TaxOption : ValueAttributeBase { }
    public class KpfsRecordStatus : ValueAttributeBase { }

    public class ValueAttributeBase
    {
        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}
