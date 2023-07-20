using Microsoft.Xna.Framework;

#nullable disable

namespace WZIMopoly.UI.Scenes;

internal class MenuScene : Scene
{
    public override void Create()
    {
        UIBaseComponent menuScene = CreateBaseComponent();

        UIImage background = new(menuScene, "Images/MenuScreen");

        UIContainer buttonContainer = new(menuScene);
        buttonContainer.SetBackground(new Color(100, 100, 100, 100));
        buttonContainer.Transform.Alignment = Alignment.Bottom;
        buttonContainer.Transform.RelativeSize = new(0.6f);
        buttonContainer.Transform.Ratio = new(1, 1);
        buttonContainer.Transform.RelativePadding = new(0.01f);
        {
            UITextureButton newGameButton = new(buttonContainer, "Images/NewGame");
            newGameButton.Transform.Alignment = Alignment.Top;
            newGameButton.OnClicked += (s, e) =>
            {
                Debug.WriteLine("Starting new game...");
            };

            UITextButton quitButton = new(buttonContainer, new Color(200, 200, 200, 100), UIText.ToLazy("Wyjście z gry", Color.Black));
            quitButton.Transform.Ratio = newGameButton.Transform.Ratio;
            quitButton.Transform.RelativeOffset = new(0.0f, 0.21f);
            quitButton.Transform.RelativeMargin = new(0.03f);
            quitButton.OnClicked += (s, e) =>
            {
                WZIMopoly.Instance.Exit();
            };
        }
    }
}