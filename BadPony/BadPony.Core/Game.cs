using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class Game
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<Location> _gameLocations = new List<Location>();
        private object _lockObject = new object();

        public Game()
        {
            TemporaryHardcodedWorldSetup();
        }

        private void TemporaryHardcodedWorldSetup()
        {
            Location theVoid = new Location()
            {
                Name = "The void",
                Description = "You are floating in a formless void.",
                Type = GameObjectType.Location
            };

            Location backAlley = new Location()
            {
                Name = "Back alley",
                Description = "You are in a dark alleyway. Foul smells mix together from nearby dumpsters and air-conditioning vents." ,
                Type = GameObjectType.Location
            };

            GameObject door = new GameObject()
            {
                Name = "Door to Fat Tony's Pizzeria",
                Description = "A large service door for inward goods. You suspect it was once painted white.",
                Type = GameObjectType.Door,
                ContainerId = backAlley.Id
            };

            GameObject bin = new GameObject()
            {
                Name = "Smelly old bin",
                Description = "A dented, aluminium rubbish bin full of old anchovy cans and slightly rotten vegetables.",
                Type = GameObjectType.Item,
                ContainerId = backAlley.Id
            };

            _gameObjects.AddRange(
                new[] {                    
                    theVoid, 
                    backAlley,
                    door,
                    bin
                }
            );
            
            _gameLocations.AddRange(
                new []
                {
                    theVoid,
                    backAlley
                }
            );

        }

        public IEnumerable<GameObject> GetAllObjects()
        {
            lock (_lockObject)
                return _gameObjects.ToList();
        }

        public IEnumerable<GameObject> GetContainedObjects(int containerId)
        {
            lock (_lockObject)
                return _gameObjects.Where(g => g.ContainerId == containerId).ToList();
        }

        public GameObject GetObject(int id)
        {
            // Super inefficient, but simplest approach for now.
            lock (_lockObject)
                return _gameObjects.FirstOrDefault(g => g.Id == id);
        }

        public Location GetLocation(int id)
        {
            lock (_lockObject)
                return _gameLocations.FirstOrDefault(l => l.Id == id);
        }        

        public Player GetPlayerByUsername(string userName)
        {
            lock (_lockObject)
                return _gameObjects.OfType<Player>().FirstOrDefault(p => p.UserName.ToLowerInvariant() == userName.ToLowerInvariant());
        }

        public void PostMessage(IGameMessage message)
        {
            // Dispatch messages at some point.
            if (message is CreateNewPlayerMessage)
            {
                var m = message as CreateNewPlayerMessage;
                var player = new Player()
                {
                    UserName = m.UserName,
                    Name = m.Name,
                    ContainerId = 1,
                    Type = GameObjectType.Player
                };

                _gameObjects.Add(player);
            }
        }
    }
}
