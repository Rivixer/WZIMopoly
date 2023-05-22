using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZIMopoly;
using WZIMopoly.Controllers;
using WZIMopoly.Enums;
using WZIMopoly.GUI;
using WZIMopoly.Models;


namespace TestWZIMopoly.Test_Utils
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

    internal class View2: GUIElement
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }

        public override void Load(ContentManager content) { }

        public override void Recalculate() { }
    }

    internal class Controller0 : Controller<Model0, View0>
    {
        public Controller0(Model0 model, View0 view) : base(model, view) { }
    }

    internal class Controller1_1 : Controller<Model1_1,View1_1>
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

    internal class ControllerX : Controller<ModelX, View2>
    {
        public ControllerX(ModelX model, View2 view) : base(model, view) { }
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

            _controller0 = new Controller0(_model0,_view0);
            _controller1_1 = new Controller1_1(_model1_1, _view1_1);
            _controller1_2 = new Controller1_2(_model1_2,_view1_2);
            _controller2 = new Controller2(_model2,_view2);
            _controllerX = new ControllerX(_modelX, _view2);

            _model0.AddChild(_controller1_1);
            _model0.AddChild(_controller1_2);
            _model1_1.AddChild(_controller2);
        }

        [TestMethod]
        public void Test_GetController_ReturnController()
        {
            //arrange
            var ExpectedResult = _controller1_2;

            //act
            var Result = _model0.GetController<Controller1_2>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }
        [TestMethod]
        public void Test_GetController_ReturnNULL()
        {
            //arrange
            string x = null;
            var ExpectedResult = x;

            //act
            var Result = _model0.GetController<ControllerX>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetControllerPredicate_ReturnController()
        {
            //arrange
            var ExpectedResult = _controller1_1;

            //act
            var Result = _model0.GetController<Controller1_1>(_predicate0);

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }
        [TestMethod]
        public void Test_GetControllerPredicate_ReturnNULL()
        {
            //arrange
            string x = null;
            var ExpectedResult = x;

            //act
            var Result = _model0.GetController<Controller1_1>(_predicate1);

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetControllerRecursively_ReturnController()
        {
            //arrange
            var ExpectedResult = _controller1_2;

            //act
            var Result = _model0.GetControllerRecursively<Controller1_2>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetControllerRecursively_ReturnNULL()
        {
            //arrange
            string x = null;
            var ExpectedResult = x;

            //act
            var Result = _model0.GetControllerRecursively<ControllerX>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetControllerRecursivelyPredicate_ReturnController()
        {
            //arrange
            var ExpectedResult = _controller1_1;

            //act
            var Result = _model0.GetControllerRecursively<Controller1_1>(_predicate0);

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetControllerRecursivelyPredicate_ReturnNULL()
        {
            //arrange
            string x = null;
            var ExpectedResult = x;

            //act
            var Result = _model0.GetControllerRecursively<Controller1_1>(_predicate1);

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetAllControllers_ReturnControllers()
        {
            //arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_1);
            controllers.Add(_controller1_2);
            var ExpectedResult = controllers;

            //act
            var Result = _model0.GetAllControllers<IControllerable>();

            //assert
            CollectionAssert.AreEqual(ExpectedResult, Result);

        }
        [TestMethod]
        public void Test_GetAllControllersPredicate_ReturnControllers()
        {
            //arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_2);
            var ExpectedResult = controllers;

            //act
            var Result = _model0.GetAllControllers<IControllerable>(_predicate1);

            //assert
            CollectionAssert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetAllControllersRecursively_ReturnControllers()
        {
            //arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_1);
            controllers.Add(_controller1_2);
            controllers.Add(_controller2);
            var ExpectedResult = controllers;

            //act
            var Result = _model0.GetAllControllersRecursively<IControllerable>();

            //assert
            CollectionAssert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetAllControllersRecursivelyPredicate_ReturnControllers()
        {
            //arrange
            List<IControllerable> controllers = new List<IControllerable>();
            controllers.Add(_controller1_2);

            var ExpectedResult = controllers;

            //act
            var Result = _model0.GetAllControllersRecursively<IControllerable>(_predicate1);

            //assert
            CollectionAssert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetModel_ReturnModel()
        {
            //arrange
            var ExpectedResult = _model1_2;

            //act
            var Result = _model0.GetModel<Model1_2>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }
        [TestMethod]
        public void Test_GetModel_ReturnNULL()
        {
            //arrange
            string x = null;
            var ExpectedResult = x;

            //act
            var Result = _model0.GetModel<ModelX>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetModelRecursively_ReturnModel()
        {
            //arrange
            var ExpectedResult = _model1_2;

            //act
            var Result = _model0.GetModelRecursively<Model1_2>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetModelRecursively_ReturnNULL()
        {
            //arrange
            string x = null;
            var ExpectedResult = x;

            //act
            var Result = _model0.GetModelRecursively<ModelX>();

            //assert
            Assert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetAllModels_ReturnModels()
        {
            //arrange
            List<IModelable> models = new List<IModelable>();
            models.Add(_model1_1);
            models.Add(_model1_2);
            var ExpectedResult = models;

            //act
            var Result = _model0.GetAllModels<IModelable>();

            //assert
            CollectionAssert.AreEqual(ExpectedResult, Result);

        }

        [TestMethod]
        public void Test_GetAllModelsRecursively_ReturnModels()
        {
            //arrange
            List<IModelable> models = new List<IModelable>();
            models.Add(_model1_1);
            models.Add(_model1_2);
            models.Add(_model2);
            var ExpectedResult = models;

            //act
            var Result = _model0.GetAllModelsRecursively<IModelable>();

            //assert
            CollectionAssert.AreEqual(ExpectedResult, Result);

        }

    }
}
