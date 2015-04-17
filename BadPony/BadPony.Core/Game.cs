using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class Game
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        private object _lockObject = new object();

        public Game()
        {
            TemporaryHardcodedWorldSetup();
        }

        private void TemporaryHardcodedWorldSetup()
        {
            _gameObjects.AddRange(
                new[] {
                    new GameObject()
                    {
                        Name = "The void",
                        Description =
                            "You are floating in a formless void."
                    }, 
                    new GameObject()
                    {
                        Name = "Back alley",
                        Description =
                            "You are in a dark alleyway. Foul smells mix together from nearby dumpsters and air-conditioning vents."
                    }, 
                    new Player()
                    {
                        Name = "Player",
                        Description =
                            "A human person.",
                        UserName = "Kazetsukai",
                        ContainerId = 1
                    }, 
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

        public Player GetPlayerByUsername(string userName)
        {
            lock (_lockObject)
                return _gameObjects.OfType<Player>().FirstOrDefault(p => p.UserName.ToLowerInvariant() == userName.ToLowerInvariant());
        }
    }
}
