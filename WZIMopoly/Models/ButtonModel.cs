using Microsoft.Xna.Framework;

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
        /// The default destination rectangle of the button.
        /// </summary>
        /// <remarks>
        /// It specifies the position and size of the button.<br/>
        /// The X and Y coordinates refer to the top-left corner of the button.
        /// </remarks>
        internal readonly Rectangle DefDstRect;

        /// <summary>
        /// Whether the button is active.
        /// </summary>
        internal bool IsActive = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonModel"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the button.<br/>
        /// Used to identify the button and load the texture.
        /// </param>
        /// <param name="defDstRect">
        /// The default destination rectangle of the button.<br/>
        /// It specifies the position and size of the button.<br/>
        /// </param>
        internal ButtonModel(string name, Rectangle defDstRect)
        {
            Name = name;
            DefDstRect = defDstRect;
        }
    }
}
