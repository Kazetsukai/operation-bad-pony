namespace BadPony.Core
{
    public class Item : GameObject
    {
        public override GameObjectType Type
        {
            get { return GameObjectType.Item; }
        }
    }
}
