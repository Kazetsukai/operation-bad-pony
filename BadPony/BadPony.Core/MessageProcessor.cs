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

        public bool ProcessMessage(IGameMessage message)
        {
            if(message is CreateNewPlayerMessage)
            {
                CreateNewPlayerMessage createPlayerMessage = (CreateNewPlayerMessage)message;
                Player newPlayer = new Player(createPlayerMessage);
                _game.AddGameObject(newPlayer);
                return true;            
            }
            return false;
        }
    }
}
