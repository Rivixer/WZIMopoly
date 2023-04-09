﻿#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Board
{
    /// <summary> 
    /// Represents a Canteen tile. <br/>
    /// When a counter lands on this tile, a card is drawn. It can have a positive or negative 
    /// effect on player's turn. Sometimes this card can be saved for later (e.g. Get out of 
    /// the Must-have Lecture card).
    /// In case of taking a card from the 'Canteen' tile that moves counters, 
    /// money is also rewarded for crossing the start tile in the case of moving a pawn, 
    /// unless it says 'go back'.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Chance">'Chance'</see> 
    /// tile in Monopoly. 
    /// </summary>
    class Canteen : ChanceTile
    {
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        public Canteen(XmlNode node) : base(node)
        {

        }
    }
}
