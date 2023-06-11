using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene.PlayersList;
using WZIMopoly.Models;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.Controllers.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the controller of the lobby player.
    /// </summary>
    internal class LobbyPlayerController : Controller<LobbyPlayerModel, GUILobbyPlayer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyPlayerController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the lobby player.
        /// </param>
        /// <param name="view">
        /// The view of the lobby player.
        /// </param>
        public LobbyPlayerController(LobbyPlayerModel model, GUILobbyPlayer view)
            : base(model, view)
        {
            var position = GUILobbyPlayer.Position[model.Player.Color];

            var addPlayerModel = new AddPlayerButtonModel(model.Player);
            var addPlayerRect = new Rectangle(position.X, position.Y, 53, 53);
            var addPlayerView = new GUIAddPlayerButton(addPlayerModel, addPlayerRect);
            var addPlayerController = new AddPlayerButtonController(addPlayerModel, addPlayerView);
            Model.AddChild(addPlayerController);

            var removePlayerModel = new RemovePlayerButtonModel(model.Player);
            var removePlayerRect = new Rectangle(position.X + 170, position.Y, 40, 40);
            var removePlayerView = new GUIRemovePlayerButton(removePlayerModel, removePlayerRect);
            var removePlayerController = new RemovePlayerButtonController(removePlayerModel, removePlayerView);
            Model.AddChild(removePlayerController);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            if (Model.Player.PlayerType != PlayerType.Local)
            {
                return;
            }
            
            if (MouseController.WasLeftBtnClicked() && View.IsHovered && !View.NickText.IsSelected)
            {
                View.NickText.IsSelected = true;
                View.NickText.MoveCursorToEnd();
            }
            else if (MouseController.WasLeftBtnClicked() && !View.IsHovered && View.NickText.IsSelected || KeyboardController.WasClicked(Keys.Enter))
            {
                View.NickText.IsSelected = false;
                Model.Player.Nick = View.NickText.Text;
            }

            // Some day this mess will be refactored...
            if (View.NickText.IsSelected)
            {
                var clickedKeys = KeyboardController.GetAllClickedKeys();
                if (clickedKeys.Count == 0)
                {
                    return;
                }

                switch (clickedKeys[0])
                {
                    case Keys.Left:
                        View.NickText.MoveCursorLeft();
                        break;
                    case Keys.Right:
                        View.NickText.MoveCursorRight();
                        break;
                    case Keys.Home:
                        View.NickText.MoveCursorToHome();
                        break;
                    case Keys.End:
                        View.NickText.MoveCursorToEnd();
                        break;
                    case Keys.Back:
                        View.NickText.RemovePreviousChar();
                        break;
                    case Keys.Delete:
                        View.NickText.RemoveNextChar();
                        break;
                    default:
                        char? c = KeyboardController.GetClickedKey();
                        if (c != null)
                        {
                            View.NickText.AddChar((char)c);
                        }
                        break;
                }
            }
        }
    }
}
