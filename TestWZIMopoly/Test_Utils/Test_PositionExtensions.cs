using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.Utils.PositionExtensions;

namespace TestWZIMopoly.Test_Utils
{
    /// <summary>
    /// Represents a mock of PositionExtensions
    /// </summary>
    [TestClass]
    public class Test_PositionExtensions
    {
        private Rectangle _rectangle;
        private Vector2 _vector2;
        private Point _point;

        [TestInitialize]
        public void Setup()
        {
            _rectangle = new Rectangle(100, 200, 300, 400);
            _vector2 = new Vector2(200, 300);
            _point = new Point(10, 20);
            ScreenController.ChangeResolution(1920, 1080, true);
        }

        /// <summary>
        /// PositionExtensions test for rectangle.
        /// </summary>
        [TestMethod]
        public void Test_PositionExtensions_ToCurrentResolution_Rectangle()
        {
            //Arrange
            var rectangle = _rectangle;
            var expected = new Rectangle(66, 133, 200, 266);

            //Act
            ScreenController.ChangeResolution(1280, 720, false);
            Rectangle actual = rectangle.ToCurrentResolution();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// PositionExtensions test for vector.
        /// </summary>
        [TestMethod]
        public void Test_PositionExtensions_ToCurrentResolution_Vector2()
        {
            //Arrange
            var vector2 = _vector2;
            var expected = new Vector2((float)133.33333, 200);

            //Act
            ScreenController.ChangeResolution(1280, 720, false);
            Vector2 actual = vector2.ToCurrentResolution();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// PositionExtension test for point.
        /// </summary>
        [TestMethod]
        public void Test_PositionExtensions_ToCurrentResolution_Point()
        {
            //Arrange
            var point = _point;
            var expected = new Point(6, 13);

            //Act
            ScreenController.ChangeResolution(1280, 720, false);
            Point actual = point.ToCurrentResolution();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
