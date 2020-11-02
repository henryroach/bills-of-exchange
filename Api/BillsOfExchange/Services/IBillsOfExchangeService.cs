using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public interface IBillsOfExchangeService : IPagedService<BillOfExchangeDto>
    {
        BillOfExchangeDetailDto GetById(int id);

        IEnumerable<BillOfExchangeDto> GetByDrawerId(int drawerId);

        IEnumerable<BillOfExchangeDto> GetByBeneficiaryId(int beneficiaryId);
    }
}