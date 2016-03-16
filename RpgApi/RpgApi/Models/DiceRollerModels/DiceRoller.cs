using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.DiceRollerModels
{
    public class DiceRoller
    {
        public Dictionary<int, int> Dice { get; private set; }

        private DiceRoller()
        {

        }

        public DiceRoller(Dictionary<int, int> diceToRoll)
        {
            Dice = diceToRoll;
        }

        public Dictionary<int, IEnumerable<int>> CalculateRoll()
        {
            var resultList = new Dictionary<int, IEnumerable<int>>();

            int lastTotal = new Random().Next();

            foreach (var dieSet in Dice)
            {
                var random = new Random((lastTotal * new Random().Next()));

                int totalResults = 0;

                var results = new List<int>();

                for (int i = 0; i < dieSet.Value; i++)
                {
                    var dieRoll = random.Next(1, dieSet.Key);

                    results.Add(dieRoll);

                    totalResults = totalResults + dieRoll;
                }

                results.Add(totalResults);

                lastTotal = totalResults;

                resultList.Add(dieSet.Key, results);
            }

            return resultList;
        }
    }
}