﻿using Microsoft.Xna.Framework;

namespace WZIMopoly.UI.Scenes;

#pragma warning disable CA2000 // Dispose objects before losing scope

internal class MenuScene : Scene
{
    public override void Create()
    {
        UIImage background = new("Images/MenuScreen");
        AddComponent(background);

        UIContainer buttonContainer = new()
        {
            Parent = background,
            Alignment = Alignment.Center,
            RelativeSize = new(0.25f),
        };

        UIButton newGameButton = new()
        {
            Parent = buttonContainer,
            Background = new("Images/Button", useCache: true),
            Text = new("Nowa gra", Color.Black),
        };
        newGameButton.OnClicked += (s, e) => Debug.WriteLine("Starting new game...");

        UIButton quitGameButton = new()
        {
            Parent = buttonContainer,
            Background = new("Images/Button", useCache: true),
            Text = new("Wyjście z gry", Color.Black),
            RelativeOffset = new(0.0f, 0.5f),
        };
        quitGameButton.OnClicked += (s, e) => WZIMopoly.Instance.Exit();

        UIText versionText = new("There will be a version of the game here someday", Color.White) { Size = 0.5f };
        UIFrame versionFrame = new(thickness: 4, new Color(255, 255, 255, 150))
        {
            Parent = background,
            Ratio = versionText.MeasureDimensions().ToPoint().ToRatio(),
            // TODO: A separate method for determining this size would be useful
            RelativeSize = (versionText.MeasureDimensions() + new Vector2(50, 20)) / background.UnscaledDestinationRectangle.Size.ToVector2(),
            Alignment = Alignment.Bottom,
            RelativeOffset = new(0.0f, -0.01f),
        };
        versionText.Parent = versionFrame;
        UIImage versionBackground = new(new Color(255, 255, 255, 25)) { Parent = versionFrame };
    }
}