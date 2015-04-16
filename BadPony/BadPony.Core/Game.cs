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
            return _gameObjects.ToList();
        }

        public IEnumerable<GameObject> GetContainedObjects(GameObject container)
        {
            return _gameObjects.Where(g => g.Container == container).ToList();
        }
    }
}
