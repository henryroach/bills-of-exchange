using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
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
                result.Data = MapToDto(list);
            }

            return result;
        }

        public IEnumerable<PartyDto> GetByIds(IEnumerable<int> ids)
        {
            var list = _repository.GetByIds(new ReadOnlyCollection<int>(new List<int>(ids))).ToList();

            return MapToDto(list);
        }

        private static IEnumerable<PartyDto> MapToDto(IEnumerable<Party> parties)
        {
            foreach (var p in parties)
            {
                yield return new PartyDto
                {
                    Id = p.Id,
                    Name = p.Name
                };
            }
        }
    }
}