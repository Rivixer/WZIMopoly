﻿using System;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the dice model.
    /// </summary>
    internal sealed class DiceModel : Model
    {
        /// <summary>
        /// The result of the last dice roll.
        /// </summary>
        public Tuple<int, int> LastRoll;

        /// <summary>
        /// Pseudo-random numbers generator.
        /// </summary>
        private readonly static Random _random = new();

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
            LastRoll = new Tuple<int, int>(_random.Next(1, 7), _random.Next(1, 7));
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
