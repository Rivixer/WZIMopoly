using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using WZIMopoly.Source.UI.Components;

namespace WZIMopoly;

internal class Dispatcher
{
    public Dispatcher(GameWindow window)
    {
        EventInput.Initialize(window);
    }
}

public interface IKeyboardSubscriber
{
    void ReceiveTextInput(char inputChar);
    void ReceiveTextInput(string text);
    void ReceiveCommandInput(char command);
    void ReceiveSpecialInput(Keys key);
    void ReceiveEditingInput(string text, int start, int length);

    bool Selected { get; set; } //or Focused
}

internal static class KeyboardSystem
{
    private static KeyboardState s_previousKeyboard;
    private static KeyboardState s_currentKeyboard;

    public static Dispatcher Dispatcher { get; private set; } = default!;

    public static void Initialize(GameWindow window)
    {
        Dispatcher = new Dispatcher(window);
        EventInput.CharEntered += EventInput_CharEntered;
    }

    public static IKeyboardSubscriber? Subscriber { get; set; }
    private static void EventInput_CharEntered(object sender, CharacterEventArgs e)
    {
        Subscriber?.ReceiveTextInput(e.Character);
    }

    public static void Update()
    {
        s_previousKeyboard = s_currentKeyboard;
        s_currentKeyboard = Keyboard.GetState();
    }

    public static Func<Keys, bool> IsKeyUp => s_currentKeyboard.IsKeyUp;
    public static Func<Keys, bool> IsKeyDown => s_currentKeyboard.IsKeyDown;

    public static bool WasClicked(this Keys key)
    {
        return s_previousKeyboard.IsKeyUp(key)
            && s_currentKeyboard.IsKeyDown(key);
    }

    public static bool WasReleased(this Keys key)
    {
        return s_previousKeyboard.IsKeyDown(key)
            && s_currentKeyboard.IsKeyDown(key);
    }

    public static IEnumerable<Keys> GetClickedKeys()
    {
        Keys[] wasReleasedKeys = s_previousKeyboard.GetPressedKeys();
        Keys[] isPressedKeys = s_currentKeyboard.GetPressedKeys();

        foreach (Keys pressedKey in isPressedKeys)
        {
            if (!wasReleasedKeys.Contains(pressedKey))
            {
                yield return pressedKey;
            }
        }
    }

    public static void TryGetLetterFromKey(Keys key, out char? letter)
    {
        letter = TryGetLetterFromKey(key);
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetKeyboardLayout(uint idTrhead);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();


    public static char? TryGetLetterFromKey(Keys key)
    {
        bool isCapsLockOn = s_currentKeyboard.CapsLock;
        bool isShiftDown = IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift);
        bool shouldBeCapital = isCapsLockOn ^ isShiftDown;

        bool isAltGrDown = IsKeyDown(Keys.RightAlt);

        IntPtr keyboardLayoutHandle = GetKeyboardLayout(GetCurrentThreadId());
        uint layoutId = (uint)keyboardLayoutHandle.ToInt64() & 0xFFFF;

        const uint polishProgrammersLayoutId = 0x0415;
        bool isPolishProgrammersLayout = layoutId == polishProgrammersLayoutId;

        if (!isAltGrDown && ('A' <= (char)key && (char)key <= 'Z'
                            || '0' <= (char)key && (char)key <= '9'))
        {
            return shouldBeCapital ? (char)key : (char)(key + 32);
        }

        if (isPolishProgrammersLayout)
        {
            return key switch
            {
                Keys.A => shouldBeCapital ? 'Ą' : 'ą',
                Keys.C => shouldBeCapital ? 'Ć' : 'ć',
                Keys.E => shouldBeCapital ? 'Ę' : 'ę',
                Keys.L => shouldBeCapital ? 'Ł' : 'ł',
                Keys.N => shouldBeCapital ? 'Ń' : 'ń',
                Keys.O => shouldBeCapital ? 'Ó' : 'ó',
                Keys.S => shouldBeCapital ? 'Ś' : 'ś',
                Keys.X => shouldBeCapital ? 'Ź' : 'ź',
                Keys.Z => shouldBeCapital ? 'Ż' : 'ż',
                _ => null,
            };
        }

        return null;
    }
}


public class CharacterEventArgs : EventArgs
{
    private readonly char character; private readonly int lParam;
    public CharacterEventArgs(char character, int lParam)
    {
        this.character = character; this.lParam = lParam;
    }
    public char Character
    {
        get { return character; }
    }
    public int Param
    {
        get { return lParam; }
    }
    public int RepeatCount
    {
        get { return lParam & 0xffff; }
    }
    public bool ExtendedKey
    {
        get { return (lParam & (1 << 24)) > 0; }
    }
    public bool AltPressed
    {
        get { return (lParam & (1 << 29)) > 0; }
    }
    public bool PreviousState
    {
        get { return (lParam & (1 << 30)) > 0; }
    }
    public bool TransitionState
    {
        get { return (lParam & (1 << 31)) > 0; }
    }
}
public class KeyEventArgs : EventArgs
{
    private Keys keyCode;
    public KeyEventArgs(Keys keyCode)
    {
        this.keyCode = keyCode;
    }
    public Keys KeyCode { get { return keyCode; } }
}
public delegate void CharEnteredHandler(object sender, CharacterEventArgs e);
public delegate void KeyEventHandler(object sender, KeyEventArgs e);
public static class EventInput
{
    /// <summary>
    /// Event raised when a character has been entered.
    /// </summary>	
    public static event CharEnteredHandler CharEntered;

    /// <summary>
    /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
    /// </summary>	
    public static event KeyEventHandler KeyDown;

    /// <summary>	
    /// Event raised when a key has been released.	
    /// </summary>	
    public static event KeyEventHandler KeyUp;
    static bool initialized;



    /// <summary>
    /// Initialize the TextInput with the given GameWindow.	
    /// </summary>	
    /// <param name="window">The XNA window to which text input should be linked.</param>	
    internal static void Initialize(GameWindow window)
    {
        if (initialized)
            throw new InvalidOperationException("TextInput.Initialize can only be called once!");

        window.TextInput += ReceiveInput;
        initialized = true;
    }

    private static void ReceiveInput(object? sender, TextInputEventArgs e)
    {
        OnCharEntered(e.Character);
    }

    public static void OnCharEntered(char character)
    {
        CharEntered?.Invoke(null, new CharacterEventArgs(character, 0));
    }
}