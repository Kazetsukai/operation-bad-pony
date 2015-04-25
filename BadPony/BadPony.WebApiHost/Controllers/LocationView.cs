using System.Collections.Generic;
using System.Linq;
using BadPony.Core;
using NLog;

namespace BadPony.WebApiHost.Controllers
{
    public class LocationView
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public LocationView()
        {
            Contents = new List<Item>();
            Players = new List<Player>();
            Exits = new List<Door>();
        }

        public LocationView(GameObject location)
            : this()
        {
            logger.Debug("\tWAPI\tBuilding Location View for {0}", location.Name);
            // Do not pull objects for the void!!!
            if (location.Id != 0)
            {
                var everything = Program.Game.GetContainedObjects(location.Id);

                Contents = everything.OfType<Item>();
                Players = everything.OfType<Player>();
                Exits = everything.OfType<Door>();
            }
            else
            {
                logger.Warn("\tWAPI\tSkipping objects as there is nothing in the void. It is a vast and desolate place, neither hot nor cold for temperature has no meaning.\n\t\t\t\t\t\t\t\t\t\tTime does not pass and space is not even a concept. Some say all things are possible but in the void you will quickly realise, not even possibility is possible...\n\t\t\t\t\t\t\t\t\t\t...or some deep shit like that.");
            }

            Name = location.Name;
            Description = location.Description;
            logger.Debug("\tWAPI\tView built for {0}", location.Name);
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Item> Contents { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Door> Exits { get; set; }
    }
}