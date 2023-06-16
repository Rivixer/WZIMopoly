using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.GUI.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the add player button view.
    /// </summary>
    internal class GUIAddPlayerButton : GUIButton<AddPlayerButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIAddPlayerButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the add player button.
        /// </param>
        /// <param name="defDstRect">
        /// The rectangle of button where X and Y are specified
        /// for center of the button and for 1920x1080 resolution.
        /// </param>
        public GUIAddPlayerButton(AddPlayerButtonModel model, Rectangle defDstRect)
            : base(model, defDstRect, GUIStartPoint.Center, hoverTexture: false, disableTexture: false) { }
    }
}
