using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.GUI.GameScene
{
    internal class GUISubjectTile : GUIPurchasableTile
    {
        /// <summary>
        /// The grade on the tile.
        /// </summary>
        private readonly GUIText _grade;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUISubjectTile"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node that contains the tile data.
        /// </param>
        /// <param name="model">
        /// The model of the tile.
        /// </param>
        /// <param name="tileCard">
        /// The card of the tile.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The XML tile data is invalid.
        /// </exception>
        internal GUISubjectTile(XmlNode node, SubjectTileModel model, TileCardController tileCard) 
            : base(node, model, tileCard)
        {
            _grade = _orientation switch
            {
                TileOrientation.Vertical => new GUIText("Fonts/WZIMFont", new Vector2(Position.Center.X, Position.Top + 11), Color.Black, GUIStartPoint.Center, "", 0.3f),
                TileOrientation.HorizontalLeft => new GUIText("Fonts/WZIMFont", new Vector2(Position.Right + 18, Position.Center.Y - 2), Color.Black, GUIStartPoint.Center, "", 0.3f, MathHelper.ToRadians(90)),
                TileOrientation.HorizontalRight => new GUIText("Fonts/WZIMFont", new Vector2(Position.Left + 16, Position.Center.Y + 27), Color.Black, GUIStartPoint.Center, "", 0.3f, MathHelper.ToRadians(270)),
                _ => throw new ArgumentException($"Displaying a grade is implemented only for vertical and horizontal tiles.")
            };
        }

        /// <summary>
        /// Gets the subject tile model.
        /// </summary>
        protected new SubjectTileModel Model => (SubjectTileModel)base.Model;

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            _grade.Load(content);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Model.Owner is not null)
            {
                _grade.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            if (Model.IsMortgaged)
            {
                _grade.Text = WZIMopoly.Language switch
                {
                    Language.Polish => " Zast. ",
                    Language.English => " Mort. ",
                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                _grade.Text = Model.Grade.ConvertToString();
            }

            if (_grade.Text.Length == 3)
            {
                if (_orientation == TileOrientation.HorizontalLeft)
                    _grade.SetNewDefPosition(new Vector2(Position.Right + 18, Position.Center.Y - 2), GUIStartPoint.Center);
                else if (_orientation == TileOrientation.HorizontalRight)
                    _grade.SetNewDefPosition(new Vector2(Position.Left + 16, Position.Center.Y + 27), GUIStartPoint.Center);
            }
            else
            {
                if (_orientation == TileOrientation.HorizontalLeft)
                    _grade.SetNewDefPosition(new Vector2(Position.Right + 32, Position.Center.Y - 16), GUIStartPoint.Center);
                else if (_orientation == TileOrientation.HorizontalRight)
                    _grade.SetNewDefPosition(new Vector2(Position.Left + 30, Position.Center.Y + 42), GUIStartPoint.Center);
            }
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();
            _grade.Recalculate();
        }
    }
}
