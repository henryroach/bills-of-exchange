using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Party;
using System.Collections.Generic;

namespace BillsOfExchange.Core.Services
{
    public interface IPartyService
    {
        /// <summary>
        /// Get the list of parties
        /// </summary>
        /// <param name="take">How much records should be taken</param>
        /// <param name="skip">How much records should be skiped</param>
        /// <returns>list of party contracts</returns>
        IEnumerable<PartyItem> Get(int take, int skip);

        /// <summary>
        /// Get information about party
        /// </summary>
        /// <param name="id">id of party</param>
        /// <returns>PartyItem</returns>
        PartyItem GetById(int id);

        /// <summary>
        /// Get all bills, which have the same drawer
        /// </summary>
        /// <param name="id">id of bills' drawer</param>
        /// <returns>list of bills</returns>
        IEnumerable<BillOfExchangeItem> GetBillsByDrawerId(int id);

        /// <summary>
        /// Get all bills, which now belongs to specific party
        /// </summary>
        /// <param name="id">id of bills' owner</param>
        /// <returns>list of bills</returns>
        IEnumerable<BillOfExchangeItem> GetBillsByBeneficiaryId(int id);
    }
}
