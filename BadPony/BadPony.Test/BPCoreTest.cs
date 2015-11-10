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
            Assert.Greater(game.GetAllObjects().OfType<Player>().ToList().Count, initialObjectCount, "Number of players has not increased");
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
            MoveObjectMessage moveMsg = new MoveObjectMessage
            {
                ObjectId = game.GetPlayerByUsername(defaultUserName).Id,
                OriginId = backAlley.Id,
                DestinationId = backOfTonys.Id
            };
            game.PostMessage(moveMsg);
            Assert.Less(game.GetContainedObjects(backAlley.Id).OfType<Player>().ToList().Count, initialObjectCount, "Object not moved from Back Alley");
            Assert.Greater(game.GetContainedObjects(backOfTonys.Id).OfType<Player>().ToList().Count, initialTonysCount, "Object not moved to Back of Fat Tony's");
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
            string testKey = "TestKey"; 
            string testValue = "TestValue";
            var testObject = game.GetAllObjects().First();
            var properties = testObject.Properties;
            int initialPropertyCount = properties.Count;
            Assert.IsNull(properties.FirstOrDefault(p => p.Key == testKey).Value, "TestKey already exists at initialisation");
            SetPropertyMessage propertyMessage = new SetPropertyMessage
            {
                ObjectId = testObject.Id,
                PropertyName = testKey,
                Value = testValue
            };
            game.PostMessage(propertyMessage);
            Assert.AreEqual(testValue, properties.FirstOrDefault(p => p.Key == testKey).Value );
        }

        [Test]
        public void TestExecutePropertyMessage()
        {
            var game = CreateTestGame();
            Assert.AreEqual(48, Game.DailyAP, "Default DailyAP value has been changed. Pleast verify TestExecutePropertyMessage test case is still valid and update this assertion.");
            string testKey = "SetAPTo100";
            string testValue = "msg.setDailyAP(100);";
            var testObject = game.GetAllObjects().First();
            SetPropertyMessage setPropertyMessage = new SetPropertyMessage
            {
                ObjectId = testObject.Id,
                PropertyName = testKey,
                Value = testValue
            };
            game.PostMessage(setPropertyMessage);
            ExecutePropertyMessage executePropertyMessage = new ExecutePropertyMessage
            {
                ObjectId = testObject.Id,
                PropertyName = testKey
            };
            game.PostMessage(executePropertyMessage);
            Assert.AreEqual(100, Game.DailyAP);
        }

        [Test]
        public void TestIncrementAPMessage()
        {
            var game = CreateTestGame();
            Player defaultPlayer = game.GetPlayerByUsername("default");
            int initialAP = defaultPlayer.ActionPoints;
            IncrementAPMessage incrementAP = new IncrementAPMessage { PlayerID = defaultPlayer.Id, Time = 60};
            game.PostMessage(incrementAP);
            Assert.Greater(48, initialAP);
            Assert.Greater(defaultPlayer.ActionPoints, initialAP);
        }
    }
}
