using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
#if DEBUG
        private ConsoleController _consoleController;
#endif
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
        public Scene(GUIElement view, Model model) : base(view, model, true) 
        {
#if DEBUG
            InitializeConsole();
#endif
        }

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
        public Scene(GUIElement view, Model model, List<Controller> children)
            : base(view, model, true, children) 
        {
#if DEBUG
            InitializeConsole();
#endif
        }

        private void InitializeConsole()
        {
            ConsoleModel model;
            GUIConsole view;
            ConsoleController controller;

            //Console
            model = new ConsoleModel("Console", new Rectangle(0, 0, 900, 450));
            view = new GUIConsole(model);
            controller = new ConsoleController(view, model);
            _consoleController = controller;
        }

        internal void ToggleConsole()
        {
            if (!Children.Contains(_consoleController))
            {
                Children.Add(_consoleController);
            }
            else
            {
                Children.Remove(_consoleController);
            }
        }
    }
}


