namespace BillsOfExchange.DataProvider.Models
{
    /// <summary>
    /// Endorsement can change the Beneficiary of a Bill
    /// Endorsements are ordered, the Beneficiary of the last Endorsement is entitled to the Bill
    /// </summary>
    public class Endorsement
    {
        public int Id { get; set; }

        /// <summary>
        /// Bill the endorsement is attached to
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// New beneficiary entitled to the Bill
        /// </summary>
        public int NewBeneficiaryId { get; set; }

        /// <summary>
        /// Previous endorsement in the order
        /// Is null when the endorsement is first in the order
        /// </summary>
        public int? PreviousEndorsementId { get; set; }
    }
}