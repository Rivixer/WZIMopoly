using System;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a button model.
    /// </summary>
    internal class ButtonModel : Model
    {
        /// <summary>
        /// The name of the button.
        /// </summary>
        /// <remarks>
        /// It is used to identify the button and load the texture.
        /// </remarks>
        internal readonly string Name;

        /// <summary>
        /// Whether the button is active.
        /// </summary>
        internal bool IsActive = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonModel"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the button.<br/>
        /// Used to identify the button and load the texture.
        /// </param>
        public ButtonModel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the button meets requirements to be clicked.
        /// </summary>
        /// <remarks>
        /// Function returns true by default.
        /// </remarks>
        public Func<bool> Conditions { get; set; } = () => true;

        /// <summary>
        /// Gets or sets whether the button has been clicked in this frame.
        /// </summary>
        public bool WasClickedInThisFrame { get; set; }

        /// <inheritdoc/>
        public void Activate()
        {
            IsActive = true;
        }

        /// <inheritdoc/>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <inheritdoc/>
        public override void BeforeUpdate()
        {
            WasClickedInThisFrame = false;
        }
    }
}
