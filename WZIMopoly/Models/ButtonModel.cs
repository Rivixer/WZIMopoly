using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.GUI;

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
        internal bool IsActive = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonModel"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the button.<br/>
        /// Used to identify the button and load the texture.
        /// </param>
        internal ButtonModel(string name)
        {
            Name = name;  
        }
    }
}
