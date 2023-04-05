namespace WZIMopoly
{
    public class Player
    {
        public readonly string Color;
        private string _nick;
        private Tile _tile;
        
        public Player(string nick, string color)
        {
            _nick = nick;
            Color = color;
        }

        public string Nick
        {
            get => _nick;
            set => _nick = value;
        }

        public Tile Tile
        {
            get => _tile;
            set => _tile = value;
        }
    }
}
