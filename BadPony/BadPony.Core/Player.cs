namespace BadPony.Core
{
    public class Player : GameObject
    {
        public string UserName { get; set; }

        public override GameObjectType Type {
            get { return GameObjectType.Player; }
        }
    }
}