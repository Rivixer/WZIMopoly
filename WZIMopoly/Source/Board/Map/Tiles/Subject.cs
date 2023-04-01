#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    enum SubjectGrade
    {
        three,
        three_half,
        four,
        four_half,
        five,
        exemption
    }

    enum SubjectColor
    {
        brown,
        light_blue,
        pink,
        orange,
        red,
        yellow,
        green,
        blue
    }

    class Subject : PurchasableTile
    {
        public SubjectGrade Grade;
        public readonly int UpgradePrice;
        public readonly Dictionary<SubjectGrade, int> TaxPrices;
        public readonly SubjectColor Color;

        public Subject(XmlNode node) : base(node)
        {

        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
