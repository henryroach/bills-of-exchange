namespace BillsOfExchange.Dto
{
    public class EndorsementDto
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        public int NewBeneficiaryId { get; set; }

        public int? PreviousEndorsementId { get; set; }
    }
}