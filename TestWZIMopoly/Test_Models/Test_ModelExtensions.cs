using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using WZIMopoly;
using WZIMopoly.Controllers;
using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.Models;

#nullable disable


namespace TestWZIMopoly.Test_Models
{
    internal class Model0 : Model { }

    internal class Model1_1 : Model { }

    internal class Model1_2 : Model { }

    internal class Model2 : Model { }

    internal class ModelX : Model { }

    public class View0 : GUIElement
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }

        public override void Load(ContentManager content) { }

        public override void Recalculate() { }
    }

    internal class View1_1 : GUIText
    {
        internal View1_1(string fontPath, Vector2 defPosition, GUIStartPoint startPoint = GUIStartPoint.TopLeft, string text = "", float scale = 1) : base(fontPath, defPosition, startPoint, text, scale)
        {
        }
    }

    internal class View1_2 : GUITexture
    {
        internal View1_2(string path, Rectangle defDstRect) : base(path, defDstRect)
        {
        }
    }

    internal class View2 : GUIElement
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }

        public override void Load(ContentManager content) { }

        public override void Recalculate() { }
    }

    internal class ViewX : GUIText
    {
        internal ViewX(string fontPath, Vector2 defPosition, GUIStartPoint startPoint = GUIStartPoint.TopLeft, string text = "", float scale = 1) : base(fontPath, defPosition, startPoint, text, scale)
        {
        }
    }

    internal class Controller0 : Controller<Model0, View0>
    {
        public Controller0(Model0 model, View0 view) : base(model, view) { }
    }
    internal class Controller1_1 : Controller<Model1_1, View1_1>
    {
        public Controller1_1(Model1_1 model, View1_1 view) : base(model, view) { }
    }

    internal class Controller1_2 : Controller<Model1_2, View1_2>
    {
        public Controller1_2(Model1_2 model, View1_2 view) : base(model, view) { }
    }

    internal class Controller2 : Controller<Model2, View2>
    {
        public Controller2(Model2 model, View2 view) : base(model, view) { }
    }

    internal class ControllerX : Controller<ModelX, ViewX>
    {
        public ControllerX(ModelX model, ViewX view) : base(model, view) { }
    }

    [TestClass]
    public class Test_ModelExtensions
    {
        private Model0 _model0;
        private Model1_1 _model1_1;
        private Model1_2 _model1_2;
        private Model2 _model2;
        private ModelX _modelX;

        private View0 _view0;
        private View1_1 _view1_1;
        private View1_2 _view1_2;
        private View2 _view2;
        private ViewX _viewX;

        private Controller0 _controller0;
        private Controller1_1 _controller1_1;
        private Controller1_2 _controller1_2;
        private Controller2 _controller2;
        private ControllerX _controllerX;

        private Rectangle _rectangle = new Rectangle(100, 200, 300, 400);
        private Vector2 _vector2 = new Vector2(1, 2);
        private string _path = "TestTestTest";

        internal static bool FindController0(IControllerable controller)
        {
            return controller.View is GUIText;

        }
        internal static bool FindController1(IControllerable controller)
        {
            return controller.View is GUITexture;
        }

        Predicate<IControllerable> _predicate0 = FindController0;
        Predicate<IControllerable> _predicate1 = FindController1;

        [TestInitialize]
        public void Setup()
        {
            _model0 = new Model0();
            _model1_1 = new Model1_1();
            _model1_2 = new Model1_2();
            _model2 = new Model2();
            _modelX = new ModelX();

            _view0 = new View0();
            _view1_1 = new View1_1(_path, _vector2);
            _view1_2 = new View1_2(_path, _rectangle);
            _view2 = new View2();
            _viewX = new ViewX(_path, _vector2);

            _controller0 = new Controller0(_model0, _view0);
            _controller1_1 = new Controller1_1(_model1_1, _view1_1);
            _controller1_2 = new Controller1_2(_model1_2, _view1_2);
            _controller2 = new Controller2(_model2, _view2);
            _controllerX = new ControllerX(_modelX, _viewX);

            _model0.AddChild(_controller1_1);
            _model0.AddChild(_controller1_2);
            _model1_1.AddChild(_controller2);
        }

        [TestMethod]
        public void Test_GetController_ReturnController()
        {
            // Arrange
            var ExpectedResult = _controller1_2;

            // Act
            var Result = _model0.GetController<Controller1_2>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetController_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetController<ControllerX>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetControllerPredicate_ReturnController()
        {
            // Arrange
            var ExpectedResult = _controller1_1;

            // Act
            var Result = _model0.GetController<Controller1_1>(_predicate0);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetControllerPredicate_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetController<Controller1_1>(_predicate1);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetControllerRecursively_ReturnController()
        {
            // Arrange
            var ExpectedResult = _controller1_2;

            // Act
            var Result = _model0.GetControllerRecursively<Controller1_2>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetControllerRecursively_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetControllerRecursively<ControllerX>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetControllerRecursivelyPredicate_ReturnController()
        {
            // Arrange
            var ExpectedResult = _controller1_1;

            // Act
            var Result = _model0.GetControllerRecursively<Controller1_1>(_predicate0);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetControllerRecursivelyPredicate_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetControllerRecursively<Controller1_1>(_predicate1);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllControllers_ReturnControllers()
        {
            // Arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_1);
            controllers.Add(_controller1_2);
            var ExpectedResult = controllers;

            // Act
            var Result = _model0.GetAllControllers<IControllerable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllControllersPredicate_ReturnControllers()
        {
            // Arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_2);
            var ExpectedResult = controllers;

            // Act
            var Result = _model0.GetAllControllers(_predicate1);

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllControllersRecursively_ReturnControllers()
        {
            // Arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_1);
            controllers.Add(_controller1_2);
            controllers.Add(_controller2);
            var ExpectedResult = controllers;

            // Act
            var Result = _model0.GetAllControllersRecursively<IControllerable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllControllersRecursivelyPredicate_ReturnControllers()
        {
            // Arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_2);

            var ExpectedResult = controllers;

            // Act
            var Result = _model0.GetAllControllersRecursively(_predicate1);

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetModel_ReturnModel()
        {
            // Arrange
            var ExpectedResult = _model1_2;

            // Act
            var Result = _model0.GetModel<Model1_2>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetModel_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetModel<ModelX>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetModelRecursively_ReturnModel()
        {
            // Arrange
            var ExpectedResult = _model1_2;

            // Act
            var Result = _model0.GetModelRecursively<Model1_2>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetModelRecursively_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetModelRecursively<ModelX>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllModels_ReturnModels()
        {
            // Arrange
            List<IModelable> models = new List<IModelable>();
            models.Add(_model1_1);
            models.Add(_model1_2);
            var ExpectedResult = models;

            // Act
            var Result = _model0.GetAllModels<IModelable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllModelsRecursively_ReturnModels()
        {
            // Arrange
            List<IModelable> models = new List<IModelable>();
            models.Add(_model1_1);
            models.Add(_model1_2);
            models.Add(_model2);
            var ExpectedResult = models;

            // Act
            var Result = _model0.GetAllModelsRecursively<IModelable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetView_ReturnView()
        {
            // Arrange
            var ExpectedResult = _view1_2;

            // Act
            var Result = _model0.GetView<View1_2>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetView_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetView<ViewX>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetViewRecursively_ReturnView()
        {
            // Arrange
            var ExpectedResult = _view1_2;

            // Act
            var Result = _model0.GetViewRecursively<View1_2>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetViewRecursively_ReturnNULL()
        {
            // Arrange
            string x = null;
            var ExpectedResult = x;

            // Act
            var Result = _model0.GetViewRecursively<ViewX>();

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllViews_ReturnViews()
        {
            // Arrange
            List<IGUIable> views = new List<IGUIable>();
            views.Add(_view1_1);
            views.Add(_view1_2);
            var ExpectedResult = views;

            // Act
            var Result = _model0.GetAllViews<IGUIable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        [TestMethod]
        public void Test_GetAllViewsRecursively()
        {
            // Arrange
            List<IGUIable> views = new List<IGUIable>();
            views.Add(_view1_1);
            views.Add(_view1_2);
            views.Add(_view2);
            var ExpectedResult = views;

            // Act
            var Result = _model0.GetAllViewsRecursively<IGUIable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }
    }
}
