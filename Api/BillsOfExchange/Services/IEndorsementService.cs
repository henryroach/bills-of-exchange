using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.Services
{
    public interface IEndorsementService
    {
        LinkedList<Endorsement> GetEndorsements(int billId);
    }
}