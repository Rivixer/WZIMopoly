using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller of the map.
    /// </summary>
    internal class MapController : Controller<MapModel, GUIMap>
    {
        /// <summary>
        /// Initilizes a new instance of the <see cref="MapController"/> class.
        /// </summary>
        /// <remarks>
        /// Loads tiles from a xml file calling <see cref="LoadTiles()"/> method.
        /// </remarks>
        internal MapController(MapModel model, GUIMap view)
            : base(model, view) { }

        /// <summary>
        /// Creates pawns for all players.
        /// </summary>
        /// <remarks>
        /// Adds pawns to the list of pawns and to the children of the map.
        /// </remarks>
        /// <param name="players">
        /// The list of players to create pawns for.
        /// </param>
        internal void CreatePawns(List<PlayerModel> players)
        {
            foreach (PlayerModel player in players)
            {
                InitializeChild<PawnModel, GUIPawn, PawnController>(player.Color);
            }
        }
    }
}
