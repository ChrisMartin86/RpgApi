using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.DiceRollerModels.XWing
{
    public class XWingDefenseDiceRollerResponse
    {
        private IEnumerable<int> intResults { get; set; }

        public int NumberOfDiceRolled { get; set; }

        public IEnumerable<string> IndividualResults { get; set; }

        private XWingDefenseDiceRollerResponse()
        {

        }

        public XWingDefenseDiceRollerResponse(IEnumerable<int> intResults)
        {
            NumberOfDiceRolled = intResults.Count();

            var individualResults = new List<string>();

            foreach (int intResult in intResults)
            {
                if (intResult == 1 || intResult == 2 || intResult == 3)
                    individualResults.Add("Evade");
                else if (intResult == 4 || intResult == 5)
                    individualResults.Add("Focus");
                else if (intResult == 6 || intResult == 7 || intResult == 8)
                    individualResults.Add("Nothing");
                else
                    throw new ArgumentException("Only valid values are 1-8");
            }

            IndividualResults = individualResults;
        }
    }
}