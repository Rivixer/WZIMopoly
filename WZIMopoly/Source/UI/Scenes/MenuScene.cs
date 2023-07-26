using Microsoft.Xna.Framework;

#nullable disable

namespace WZIMopoly.UI.Scenes;

internal class MenuScene : Scene
{
    public override void Create()
    {
        UIImage background = new("Images/MenuScreen");
        AddComponent(background);

        UITextButton newGameButton = new()
        {
            Parent = background,
            TransformType = TransformType.Relative,
            Background = new UIImage("Images/Button"),
            Text = new UIText("Nowa gra", Color.Black)
        };

        /*UIFrame buttonContainer = new(menuScene, 10, Color.Black);
        buttonContainer.Transform.Alignment = Alignment.Center;
        buttonContainer.Transform.RelativeOffset = new(0.0f, 0.1f);
        buttonContainer.Transform.RelativeSize = new(0.45f);
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

            UITextButton quitButton = new(buttonContainer);
            quitButton.Background = new UIImage(quitButton, "Images/Button", useCache: true);
            quitButton.Text = new UIText(quitButton, "Wyjście z gry", Color.Black);
            quitButton.Transform.RelativeOffset = new(0.0f, 0.25f);
            quitButton.OnClicked += (s, e) => WZIMopoly.Instance.Exit();
        }*/
    }
}