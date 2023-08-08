using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable

namespace WZIMopoly;

internal static class ContentSystem
{
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }

    public static IDisposable UseScissorRectangle(this SpriteBatch spriteBatch, Rectangle scaledRect)
    {
        spriteBatch.End();

#pragma warning disable CA2000 // Dispose objects before losing scope
        RasterizerState rasterizerState = new() { ScissorTestEnable = true };
#pragma warning restore CA2000 // Dispose objects before losing scope

        spriteBatch.Begin(
            sortMode: SpriteSortMode.Immediate,
            blendState: BlendState.AlphaBlend,
            samplerState: null,
            depthStencilState: null,
            rasterizerState: rasterizerState);

        Rectangle tempRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
        spriteBatch.GraphicsDevice.ScissorRectangle = scaledRect;

        DrawMethod method = new();

        void Dispose(object sender, EventArgs e)
        {
            spriteBatch.End();
            rasterizerState.Dispose();
            spriteBatch.GraphicsDevice.ScissorRectangle = tempRectangle;
            spriteBatch.Begin();
            method.OnDispose -= Dispose;
        }

        method.OnDispose += Dispose;

        return method;
    }

    private class DrawMethod : IDisposable
    {
        public event EventHandler OnDispose;

        public void Dispose()
        {
            OnDispose?.Invoke(this, EventArgs.Empty);
        }
    }
}
