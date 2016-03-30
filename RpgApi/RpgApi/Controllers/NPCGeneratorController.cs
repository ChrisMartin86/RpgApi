using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using RpgApi.Models.NPCGeneratorModels.DnD.FifthEdition;

namespace RpgApi.Controllers
{
    public class NPCGeneratorController : ApiController
    {
        public async Task<HttpResponseMessage> Get(NPCTemplate baseTemplate = NPCTemplate.Random, NPCRace baseRace = NPCRace.Random)
        {
            try
            {
                var myNpc = new NPC(baseTemplate, baseRace);

                return Request.CreateResponse(HttpStatusCode.OK, myNpc);
            }
            catch (InvalidOperationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            
        }
    }
}
