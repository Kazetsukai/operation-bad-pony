using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BadPony.Core;

namespace BadPony.Test
{
    [TestFixture]
    public class BPCoreTest
    {
        private const string name = "Test";
        private const string userName = "TestUser";
        private const string defaultUserName = "default";
        private const int containerId = 0;
        private const string description = "Test description";

        [Test]
        public void TestCreatePlayerMessage()
        {
            var game = CreateTestGame();
            var initialObjectCount = game.GetAllObjects().OfType<Player>().ToList().Count;
            
            CreateNewPlayerMessage playerMessage = new CreateNewPlayerMessage
            {
                Name = name,
                UserName = userName,
                Description = description
            };
            game.PostMessage(playerMessage);
            Assert.Greater(game.GetAllObjects().ToList().Count, initialObjectCount, "Number of players has not increased");
            Assert.IsNotNull(game.GetPlayerByUsername(userName), "User " + userName + " was not found");
        }

        private Game CreateTestGame()
        {
            var game = new Game();

            return game;
        }

        [Test]
        public void TestMoveObjectMessage()
        {
            var game = CreateTestGame();
            var initialObjectCount = game.GetAllObjects().OfType<Player>().ToList().Count;
            
            List<Location> locations = game.GetAllObjects().OfType<Location>().ToList();
            Location backAlley = locations.Where(l => l.Name == "Back alley").First();
            Location backOfTonys = locations.Where(l => l.Name == "Back of Fat Tony's Pizzeria").First();
            initialObjectCount = game.GetContainedObjects(backAlley.Id).OfType<Player>().ToList().Count;
            int initialTonysCount = game.GetContainedObjects(backOfTonys.Id).OfType<Player>().ToList().Count;
            MoveObjectMessage moveMsg = new MoveObjectMessage { ObjectId = game.GetPlayerByUsername(defaultUserName).Id, OriginId = backAlley.Id, DestinationId = backOfTonys.Id };
            game.PostMessage(moveMsg);
            Assert.Less(game.GetContainedObjects(backAlley.Id).OfType<Player>().ToList().Count, initialObjectCount, "Back alley ");
            Assert.Greater(game.GetContainedObjects(backOfTonys.Id).OfType<Player>().ToList().Count, initialTonysCount);
        }

        [Test]
        public void TestGetAllObjects()
        {
            var game = CreateTestGame();
            var initialObjectCount = game.GetAllObjects().OfType<Player>().ToList().Count;
            
            initialObjectCount = game.GetAllObjects().ToList().Count;
            List<GameObject> objects = game.GetAllObjects().ToList();
            Assert.IsNotNull(objects, "No objects were returned");
            Assert.AreEqual(initialObjectCount, objects.Count, "Incorrect number of GameObjects returned");
        }

        [Test]
        public void TestSetPropertyMessage()
        {
            var game = CreateTestGame();
            var initialObjectCount = game.GetAllObjects().OfType<Player>().ToList().Count;
            
        }
    }
}
