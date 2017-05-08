using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.DiceRollerModels
{
    /// <summary>
    /// The result of several dice rolls
    /// </summary>
    public class DiceRollerResult : IList<DiceRoll>
    {
        private List<DiceRoll> rolls;

        public DiceRollerResult()
        {
            rolls = new List<DiceRoll>();
        }

        public DiceRollerResult(IEnumerable<DiceRoll> rolls)
        {
            rolls = rolls.ToList();
        }

        public DiceRoll this[int index] { get => ((IList<DiceRoll>)rolls)[index]; set => ((IList<DiceRoll>)rolls)[index] = value; }

        public int Count => ((IList<DiceRoll>)rolls).Count;

        public bool IsReadOnly => ((IList<DiceRoll>)rolls).IsReadOnly;

        public void Add(DiceRoll item)
        {
            ((IList<DiceRoll>)rolls).Add(item);
        }

        public void Clear()
        {
            ((IList<DiceRoll>)rolls).Clear();
        }

        public bool Contains(DiceRoll item)
        {
            return ((IList<DiceRoll>)rolls).Contains(item);
        }

        public void CopyTo(DiceRoll[] array, int arrayIndex)
        {
            ((IList<DiceRoll>)rolls).CopyTo(array, arrayIndex);
        }

        public IEnumerator<DiceRoll> GetEnumerator()
        {
            return ((IList<DiceRoll>)rolls).GetEnumerator();
        }

        public int IndexOf(DiceRoll item)
        {
            return ((IList<DiceRoll>)rolls).IndexOf(item);
        }

        public void Insert(int index, DiceRoll item)
        {
            ((IList<DiceRoll>)rolls).Insert(index, item);
        }

        public bool Remove(DiceRoll item)
        {
            return ((IList<DiceRoll>)rolls).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<DiceRoll>)rolls).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<DiceRoll>)rolls).GetEnumerator();
        }
    }

    /// <summary>
    /// The result of a single type of dice rolled
    /// </summary>
    public class DiceRoll
    {
        public int NumberOfSides { get; private set; }
        public int NumberOfDiceRolled { get; private set; }
        public IEnumerable<int> IndividualResults { get; private set; }
        public long Total { get; private set; }

        public int HighestRoll { get; private set; }

        public int LowestRoll { get; private set; }

        public DiceRoll(int numberOfSides, int numberOfDiceRolled, IEnumerable<int> individualResults)
        {
            try
            {
                validate(numberOfSides, numberOfDiceRolled, individualResults);
            }
            catch (Exception e)
            {
                throw e;
            }
            NumberOfSides = numberOfSides;
            NumberOfDiceRolled = numberOfDiceRolled;
            IndividualResults = individualResults;
            Total = IndividualResults.Sum();
            HighestRoll = IndividualResults.Max();
            LowestRoll = IndividualResults.Min();
        }

        private static void validate(int numberOfSides, int numberOfDiceRolled, IEnumerable<int> individualResults)
        {
            if (numberOfSides <= 1)
                throw new ArgumentOutOfRangeException("numberOfSides", "numberOfSides must be greater than 1");
            if (numberOfDiceRolled <= 0)
                throw new ArgumentOutOfRangeException("numberOfDiceRolled", "numberOfDiceRolled must be at least 1");
            if (null == individualResults)
                throw new ArgumentException("individualResults must not be null", "individualResults");
            if (individualResults.Count() != numberOfDiceRolled)
                throw new ArgumentException("The number of results must match numberOfDiceRolled");
            
        }
    }

    

    

    


}