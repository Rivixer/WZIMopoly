﻿using Microsoft.Xna.Framework.Input;
using WZIMopoly.Engine;
using WZIMopoly.GUI.JoinScene;
using WZIMopoly.Models.JoinScene;

namespace WZIMopoly.Controllers.JoinScene
{
    /// <summary>
    /// Represents a controller for the lobby code.
    /// </summary>
    internal class LobbyCodeController : Controller<LobbyCodeModel, GUILobbyCode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyCodeController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the lobby code.
        /// </param>
        /// <param name="view">
        /// The view of the lobby code.
        /// </param>
        public LobbyCodeController(LobbyCodeModel model, GUILobbyCode view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            if (MouseController.WasLeftBtnClicked() && View.IsHovered && !View.IsSelected)
            {
                View.IsSelected = true;
                View.MoveCursorToEnd();
            }
            else if (MouseController.WasLeftBtnClicked() && !View.IsHovered && View.IsSelected || KeyboardController.WasClicked(Keys.Enter))
            {
                View.IsSelected = false;
                Model.LobbyCode = View.Text;
            }

            if (View.IsSelected)
            {
                var clickedKeys = KeyboardController.GetAllClickedKeys();
                if (clickedKeys.Count == 0)
                {
                    return;
                }

                switch (clickedKeys[0])
                {
                    case Keys.Left:
                        View.MoveCursorLeft();
                        break;
                    case Keys.Right:
                        View.MoveCursorRight();
                        break;
                    case Keys.Home:
                        View.MoveCursorToHome();
                        break;
                    case Keys.End:
                        View.MoveCursorToEnd();
                        break;
                    case Keys.Back:
                        View.RemovePreviousChar();
                        break;
                    case Keys.Delete:
                        View.RemoveNextChar();
                        break;
                    default:
                        char? c = KeyboardController.GetClickedKey();
                        if (c != null)
                        {
                            View.AddChar((char)c);
                        }
                        break;
                }
            }
        }
    }
}
