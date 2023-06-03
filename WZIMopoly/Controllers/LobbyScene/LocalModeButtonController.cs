using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the local mode button controller.
    /// </summary>
    internal class LocalModeButtonController : ButtonController<LocalModeButtonModel, GUILocalModeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalModeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the local mode button.
        /// </param>
        /// <param name="view">
        /// The view of the local mode button.
        /// </param>
        public LocalModeButtonController(LocalModeButtonModel model, GUILocalModeButton view)
            : base(model, view)
        {
            OnButtonClicked += () => WZIMopoly.GameType = GameType.Local;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            // We have to override this, because we want to make
            // this button clickable even if it is inactive.

            if (Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && View.IsHovered)
            {
                OnClick();
            }
        }
    }
}
