using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Engine;
using WZIMopoly.GUI.LobbyScene.PlayersList;
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
                // TODO: Add caps lock support
                bool shift = KeyboardController.IsPressed(Keys.LeftShift)
                    || KeyboardController.IsPressed(Keys.RightShift);

                var clickedKeys = KeyboardController.GetAllClickedKeys();
                if (clickedKeys.Count == 0)
                {
                    return;
                }

                // TODO: Add support to other languages
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
                    case Keys.A:
                        View.NickText.AddChar(shift ? 'A' : 'a');
                        break;
                    case Keys.B:
                        View.NickText.AddChar(shift ? 'B' : 'b');
                        break;
                    case Keys.C:
                        View.NickText.AddChar(shift ? 'C' : 'c');
                        break;
                    case Keys.D:
                        View.NickText.AddChar(shift ? 'D' : 'd');
                        break;
                    case Keys.E:
                        View.NickText.AddChar(shift ? 'E' : 'e');
                        break;
                    case Keys.F:
                        View.NickText.AddChar(shift ? 'F' : 'f');
                        break;
                    case Keys.G:
                        View.NickText.AddChar(shift ? 'G' : 'g');
                        break;
                    case Keys.H:
                        View.NickText.AddChar(shift ? 'H' : 'h');
                        break;
                    case Keys.I:
                        View.NickText.AddChar(shift ? 'I' : 'i');
                        break;
                    case Keys.J:
                        View.NickText.AddChar(shift ? 'J' : 'j');
                        break;
                    case Keys.K:
                        View.NickText.AddChar(shift ? 'K' : 'k');
                        break;
                    case Keys.L:
                        View.NickText.AddChar(shift ? 'L' : 'l');
                        break;
                    case Keys.M:
                        View.NickText.AddChar(shift ? 'M' : 'm');
                        break;
                    case Keys.N:
                        View.NickText.AddChar(shift ? 'N' : 'n');
                        break;
                    case Keys.O:
                        View.NickText.AddChar(shift ? 'O' : 'o');
                        break;
                    case Keys.P:
                        View.NickText.AddChar(shift ? 'P' : 'p');
                        break;
                    case Keys.Q:
                        View.NickText.AddChar(shift ? 'Q' : 'q');
                        break;
                    case Keys.R:
                        View.NickText.AddChar(shift ? 'R' : 'r');
                        break;
                    case Keys.S:
                        View.NickText.AddChar(shift ? 'S' : 's');
                        break;
                    case Keys.T:
                        View.NickText.AddChar(shift ? 'T' : 't');
                        break;
                    case Keys.U:
                        View.NickText.AddChar(shift ? 'U' : 'u');
                        break;
                    case Keys.V:
                        View.NickText.AddChar(shift ? 'V' : 'v');
                        break;
                    case Keys.W:
                        View.NickText.AddChar(shift ? 'W' : 'w');
                        break;
                    case Keys.X:
                        View.NickText.AddChar(shift ? 'X' : 'x');
                        break;
                    case Keys.Y:
                        View.NickText.AddChar(shift ? 'Y' : 'y');
                        break;
                    case Keys.Z:
                        View.NickText.AddChar(shift ? 'Z' : 'z');
                        break;
                    case Keys.D0:
                        View.NickText.AddChar('0');
                        break;
                    case Keys.D1:
                        View.NickText.AddChar('1');
                        break;
                    case Keys.D2:
                        View.NickText.AddChar('2');
                        break;
                    case Keys.D3:
                        View.NickText.AddChar('3');
                        break;
                    case Keys.D4:
                        View.NickText.AddChar('4');
                        break;
                    case Keys.D5:
                        View.NickText.AddChar('5');
                        break;
                    case Keys.D6:
                        View.NickText.AddChar('6');
                        break;
                    case Keys.D7:
                        View.NickText.AddChar('7');
                        break;
                    case Keys.D8:
                        View.NickText.AddChar('8');
                        break;
                    case Keys.D9:
                        View.NickText.AddChar('9');
                        break;
                }
            }
        }
    }
}
