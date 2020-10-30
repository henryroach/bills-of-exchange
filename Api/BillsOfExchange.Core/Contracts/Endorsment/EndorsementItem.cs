using System;
using System.Collections.Generic;
using System.Text;

namespace BillsOfExchange.Core.Contracts.Endorsment
{
    /// <summary>
    /// Endorsement can change the Beneficiary of a Bill
    /// Endorsements are ordered, the Beneficiary of the last Endorsement is entitled to the Bill
    /// </summary>
    public class EndorsementItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Previous endorsement in the order
        /// Is null when the endorsement is first in the order
        /// </summary>
        public int? PreviousEndorsementId { get; set; }

        /// <summary>
        /// Bill the endorsement is attached to
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// New beneficiary entitled to the Bill
        /// </summary>
        public int NewBeneficiaryId { get; set; }
        public string NewBeneficiaryName { get; set; }

        /// <summary>
        /// List of warnings for user about wrong data
        /// </summary>
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
