﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPFS.Data.Entities.Base;

namespace KPFS.Data.Entities
{
    public class BankAccount : EntityBase<int>
    {
        [Required]
        [StringLength(100)]
        public string BankName { get; set; }


        [Required]
        [StringLength(100)]
        public string AccountNumber { get; set; }

        [Required]
        public int FundId { get; set; }

        [ForeignKey(nameof(FundId))]
        public virtual Fund Fund { get; set; }
    }
}