using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsOfExchange.Dto
{
    public class BillOfExchangeDetailDto : BillOfExchangeDto
    {
        public PartyDto Drawer { get; set; }

        public PartyDto FirstBeneficiary { get; set; }
    }
}