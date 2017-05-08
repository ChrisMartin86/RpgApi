using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RpgApi.Models.DiceRollerModels;
using System.Collections.Generic;

namespace RpgApiTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_DiceRoll_Instantiation()
        {
            int diceToRoll = 5;
            int numberOfSides = 2;
            IEnumerable<int> individualResults = new int[] { 1, 1, 1, 1, 1 };

            var item = new DiceRoll(numberOfSides, diceToRoll, individualResults);

            Assert.AreEqual(5, item.Total);
            Assert.AreEqual(5, item.NumberOfDiceRolled);
            Assert.AreEqual(2, numberOfSides);
            Assert.AreEqual(1, item.HighestRoll);
            Assert.AreEqual(1, item.LowestRoll);

        }
    }
}
