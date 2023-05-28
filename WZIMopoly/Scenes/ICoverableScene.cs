using WZIMopoly.Controllers;

#nullable enable

namespace WZIMopoly.Scenes
{
    internal interface ICoverableScene
    {
        public IPrimaryController? SecondScene { get; set; }
    }
}
