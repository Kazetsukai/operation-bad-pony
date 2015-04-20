using System.Collections.Generic;
using System.Linq;
using BadPony.Core;

namespace BadPony.WebApiHost.Controllers
{
    public class LocationView
    {
        public LocationView()
        {
            Contents = new List<Item>();
            Players = new List<Player>();
            Exits = new List<Door>();
        }

        public LocationView(GameObject location)
            : this()
        {
            // Do not pull objects for the void!!!
            if (location.Id != 0)
            {
                var everything = Program.Game.GetContainedObjects(location.Id);

                Contents = everything.OfType<Item>();
                Players = everything.OfType<Player>();
                Exits = everything.OfType<Door>();
            }

            Name = location.Name;
            Description = location.Description;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Item> Contents { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Door> Exits { get; set; }
    }
}