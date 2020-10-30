using System;
using System.Collections.Generic;
using System.Text;

namespace BillsOfExchange.Core.Contracts.Party
{
    /// <summary>
    /// Represents a legal person
    /// </summary>
    public class PartyItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Full name of the person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of warnings for user about wrong data
        /// </summary>
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
