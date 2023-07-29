using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#nullable disable

namespace WZIMopoly;

internal enum ScreenType
{
    FullScreen,
    Windowed,
    Borderless
}

internal static class ScreenSystem
{
    public delegate void OnScreenChangedEventHandler();
    public static event OnScreenChangedEventHandler OnScreenChanged;

    /// <summary>
    /// The default size the UI is designed for.
    /// </summary>
    public static Point DefaultSize { get; } = new(1920, 1080);

    public static int Width { get; private set; } = 1366;
    public static int Height { get; private set; } = 768;
    public static ScreenType ScreenType { get; private set; } = ScreenType.Windowed;

    public static Vector2 Scale => new(Width / (float)DefaultSize.X, Height / (float)DefaultSize.Y);

    public static GraphicsDevice GraphicsDevice => WZIMopoly.Instance.GraphicsDevice;
    public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
    public static GameWindow GameWindow { get; private set; }
    
    public static void Update()
    {
        if (Keys.F11.WasClicked())
        {
            if (ScreenType == ScreenType.FullScreen)
            {
                SetResolution(1366, 768, ScreenType.Windowed);
            }
            else
            {
                SetResolution(1920, 1080, ScreenType.FullScreen);
            }
            ApplyChanges();
        }
    }

    public static void Initialize(GraphicsDeviceManager graphicsDeviceManager, GameWindow gameWindow)
    {
        GraphicsDeviceManager = graphicsDeviceManager;
        GameWindow = gameWindow;
    }

    public static void SetResolution(int width, int height, ScreenType type)
    {
        Width = width;
        Height = height;
        ScreenType = type;
    }

    public static void ApplyChanges()
    {
        GraphicsDeviceManager.PreferredBackBufferWidth = Width;
        GraphicsDeviceManager.PreferredBackBufferHeight = Height;
        GraphicsDeviceManager.IsFullScreen = ScreenType is ScreenType.FullScreen or ScreenType.Borderless;
        GameWindow.IsBorderless = ScreenType is ScreenType.Borderless;
        GraphicsDeviceManager.ApplyChanges();
        OnScreenChanged?.Invoke();
    }
}