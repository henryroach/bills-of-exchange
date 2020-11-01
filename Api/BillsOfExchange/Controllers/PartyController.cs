using BillsOfExchange.Dto;
using BillsOfExchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly PartyService _billsOfExchangeService;

        public PartyController(PartyService billsOfExchangeService)
        {
            _billsOfExchangeService = billsOfExchangeService;
        }

        [HttpGet("get")]
        public PagedResultDto<PartyDto> GetParties(PagedRequestDto request)
        {
            return _billsOfExchangeService.GetList(request);
        }
    }
}