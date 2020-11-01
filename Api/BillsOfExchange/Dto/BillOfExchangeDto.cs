namespace BillsOfExchange.Dto
{
    public class BillOfExchangeDto
    {
        public int Id { get; set; }

        public int DrawerId { get; set; }

        public int BeneficiaryId { get; set; }

        public decimal Amount { get; set; }
    }
}