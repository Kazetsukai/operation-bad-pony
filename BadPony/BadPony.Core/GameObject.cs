namespace BadPony.Core
{
    public class GameObject
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
        public GameObject Container { get; set; }

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