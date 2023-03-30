#region Using Statements
using System.Xml;
#endregion

namespace WZIMpoly.Source.Board.Map.Tiles
{
    enum SubjectGrade
    {

    }

    class Subject : PurchasableTile
    {
        SubjectGrade Grade;
        public Subject(XmlNode node) : base(node)
        {

        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
