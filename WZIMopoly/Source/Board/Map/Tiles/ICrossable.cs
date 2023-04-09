namespace WZIMopoly.Board
{
    /// <summary>
    /// An interface that allows to call a function that registers exceeding a given tile.
    /// </summary>
    interface ICrossable
    {
        public abstract void OnCross(Player player);
    }
}
