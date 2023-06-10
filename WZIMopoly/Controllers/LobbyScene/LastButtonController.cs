using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the last not bankrupt button controller.
    /// </summary>
    internal class LastButtonController : ButtonController<LastButtonModel, GUILastButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the last not bankrupt button button.
        /// </param>
        /// <param name="view">
        /// The view of the last not bankrupt button button.
        /// </param>
        public LastButtonController(LastButtonModel model, GUILastButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                GameSettings.gameEndType = GameEndType.LastNotBankrupt;
            };
        }

        /// <inheritdoc/>
        public override void Update()
        {
            Model.Update();

            if (Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && View.IsHovered
                && !Model.IsActive
                && GameSettings.gameEndType == GameEndType.FirstBankruptcy)
            {
                OnClick();
            }
        }
    }
}
