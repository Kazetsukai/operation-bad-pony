namespace BadPony.Core
{
    public class MoveObjectMessage : IGameMessage
    {
        public int ObjectId { get; set; }
        public int DestinationId { get; set; }
        public int? OriginId { get; set; }
    }
}