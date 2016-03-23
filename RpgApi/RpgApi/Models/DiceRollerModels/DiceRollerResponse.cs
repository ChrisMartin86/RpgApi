using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.DiceRollerModels
{
    public class DiceRollerResponse
    {
        public int NumberOfSides { get; set; }
        public int NumberOfDiceRolled { get; set; }
        public IEnumerable<int> IndividualResults { get; set; }
        public long Total { get { try { return IndividualResults.Sum(); } catch { return 0; } }  }

        public int HighestRoll { get { try { return IndividualResults.Max(); } catch { return 0; } } }

        public int LowestRoll { get { try { return IndividualResults.Min(); } catch { return 0; } } }

        public DiceRollerResponse()
        {
            NumberOfSides = 0;
            NumberOfDiceRolled = 0;
            IndividualResults = new int[0];
        }

    }

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

    public class XWingAttackDiceRollerResponse
    {
        public int NumberOfDiceRolled { get; set; }

        public IEnumerable<string> IndividualResults { get; set; }

        private XWingAttackDiceRollerResponse()
        {

        }

        public XWingAttackDiceRollerResponse(IEnumerable<int> intResults)
        {
            NumberOfDiceRolled = intResults.Count();

            var individualResults = new List<string>();

            foreach (int intResult in intResults)
            {
                if (intResult == 1 || intResult == 2 || intResult == 3)
                    individualResults.Add("Hit");
                else if (intResult == 4)
                    individualResults.Add("Crit");
                else if (intResult == 5 || intResult == 6)
                    individualResults.Add("Focus");
                else if (intResult == 7 || intResult == 8)
                    individualResults.Add("Nothing");
                else
                    throw new ArgumentException("Only valid values are 1-8");
            }

            IndividualResults = individualResults;
        }
    }

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