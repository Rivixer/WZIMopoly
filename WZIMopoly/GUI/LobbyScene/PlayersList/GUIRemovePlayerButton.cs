using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.GUI.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the remove player button view.
    /// </summary>
    internal class GUIRemovePlayerButton : GUIButton<RemovePlayerButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIRemovePlayerButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the remove player button.
        /// </param>
        /// <param name="defDstRect">
        /// The rectangle of button where X and Y are specified
        /// for center of the button and for 1920x1080 resolution.
        /// </param>
        public GUIRemovePlayerButton(RemovePlayerButtonModel model, Rectangle defDstRect)
            : base(model, defDstRect, GUIStartPoint.Center, hoverTexture: false, disableTexture: false) { }
    }
}
