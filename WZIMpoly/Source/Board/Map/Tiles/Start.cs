using System.Xml;

namespace WindowsWZIMpoly.Source.Board.Map.Tiles
{
    class Start
    {
        private int _money;
        public Start(XmlNode node):base(node)
        {
            _money = 0; 
        }

    }
}
