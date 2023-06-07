using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene.TileModels;

namespace TestWZIMopoly.Test_Models
{
    [TestClass]
    public class Test_PlayerModel
    {
        [TestMethod]
        public void Test_ResetName()
        {
            // Arrange
            var expectedName = "player";
            PlayerModel player = new PlayerModel(expectedName, "blue");
            // Act
            player.Nick = "test";
            player.ResetNick();

            // Assert
            Assert.AreEqual(expectedName, player.Nick);
        }
        [TestMethod]
        public void Test_Reset()
        {
            // Arrange
            var expectedName = "player";
            var expectedMoney = 1500;
            var expectedPlayerType = PlayerType.None;
            PlayerModel player = new PlayerModel(expectedName, "blue");
            // Act
            player.Nick = "test";
            player.Money = 1000;
            player.PlayerType = PlayerType.Bot;
            player.Reset();

            // Assert
            Assert.AreEqual(expectedName, player.Nick);
            Assert.AreEqual(expectedMoney, player.Money);
            Assert.AreEqual(expectedPlayerType, player.PlayerType);
        }

    }
}
