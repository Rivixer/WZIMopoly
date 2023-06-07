using System;

namespace WZIMopoly.Attributes
{
    /// <summary>
    /// Represents an attribute that indicates
    /// that the class should update the network.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class UpdatesNetwork : Attribute { }
}
