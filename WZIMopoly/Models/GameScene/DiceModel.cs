﻿using System;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the dice model.
    /// </summary>
    internal sealed class DiceModel : Model
    {
        /// <value>
        /// The result of the last dice roll.
        /// </valuey>
        public Tuple<int, int> LastRoll { get; private set; }

        /// <summary>
        /// Pseudo-random numbers generator.
        /// </summary>
        private readonly static Random s_random = new();

        /// <summary>
        /// Gets the sum of the last dice roll.
        /// </summary>
        public ushort Sum => (ushort)(LastRoll.Item1 + LastRoll.Item2);

        /// <summary>
        /// Simulates the roll of two dice.
        /// </summary>
        /// <remarks>
        /// Saves the result in <see cref="LastRoll"/>.
        /// </remarks>
        /// <returns>
        /// The tuple with two random numbers in the range of &lt;1,6&gt;.
        /// </returns>
        public Tuple<int, int> RollDice()
        {
            LastRoll = new Tuple<int, int>(s_random.Next(1, 7), s_random.Next(1, 7));
            return LastRoll;
        }
        /// <summary>
        /// Resets data about the last roll.
        /// </summary>
        public void Reset()
        {
            LastRoll = null;
        }
    }
}
