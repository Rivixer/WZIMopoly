﻿namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public class PlayerModel : Model
    {
        /// <summary>
        /// The color of the player.
        /// </summary>
        public readonly string Color;

        /// <summary>
        /// The amount of money player has.
        /// </summary>
        internal int Money { get; set; } = 1500;

        /// <summary>
        /// The nick of the player.
        /// </summary>
        private string _nick;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class.
        /// </summary>
        /// <param name="nick">
        /// The nick of the player.
        /// </param>
        /// <param name="color">
        /// The color of the player.
        /// </param>
        public PlayerModel(string nick, string color)
        {
            _nick = nick;
            Color = color;
        }

        /// <summary>
        /// Gets or sets the nick of the player.
        /// </summary>
        public string Nick
        {
            get => _nick;
            set => _nick = value;
        }

    }
}
