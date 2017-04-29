using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.DiceRollerModels.Fudge
{
    public class FudgeDiceRollerResponse
    {
        public int NumberOfDiceRolled { get; set; }
        public long Total { get { try { return getTotal(); } catch { return 0; } } }

        public IEnumerable<string> IndividualResults { get; set; }

        private long getTotal()
        {
            long total = 0;

            foreach (var result in IndividualResults)
            {
                if (result == "0")
                { continue; }
                else if (result == "-1")
                { total--; continue; }
                else if (result == "+1")
                { total++; continue; }
            }

            return total;
        }

        private FudgeDiceRollerResponse()
        {
            NumberOfDiceRolled = 0;
            IndividualResults = new string[0];

        }

        public FudgeDiceRollerResponse(IEnumerable<int> intResults)
        {
            NumberOfDiceRolled = intResults.Count();

            var individualResults = new List<string>();

            foreach (int intResult in intResults)
            {
                if (intResult == 1 || intResult == 2)
                    individualResults.Add("0");
                else if (intResult == 3 || intResult == 4)
                    individualResults.Add("-1");
                else if (intResult == 5 || intResult == 6)
                    individualResults.Add("+1");
            }

            IndividualResults = individualResults;
        }
    }
}