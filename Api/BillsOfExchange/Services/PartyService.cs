using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public class PartyService : IPagedService<PartyDto>
    {
        private readonly IPartyRepository _repository;

        public PartyService(IPartyRepository repository)
        {
            _repository = repository;
        }

        public PagedResultDto<PartyDto> GetList(PagedRequestDto request)
        {
            var list = _repository.Get(request.Take, request.Skip).ToList();

            var result = new PagedResultDto<PartyDto>();
            if (list.Any())
            {
                foreach (var item in list)
                {
                    result.Data.Append(new PartyDto { Id = item.Id, Name = item.Name });
                }
            }

            return result;
        }
    }
}