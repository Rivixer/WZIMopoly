using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using WZIMopoly;
using WZIMopoly.Controllers;
using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.Models;

#nullable disable
#pragma warning disable CS0184

namespace TestWZIMopoly.Test_Models
{
    /// <summary>
    /// Represents Model0 class.
    /// </summary>
    internal class Model0 : Model { }

    /// <summary>
    /// Represents Model1_1 class.
    /// </summary>
    internal class Model1_1 : Model { }

    /// <summary>
    /// Represents Model1_2 class.
    /// </summary>
    internal class Model1_2 : Model { }

    /// <summary>
    /// Represents Model2 class.
    /// </summary>
    internal class Model2 : Model { }

    /// <summary>
    /// Represents ModelX class.
    /// </summary>
    internal class ModelX : Model { }

    /// <summary>
    /// Represents View0 class.
    /// </summary>
    public class View0 : GUIElement
    {
        /// <summary>
        /// Draws the View0.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw on.</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }

        /// <summary>
        /// Loads the content for View0.
        /// </summary>
        /// <param name="content">The content manager to load from.</param>
        public override void Load(ContentManager content) { }

        /// <summary>
        /// Recalculates the View0.
        /// </summary>
        public override void Recalculate() { }
    }

    /// <summary>
    /// Represents View1_1 class.
    /// </summary>
    internal class View1_1 : GUIText
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="View1_1"/> class.
        /// </summary>
        internal View1_1()
            : base(null, Vector2.Zero) { }
    }

    /// <summary>
    /// Represents View1_2 class.
    /// </summary>
    internal class View1_2 : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="View1_2"/> class.
        /// </summary>
        internal View1_2()
            : base(null, Rectangle.Empty) { }

    }

    /// <summary>
    /// Represents View2 class.
    /// </summary>
    internal class View2 : GUIElement
    {
        /// <summary>
        /// Draws the View2.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw on.</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }

        /// <summary>
        /// Loads the content for View2.
        /// </summary>
        /// <param name="content">The content manager to load from.</param>
        public override void Load(ContentManager content) { }

        /// <summary>
        /// Recalculates the View2.
        /// </summary>
        public override void Recalculate() { }
    }

    /// <summary>
    /// Represents ViewX class.
    /// </summary>
    internal class ViewX : GUIText
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewX"/> class.
        /// </summary>
        internal ViewX()
            : base(null, Vector2.Zero) { }

    }

    /// <summary>
    /// Represents Controller0 class.
    /// </summary>
    internal class Controller0 : Controller<Model0, View0>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller0"/> class.
        /// </summary>
        /// <param name="model">The model for the controller.</param>
        /// <param name="view">The view for the controller.</param>
        public Controller0(Model0 model, View0 view) : base(model, view) { }
    }

    /// <summary>
    /// Represents Controller1_1 class.
    /// </summary>
    internal class Controller1_1 : Controller<Model1_1, View1_1>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller1_1"/> class.
        /// </summary>
        /// <param name="model">The model for the controller.</param>
        /// <param name="view">The view for the controller.</param>
        public Controller1_1(Model1_1 model, View1_1 view) : base(model, view) { }
    }

    /// <summary>
    /// Represents Controller1_2 class.
    /// </summary>
    internal class Controller1_2 : Controller<Model1_2, View1_2>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller1_2"/> class.
        /// </summary>
        /// <param name="model">The model for the controller.</param>
        /// <param name="view">The view for the controller.</param>
        public Controller1_2(Model1_2 model, View1_2 view) : base(model, view) { }
    }

    /// <summary>
    /// Represents Controller2 class.
    /// </summary>
    internal class Controller2 : Controller<Model2, View2>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller2"/> class.
        /// </summary>
        /// <param name="model">The model for the controller.</param>
        /// <param name="view">The view for the controller.</param>
        public Controller2(Model2 model, View2 view) : base(model, view) { }
    }

    /// <summary>
    /// Represents ControllerX class.
    /// </summary>
    internal class ControllerX : Controller<ModelX, ViewX>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerX"/> class.
        /// </summary>
        /// <param name="model">The model for the controller.</param>
        /// <param name="view">The view for the controller.</param>
        public ControllerX(ModelX model, ViewX view) : base(model, view) { }
    }

    /// <summary>
    /// Represents the test class for ModelExtensions.
    /// </summary>
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

        /// <summary>
        /// Initializes the test setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _model0 = new Model0();
            _model1_1 = new Model1_1();
            _model1_2 = new Model1_2();
            _model2 = new Model2();
            _modelX = new ModelX();

            _view0 = new View0();
            _view1_1 = new View1_1();
            _view1_2 = new View1_2();
            _view2 = new View2();
            _viewX = new ViewX();

            _controller0 = new Controller0(_model0, _view0);
            _controller1_1 = new Controller1_1(_model1_1, _view1_1);
            _controller1_2 = new Controller1_2(_model1_2, _view1_2);
            _controller2 = new Controller2(_model2, _view2);
            _controllerX = new ControllerX(_modelX, _viewX);

            _model0.AddChild(_controller1_1);
            _model0.AddChild(_controller1_2);
            _model1_1.AddChild(_controller2);
        }

        /// <summary>
        /// Tests the GetController method and expects to return the specified controller.
        /// </summary>
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

        /// <summary>
        /// Tests the GetController method and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetController_ReturnNULL()
        {
            // Act
            var Result = _model0.GetController<ControllerX>();

            // Assert
            Assert.IsNull(Result);
        }

        /// <summary>
        /// Tests the GetController method with a predicate and expects to return the specified controller.
        /// </summary>
        [TestMethod]
        public void Test_GetController_PredicateViewIsGUIText_ReturnController()
        {
            // Arrange
            var ExpectedResult = _controller1_1;

            // Act
            var Result = _model0.GetController<Controller1_1>(x => x.View is GUIText);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetController method with a predicate and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetController_PredicateViewIsGUITexture_ReturnNULL()
        {
            // Arrange
            object ExpectedResult = null;

            // Act
            var Result = _model0.GetController<Controller1_1>(x => x.View is GUITexture);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetControllerRecursively method and expects to return the specified controller.
        /// </summary>
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

        /// <summary>
        /// Tests the GetControllerRecursively method and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetControllerRecursively_ReturnNULL()
        {
            // Act
            var Result = _model0.GetControllerRecursively<ControllerX>();

            // Assert
            Assert.IsNull(Result);
        }

        /// <summary>
        /// Tests the GetControllerRecursively method with a predicate and expects to return the specified controller.
        /// </summary>
        [TestMethod]
        public void Test_GetControllerRecursively_PredicateViewIsGUIText_ReturnController()
        {
            // Arrange
            var ExpectedResult = _controller1_1;

            // Act
            var Result = _model0.GetControllerRecursively<Controller1_1>(x => x.View is GUIText);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetControllerRecursively method with a predicate and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetControllerRecursively_PredicateViewIsGUITexture_ReturnNULL()
        {
            // Arrange
            object ExpectedResult = null;

            // Act
            var Result = _model0.GetControllerRecursively<Controller1_1>(x => x.View is GUITexture);

            // Assert
            Assert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetAllControllers method and expects to return the specified controllers.
        /// </summary>
        [TestMethod]
        public void Test_GetAllControllers_ReturnControllers()
        {
            // Arrange
            var ExpectedResult = new List<IControllerable>
        {
        _controller1_1,
        _controller1_2
        };

            // Act
            var Result = _model0.GetAllControllers<IControllerable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetAllControllers method with a predicate and expects to return the specified controllers.
        /// </summary>
        [TestMethod]
        public void Test_GetAllControllers_PredicateViewIsGUITexture_ReturnControllers()
        {
            // Arrange
            var ExpectedResult = new List<IControllerable>
        {
        _controller1_2
        };

            // Act
            var Result = _model0.GetAllControllers<IControllerable>(x => x.View is GUITexture);

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetAllControllersRecursively method and expects to return the specified controllers.
        /// </summary>
        [TestMethod]
        public void Test_GetAllControllersRecursively_ReturnControllers()
        {
            // Arrange
            var ExpectedResult = new List<IControllerable>
        {
        _controller1_1,
        _controller1_2,
        _controller2
        };

            // Act
            var Result = _model0.GetAllControllersRecursively<IControllerable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetAllControllersRecursively method with a predicate and expects to return the specified controllers.
        /// </summary>
        [TestMethod]
        public void Test_GetAllControllersRecursively_PredicateViewIsGUITexture_ReturnControllers()
        {
            // Arrange
            var ExpectedResult = new List<IControllerable>
        {
        _controller1_2
        };

            // Act
            var Result = _model0.GetAllControllersRecursively<IControllerable>(x => x.View is GUITexture);

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetModel method and expects to return the specified model.
        /// </summary>
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

        /// <summary>
        /// Tests the GetModel method and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetModel_ReturnNULL()
        {
            // Act
            var Result = _model0.GetModel<ModelX>();

            // Assert
            Assert.IsNull(Result);
        }

        /// <summary>
        /// Tests the GetModelRecursively method and expects to return the specified model.
        /// </summary>
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

        /// <summary>
        /// Tests the GetModelRecursively method and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetModelRecursively_ReturnNULL()
        {

            // Act
            var Result = _model0.GetModelRecursively<ModelX>();

            // Assert
            Assert.IsNull(Result);
        }

        /// <summary>
        /// Tests the GetAllModels method and expects to return the specified models.
        /// </summary>
        [TestMethod]
        public void Test_GetAllModels_ReturnModels()
        {
            // Arrange
            var ExpectedResult = new List<IModelable>
        {
        _model1_1,
        _model1_2
        };

            // Act
            var Result = _model0.GetAllModels<IModelable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetAllModelsRecursively method and expects to return the specified models.
        /// </summary>
        [TestMethod]
        public void Test_GetAllModelsRecursively_ReturnModels()
        {
            // Arrange
            var ExpectedResult = new List<IModelable>
        {
        _model1_1,
        _model1_2,
        _model2
        };

            // Act
            var Result = _model0.GetAllModelsRecursively<IModelable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetView method and expects to return the specified view.
        /// </summary>
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

        /// <summary>
        /// Tests the GetView method and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetView_ReturnNULL()
        {
            // Act
            var Result = _model0.GetView<ViewX>();

            // Assert
            Assert.IsNull(Result);
        }

        /// <summary>
        /// Tests the GetViewRecursively method and expects to return the specified view.
        /// </summary>
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

        /// <summary>
        /// Tests the GetViewRecursively method and expects to return null.
        /// </summary>
        [TestMethod]
        public void Test_GetViewRecursively_ReturnNULL()
        {
            // Act
            var Result = _model0.GetViewRecursively<ViewX>();

            // Assert
            Assert.IsNull(Result);
        }

        /// <summary>
        /// Tests the GetAllViews method and expects to return the specified views.
        /// </summary>
        [TestMethod]
        public void Test_GetAllViews_ReturnViews()
        {
            // Arrange
            var ExpectedResult = new List<IGUIable>
        {
        _view1_1,
        _view1_2
        };

            // Act
            var Result = _model0.GetAllViews<IGUIable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }

        /// <summary>
        /// Tests the GetAllViewsRecursively method and expects to return the specified views.
        /// </summary>
        [TestMethod]
        public void Test_GetAllViewsRecursively()
        {
            // Arrange
            var ExpectedResult = new List<IGUIable>
         {
        _view1_1,
        _view1_2,
        _view2
         };

            // Act
            var Result = _model0.GetAllViewsRecursively<IGUIable>();

            // Assert
            CollectionAssert.AreEqual(ExpectedResult, Result);
        }
    }
}