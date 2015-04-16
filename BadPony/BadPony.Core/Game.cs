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
                new [] {
                    new GameObject()
                    {
                        Name = "Back alley",
                        Description =
                            "You are in a dark alleyway. Foul smells mix together from nearby dumpsters and air-conditioning vents."
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
    }
}
