using Microsoft.Xna.Framework;
using WZIMopoly.Controllers.SettingsScene;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.GUI.SettingsScene;
using WZIMopoly.Models;
using WZIMopoly.Models.SettingsScene;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents the menu scene.
    /// </summary>
    internal class SettingsScene : Scene<SettingsModel, SettingsView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsScene"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings.
        /// </param>
        /// <param name="view">
        /// The view of the settings.
        /// </param>
        public SettingsScene(SettingsModel model, SettingsView view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Initialize()
        {
            var model1920 = new ResolutionButtonModel("Settings1920", 1920, 1080);
            var view1920 = new GUIResolutionButton(model1920, new Rectangle(690, 567, 249, 73), GUIStartPoint.Center);
            var controller1920 = new ResolutionButtonController(model1920, view1920);
            controller1920.OnButtonClicked += () =>
            {
                ScreenController.ChangeResolution(1920, 1080, ScreenController.IsFullScreen);
                ScreenController.ApplyChanges();
                RecalculateAll();
            };
            Model.AddChild(controller1920);

            var model1600 = new ResolutionButtonModel("Settings1600", 1600, 900);
            var view1600 = new GUIResolutionButton(model1600, new Rectangle(961, 567, 249, 73), GUIStartPoint.Center);
            var controller1600 = new ResolutionButtonController(model1600, view1600);
            controller1600.OnButtonClicked += () =>
            {
                ScreenController.ChangeResolution(1600, 900, ScreenController.IsFullScreen);
                ScreenController.ApplyChanges();
                RecalculateAll();
            };
            Model.AddChild(controller1600);

            var model1366 = new ResolutionButtonModel("Settings1366", 1366, 768);
            var view1366 = new GUIResolutionButton(model1366, new Rectangle(1231, 567, 249, 73), GUIStartPoint.Center);
            var controller1366 = new ResolutionButtonController(model1366, view1366);
            controller1366.OnButtonClicked += () =>
            {
                ScreenController.ChangeResolution(1366, 768, ScreenController.IsFullScreen);
                ScreenController.ApplyChanges();
                RecalculateAll();
            };
            Model.AddChild(controller1366);

            var modelWindowed = new ScreenModeButtonModel("SettingsWindowed", false);
            var viewWindowed = new GUIScreenModeButton(modelWindowed, new Rectangle(817, 412, 249, 73), GUIStartPoint.Center);
            var controllerWindowed = new ScreenModeButtonController(modelWindowed, viewWindowed);
            controllerWindowed.OnButtonClicked += () =>
            {
                ScreenController.ChangeResolution(ScreenController.Width, ScreenController.Height, false);
                ScreenController.ApplyChanges();
                RecalculateAll();
            };
            Model.AddChild(controllerWindowed);

            var modelFullscreen = new ScreenModeButtonModel("SettingsFullscreen", true);
            var viewFullscreen = new GUIScreenModeButton(modelFullscreen, new Rectangle(1103, 412, 249, 73), GUIStartPoint.Center);
            var controllerFullscreen = new ScreenModeButtonController(modelFullscreen, viewFullscreen);
            controllerFullscreen.OnButtonClicked += () =>
            {
                ScreenController.ChangeResolution(ScreenController.Width, ScreenController.Height, true);
                ScreenController.ApplyChanges();
                RecalculateAll();
            };
            Model.AddChild(controllerFullscreen);

            var modelVolumeSlider = new VolumeSliderModel("Mortgage", 200, 200);
            var viewVolumeSlider = new GUIVolumeSlider(modelVolumeSlider, new Rectangle(1000, 170, 100, 100), GUIStartPoint.TopLeft);
            var controllerVolumeSlider = new VolumeSliderController(modelVolumeSlider, viewVolumeSlider);
            controllerVolumeSlider.OnButtonClicked += () =>
            {
                //Rectangle rectangle = viewVolumeSlider.rectangleSlider1.ToCurrentResolution();
                if (MouseController.IsHover(new Rectangle(1000, 100, 300, 300).ToCurrentResolution()))//rectangle))
                {
                    viewVolumeSlider.SetNewArea(MouseController.Position.X);
                }
            };
            Model.AddChild(controllerVolumeSlider);

            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// Checks if the language button was clicked.
        /// </para>
        /// <para>
        /// It is not possible to use the <see cref="GUIButton"/> class here, 
        /// because the language button has no texture.
        /// </para>
        /// </remarks>
        public override void Update()
        {
            base.Update();
            if (MouseController.WasLeftBtnClicked())
            {
                if (MouseController.IsHover(new Rectangle(986, 832, 75, 75).ToCurrentResolution()))
                {
                    WZIMopoly.Language = Language.Polish;
                }
                else if (MouseController.IsHover(new Rectangle(1086, 832, 75, 75).ToCurrentResolution()))
                {
                    WZIMopoly.Language = Language.English;
                }
                //else if (MouseController.IsHover(new Rectangle(100, 100, 300, 300).ToCurrentResolution()))
                //{
                    //Model.GetAllControllers<VolumeSliderController>()?[0];
                    //Model.GetAllViews<GUIVolumeSlider>()?[0].SetNewArea(500);
                //}
            }
            //if (MouseController.IsLeftBtnPressed() && MouseController.IsHover(new Rectangle(100, 100, 300, 300).ToCurrentResolution()))
            //{
                //Model.GetAllViews<GUIVolumeSlider>()?[0].SetNewArea(500);
            //}
        }
    }
}
