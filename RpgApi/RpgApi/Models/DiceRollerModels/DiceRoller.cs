using System;
using System.Collections.Generic;

namespace RpgApi.Models.DiceRollerModels
{
    public class DiceRoller
    {
        public IDictionary<int, int> Dice { get; private set; }

        public DiceRoller(IDictionary<int, int> diceToRoll)
        {
            Dice = diceToRoll;
        }

        public IEnumerable<DiceRollerResponse> CalculateRoll()
        {
            try
            {
                System.Threading.Thread.Sleep(1);

                var resultList = new List<DiceRollerResponse>();

                var myRandom = new Random();

                foreach (var dieSet in Dice)
                {
                    var response = new DiceRollerResponse();

                    var results = new List<int>();

                    for (int i = 0; i < dieSet.Value; i++)
                    {
                        var dieRoll = myRandom.Next(1, (dieSet.Key + 1));

                        results.Add(dieRoll);
                    }

                    response.NumberOfSides = dieSet.Key;
                    response.NumberOfDiceRolled = dieSet.Value;
                    response.IndividualResults = results;

                    resultList.Add(response);
                }

                return resultList;
            }
            catch (System.OutOfMemoryException e)
            {
                throw new OutOfMemoryException("Numbers too large. Roll less dice at a time.", e);
            }
            catch (Exception e)
            {
                throw new Exception("There has been an unknown error", e);
            }
        }
    }

    public enum DiceType
    {
        Standard = 0,
        Fudge = 1,
        XWingAttack = 2,
        XWingDefense = 3

    }
}