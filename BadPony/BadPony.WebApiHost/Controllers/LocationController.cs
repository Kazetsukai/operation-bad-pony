using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;

namespace BadPony.WebApiHost.Controllers
{
    public class LocationController : ApiController
    {
        public LocationView Get(int id = 0)
        {
            Console.WriteLine("{0}:\tLocation {1} requested", DateTime.Now, id);
            GameObject requestedLocation = Program.Game.GetObject(id);
            if (requestedLocation != null)
            {
                var view = new LocationView(requestedLocation);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tSuccess: Retrieved - {0}", requestedLocation.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                
                return view;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tFailed: Location {0} does not exist.", id);
            Console.ForegroundColor = ConsoleColor.Gray;

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
