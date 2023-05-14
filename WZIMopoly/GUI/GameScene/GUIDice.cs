using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    internal class GUIDice : GUIElement
    {
        /// <summary>
        /// List of textures for the first dice.
        /// </summary>
        private readonly List<GUITexture> _firstDiceTextures;
        /// <summary>
        /// List of textures for the second dice.
        /// </summary>
        private readonly List<GUITexture> _secondDiceTextures;
        private readonly DiceModel _model;

        internal GUIDice(DiceModel model)
        {
            _model = model;
            _firstDiceTextures = new List<GUITexture>();
            _secondDiceTextures = new List<GUITexture>();
            for (int i = 0; i < 6; i++)
            {
                var text = $"images\\dice{i + 1}";
                _firstDiceTextures.Add(new GUITexture(text, new Rectangle(660, 540, 160, 160),GUIStartPoint.Center));
                _secondDiceTextures.Add(new GUITexture(text, new Rectangle(1260, 540, 160, 160),GUIStartPoint.Center));
            }
        }

        /// <summary>
        /// Draws dices to the screen.
        /// </summary>
        internal override void Draw(SpriteBatch spriteBatch)        
        {
            if(_model.LastRoll != null)
            {
                _firstDiceTextures[_model.LastRoll.Item1-1].Draw(spriteBatch);
                _secondDiceTextures[_model.LastRoll.Item2-1].Draw(spriteBatch);
            }
        }

        internal override void Load(ContentManager content) {
            _firstDiceTextures.ForEach(x => x.Load(content));
            _secondDiceTextures.ForEach(x => x.Load(content));
        }

        internal override void Recalculate() {
            _firstDiceTextures.ForEach(x=>x.Recalculate());
            _secondDiceTextures.ForEach(x=>x.Recalculate());
        }
    }
}
