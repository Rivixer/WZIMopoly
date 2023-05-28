using System;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the dice model.
    /// </summary>
    internal sealed class DiceModel : Model
    {
        /// <summary>
        /// The pseudo-random numbers generator.
        /// </summary>
        private readonly static Random s_random = new();

        /// <summary>
        /// The result of the last dice roll.
        /// </summary>
        public Tuple<int, int> LastRoll { get; private set; }

        /// <summary>
        /// Count of the previous doubles.
        /// </summary>
        public int DoubleCounter { get; private set; }
        /// <summary>
        /// Gets the sum of the last dice roll.
        /// </summary>
        public ushort Sum => (ushort)(LastRoll.Item1 + LastRoll.Item2);

        /// <summary>
        /// Gets a value indicating whether the last roll was a double.
        /// </summary>
        public bool LastRollWasDouble => LastRoll != null && LastRoll.Item1 == LastRoll.Item2;

        /// <summary>
        /// Simulates the roll of two dice.
        /// </summary>
        /// <returns>
        /// The tuple with two random numbers in the range of &lt;1,6&gt;.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Saves the result in <see cref="LastRoll"/>.
        /// </para>
        /// <para>
        /// Increments <see cref="DoubleCounter"/> if a double is rolled.
        /// </para>
        /// </remarks>
        public Tuple<int, int> RollDice()
        {
            LastRoll = new Tuple<int, int>(s_random.Next(1, 7), s_random.Next(1, 7));
            if (LastRollWasDouble)
            {
                DoubleCounter++;
            }
            return LastRoll;
        }

        /// <summary>
        /// Resets data about the last roll.
        /// </summary>
        /// <remarks>
        /// It doesn't reset the double counter.
        /// </remarks>
        public void Reset()
        {
            LastRoll = null;
        }

        /// <summary>
        /// Resets the double counter.
        /// </summary>
        public void ResetDoubleCounter()
        {
            DoubleCounter = 0;
        }
    }
}
