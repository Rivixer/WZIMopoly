using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the first bankruptcy button controller.
    /// </summary>
    internal class FirstButtonController : ButtonController<FirstButtonModel, GUIFirstButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the first bankruptcy button.
        /// </param>
        /// <param name="view">
        /// The view of the first bankruptcy button.
        /// </param>
        public FirstButtonController(FirstButtonModel model, GUIFirstButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                GameSettings.gameEndType = GameEndType.FirstBankruptcy;
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
                && GameSettings.gameEndType == GameEndType.LastNotBankrupt)
            {
                OnClick();
            }
        }
    }
}
