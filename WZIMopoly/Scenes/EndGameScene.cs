using WZIMopoly.Controllers.EndGameScene;
using WZIMopoly.GUI;
using WZIMopoly.GUI.EndGameScene;
using WZIMopoly.Models;
using WZIMopoly.Models.EndGameScene;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents the end game scene.
    /// </summary>
    internal class EndGameScene : Scene<EndGameModel, EndGameView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndGameScene"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the end game.
        /// </param>
        /// <param name="view">
        /// The view of the end game.
        /// </param>
        public EndGameScene(EndGameModel model, EndGameView view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Initialize()
        {
            Model.InitializeChild<ReturnToMenuButtonModel, GUIReturnToMenuButton, ReturnToMenuButtonController>();
        }
    }
}
