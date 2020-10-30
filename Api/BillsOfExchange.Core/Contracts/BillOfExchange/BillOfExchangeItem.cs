using System;
using System.Collections.Generic;
using System.Text;

namespace BillsOfExchange.Core.Contracts.BillOfExchange
{
    /// <summary>
    /// Bill of Exchange is a security representing amount of something
    /// </summary>
    public class BillOfExchangeItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Party that issued the Bill
        /// </summary>
        public int DrawerId { get; set; }
        public string DrawerName { get; set; }
        //public bool IsDrawerNameInvalid { get; set; }

        /// <summary>
        /// Party that was the Bill issued to
        /// </summary>
        public int BeneficiaryId { get; set; }
        public string BeneficiaryName { get; set; }
        //public bool IsBeneficiaryNameInvalid { get; set; }

        /// <summary>
        /// Amount of goods the Bill represents
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// List of warnings for user about wrong data
        /// </summary>
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
