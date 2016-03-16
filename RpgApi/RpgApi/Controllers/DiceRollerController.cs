using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace RpgApi.Controllers
{
    public class DiceRollerController : ApiController
    {
        /// <summary>
        /// Gets dice results for requested dice.
        /// </summary>
        /// <param name="diceToRoll">Key-Value pairs, with the key being the number of sides of each type to roll, and the value being the number of that type to roll.</param>
        /// <returns>HttpResponseCode and/or List of key-value pairs containing each roll of each type of die. The last (and largest) number in each list is the total result of that type of die.</returns>
        public async Task<HttpResponseMessage> Post(Dictionary<int, int> diceToRoll)
        {
            var diceRoller = new Models.DiceRollerModels.DiceRoller(diceToRoll);

            try
            {
                var results = await Task.Factory.StartNew(() => diceRoller.CalculateRoll());

                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
