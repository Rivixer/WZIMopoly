using System;
using WZIMopoly.Models;

namespace WZIMopoly.Exceptions
{
    /// <summary>
    /// Rperesents the exception thrown when
    /// the player does not have enough money.
    /// </summary>
    internal class NotEnoughMoney : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughMoney"/> class.
        /// </summary>
        /// <param name="player">
        /// The player that does not have enough money.
        /// </param>
        /// <param name="amount">
        /// The amount of money that the player
        /// is short of for payment, in the negative.
        /// </param>
        public NotEnoughMoney(PlayerModel player, int amount)
        {
            Player = player;
            Amount = amount;
        }

        /// <summary>
        /// Gets the player model.
        /// </summary>
        /// <value>
        /// The player that does not have enough money.
        /// </value>
        public PlayerModel Player { get; private set; }

        /// <summary>
        /// Gets the amount of money that the player does not have.
        /// </summary>
        /// <value>
        /// The amount of money that the player
        /// is short of for payment, in the negative.
        /// </value>
        public int Amount { get; private set; }
    }
}
