using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace RpgApi.Controllers
{
    /// <summary>
    /// Dice Roller API
    /// </summary>
    public class DiceRollerController : ApiController
    {
        /// <summary>
        /// Roll special dice.
        /// </summary>
        /// <param name="diceType">1 = Fudge, 2 = X Wing Attack, 3 = X Wing Defense</param>
        /// <param name="numberOfDice">The number of dice to roll.</param>
        /// <returns>HttpResponseMessage with status code and/or requested roll.</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get(Models.DiceRollerModels.DiceType diceType, int numberOfDice)
        {
            if (null == diceType || null == numberOfDice)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad value for diceType or numberOfDice");

            var diceDict = new Dictionary<int, int>();

            if (diceType == Models.DiceRollerModels.DiceType.Standard)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException("Pass dictionary pairs of int values in body instead of url for standard rolls."));
            }

            else if (diceType == Models.DiceRollerModels.DiceType.Fudge)
            {
                diceDict.Add(6, numberOfDice);

                var roller = new Models.DiceRollerModels.DiceRoller(diceDict);

                var myResults = await Task.Factory.StartNew(() => roller.CalculateRoll());

                foreach (var result in myResults)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new Models.DiceRollerModels.FudgeDiceRollerResponse(result.IndividualResults));
                }

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There was a problem.");
            }

            else if (diceType == Models.DiceRollerModels.DiceType.XWingAttack)
            {
                diceDict.Add(8, numberOfDice);

                var roller = new Models.DiceRollerModels.DiceRoller(diceDict);

                var myResults = await Task.Factory.StartNew(() => roller.CalculateRoll());

                foreach (var result in myResults)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new Models.DiceRollerModels.XWingAttackDiceRollerResponse(result.IndividualResults));
                }

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There was a problem.");
            }

            else if (diceType == Models.DiceRollerModels.DiceType.XWingDefense)
            {
                diceDict.Add(8, numberOfDice);

                var roller = new Models.DiceRollerModels.DiceRoller(diceDict);

                var myResults = await Task.Factory.StartNew(() => roller.CalculateRoll());

                foreach (var result in myResults)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new Models.DiceRollerModels.XWingDefenseDiceRollerResponse(result.IndividualResults));
                }

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There was a problem.");
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ArgumentException("Bad value passed for die type."));
            }

        }

        /// <summary>
        /// Gets dice results for requested dice.
        /// </summary>
        /// <param name="diceToRoll">Key-Value pairs, with the key being the number of sides of each type to roll, and the value being the number of that type to roll.</param>
        /// <returns>HttpResponseCode and/or List of results. Results will have the following properties: NumberOfSides, NumberOfDiceRolled, IndividualResults,HighestRoll, LowestRoll, and Total</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]IDictionary<int, int> diceToRoll)
        {
            if (null == diceToRoll)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "diceToRoll cannot be null");

            var diceRoller = new Models.DiceRollerModels.DiceRoller(diceToRoll);

            try
            {
                var results = await Task.Factory.StartNew(() => diceRoller.CalculateRoll());

                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (System.OutOfMemoryException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Numbers too big. Roll less dice at a time.");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
