using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the online mode button controller.
    /// </summary>
    internal class OnlineModeButtonController : ButtonController<OnlineModeButtonModel, GUIOnlineModeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineModeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the online mode button.
        /// </param>
        /// <param name="view">
        /// The view of the online mode button.
        /// </param>
        public OnlineModeButtonController(OnlineModeButtonModel model, GUIOnlineModeButton view)
            : base(model, view)
        {
            OnButtonClicked += () => WZIMopoly.GameType = GameType.Online;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            // We have to override this, because we want to make
            // this button clickable even if it is inactive.

            Model.Update();

            if (Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && View.IsHovered
                && !Model.IsActive
                && GameSettings.Client.PlayerType == PlayerType.Local)
            {
                OnClick();
            }
        }
    }
}
