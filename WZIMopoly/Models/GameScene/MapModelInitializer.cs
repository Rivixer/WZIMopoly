using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System;
using WZIMopoly.Controllers.GameScene.TileControllers;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Exceptions;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Extends the <see cref="MapModel"/> class with initialization methods.
    /// </summary>
    internal partial class MapModel : Model
    {
        /// <summary>
        /// Loads tiles from a xml file.
        /// </summary>
        /// <returns>
        /// The list of loaded tiles.
        /// </returns>
        public List<TileController> LoadTiles()
        {
            var tilesXml = new XmlDocument();
#if WINDOWS
            var path = "../../../Properties/Tiles.xml";
#elif LINUX
            var path = "WZIMopoly/Properties/Tiles.xml";
#endif

            tilesXml.Load(path);

            var tiles = new List<TileController>();
            string controllerNamespace = "WZIMopoly.Controllers.GameScene.TileControllers";
            string modelNamespace = "WZIMopoly.Models.GameScene.TileModels";
            foreach (XmlNode tileNode in tilesXml.DocumentElement.ChildNodes)
            {
                string rawTileType = tileNode.Attributes["type"].Value;
                Type tileControllerType = Type.GetType($"{controllerNamespace}.{rawTileType}TileController");
                Type tileGenericControllerType = Type.GetType($"{controllerNamespace}.TileController");
                Type tileModelType = Type.GetType($"{modelNamespace}.{rawTileType}TileModel");

                TileModel tileModel;
                if (tileModelType.IsAssignableTo(typeof(TileModel)))
                {
                    MethodInfo loadMethod = tileModelType.GetMethod("LoadFromXml");
                    tileModel = (TileModel)loadMethod.Invoke(null, new object[] { tileNode });
                }
                else
                {
                    throw new InvalidTypeException(
                        $"Tile model type {tileModelType} is not assignable to {typeof(TileModel)}");
                }

                TileController tileController;
                if (tileModel is SubjectTileModel)
                {
                    tileController = (TileController)Activator.CreateInstance(
                        type: tileControllerType,
                        bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        args: new object[] { tileModel, new GUISubjectTile(tileNode, tileModel as SubjectTileModel) },
                        culture: null
                    );
                }
                else if (tileModel is PurchasableTileModel)
                {
                    tileController = (TileController)Activator.CreateInstance(
                        type: tileControllerType,
                        bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        args: new object[] { tileModel, new GUIPurchasableTile(tileNode, tileModel as PurchasableTileModel) },
                        culture: null
                    );
                }
                else
                {
                    tileController = (TileController)Activator.CreateInstance(
                        type: tileControllerType,
                        bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        args: new object[] { tileModel, new GUITile(tileNode, tileModel) },
                        culture: null
                    );
                }

                tiles.Add(tileController);
            }

            tiles.ForEach(AddChild);
            tiles.ForEach(x => x.Model.SetAllTiles(tiles.Select(x => x.Model).ToList()));

            var deaneryTile = tiles.First(x => x.Model is DeaneryTileModel);
            var mandatoryLectureTile = GetModel<MandatoryLectureTileModel>();
            deaneryTile.Model.OnStand += (player) =>
            {
                TeleportPlayer(player, mandatoryLectureTile);
                mandatoryLectureTile.AddPrisoner(player);
                ActivateOnStandTile(player);
            };

            var canteenTiles = tiles.Select(x => x.Model).Where(x => x is CanteenTileModel).ToList();
            foreach (var canteenTile in canteenTiles.Cast<CanteenTileModel>())
            {
                canteenTile.OnStand += (player) =>
                {
                    var canteenCards = GetAllModels<ChanceCardModel>(x => x.Type == ChanceCardType.Canteen);
                    var cardNumber = s_random.Next(canteenCards.Count);
                    canteenTile.DrawnCard = canteenCards[cardNumber];
                    canteenTile.DrawnCard.OnCardDrawn(player);
                };
            }

            var vendingMachineTiles = tiles.Select(x => x.Model).Where(x => x is VendingMachineTileModel).ToList();
            foreach (var vendingMachineTile in vendingMachineTiles.Cast<VendingMachineTileModel>())
            {
                vendingMachineTile.OnStand += (player) =>
                {
                    var vendingMachineCards = GetAllModels<ChanceCardModel>(x => x.Type == ChanceCardType.VendingMachine);
                    int cardNumber;
                    do
                    {
                        cardNumber = s_random.Next(vendingMachineCards.Count);
                    } while (cardNumber == 11 && GameSettings.ActivePlayers.Select(x => x.Money).Any(x => x < 10));
                    // The 12th card is the "It is your birthday! - get 10 ECTS from each student" card
                    // and I don't want to implement bankruptcy handling, so I'm just skipping it.
                    
                    vendingMachineTile.DrawnCard = vendingMachineCards[cardNumber];
                    vendingMachineTile.DrawnCard.OnCardDrawn(player);
                };
            }

            return tiles;
        }

        /// <summary>
        /// Initializes the chance cards.
        /// </summary>
        /// <param name="mortgageCtrl">
        /// The mortgage controller used to handle bankruptcies.
        /// </param>
        /// <param name="gameModel">
        /// The game model used to send data to the server.
        /// </param>
        public void InitializeChanceCards(MortgageController mortgageCtrl, GameModel gameModel)
        {
            ChanceCardController ctrl;
            var mapView = gameModel.GetView<GUIMap>();
            var chanceTiles = GetAllModels<ChanceTileModel>();

            // Go to Physics - if you go through start, get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(1, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var physicsTile = GetModel<SubjectTileModel>(x => x.Id == 10);
                List<TileController> passedTiles = MovePlayer(player, physicsTile);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                ActivateCrossableTiles(player, passedTiles);
                mapView.UpdatePawnPositions();
            };

            // Go to nearest restroom - if you go through start, get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(2, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var nearestRestroom = FindNearestTile<RestroomTileController>(player);
                MovePlayer(player, nearestRestroom);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                mapView.UpdatePawnPositions();
            };

            // You did nothing in group project so your team failed - pay 50 ECTS each student
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(3, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                void TransferMoneyToPlayers(List<PlayerModel> players)
                {
                    var moneyNeeded = 50 * players.Count;
                    if (player.Money < moneyNeeded)
                    {
                        throw new NotEnoughMoney(player, player.Money - moneyNeeded);
                    }
                    players.ForEach(x => player.TransferMoneyTo(x, 50));
                }
                var otherPlayers = GameSettings.ActivePlayers.Where(x => x.PlayerStatus != PlayerStatus.Bankrupt && !player.Equals(x)).ToList();
                HandleBankrupt( delegate { TransferMoneyToPlayers(otherPlayers); }, player, mortgageCtrl, gameModel);
            };

            // Go to nearest restroom - if it is occupied roll the dice again
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(4, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var nearestRestroom = FindNearestTile<RestroomTileController>(player);
                MovePlayer(player, nearestRestroom);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                mapView.UpdatePawnPositions();
                if (nearestRestroom.Model.Owner is not null)
                {
                    player.AdditionalRoll = true;
                }
            };

            // Go to nearest elevator
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(5, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var nearestElevator = FindNearestTile<ElevatorTileController>(player);
                MovePlayer(player, nearestElevator);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                mapView.UpdatePawnPositions();
            };

            // It is time for examination session - pay 20 ECTTS for each grade and 100 ECTS for each exemption
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(6, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                int amount = 0;
                foreach (TileModel tile in player.PurchasedTiles)
                {
                    if (tile is SubjectTileModel t)
                    {
                        amount += t.Grade == SubjectGrade.Exemption ? 100 : ((int)t.Grade - 1) * 20;
                    }
                }
                HandleBankrupt(delegate { player.Money -= amount; }, player, mortgageCtrl, gameModel);
            };

            // You are going to mandatory lecture
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(7, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var mandatoryLecture = GetModel<MandatoryLectureTileModel>();
                TeleportPlayer(player, mandatoryLecture);
                mandatoryLecture.AddPrisoner(player);
                mapView.UpdatePawnPositions();
            };

            // Run away imperceptibly from lecture, when lecturer do not see
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(8, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.NumberOfLeaveJailCards++;
            };

            // You stumbled and broke the leg - pay 40 ECTS for treatment
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(9, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 50; }, player, mortgageCtrl, gameModel);
            };

            // Go to Discrete mathematic - if you go through start, get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(10, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var discreteMathTile = GetModel<SubjectTileModel>(x => x.Id == 24);
                List<TileController> passedTiles = MovePlayer(player, discreteMathTile);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                ActivateCrossableTiles(player, passedTiles);
                mapView.UpdatePawnPositions();
            };

            // You passed Computer Architecture test - get 50 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(11, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 50;
            };

            // It turned out that you got 51 points from Algebra - get 150 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(12, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 150;
            };

            // You passed semester with commendation - go to start and get 200 ects
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(13, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var startTile = GetModel<StartTileModel>();
                MovePlayer(player, startTile);
                ActivateOnStandTile(player);
                mapView.UpdatePawnPositions();
            };

            // You cheated on exam - pay 20 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(14, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 20; }, player, mortgageCtrl, gameModel);
            };

            // You forgot your backpack - go back 3 tiles
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(15, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                MovePlayer(player, -3);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                mapView.UpdatePawnPositions();
            };

            // You forgot how to multiply matrices - go back to Algebra
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(16, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var algebraTile = GetModel<SubjectTileModel>(x => x.Id == 39);
                TeleportPlayer(player, algebraTile);
                HandleBankrupt(delegate { ActivateOnStandTile(player); }, player, mortgageCtrl, gameModel);
                mapView.UpdatePawnPositions();
            };

            // Go to sofas - if you through start, get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(17, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var sofas = GetModel<SofasTileModel>();
                List<TileController> passedTiles = MovePlayer(player, sofas);
                ActivateCrossableTiles(player, passedTiles);
                mapView.UpdatePawnPositions();
            };

            // You failed test - go to mandatory lecture (do not go through start)
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(18, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var mandatoryLecture = GetModel<MandatoryLectureTileModel>();
                TeleportPlayer(player, mandatoryLecture);
                mandatoryLecture.AddPrisoner(player);
                mapView.UpdatePawnPositions();
            };

            // You got cought on cheating - pay 100 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(19, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 100; }, player, mortgageCtrl, gameModel);
            };

            // Your project is a plagiarism - pay 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(20, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 200; }, player, mortgageCtrl, gameModel);
            };

            // Justification card
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(21, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.NumberOfLeaveJailCards++;
            };

            // You are dumb at Probability and Statistic - pay 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(22, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 200; }, player, mortgageCtrl, gameModel);
            };

            // Your fiend gave you answers from OOP - get 30 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(23, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 30;
            };

            // You have been detained by Lillennium employees and forced to open a bank account - pay 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(24, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 200; }, player, mortgageCtrl, gameModel);
            };

            // While writing a calculator in Assembly... - pay 50 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(25, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 50; }, player, mortgageCtrl, gameModel);
            };

            // Go back to western elevator - you cannot use it
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(26, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var elevatorButton = gameModel.GetModel<UseElevatorButtonModel>();
                elevatorButton.SetWasUsed();
                var westernElevator = GetModel<ElevatorTileModel>(x => x.Id < 20);
                TeleportPlayer(player, westernElevator);
                mapView.UpdatePawnPositions();
            };

            // Go back to eastern elevator - you cannot use it
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(27, ChanceCardType.Canteen, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var elevatorButton = gameModel.GetModel<UseElevatorButtonModel>();
                elevatorButton.SetWasUsed();
                var easternElevator = GetModel<ElevatorTileModel>(x => x.Id >= 20);
                TeleportPlayer(player, easternElevator);
                mapView.UpdatePawnPositions();
            };

            // You bought borscht in vending machine and now you have food poisoning - pay 30 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(1, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 30; }, player, mortgageCtrl, gameModel);
            };

            // You participated in SGGW open datys - get 100 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(2, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 100;
            };

            // You passed in second deadline - get 100 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(3, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 100;
            };

            // You helped your friend with task during test
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(4, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 30;
            };

            // As IT specialist you passed... - get 20 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(5, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 20;
            };

            // You made medical check-ups at an occupational medicine center... - pay 50 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(6, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 50; }, player, mortgageCtrl, gameModel);
            };

            // Hot-dog sale in Żabka - pay 50 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(7, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 50; }, player, mortgageCtrl, gameModel);
            };

            // You passed semester without any difficulty - get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(8, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var startTile = GetModel<StartTileModel>();
                TeleportPlayer(player, startTile);
                ActivateOnStandTile(player);
            };

            // You forgot your lunch so you eat in canteen - pay 50 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(9, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 50; }, player, mortgageCtrl, gameModel);
            };
            
            // You loose a tooth during P.E. - pay 100 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(10, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                HandleBankrupt(delegate { player.Money -= 100; }, player, mortgageCtrl, gameModel);
            };

            // You got maintenance grant - get 100 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(11, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 100;
            };

            // It is your birthday - get 10 ECTS from each student
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(12, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                foreach (PlayerModel p in GameSettings.ActivePlayers)
                {
                    if (p.PlayerStatus != PlayerStatus.Bankrupt && !p.Equals(player))
                    {
                        p.TransferMoneyTo(player, 10);
                    }
                }
            };

            // Go to sofas - if you go through start, get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(13, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                var sofasTile = GetModel<SofasTileModel>();
                List<TileController> passedTiles = MovePlayer(player, sofasTile);
                ActivateCrossableTiles(player, passedTiles);
                mapView.UpdatePawnPositions();
            };

            // During class you helped student council...
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(14, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.NumberOfLeaveJailCards++;
            };

            // For high academic performance you got scholarship - get 200 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(15, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 200;
            };

            // An error occured in the virtual dean's office - get 10 ECTS
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(16, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                player.Money += 10;
            };

            // You haven't studied regularly and... - pay 40 ECTS for every grade and 110 ECTS for every exemption
            ctrl = InitializeChild<ChanceCardModel, GUIChanceCard, ChanceCardController>(17, ChanceCardType.VendingMachine, chanceTiles);
            ctrl.Model.OnDrawn += (player) =>
            {
                int amount = 0;
                foreach (TileModel tile in player.PurchasedTiles)
                {
                    if (tile is SubjectTileModel t)
                    {
                        amount += t.Grade == SubjectGrade.Exemption ? 110 : ((int)t.Grade - 1) * 40;
                    }
                }
                HandleBankrupt(delegate { player.Money -= amount; }, player, mortgageCtrl, gameModel);
            };
        }

        /// <summary>
        /// Creates pawns for all players.
        /// </summary>
        /// <remarks>
        /// Adds pawns to the list of pawns and to the children of the map.
        /// </remarks>
        /// <param name="players">
        /// The list of players to create pawns for.
        /// </param>
        public void CreatePawns(List<PlayerModel> players)
        {
            foreach (PlayerModel player in players)
            {
                var model = new PawnModel(player.Color);
                var view = new GUIPawn(model);
                var controller = new PawnController(model, view);
                AddChildBefore<TileController>(controller);
            }
        }
    }
}
