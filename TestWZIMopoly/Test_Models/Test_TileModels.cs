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

    /// <summary>
    /// Test class for Tile Models.
    /// </summary>
    [TestClass]
    public class Test_TileModels
    {
        /// <summary>
        /// Test for the OnStand method in ConditionalPassTileModel.
        /// </summary>
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

        /// <summary>
        /// Test for the OnStand method in MandatoryLectureTileModel.
        /// </summary>
        [TestMethod]
        public void Test_AddPrisoner_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = true;

            // Act
            mandatorylecture.AddPrisoner(player);
            var result = mandatorylecture.IsPrisoner(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Test for the CanPurchase method in PurchasableTileModel when the player can purchase the tile.
        /// </summary>
        [TestMethod]
        public void Test_CanPrisonerPayForRelease_True_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = true;

            // Act
            mandatorylecture.AddPrisoner(player);
            var result = mandatorylecture.CanPrisonerPayForRelease(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanPrisonerPayForRelease_FalseLowMoney_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = false;

            // Act
            mandatorylecture.AddPrisoner(player);
            player.Money = 0;
            var result = mandatorylecture.CanPrisonerPayForRelease(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_ReleasePrisoner_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = false;

            // Act
            mandatorylecture.AddPrisoner(player);
            mandatorylecture.ReleasePrisoner(player);
            var result = mandatorylecture.IsPrisoner(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_GetRemainingTurns_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = 4;

            // Act
            mandatorylecture.AddPrisoner(player);
            var result = mandatorylecture.GetRemainingTurns(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_AddPrisonerTurn_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = 3;

            // Act
            mandatorylecture.AddPrisoner(player);
            mandatorylecture.AddPrisonerTurn(player);
            var result = mandatorylecture.GetRemainingTurns(player);

            // Assert
            Assert.AreEqual(expectedResult,result);
        }

        [TestMethod]
        public void Test_CanPrisonerRelease_True_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = true;

            // Act
            mandatorylecture.AddPrisoner(player);
            mandatorylecture.AddPrisonerTurn(player);
            mandatorylecture.AddPrisonerTurn(player);
            mandatorylecture.AddPrisonerTurn(player);
            mandatorylecture.AddPrisonerTurn(player);
            var result = mandatorylecture.CanPrisonerRelease(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Test_CanPrisonerRelease_FalseLowTurns_MandatoryLecture()
        {
            // Arrange
            PlayerModel player = new PlayerModel("player", "blue");
            MandatoryLectureTileModel mandatorylecture = new MandatoryLectureTileModel(1, 20);
            var expectedResult = false;

            // Act
            mandatorylecture.AddPrisoner(player);
            var result = mandatorylecture.CanPrisonerRelease(player);

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

            // Act
            var result = purchasableTile.CanPurchase(player);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Test for the CanPurchase method in PurchasableTileModel when the player has low money to purchase the tile.
        /// </summary>
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

        /// <summary>
        /// Test for the CanPurchase method in PurchasableTileModel when the tile is already owned by another player.
        /// </summary>
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

        /// <summary>
        /// Test for the Purchase method in PurchasableTileModel to check if the purchase is successful and the player becomes the owner of the tile.
        /// </summary>

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

        /// <summary>
        /// Test case to verify that the price is correctly subtracted from the player's money when purchasing a purchasable tile.
        /// </summary>
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

        /// <summary>
        /// Test case to verify that a purchased tile is added to the player's list of purchased tiles.
        /// </summary>
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

        /// <summary>
        /// Test case to verify that the player's money is correctly subtracted when landing on a RestroomTile.
        /// </summary>
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

        /// <summary>
        /// Test case to verify that the money is correctly transferred to the player when landing on a RestroomTile.
        /// </summary>
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

        ///<summary>
        ///Test case for the 'Test_OnStand_Start' method.
        ///It verifies that when a player stands on a start tile, their money is increased by 20.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_OnStand_TransferMoney_SubjectTile' method.
        ///It verifies that when a player stands on a subject tile owned by another player, their money is increased by 20.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_OnStand_SubstractMoney_SubjectTile' method.
        ///It verifies that when a player stands on a subject tile owned by another player, the owner's money is decreased by 20.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_Upgrade_HigherGrade_SubjectTile' method.
        ///It verifies that when a player upgrades a subject tile, the tile's grade is increased to the next grade.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_Upgrade_SubstractMoney_SubjectTile' method.
        ///It verifies that when a player upgrades a subject tile, their money is decreased by 20.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanMortgage_True_SubjectTile' method.
        ///It verifies that a subject tile can be mortgaged when it is owned by a player.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanMortgage_FalseOwned_SubjectTile' method.
        ///It verifies that a subject tile cannot be mortgaged when it is not owned by any player.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanMortgage_FalseIsMortgaged_SubjectTile' method.
        ///It verifies that a subject tile cannot be mortgaged when it is already mortgaged.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanMortgage_FalseGrade_SubjectTile' method.
        ///It verifies that a subject tile cannot be mortgaged when its grade is higher than the minimum grade for mortgage.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanUnmortgage_True_SubjectTile' method.
        ///It verifies that a mortgaged subject tile can be unmortgaged when it is owned by a player and they have enough money to pay the unmortgage cost.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanUnmortgage_FalseOwned_SubjectTile' method.
        ///It verifies that a mortgaged subject tile cannot be unmortgaged when it is not owned by the player trying to unmortgage it.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanUnmortgage_FalseIsMortgaged_SubjectTile' method.
        ///It verifies that a subject tile cannot be unmortgaged when it is not mortgaged.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanUnmortgage_FalseLowMoney_SubjectTile' method.
        ///It verifies that a mortgaged subject tile cannot be unmortgaged when the player does not have enough money to pay the unmortgage cost.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanSellGrade_True_SubjectTile' method.
        ///It verifies that a subject tile's grade can be sold when it is owned by a player and its grade is higher than the minimum grade for selling.
        ///</summary>
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
        ///<summary>
        ///Test case for the 'Test_CanSellGrade_FalseOwned_SubjectTile' method.
        ///It verifies that a subject tile's grade cannot be sold when it is not owned by the player trying to sell the grade.
        ///</summary>
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

        ///<summary>
        ///Test case for the 'Test_CanSellGrade_FalseGrade_SubjectTile' method.
        ///It verifies that a subject tile's grade cannot be sold when its grade is equal to the minimum grade for selling.
        ///</summary>
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
        /// <summary>
        /// Test case for the 'Test_Mortgage_MortgagedTiles_SubjectTile' method.
        /// It verifies that when a player mortgages a subject tile, the tile is added to the player's mortgaged tiles.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_Mortgage_AddMoney_SubjectTile' method.
        /// It verifies that when a player mortgages a subject tile, their money decreases by the mortgage value.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_Mortgage_IsMortgaged_SubjectTile' method.
        /// It verifies that when a subject tile is mortgaged, its 'IsMortgaged' property is set to 'true'.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_Unmortgage_SubstractMoney_SubjectTile' method.
        /// It verifies that when a player unmortgages a subject tile, their money decreases by the unmortgage value.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_Unmortgage_MortgagedTiles_SubjectTile' method.
        /// It verifies that when a player unmortgages a subject tile, the tile is removed from the player's mortgaged tiles.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_Unmortgage_IsMortgaged_SubjectTile' method.
        /// It verifies that when a subject tile is unmortgaged, its 'IsMortgaged' property is set to 'false'.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_SellGrade_LowerGrade_SubjectTile' method.
        /// It verifies that when a player sells a subject tile's grade, the tile's grade is decreased by 1.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_SellGrade_AddMoney_SubjectTile' method.
        /// It verifies that when a player sells a subject tile's grade, their money increases by the sell grade value.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_CanUpgrade_True_SubjectTile' method.
        /// It verifies that a subject tile can be upgraded when it is owned by a player, has all tiles of the same set owned, has enough money to upgrade, and is not mortgaged.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_CanUpgrade_FalseLowMoney_SubjectTile' method.
        /// It verifies that a subject tile cannot be upgraded when the player does not have enough money to upgrade.
        /// </summary>
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
        // <summary>
        /// Test case for the 'Test_CanUpgrade_FalseGradeExemption_SubjectTile' method.
        /// It verifies that a subject tile cannot be upgraded when its grade is 'Exemption'.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_CanUpgrade_FalseIsMortgaged_SubjectTile' method.
        /// It verifies that a subject tile cannot be upgraded when it is mortgaged.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_CanUpgrade_FalseMortgagdInSet_SubjectTile' method.
        /// It verifies that a subject tile cannot be upgraded when any other tile in its set is mortgaged.
        /// </summary>
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
        /// <summary>
        /// Test case for the 'Test_CanUpgrade_FalseColorSet_SubjectTile' method.
        /// It verifies that a subject tile cannot be upgraded when all tiles in its set are not owned by the same player.
        /// </summary>
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
