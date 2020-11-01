namespace BillsOfExchange.Dto
{
    public class PagedRequestDto
    {
        public int Skip { get; set; }

        public int Take { get; set; } = 10;
    }
}