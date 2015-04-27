using System.Runtime.Serialization;

namespace BadPony.Core
{
    public enum GameObjectType
    {
        Location,
        Player,
        Door,
        Item,
        Job
    }

    public abstract class GameObject
    {
        private static readonly object LockObject = new object();
        private static int _nextId;
        
        public GameObject()
        {
            Id = GetNextId();
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ContainerId { get; set; }
        public abstract GameObjectType Type { get ; }

        // Threadsafe ID generation
        private static int GetNextId()
        {
            int nextId;

            lock (LockObject)
            {
                nextId = _nextId++;
            }

            return nextId;
        }
    }
}