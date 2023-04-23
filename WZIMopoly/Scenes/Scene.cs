using System.Collections.Generic;
using WZIMopoly.Controllers;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents a scene of the program.
    /// </summary>
    internal abstract class Scene : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        /// <remarks>
        /// The scene is a primary controller.
        /// </remarks>
        /// <param name="view">
        /// The view of the controller.
        /// </param>
        /// <param name="model">
        /// The model of the controller.
        /// </param>
        public Scene(IGUIable view, Model model) : base(view, model, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        /// <remarks>
        /// The scene is a primary controller.
        /// </remarks>
        /// <param name="view">
        /// The view of the controller.
        /// </param>
        /// <param name="model">
        /// The model of the controller.
        /// </param>
        /// <param name="children">
        /// The list of children of controller.
        /// </param>
        public Scene(IGUIable view, Model model, List<Controller> children)
            : base(view, model, true, children) { }
    }
}


