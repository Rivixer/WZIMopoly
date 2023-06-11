using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the first bankruptcy button controller.
    /// </summary>
    internal class FirstBankruptcyButtonController : ButtonController<FirstBankruptcyButtonModel, GUIFirstBankruptcyButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstBankruptcyButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the first bankruptcy button.
        /// </param>
        /// <param name="view">
        /// The view of the first bankruptcy button.
        /// </param>
        public FirstBankruptcyButtonController(FirstBankruptcyButtonModel model, GUIFirstBankruptcyButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                GameSettings.GameEndType = GameEndType.FirstBankruptcy;
                GameSettings.SendLobbyData();
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
                && GameSettings.GameEndType == GameEndType.LastNotBankrupt)
            {
                OnClick();
            }
        }
    }
}
