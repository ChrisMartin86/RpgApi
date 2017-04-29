using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using RpgApi.Models.DiceRollerModels;

namespace RpgApi.Controllers
{
    /// <summary>
    /// Dice Roller API
    /// </summary>
    public class DiceRollerController : ApiController
    {
        /// <summary>
        /// Generate dice results
        /// </summary>
        /// <seealso cref="DiceRollerResult"/>
        /// <seealso cref="IDictionary{TKey, TValue}"/>
        /// <seealso cref="HttpResponseMessage"/>
        /// <seealso cref="HttpStatusCode"/>
        /// <param name="diceToRoll">Key-Value pairs, with both key and value being of type int.Key should be the number of sides of each type to roll, and the value should be how many to roll.</param>
        /// <returns>HttpStatusCode and/or DiceRollerResult of results.</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]IDictionary<int, int> diceToRoll)
        {
            if (null == diceToRoll || diceToRoll.Count == 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "diceToRoll cannot be null or empty.");

            var diceRoller = new DiceRoller(diceToRoll);

            try
            {
                IEnumerable<DiceRollerResult> results = await Task.Run(() => diceRoller.CalculateRoll());

                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (OutOfMemoryException)
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
