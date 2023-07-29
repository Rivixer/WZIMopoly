using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;

// Any better name idea?
internal interface IUIPositionInfluencer
{
    Vector2 GetAdditionalRelativeOffsetForChildren();
    Vector2 GetAdditionalRelativeSizeForChildren();
}
