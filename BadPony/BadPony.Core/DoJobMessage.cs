namespace BadPony.Core
{
    public class DoJobMessage : IGameMessage
    {
        public int JobId { get; set; }
        public int PlayerId { get; set; }
    }
}