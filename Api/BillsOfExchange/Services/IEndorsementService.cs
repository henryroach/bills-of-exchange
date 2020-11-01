using System.Collections.Generic;
using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public interface IEndorsementService
    {
        IEnumerable<EndorsementDto> GetEndorsements(int billId);
    }
}