using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BadPony.Core
{
    public class Game
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        private object _lockObject = new object();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Game()
        {
            logger.Info("\tCORE\t==============================");
            logger.Info("\tCORE\tSetting up world");
            TemporaryHardcodedWorldSetup();
            logger.Info("\tCORE\tWorld generation complete");
            logger.Info("\tCORE\t==============================");
        }

        private void TemporaryHardcodedWorldSetup()
        {
            logger.Info("\tCORE\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            logger.Info("\tCORE\t! Temporary hardcoded world  !");
            logger.Info("\tCORE\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            logger.Info("\tCORE\t\t- Creating Locations....");
            Location theVoid = new Location()
            {
                Name = "The void",
                Description = "You are floating in a formless void.",
            };

            Location backAlley = new Location()
            {
                Name = "Back alley",
                Description = "You are in a dark alleyway. Foul smells mix together from nearby dumpsters and air-conditioning vents." ,
            };

            Location pizzeria = new Location()
            {
                Name = "Back of Fat Tony's Pizzeria",
                Description = "You are in the back of Fat Tony's Pizzeria. Around you there are several health codes being broken.",
            };

            logger.Info("\tCORE\t\t- Creating Doors");

            GameObject doorOut = new Door
            {
                Name = "Door out to the alleyway",
                Description = "A large service door leads to the alleyway out back",
                ContainerId = pizzeria.Id,
                DestinationId = backAlley.Id

            };

            GameObject door = new Door()
            {
                Name = "Door to Fat Tony's Pizzeria",
                Description = "A large service door for inward goods. You suspect it was once painted white.",
                ContainerId = backAlley.Id,
                DestinationId = pizzeria.Id
            };

            logger.Info("\tCORE\t\t- Creating Items");

            GameObject bin = new Item()
            {
                Name = "Smelly old bin",
                Description = "A dented, aluminium rubbish bin full of old anchovy cans and slightly rotten vegetables.",
                ContainerId = backAlley.Id
            };

            logger.Info("\tCORE\t\t- Creating Default Player");

            Player defaultPlayer = new Player()
            {
                Name = "Default",
                ContainerId = backAlley.Id,
                UserName = "Default"
            };

            logger.Info("\tCORE\t\t- Populating Game Object List");

            _gameObjects.AddRange(
                new[] {                    
                    theVoid, 
                    backAlley,
                    door,
                    bin,
                    pizzeria,
                    doorOut,
                    defaultPlayer
                }
            );

            
        }

        public IEnumerable<GameObject> GetAllObjects()
        {
            logger.Debug("\tCORE\tGetting all objects");
            lock (_lockObject)
                return _gameObjects.ToList();
        }

        public IEnumerable<GameObject> GetContainedObjects(int containerId)
        {
            logger.Debug("\tCORE\tGetting contained objects {0}", containerId);
            lock (_lockObject)
                return _gameObjects.Where(g => g.ContainerId == containerId).ToList();
        }

        public GameObject GetObject(int id)
        {
            logger.Debug("\tCORE\tGetting object {0}", id);
            // Super inefficient, but simplest approach for now.
            lock (_lockObject)
                return _gameObjects.FirstOrDefault(g => g.Id == id);
        }

        public Player GetPlayerByUsername(string userName)
        {
            logger.Debug("\tCORE\tGetting user {0}", userName);
            lock (_lockObject)
                return _gameObjects.OfType<Player>().FirstOrDefault(p => String.Equals(p.UserName, userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool PostMessage(IGameMessage message)
        {
            logger.Debug("\tCORE\tMessage: {0}", message.ToString());
            // Dispatch messages at some point.
            if (message is CreateNewPlayerMessage)
            {
                var m = (CreateNewPlayerMessage)message;
                var player = new Player
                {
                    UserName = m.UserName,
                    Name = m.Name,
                    ContainerId = 1,
                };

                _gameObjects.Add(player);
                
                return true;
            }
            else if (message is MoveObjectMessage)
            {
                var m = (MoveObjectMessage)message;

                var obj = GetObject(m.ObjectId);

                if (m.OriginId != null && m.OriginId.Value != obj.ContainerId)
                {
                    // Trying to move from a location you aren't in.
                    return false;
                }

                obj.ContainerId = m.DestinationId;
            }

            return false;
        }
    }

    public class MoveObjectMessage : IGameMessage
    {
        public int ObjectId { get; set; }
        public int DestinationId { get; set; }
        public int? OriginId { get; set; }
    }
}
