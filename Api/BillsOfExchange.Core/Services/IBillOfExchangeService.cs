using BillsOfExchange.Core.Contracts.BillOfExchange;
using BillsOfExchange.Core.Contracts.Endorsment;
using System.Collections.Generic;

namespace BillsOfExchange.Core.Services
{
    public interface IBillOfExchangeService
    {
        /// <summary>
        /// Get the list of BillOfExchange
        /// </summary>
        /// <param name="take">How much records should be taken</param>
        /// <param name="skip">How much records should be skiped</param>
        /// <returns>list of BillOfExchange contracts</returns>
        IEnumerable<BillOfExchangeItem> Get(int take, int skip);

        /// <summary>
        /// Get detailed information about bills of exchanges
        /// </summary>
        /// <param name="id">id of bill</param>
        /// <returns>BillOfExchangeDetail</returns>
        BillOfExchangeDetail GetById(int id);

        /// <summary>
        /// Get endorsements by billId
        /// </summary>
        /// <param name="id">id of bill</param>
        /// <returns>list of endorsements</returns>
        IEnumerable<EndorsementItem> GetEndorsementsByBillId(int id);
    }
}
