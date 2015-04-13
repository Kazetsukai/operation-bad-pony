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

        public IEnumerable<GameObject> GetAllObjects()
        {
            return _gameObjects.ToList();
        }
    }

    public class GameObject
    {
        public GameObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public GameObject Container { get; set; }
    }
}
