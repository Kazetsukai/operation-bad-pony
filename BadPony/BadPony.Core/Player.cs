namespace BadPony.Core
{
    public class Player : GameObject
    {
        public string UserName { get; set; }
        public int ActionPoints { get; set; }
        public int Cash { get; set; }
        public int LastActionTime { get; set; }

        public override GameObjectType Type {
            get { return GameObjectType.Player; }
        }

        public Player() { }

        public Player(CreateNewPlayerMessage message)
        {
            UserName = message.UserName;
            ActionPoints = 48;
            Name = message.Name;
            ContainerId = 1;
            LastActionTime = 0;
            Description = message.Description;
        }
    }
}