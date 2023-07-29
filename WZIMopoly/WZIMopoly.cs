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

        MenuScene menuScene = new();
        _scenes.Add(menuScene);
        CurrentScene = menuScene;
        menuScene.Create();
    }
    protected override void Update(GameTime gameTime)
    {
        KeyboardSystem.Update();
        MouseSystem.Update();
        ScreenSystem.Update();
        UIButton.WasClickedInThisFrame = false;

        CurrentScene.Update(gameTime);

        if (Keys.Escape.WasReleased())
        {
            Exit();
        }

        if (Keys.F3.WasReleased())
        {
            List<UIImage> images = new();
            for(int i = 0; i < 10000; i++)
            {
                UIImage image = new("Images/Button", useCache: false);
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

        _spriteBatch.End();
    }
}