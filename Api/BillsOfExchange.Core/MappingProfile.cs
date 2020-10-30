using AutoMapper;
using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Endorsment;
using BillsOfExchange.Core.Contracts.Party;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Party, PartyItem>();

            CreateMap<BillOfExchange, BillOfExchangeItem>();
            CreateMap<BillOfExchange, BillOfExchangeDetail>();

            CreateMap<Endorsement, EndorsementItem>(); 
        }
    }
}
