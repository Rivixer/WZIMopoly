using System;

namespace WZIMopoly.UI;

[Flags]
internal enum ScrollMode
{
    None = 0,
    Horizontal = 1 << 0,
    Vertical = 1 << 1,
    Both = Horizontal | Vertical,
}
