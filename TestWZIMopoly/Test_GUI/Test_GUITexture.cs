using WZIMopoly.Enums;
using WZIMopoly.GUI;
using Microsoft.Xna.Framework;
using WZIMopoly.Engine;

namespace TestWZIMopoly.Test_GUI
{
    /// <summary>
    /// Represents a mock of GUITexture.
    /// </summary>
    internal class MockGUITexture : GUITexture
    {
        internal MockGUITexture(Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
            : base("", defDstRect, startPoint) { }
    }

    [TestClass]
    public class Test_GUITexture
    {
        private Rectangle _rectangle;

        [TestInitialize]
        public void Setup()
        {
            _rectangle = new Rectangle(100, 200, 300, 400);

            ScreenController.ChangeResolution(1920, 1080, true);
        }

        [TestMethod]
        public void Test_GUITexture_Recalculate() 
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle);
            var expected = new Rectangle(66, 133, 200, 266);

            // Act
            ScreenController.ChangeResolution(1280, 720, true); 
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.DestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_TopLeft()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.TopLeft);
            var expected = new Rectangle(100, 200, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_Left()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.Left);
            var expected = new Rectangle(100, 0, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_Top()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.Top);
            var expected = new Rectangle(-50, 200, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_Center()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.Center);
            var expected = new Rectangle(-50, 0, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_Bottom()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.Bottom);
            var expected = new Rectangle(-50, -200, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_TopRight()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.TopRight);
            var expected = new Rectangle(-200, 200, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_Right()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.Right);
            var expected = new Rectangle(-200, 0, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GUITexture_ShiftStartPoint_BottomRight()
        {
            // Arrange
            var mockGUITexture = new MockGUITexture(_rectangle, GUIStartPoint.BottomRight);
            var expected = new Rectangle(-200, -200, 300, 400);

            // Act
            mockGUITexture.Recalculate();
            Rectangle actual = mockGUITexture.UnscaledDestinationRect;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
