namespace BadPony.Core
{
    public class CreateNewPlayerMessage : IGameMessage
    {
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}