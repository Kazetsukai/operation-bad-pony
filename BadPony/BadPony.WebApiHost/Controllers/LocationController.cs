using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;

namespace BadPony.WebApiHost.Controllers
{
    public class LocationController : ApiController
    {
        public Location Get(int id = 0)
        {
            Console.WriteLine("{0}:\tLocation {1} requested", DateTime.Now, id);
            Location requestedLocation = Program.Game.GetLocation(id);
            if (requestedLocation != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tSuccess: Retrieved - {0}", requestedLocation.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                // Do not pull objects for the void!!!
                if (id != 0 )
                {
                    requestedLocation.Contents = (List<GameObject>)Program.Game.GetContainedObjects(id);
                }
                return requestedLocation;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tFailed: Location {0} does not exist.", id);
            Console.ForegroundColor = ConsoleColor.Gray;
            return null;
            
        }
    }
}
