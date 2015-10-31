namespace BadPony.Core
{
    /// <summary>
    /// The Increment AP message is used to increment the Action Points of a particular player.
    /// PlayerID specifies the Player to target and Time is the time as an integer representing the number of seconds the game has been running so that the
   ///  message processor can calculate time to schedule the next Increment AP message if required.
    /// </summary>
    public class IncrementAPMessage : IGameMessage
    {
        public int PlayerID { get; set; }
        public int Time { get; set; }
    }
}
