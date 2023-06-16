using WZIMopoly.Controllers.MenuScene;
using WZIMopoly.GUI;
using WZIMopoly.GUI.MenuScene;
using WZIMopoly.Models;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents the menu scene.
    /// </summary>
    internal class MenuScene : Scene<MenuModel, MenuView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuScene"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the menu.
        /// </param>
        /// <param name="view">
        /// The view of the menu.
        /// </param>
        public MenuScene(MenuModel model, MenuView view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Initialize()
        {
            Model.InitializeChild<NewGameButtonModel, GUINewGameButton, NewGameButtonController>();
            Model.InitializeChild<JoinGameButtonModel, GUIJoinGameButton, JoinGameButtonController>();
            Model.InitializeChild<MenuSettingsButtonModel, GUIMenuSettingsButton, MenuSettingsButtonController>();
            Model.InitializeChild<QuitButtonModel, GUIQuitButton, QuitButtonController>();
        }
    }
}
