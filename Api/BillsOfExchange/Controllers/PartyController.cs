using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Party;
using BillsOfExchange.Core.Services;
using BillsOfExchange.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartyController: ControllerBase
    {
        private readonly IPartyService partyService;

        public PartyController(IPartyService partyService)
        {
            this.partyService = partyService ?? throw new ArgumentNullException(nameof(partyService));
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetItems")]
        public List<PartyItem> GetItems(int pageSize = 10, int pageNumber = 1)
        {
            int skip = PagingUtils.GetSkipedItemsCount(pageSize, pageNumber);
            var result = partyService.Get(pageSize, skip).ToList();

            return result;
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetById/{id}")]
        public PartyItem GetById(int id)
        {
            var result = partyService.GetById(id);

            return result;
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetBillsByDrawerId/{id}")]
        public IEnumerable<BillOfExchangeItem> GetBillsByDrawerId(int id)
        {
            var result = partyService.GetBillsByDrawerId(id);

            return result;
        }

        [EnableCors]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetBillsByBeneficiaryId/{id}")]
        public IEnumerable<BillOfExchangeItem> GetBillsByBeneficiaryId(int id)
        {
            var result = partyService.GetBillsByBeneficiaryId(id);

            return result;
        }

    }
}