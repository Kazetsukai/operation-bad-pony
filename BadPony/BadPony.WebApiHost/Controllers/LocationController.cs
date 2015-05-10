using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;
using NLog;

namespace BadPony.WebApiHost.Controllers
{
    [Authorize]
    public class LocationController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LocationView Get(int id = 0)
        {
            logger.Debug("\tWAPI\tLocation {0} requested", id);
            GameObject requestedLocation = Program.Game.GetObject(id);
            if (requestedLocation != null)
            {                
                var view = new LocationView(requestedLocation);
                logger.Debug("\tWAPI\tSuccess: Retrieved - {0}", requestedLocation.Name);               
                return view;
            }

            logger.Error("\tWAPI\tFailed: Location {0} does not exist.", id);
            
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
