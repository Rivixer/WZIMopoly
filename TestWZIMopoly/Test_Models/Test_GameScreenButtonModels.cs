using WZIMopoly.Models;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.GameButtonModels;
using WZIMopoly;
using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace TestWZIMopoly.Test_Models
{
  
    /// <summary>
    /// Test class for Button Models.
    /// </summary>
    [TestClass]
    public class Test_GameScreenButtonModels
    {
        [TestMethod]
        public void Test_BuyButtonActivity_PlayerOnNonPurchasableTile_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var nonPurchasableTile = new StartTileModel(0, 0);
            var buyButton = new BuyButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.AfterRollingDice;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            buyButton.Update(player, nonPurchasableTile);
            var actual = buyButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_BuyButtonActivity_PlayerOnNonPurchasableTile_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var nonPurchasableTile = new StartTileModel(0, 0);
            var buyButton = new BuyButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.AfterRollingDice;

            ///Act
            buyButton.Update(player, nonPurchasableTile);
            var actual = buyButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        internal class TileForTests : TileModel
        {
            public TileForTests(int id) : base(id)
            {
               
            }
        }
        class PurchasableTile : PurchasableTileModel
        {
            public PurchasableTile(int id, int price) : base(id, price)
            {
            }

            public override int GetValue()
            {
                return 1;
            }
        }
        [TestMethod]
        public void Test_DiceButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tile = new TileForTests(123);
            var diceButton = new DiceButtonModel();
            var expected = true;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.BeforeRollingDice;

            ///Act
            diceButton.Update(player, tile);
            var actual = diceButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_DiceButtonActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tile = new TileForTests(123);
            var diceButton = new DiceButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.BeforeRollingDice;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            diceButton.Update(player, tile);
            var actual = diceButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        public void Test_EndTurnButtonActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tile = new TileForTests(123); 
            var endTurnButton = new EndTurnButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.AfterRollingDice;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            endTurnButton.Update(player, tile);
            var actual = endTurnButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_EndTurnButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tile = new TileForTests(123);
            var endTurnButton = new EndTurnButtonModel();
            var expected = true;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.AfterRollingDice;

            ///Act
            endTurnButton.Update(player, tile);
            var actual = endTurnButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeAcceptButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeAcceptButton = new TradeAcceptButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.ReceivingTrade;

            ///Act
            tradeAcceptButton.Update();
            var actual = tradeAcceptButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeAcceptButtonActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeAcceptButton = new TradeAcceptButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.ReceivingTrade;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeAcceptButton.Update();
            var actual = tradeAcceptButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeAddMoneyButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeAddMoneyButton = new TradeAddMoneyButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.Trading;

            ///Act
            tradeAddMoneyButton.Update();
            var actual = tradeAddMoneyButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeAddMoneyButtonActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeAddMoneyButton = new TradeAddMoneyButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.Trading;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeAddMoneyButton.Update();
            var actual = tradeAddMoneyButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeButtonActivity_Local1()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tile = new PurchasableTile(0,0);
            var tradeButton = new TradeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.BeforeRollingDice;
            player.PurchasedTiles.Add(tile);

            ///Act
            tradeButton.Update();
            var actual = tradeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeButtonActivity_Local2()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeButton = new TradeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.Trading;

            ///Act
            tradeButton.Update();
            var actual = tradeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeButtonActivity_Online1()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tile = new PurchasableTile(0, 0);
            var tradeButton = new TradeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.BeforeRollingDice;
            player.PurchasedTiles.Add(tile);
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeButton.Update();
            var actual = tradeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeButtonActivity_Online2()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeButton = new TradeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.Trading;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeButton.Update();
            var actual = tradeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeDeclineActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeDeclineButton = new TradeDeclineButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.ReceivingTrade;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeDeclineButton.Update();
            var actual = tradeDeclineButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeDeclineActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeDeclineButton = new TradeDeclineButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.ReceivingTrade;

            ///Act
            tradeDeclineButton.Update();
            var actual = tradeDeclineButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_TradeMakeButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeMakeButton = new TradeMakeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.Trading;

            ///Act
            tradeMakeButton.Update();
            var actual = tradeMakeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeMakeButtonActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeMakeButton = new TradeMakeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.Trading;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeMakeButton.Update();
            var actual = tradeMakeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_TradeSubtractMoneyButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeSubtractMoneyButton = new TradeSubtractMoneyButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;
            player.PlayerStatus = PlayerStatus.Trading;

            ///Act
            tradeSubtractMoneyButton.Update();
            var actual = tradeSubtractMoneyButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_TradeSubtractMoneyButtonActivity_Online()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var tradeSubtractMoneyButton = new TradeSubtractMoneyButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Online;
            player.PlayerStatus = PlayerStatus.Trading;
            GameSettings.Players.Add(player);
            GameSettings.ClientIndex = 0;

            ///Act
            tradeSubtractMoneyButton.Update();
            var actual = tradeSubtractMoneyButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_UpgradeButtonActivity_Local()
        {
            //Arrange
            var player = new PlayerModel("Player", "Blue");
            var upgradeButton = new UpgradeButtonModel();
            var expected = false;
            WZIMopoly.WZIMopoly.GameType = GameType.Local;


            ///Act
            upgradeButton.Update();
            var actual = upgradeButton.IsActive;

            ///Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
