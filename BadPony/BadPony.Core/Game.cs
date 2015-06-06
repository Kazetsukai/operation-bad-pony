using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ClearScript.V8;

namespace BadPony.Core
{
    public class Game
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        private object _lockObject = new object();

        private static int DailyAP = 48;

        public Game()
        {
            TemporaryHardcodedWorldSetup();
        }

        private void TemporaryHardcodedWorldSetup()
        {
            Location theVoid = new Location()
            {
                Name = "The void",
                Description = "You are floating in a formless void.",
            };

            Location backAlley = new Location()
            {
                Name = "Back alley",
                Description = "You are in a dark alleyway. Foul smells mix together from nearby dumpsters and air-conditioning vents." ,
            };

            Location pizzeria = new Location()
            {
                Name = "Back of Fat Tony's Pizzeria",
                Description = "You are in the back of Fat Tony's Pizzeria. Around you there are several health codes being broken.",
            };

            GameObject doorOut = new Door
            {
                Name = "Door out to the alleyway",
                Description = "A large service door leads to the alleyway out back",
                ContainerId = pizzeria.Id,
                DestinationId = backAlley.Id

            };

            GameObject door = new Door()
            {
                Name = "Door to Fat Tony's Pizzeria",
                Description = "A large service door for inward goods. You suspect it was once painted white.",
                ContainerId = backAlley.Id,
                DestinationId = pizzeria.Id
            };

            GameObject bin = new Item()
            {
                Name = "Smelly old bin",
                Description = "A dented, aluminium rubbish bin full of old anchovy cans and slightly rotten vegetables.",
                ContainerId = backAlley.Id
            };

            Player defaultPlayer = new Player()
            {
                Name = "Default",
                ContainerId = backAlley.Id,
                UserName = "Default"
            };

            GameObject washDishes = new Job()
            {
                Name = "Wash Dishes",
                APCost = 2,
                Pay = 5,
                ContainerId = pizzeria.Id
            };

            _gameObjects.AddRange(
                new[] {                    
                    theVoid, 
                    backAlley,
                    door,
                    bin,
                    pizzeria,
                    doorOut,
                    defaultPlayer,
                    washDishes
                }
            );

            PostMessage(new SetPropertyMessage
            {
                ObjectId = defaultPlayer.Id,
                PropertyName = "lol",
                Value = "msg.Send(\"testing lol\")"
            });
            PostMessage(new ExecutePropertyMessage
            {
                ObjectId = defaultPlayer.Id,
                PropertyName = "lol"
            });
        }

        public IEnumerable<GameObject> GetAllObjects()
        {
            lock (_lockObject)
                return _gameObjects.ToList();
        }

        public IEnumerable<GameObject> GetContainedObjects(int containerId)
        {
            lock (_lockObject)
                return _gameObjects.Where(g => g.ContainerId == containerId).ToList();
        }

        public GameObject GetObject(int id)
        {
            // Super inefficient, but simplest approach for now.
            lock (_lockObject)
                return _gameObjects.FirstOrDefault(g => g.Id == id);
        }

        public Player GetPlayerByUsername(string userName)
        {
            lock (_lockObject)
                return _gameObjects.OfType<Player>().FirstOrDefault(p => String.Equals(p.UserName, userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void IncrementAP()
        {
            lock (_lockObject)
            {
                List<Player> players = _gameObjects.OfType<Player>().ToList();
                foreach (var player in players)
                {
                    if (player.ActionPoints <= 48)
                    {
                        player.ActionPoints++;
                    }
                }
            }
        }

        public bool PostMessage(IGameMessage message)
        {
            // Dispatch messages at some point.
            if (message is CreateNewPlayerMessage)
            {
                var m = (CreateNewPlayerMessage)message;
                var player = new Player
                {
                    UserName = m.UserName,
                    Name = m.Name,
                    ContainerId = 1,
                };

                _gameObjects.Add(player);
                
                return true;
            }
            else if (message is TimerMessage)
            {
                IncrementAP();
            }
            else if (message is DoJobMessage)
            {
                var jobMessage = (DoJobMessage)message;
                // fetch objects
                Job job = (Job)_gameObjects.FirstOrDefault(j => j.Id == jobMessage.JobId);
                Player player = (Player)_gameObjects.FirstOrDefault(p => p.Id == jobMessage.PlayerId);
                // validate job
                if (job != null && player != null && player.ActionPoints >= job.APCost && job.ContainerId == player.ContainerId)
                {
                    player.ActionPoints -= job.APCost;
                    player.Cash += job.Pay;
                    return true;
                }
                return false;
            }
            else if (message is MoveObjectMessage)
            {
                var m = (MoveObjectMessage)message;

                var obj = GetObject(m.ObjectId);

                if (m.OriginId != null && m.OriginId.Value != obj.ContainerId)
                {
                    // Trying to move from a location you aren't in.
                    return false;
                }

                obj.ContainerId = m.DestinationId;

                return true;
            }
            else if (message is SetPropertyMessage)
            {
                var m = (SetPropertyMessage)message;

                var obj = GetObject(m.ObjectId);

                if (obj.Properties.ContainsKey(m.PropertyName))
                {
                    obj.Properties.Remove(m.PropertyName);
                }

                obj.Properties.Add(m.PropertyName, m.Value);

                return true;
            }
            else if (message is ExecutePropertyMessage)
            {
                var m = (ExecutePropertyMessage)message;

                var obj = GetObject(m.ObjectId);

                if (obj.Properties.ContainsKey(m.PropertyName))
                {
                    var code = obj.Properties[m.PropertyName];

                    try
                    {
                        if (code != null)
                        {
                            // This is the core of the javascript magic
                            // We spin up a new v8 engine, initiliase a bunch of relevant properties, and run the script
                            // We create a new v8 engine each time so one piece of code can't mess with other code.
                            var engine = new V8ScriptEngine(V8ScriptEngineFlags.DisableGlobalMembers);

                            engine.AddHostObject("msg", new MessageHost());

                            foreach (var prop in obj.Properties)
                                engine.Script["$" + prop.Key] = prop.Value;

                            engine.Execute(code);
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }

    }

    public class MessageHost
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }
}
