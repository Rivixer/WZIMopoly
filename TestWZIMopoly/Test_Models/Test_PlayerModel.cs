using WZIMopoly.Enums;
using WZIMopoly.Exceptions;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene.TileModels;

namespace TestWZIMopoly.Test_Models
{
    [TestClass]
    public class Test_PlayerModel
    {
        /// <summary>
        /// Tests for PlayerModel method ResetName which resets its name to "player"
        /// </summary>
        [TestMethod]
        public void Test_ResetName()
        {
            // Arrange
            var expectedName = "player";
            var player = new PlayerModel(expectedName, "blue");

            // Act
            player.Nick = "test";
            player.ResetNick();

            // Assert
            Assert.AreEqual(expectedName, player.Nick);
        }
        /// <summary>
        /// Tests for PlayerModel method Reset which resets its data to default
        /// </summary>
        [TestMethod]
        public void Test_Reset()
        {
            // Arrange
            var expectedName = "player";
            var expectedMoney = 1500;
            var expectedPlayerType = PlayerType.None;
            var player = new PlayerModel(expectedName, "blue");

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
        /// <summary>
        /// Tests for PlayerModel method TransferMoney which transfers money between two players
        /// </summary>
        [TestMethod]
        public void Test_TransferMoney()
        {
            //Arrange
            var player1 = new PlayerModel("playey1", "blue");
            var player2 = new PlayerModel("player2", "blue");
            var expectedPlayer1Money = 1300;
            var expectedPlayer2Money = 1700;

            //Act
            player1.TransferMoneyTo(player2, 200);

            //Assert
            Assert.AreEqual(expectedPlayer1Money, player1.Money);
            Assert.AreEqual(expectedPlayer2Money, player2.Money);
        }
        /// <summary>
        /// Tests for PlayerModel method TransferMoney which tests the range of money while transfering money
        /// </summary>
        [TestMethod]
        public void Test_TransferMoney_MoreThanPlayerHave()
        {
            var player1 = new PlayerModel("playey1", "blue");
            var player2 = new PlayerModel("player2", "blue");

            try
            {
                player1.TransferMoneyTo(player2, 1700);
                Assert.Fail();
            }
            catch (NotEnoughMoney)
            {
            }
        }
    }
}
