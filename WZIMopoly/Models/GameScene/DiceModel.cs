using System;

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
        internal Tuple<int, int> LastRoll;

        /// <summary>
        /// Pseudo-random numbers generator.
        /// </summary>
        private readonly static Random _random = new();

        /// <summary>
        /// Gets the sum of the last dice roll.
        /// </summary>
        internal int Sum => LastRoll.Item1 + LastRoll.Item2;

        /// <summary>
        /// Simulates the roll of two dice.
        /// </summary>
        /// <remarks>
        /// Saves the result in <see cref="LastRoll"/>.
        /// </remarks>
        /// <returns>
        /// The tuple with two random numbers in the range of &lt;1,6&gt;.
        /// </returns>
        internal Tuple<int, int> RollDice()
        {
            LastRoll = new Tuple<int, int>(_random.Next(1, 7), _random.Next(1, 7));
            return LastRoll;
        }
        /// <summary>
        /// Resets data about the last roll.
        /// </summary>
        internal void Reset()
        {
            LastRoll = null;
        }
    }
}
