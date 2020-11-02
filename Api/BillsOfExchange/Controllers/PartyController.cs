using System.Collections.Generic;
using BillsOfExchange.Dto;
using BillsOfExchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly PartyService _partyService;

        public PartyController(PartyService partyService)
        {
            _partyService = partyService;
        }

        [HttpGet("get")]
        public PagedResultDto<PartyDto> GetParties([FromQuery] PagedRequestDto request)
        {
            return _partyService.GetList(request);
        }

        [HttpGet("getByIds")]
        public IEnumerable<PartyDto> GetPartiesByIds([FromQuery] IEnumerable<int> ids)
        {
            return _partyService.GetByIds(ids);
        }
    }
}