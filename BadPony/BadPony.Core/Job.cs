namespace BadPony.Core
{
    public class Job : GameObject
    {
        public int APCost { get; set; }
        public int Pay { get; set; }

        public override GameObjectType Type
        {
            get { return GameObjectType.Job; }
        }
    }
}
