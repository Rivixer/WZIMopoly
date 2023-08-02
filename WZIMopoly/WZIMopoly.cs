global using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using WZIMopoly.UI;
using WZIMopoly.UI.Scenes;

#nullable disable

namespace WZIMopoly;

internal class WZIMopoly : Game
{
    private static readonly List<Scene> _scenes = new();
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    public WZIMopoly()
    {
        Instance = this;

        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
        ContentSystem.Content = Content;
    }

    public static WZIMopoly Instance { get; private set; }

    public static Scene CurrentScene { get; private set; }

    public static void ChangeScene<T>()
        where T : Scene
    {
        CurrentScene = _scenes.Find(scene => scene is T);
    }

    protected override void Initialize()
    {
        ScreenSystem.Initialize(_graphics, Window);
        ScreenSystem.ApplyChanges();
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        ContentSystem.SpriteBatch = _spriteBatch;

        DebugConsole.Create();

        MenuScene menuScene = new();
        _scenes.Add(menuScene);
        CurrentScene = menuScene;
        menuScene.Create();

        DebugConsole.Error("To jest początek konsoli do debugowania. Poniżej znajdują się przykładowe wiadomości testowe:");
        for (int i = 0; i < 4; i ++)
        {
            DebugConsole.Error($"Wiadomość testowa nr {i+1}.");
        }
        DebugConsole.Error($"Wiadomość testowa nr 5, która jest trochę dłuższa od poprzednich, ponieważ trzeba sprawdzić, czy ładnie się linijki wrapują. Także ten tekst powinien automatycznie się podzielić, jeśli dojdzie do ściany konsoli.");
        DebugConsole.Error("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
        DebugConsole.Error("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
        DebugConsole.Error("ęśąćż");
        DebugConsole.Error("Kiedy planujecie zacząć coś robić z projektem? XD");
        DebugConsole.Warning("A tu dla zmyłki jest żółta wiadomość.");
        DebugConsole.Error("Wracamy do czerwonego.");
        DebugConsole.Error("Jak oceniacie wygląd tej konsoli?");
        DebugConsole.Error("Ogólnie rysowanie tych ramek dookoła zajęło mi aż 6 dni...");
        DebugConsole.Error("Nie da się wyjść tekstem poza ramkę");
    }
    protected override void Update(GameTime gameTime)
    {
        KeyboardSystem.Update();
        MouseSystem.Update();
        ScreenSystem.Update();
        UIButton.WasClickedInThisFrame = false;

        CurrentScene.Update(gameTime);
        DebugConsole.Update(gameTime);

        if (Keys.F3.WasReleased())
        {
            List<UIImage> images = new();
            for(int i = 0; i < 10000; i++)
            {
                UIImage image = new("Images/Button");
                images.Add(image);
                _ = image.Texture; // force load
                _ = image.TexturePixels;
            }
            images.ForEach(x => x.Destroy());
            images.Clear();
        }

        if (Keys.F4.WasReleased())
        {
            GC.Collect();
        }
       
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        base.Draw(gameTime);

        CurrentScene.Draw(gameTime);
        DebugConsole.Draw(gameTime);

        _spriteBatch.End();
    }
}