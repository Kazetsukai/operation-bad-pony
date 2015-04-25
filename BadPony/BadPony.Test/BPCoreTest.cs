using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BadPony.Core;

namespace BadPony.Test
{
    [TestFixture]
    public class BPCoreTest
    {
        private string name = "Test";
        private string userName = "TestUser";
        private int containerId = 0;
        private string description = "Test description";
        private Game game = new Game();
        private int initialObjectCount;

        [Test]
        public void TestCreatePlayerMessage()
        {
            initialObjectCount = game.GetAllObjects().OfType<Player>().ToList().Count;
            
            CreateNewPlayerMessage playerMessage = new CreateNewPlayerMessage
            {
                Name = name,
                UserName = userName,
                Type = GameObjectType.Player
            };
            game.PostMessage(playerMessage);
            Assert.Greater(game.GetAllObjects().ToList().Count, initialObjectCount, "Number of players has not increased");
            Assert.IsNotNull(game.GetPlayerByUsername(userName), "User " + userName + " was not found");
        }

        [Test]
        public void TestGetAllObjects()
        {
            initialObjectCount = game.GetAllObjects().ToList().Count;
            List<GameObject> objects = game.GetAllObjects().ToList();
            Assert.IsNotNull(objects, "No objects were returned");
            Assert.AreEqual(initialObjectCount, objects.Count, "Incorrect number of GameObjects returned");
        }
    }
}
