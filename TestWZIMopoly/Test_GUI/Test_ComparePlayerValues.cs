using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene.TileModels;
#nullable disable

namespace TestWZIMopoly.Test_GUI
{
    [TestClass]
    public class Test_ComparePlayerValues
    {
        [TestMethod]
        public void Test_PlayerModelCompare_OneNoneTypePlayer()
        {
            // Arrange
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 100, 40, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            PlayerModel player1 = new PlayerModel("Gracz", "pink");
            PlayerModel player2 = new PlayerModel("Gracz2", "red",PlayerType.Local);
            PlayerModel player3 = new PlayerModel("Gracz3", "green",PlayerType.Local);
            PlayerModel player4 = new PlayerModel("Gracz4", "blue", PlayerType.Local);

            subject.Purchase(player3);
            subject2.Purchase(player4);
            subject.Grade = SubjectGrade.Four;
            player3.Money = 1400;
            player4.Money = 1500;
            player2.Money = 200;
            var expectedResult = new List<PlayerModel>
            {
                player3,
                player4,
                player2,
                player1
            };

            // Act
            List<PlayerModel> players = new List<PlayerModel>
            { 
                player1,
                player2,
                player3,
                player4
            };
            players.Sort(new ComparePlayerValues());

            // Arange
            CollectionAssert.AreEqual(expectedResult, players);
        }

        [TestMethod]
        public void Test_PlayerModelCompare_OneBankrupt()
        {
            // Arrange
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 100, 40, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            PlayerModel player1 = new PlayerModel("Gracz", "pink");
            PlayerModel player2 = new PlayerModel("Gracz2", "red", PlayerType.Local);
            PlayerModel player3 = new PlayerModel("Gracz3", "green", PlayerType.Local);
            PlayerModel player4 = new PlayerModel("Gracz4", "blue", PlayerType.Local);

            subject.Purchase(player3);
            subject2.Purchase(player4);
            subject.Grade = SubjectGrade.Four;
            player3.Money = 1400;
            player4.PlayerStatus = PlayerStatus.Bankrupt;
            player2.Money = 200;

            var expectedResult = new List<PlayerModel>
            {
                player3,
                player2,
                player4,
                player1
            };

            // Act
            List<PlayerModel> players = new List<PlayerModel>
            {
                player1,
                player2,
                player3,
                player4
            };
            players.Sort(new ComparePlayerValues());

            // Arange
            CollectionAssert.AreEqual(expectedResult, players);
        }

        [TestMethod]
        public void Test_PlayerModelCompare_TwoBankrupts()
        {
            // Arrange
            Dictionary<SubjectGrade, int> taxPrices = new Dictionary<SubjectGrade, int>()
            {
                { SubjectGrade.Three, 20 },
                { SubjectGrade.ThreeHalf, 40 }

            };
            SubjectTileModel subject = new SubjectTileModel(1, 100, 40, taxPrices, SubjectColor.Pink);
            SubjectTileModel subject2 = new SubjectTileModel(1, 20, 20, taxPrices, SubjectColor.Pink);
            PlayerModel player1 = new PlayerModel("Gracz", "pink",PlayerType.Local);
            PlayerModel player2 = new PlayerModel("Gracz2", "red", PlayerType.Local);
            PlayerModel player3 = new PlayerModel("Gracz3", "green", PlayerType.Local);
            PlayerModel player4 = new PlayerModel("Gracz4", "blue", PlayerType.Local);

            subject.Purchase(player3);
            subject2.Purchase(player4);
            subject.Grade = SubjectGrade.Four;
            player3.Money = 1400;
            player4.PlayerStatus = PlayerStatus.Bankrupt;
            player2.Money = 200;
            player1.PlayerStatus = PlayerStatus.Bankrupt;

            DateTime var1 = new DateTime(2023, 6, 11, 18, 49, 0);
            var prop = player4.GetType().GetField("_bankcruptcyTime", System.Reflection.BindingFlags.NonPublic
    |               System.Reflection.BindingFlags.Instance);
                    prop.SetValue(player4, var1);

            DateTime var2 = DateTime.Now;
            var prop2 = player1.GetType().GetField("_bankcruptcyTime", System.Reflection.BindingFlags.NonPublic
|                      System.Reflection.BindingFlags.Instance);
                       prop2.SetValue(player1, var2);

            var expectedResult = new List<PlayerModel>
            {
                player3,
                player2,
                player4,
                player1
            };

            // Act
            List<PlayerModel> players = new List<PlayerModel>
            {
                player1,
                player2,
                player3,
                player4
            };
            players.Sort(new ComparePlayerValues());

            // Arange
            CollectionAssert.AreEqual(expectedResult, players);
        }

    }
}
