using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{

    public class MessageProcessor
    {
        private Game _game;

        public MessageProcessor(Game game)
        {
            _game = game;
        }

        public bool DispatchMessage(IGameMessage message)
        {
            if (message is CreateNewPlayerMessage)
            {
                return CreatePlayerMessageHandler((CreateNewPlayerMessage)message);
            }
            else if (message is MoveObjectMessage)
            {
                return MoveObjectMessageHandler((MoveObjectMessage)message);
            }
            else if (message is IncrementAPMessage)
            {                
                return IncrementAPMessageHandler((IncrementAPMessage)message);
            }
            else if (message is SetPropertyMessage)
            {
                return SetPropertyMessageHandler((SetPropertyMessage)message);
            }
            return false;
        }

        public bool IncrementAPMessageHandler(IncrementAPMessage message)
        {
            return _game.IncrementAP(message.PlayerID);
        }

        /// <summary>
        /// Acts as a message handler for CreateNewPlayerMessage messages.
        /// </summary>
        /// <param name="message">A CreateNewPlayerMessage used to pass along initialisation values</param>
        /// <returns>True for success, False when the universe ends during execution...or when we get around to error handling</returns>
        public bool CreatePlayerMessageHandler(CreateNewPlayerMessage message)
        {
            // Todo: Add error checking on creating players
            Player newPlayer = new Player(message);
            _game.AddGameObject(newPlayer);
            return true;
        }

        /// <summary>
        /// Acts as a message handler for MoveObjectMessage messages.
        /// </summary>
        /// <param name="message">A MoveObjectMessage used to pass along values for which object to move and where from/to.</param>
        /// <returns>True for success, false for failure</returns>
        public bool MoveObjectMessageHandler(MoveObjectMessage message)
        {
            if (message.OriginId != null)
            {
                var targetObject = _game.GetObject(message.ObjectId);
                if (message.OriginId == targetObject.ContainerId) {
                    targetObject.ContainerId = message.DestinationId;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Acts as a message handler for SetPropertyMessage messages. 
        /// </summary>
        /// <param name="message">A SetPropertyMessage used to pass along the values to be held in the property on the object</param>
        /// <returns>True for success, false for failure</returns>
        public bool SetPropertyMessageHandler(SetPropertyMessage message)
        {
            var targetObject = _game.GetObject(message.ObjectId);
            if (targetObject.Properties.ContainsKey(message.PropertyName))
            {
                targetObject.Properties.Remove(message.PropertyName);
            }
            targetObject.Properties.Add(message.PropertyName, message.Value);
            return true;            
        }
    }
}
