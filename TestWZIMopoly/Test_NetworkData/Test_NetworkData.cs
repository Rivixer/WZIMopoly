using System.Runtime.Serialization;

namespace WZIMopoly.NetworkData.Tests
{
    [TestClass]
    public class Test_NetworkData
    {
        /// <summary>
        /// Test for ToByteArray method.
        /// it verifies that when the object is valid the method returns a byte array.
        /// </summary>
        [TestMethod]
        public void Test_ToByteArray_ValidObject_ReturnsByteArray()
        {
            // Arrange
            var netData = new TestNetData();

            // Act
            var byteArray = netData.ToByteArray();

            // Assert
            Assert.IsNotNull(byteArray);
            Assert.IsTrue(byteArray.Length > 0);
        }

        /// <summary>
        /// Test for FromByteArray method.
        /// it verifies that when the Byet array is valid the method returns a NetData instance
        /// </summary>
        [TestMethod]
        public void Test_FromByteArray_ValidByteArray_ReturnsNetDataInstance()
        {
            // Arrange
            var netData = new TestNetData();
            var byteArray = netData.ToByteArray();

            // Act
            var deserializedData = NetData.FromByteArray<TestNetData>(byteArray);

            // Assert
            Assert.IsNotNull(deserializedData);
        }

        /// <summary>
        /// Test for FromByteArray method.
        /// it verifies that when the Byet array is not valid the method throws an Exception
        /// </summary>
        [TestMethod]
        public void Test_FromByteArray_InvalidByteArray_ThrowsException()
        {
            // Arrange
            var byteArray = new byte[] { 1, 2, 3, 4 };

            // Act & Assert
            Assert.ThrowsException<SerializationException>(() => NetData.FromByteArray<TestNetData>(byteArray));
        }

        // TestNetData class for testing purposes
        [Serializable]
        private class TestNetData : NetData
        {
            // Add properties or additional tests specific to TestNetData
        }
    }
}
