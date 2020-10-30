using System;
using System.Collections.Generic;
using System.Text;

namespace BillsOfExchange.Core.Contracts.BillOfExchange
{
    /// <summary>
    /// Bill of Exchange is a security representing amount of something
    /// </summary>
    public class BillOfExchangeDetail : BillOfExchangeItem
    {
        /// <summary>
        /// Party which is current holder
        /// </summary>
        public int CurrentBeneficiaryId { get; set; }
        public string CurrentBeneficiaryName { get; set; }

    }
}
