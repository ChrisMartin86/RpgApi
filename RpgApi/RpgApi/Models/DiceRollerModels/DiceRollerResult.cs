using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.DiceRollerModels
{
    /// <summary>
    /// The result of the CalculateRoll() method of the DiceRoller class. Returned as an IEnumerable. Also the result returned from the GET method of the DiceRollerController
    /// </summary>
    /// <seealso cref="DiceRoller"/>
    /// <seealso cref="IEnumerable{T}"/>
    public class DiceRollerResult
    {
        public int NumberOfSides { get; set; }
        public int NumberOfDiceRolled { get; set; }
        public IEnumerable<int> IndividualResults { get; set; }
        public long Total { get { try { return IndividualResults.Sum(); } catch { return 0; } }  }

        public int HighestRoll { get { try { return IndividualResults.Max(); } catch { return 0; } } }

        public int LowestRoll { get { try { return IndividualResults.Min(); } catch { return 0; } } }

        internal DiceRollerResult()
        {
            NumberOfSides = 0;
            NumberOfDiceRolled = 0;
            IndividualResults = new int[0];
        }

    }

    

    

    


}