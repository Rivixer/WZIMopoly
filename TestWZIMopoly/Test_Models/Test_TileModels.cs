using WZIMopoly.Models;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace TestWZIMopoly.Test_Models
{
    internal class PurchaseTile : PurchasableTileModel
    {
        public PurchaseTile(int id, int price) : base(id, price) { }
    }

    [TestClass]
    public class Test_TileModels
    {
        [TestMethod]
        public void Test_OnStand_ConditionalPass()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            ConditionalPassTileModel conditionalpass = new ConditionalPassTileModel(1, 20);
            var expectedResult = player.Money - 20;

            // Act
            conditionalpass.OnPlayerStand(player);

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_OnStand_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = true;

            // Act
            mandatorylecture.OnPlayerStand(player);
            var result = mandatorylecture.IsPrisoner(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanPurchase_True_PurchasableTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PurchaseTile purchasableTile = new PurchaseTile(1, 20);
            var expectedResult = true;

            //Act
            var result = purchasableTile.CanPurchase(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanPurchase_FalseLowMoney_PurchasableTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PurchaseTile purchasableTile = new PurchaseTile(1, 20);
            var expectedResult = false;

            // Act
            player.Money = 0;
            var result = purchasableTile.CanPurchase(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanPurchase_FalseOwned_PurchasableTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player2", "red");
            PurchaseTile purchasableTile = new PurchaseTile(1, 20);
            var expectedResult = false;

            // Act
            purchasableTile.Purchase(player);
            var result = purchasableTile.CanPurchase(player2);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_Purchase_Owner_PurchasableTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PurchaseTile purchasableTile = new PurchaseTile(1, 20);
            var expectedResult = player;

            // Act
            purchasableTile.Purchase(player);

            // Assert
            Assert.AreEqual(expectedResult, purchasableTile.Owner);
        }

        [TestMethod]
        public void Test_Purchase_SubstractPrice_PurchasableTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PurchaseTile purchasableTile = new PurchaseTile(1, 20);
            var expectedResult = player.Money - 20;

            // Act
            purchasableTile.Purchase(player);

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_Purchase_PurchasedTiles_PurchasableTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PurchaseTile purchasableTile = new PurchaseTile(1, 20);
            var expectedResult = new List<PurchasableTileModel>
            {
                purchasableTile

            };

            // Act
            purchasableTile.Purchase(player);

            // Assert
            CollectionAssert.AreEqual(expectedResult, player.PurchasedTiles);
        }

        [TestMethod]
        public void Test_OnStand_SubstractMoney_Restroom()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player2", "red");
            Dictionary<RestroomAmount, int> taxPrices = new Dictionary<RestroomAmount, int>()
            {
                { RestroomAmount.One, 20 },
                { RestroomAmount.Two, 40 }
            };
            RestroomTileModel restroom = new RestroomTileModel(1, 20, taxPrices);
            var expectedResult = player2.Money - 20;

            // Act
            restroom.Purchase(player);
            restroom.OnPlayerStand(player2);

            // Assert
            Assert.AreEqual(expectedResult, player2.Money);
        }

        [TestMethod]
        public void Test_OnStand_TransferMoney_Restroom()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player2", "red");
            Dictionary<RestroomAmount, int> taxPrices = new Dictionary<RestroomAmount, int>()
            {
                { RestroomAmount.One, 20 },
                { RestroomAmount.Two, 40 }
            };
            RestroomTileModel restroom = new RestroomTileModel(1, 20, taxPrices);
            var expectedResult = player.Money + 20;

            // Act
            restroom.Purchase(player);
            player.Money = 1500; //Resetting the money
            restroom.OnPlayerStand(player2);

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_OnStand_Start()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            StartTileModel start = new StartTileModel(1, 20);
            var expectedResult = player.Money + 20;

            // Act
            start.OnPlayerStand(player);

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_OnStand_TransferMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player2", "red");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }
            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = player.Money + 20;

            // Act
            subject.Purchase(player);
            player.Money = 1500; //Resetting the money
            subject.OnPlayerStand(player2);

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_OnStand_SubstractMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player2", "red");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                 {SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }
            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = player2.Money - 20;

            // Act
            subject.Purchase(player);
            subject.OnPlayerStand(player2);

            // Assert
            Assert.AreEqual(expectedResult, player2.Money);
        }

        [TestMethod]
        public void Test_Upgrade_HigherGrade_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }
            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = SubjectGrade.ThreeHalf;

            // Act
            subject.Purchase(player);
            subject.Upgrade();

            // Assert
            Assert.AreEqual(expectedResult, subject.Grade);
        }

        [TestMethod]
        public void Test_Upgrade_SubstractMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }
            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = player.Money - 20;

            // Act
            subject.Purchase(player);
            player.Money = 1500; //Resetting the money
            subject.Upgrade();

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_CanMortgage_True_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }
            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = true;

            // Act
            subject.Purchase(player);
            var result = subject.CanMortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanMortgage_FalseOwned_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }
            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            var result = subject.CanMortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanMortgage_FalseIsMortgaged_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player);
            subject.Mortgage();
            var result = subject.CanMortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanMortgage_FalseGrade_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player);
            subject.Grade = SubjectGrade.ThreeHalf;
            var result = subject.CanMortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUnmortgage_True_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = true;

            // Act
            subject.Purchase(player);
            subject.Mortgage();
            var result = subject.CanUnmortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUnmortgage_FalseOwned_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player", "red");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player2);
            subject.Mortgage();
            var result = subject.CanUnmortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUnmortgage_FalseIsMortgaged_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player);
            var result = subject.CanUnmortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUnmortgage_FalseLowMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player);
            subject.Mortgage();
            player.Money = 0;
            var result = subject.CanUnmortgage(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanSellGrade_True_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = true;

            // Act
            subject.Purchase(player);
            subject.Upgrade();
            var result = subject.CanSellGrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanSellGrade_FalseOwned_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            PlayerModel player2 = new PlayerModel("player", "red");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player2);
            subject.Upgrade();
            var result = subject.CanSellGrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanSellGrade_FalseGrade_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player);
            var result = subject.CanSellGrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_Mortgage_MortgagedTiles_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = new List<PurchasableTileModel>
            {
                subject
            };

            // Act
            subject.Purchase(player);
            subject.Mortgage();

            // Assert
            CollectionAssert.AreEqual(expectedResult, player.MortgagedTiles);
        }

        [TestMethod]
        public void Test_Mortgage_AddMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = player.Money + 10;

            // Act
            subject.Purchase(player);
            player.Money = 1500; //Resetting the money
            subject.Mortgage();

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_Mortgage_IsMortgaged_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = true;

            // Act
            subject.Purchase(player);
            subject.Mortgage();

            // Assert
            Assert.AreEqual(expectedResult, subject.IsMortgaged);
        }

        [TestMethod]
        public void Test_Unmortgage_SubstractMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = player.Money - 10;

            // Act
            subject.Purchase(player);
            subject.Mortgage();
            player.Money = 1500; //Resetting the money
            subject.Unmortgage();

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_Unmortgage_MortgagedTiles_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = new List<PurchasableTileModel>();

            // Act
            subject.Purchase(player);
            subject.Mortgage();
            subject.Unmortgage();

            // Assert
            CollectionAssert.AreEqual(expectedResult, player.MortgagedTiles);
        }

        [TestMethod]
        public void Test_Unmortgage_IsMortgaged_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = false;

            // Act
            subject.Purchase(player);
            subject.Mortgage();
            subject.Unmortgage();

            // Assert
            Assert.AreEqual(expectedResult, subject.IsMortgaged);
        }

        [TestMethod]
        public void Test_SellGrade_LowerGrade_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = SubjectGrade.Three;

            // Act
            subject.Purchase(player);
            subject.Upgrade();
            subject.SellGrade();

            // Assert
            Assert.AreEqual(expectedResult, subject.Grade);
        }

        [TestMethod]
        public void Test_SellGrade_AddMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            var expectedResult = player.Money + 10;

            // Act
            subject.Purchase(player);
            subject.Upgrade();
            player.Money = 1500; //Resetting the money
            subject.SellGrade();

            // Assert
            Assert.AreEqual(expectedResult, player.Money);
        }

        [TestMethod]
        public void Test_CanUpgrade_True_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject3 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject4 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Blue);
            List<TileModel> tiles = new List<TileModel>
            {
                subject, subject2, subject3, subject4
            };
            var expectedResult = true;

            // Act
            subject.SetAllTiles(tiles);
            subject.Purchase(player);
            subject2.Purchase(player);
            subject3.Purchase(player);
            subject4.Purchase(player);
            var result = subject.CanUpgrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUpgrade_FalseLowMoney_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject3 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject4 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Blue);
            List<TileModel> tiles = new List<TileModel>
            {
                subject, subject2, subject3, subject4
            };
            var expectedResult = false;

            // Act
            subject.SetAllTiles(tiles);
            subject.Purchase(player);
            subject2.Purchase(player);
            subject3.Purchase(player);
            subject4.Purchase(player);
            player.Money = 0;
            var result = subject.CanUpgrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUpgrade_FalseGradeExemption_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject3 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject4 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Blue);
            List<TileModel> tiles = new List<TileModel>
            {
                subject, subject2, subject3, subject4
            };
            var expectedResult = false;

            // Act
            subject.SetAllTiles(tiles);
            subject.Purchase(player);
            subject2.Purchase(player);
            subject3.Purchase(player);
            subject4.Purchase(player);
            subject.Grade = SubjectGrade.Exemption;
            var result = subject.CanUpgrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUpgrade_FalseIsMortgaged_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject3 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject4 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Blue);
            List<TileModel> tiles = new List<TileModel>
            {
                subject, subject2, subject3, subject4
            };
            var expectedResult = false;

            // Act
            subject.SetAllTiles(tiles);
            subject.Purchase(player);
            subject2.Purchase(player);
            subject3.Purchase(player);
            subject4.Purchase(player);
            subject.Mortgage();
            var result = subject.CanUpgrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUpgrade_FalseMortgagdInSet_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject3 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject4 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Blue);
            List<TileModel> tiles = new List<TileModel>
            {
                subject, subject2, subject3, subject4
            };
            var expectedResult = false;

            // Act
            subject.SetAllTiles(tiles);
            subject.Purchase(player);
            subject2.Purchase(player);
            subject3.Purchase(player);
            subject4.Purchase(player);
            subject2.Mortgage();
            var result = subject.CanUpgrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanUpgrade_FalseColorSet_SubjectTile()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject3 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject4 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Blue);
            List<TileModel> tiles = new List<TileModel>
            {
                subject, subject2, subject3, subject4
            };
            var expectedResult = false;

            // Act
            subject.SetAllTiles(tiles);
            subject.Purchase(player);
            subject3.Purchase(player);
            subject4.Purchase(player);
            var result = subject.CanUpgrade(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

    }
}
