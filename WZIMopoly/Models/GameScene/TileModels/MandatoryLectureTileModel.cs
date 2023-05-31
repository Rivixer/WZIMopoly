using System.Collections.Generic;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Mandatory Lecture tile model.
    /// </summary>
    internal class MandatoryLectureTileModel : TileModel
    {
        /// <summary>
        /// The list of prisoners and the number of turns they have spent in jail.
        /// </summary>
        private readonly Dictionary<PlayerModel, int> _prisoners;

        /// <summary>
        /// The amount of money that the player has to pay to leave the jail.
        /// </summary>
        private readonly int _payForLeave;

        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryLectureTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal MandatoryLectureTileModel(int id, int payForLeave) : base(id)
        {
            _prisoners = new Dictionary<PlayerModel, int>();
            _payForLeave = payForLeave;

            OnStand += AddPrisoner;
        }

        /// <summary>
        /// Gets the amount of money that the player has to pay to leave the jail.
        /// </summary>
        public int PayForLeave => _payForLeave;

        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryLectureTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="MandatoryLectureTileModel"/> instance.
        /// </returns>
        public static MandatoryLectureTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            int payForLeave = int.Parse(node["pay_for_leave"].InnerText);
            var tile = new MandatoryLectureTileModel(id, payForLeave);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <summary>
        /// Checks if the player is a prisoner.
        /// </summary>
        /// <param name="model">
        /// The player to check.
        /// </param>
        /// <returns>
        /// True if the player is a prisoner, false otherwise.
        /// </returns>
        public bool IsPrisoner(PlayerModel model)
        {
            return _prisoners.ContainsKey(model);
        }

        /// <summary>
        /// Increased the number of turns that the player has spent in jail.
        /// </summary>
        /// <param name="player">
        /// The player to increase the number of turns for.
        /// </param>
        public void AddPrisonerTurn(PlayerModel player)
        {
            _prisoners[player]++;
        }

        /// <summary>
        /// Checks if the player can be released from jail.
        /// </summary>
        /// <param name="player">
        /// The player to check if they can be released from jail
        /// due to the number of turns.
        /// </param>
        /// <returns></returns>
        public bool CanPrisonerRelease(PlayerModel player)
        {
            return _prisoners[player] > 3;
        }

        /// <summary>
        /// Checks if the player can pay for their release.
        /// </summary>
        /// <param name="player">
        /// The player to check if they can pay for their release.
        /// </param>
        /// <returns>
        /// True if the player can pay for their release, false otherwise.
        /// </returns>
        public bool CanPrisonerPayForRelease(PlayerModel player)
        {
            return player.Money >= _payForLeave;
        }

        /// <summary>
        /// Adds the player to the list of prisoners.
        /// </summary>
        /// <param name="player">
        /// The player to add to the list of prisoners.
        /// </param>
        private void AddPrisoner(PlayerModel player)
        {
            _prisoners.Add(player, 0);
        }

        /// <summary>
        /// Released the player from jail.
        /// </summary>
        /// <param name="player">
        /// The player to release from jail.
        /// </param>
        public void ReleasePrisoner(PlayerModel player)
        {
            _prisoners.Remove(player);
        }

        /// <summary>
        /// Returns the number of turns the player
        /// must spend in jail before being released.
        /// </summary>
        /// <param name="player">
        /// The player to get the number of remaining turns for.
        /// </param>
        /// <returns>
        /// The number of remaining turns the player
        /// must spend in jail before being released.
        /// </returns>
        public int GetRemainingTurns(PlayerModel player)
        {
            return 4 - _prisoners[player];
        }
    }
}
