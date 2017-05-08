using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RpgApi.Models.DiceRollerModels
{
    /// <summary>
    /// A class for rolling standard dice.
    /// </summary>
    internal class DiceRoller
    {
        // This uses a lot of the example code from teh RNGCryptoServiceProvider documentation found at https://msdn.microsoft.com/en-us/library/system.security.cryptography.rngcryptoserviceprovider%28v=vs.110%29.aspx.
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public IDictionary<int, int> Dice { get; private set; }

        public DiceRoller(IDictionary<int, int> dice)
        {
            Dice = dice;
        }

        public DiceRollerResult CalculateRoll()
        {
            try
            {
                var resultList = new DiceRollerResult();

                foreach (KeyValuePair<int, int> dieSet in Dice)
                {
                    var results = new List<int>();

                    for (int i = 0; i < dieSet.Value; i++)
                    {
                        byte dieRoll = RollDice((byte)dieSet.Key);

                        results.Add(dieRoll);
                    }

                    int numberOfSides = dieSet.Key;
                    int numberOfDiceRolled = dieSet.Value;
                    

                    resultList.Add(new DiceRoll(numberOfSides, numberOfDiceRolled, results));
                }

                return resultList;
            }
            catch
            {
                throw;
            }
        }

        public static byte RollDice(byte numberOfSides)
        {
            if (numberOfSides <= 0)
                throw new ArgumentOutOfRangeException("numberOfSides cannot be less than zero");

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!isFairRoll(randomNumber[0], numberOfSides));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % numberOfSides) + 1);
        }

        private static bool isFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }
    }
}