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
        buttonContainer.Transform.Alignment = Alignment.Center;
        buttonContainer.Transform.RelativeOffset = new(0.0f, 1f);
        buttonContainer.Transform.RelativeSize = new(0.5f);
        buttonContainer.Transform.Ratio = new(1, 1);
        {
            UITextButton newGameButton = new(buttonContainer);
            newGameButton.Background = new UIImage(newGameButton, "Images/Button", useCache: true);
            newGameButton.Text = new UIText(newGameButton, "Nowa gra", Color.Black);
            newGameButton.Transform.Alignment = Alignment.Top;
            newGameButton.OnClicked += (s, e) =>
            {
                Debug.WriteLine("Starting new game...");
            };

            //UIFrame frame = new(buttonContainer, 5, Color.Black);

            UITextButton quitButton = new(buttonContainer);
            quitButton.Background = new UIImage(quitButton, "Images/Button", useCache: true);
            quitButton.Text = new UIText(quitButton, "Wyjście z gry", Color.Black);
            quitButton.Transform.RelativeOffset = new(0.0f, 0.3f);
            quitButton.OnClicked += (s, e) => WZIMopoly.Instance.Exit();
        }
    }
}