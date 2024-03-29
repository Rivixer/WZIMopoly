﻿using System.Text;
using WZIMopoly.Utils;

namespace TestWZIMopoly.Test_Utils
{
    /// <summary>
    /// Unit tests for the NamingConverter class.
    /// </summary>
    [TestClass]
    public class Test_NamingConverter
    {
        /// <summary>
        /// Tests the ConvertSnakeCaseToPascalCase method with a lowercase input.
        /// </summary>
        [TestMethod]
        public void Test_ConvertSnakeCaseToPascalCase_LowerCase()
        {
            //Arrange
            var exampleSnakeCase = "mono_poly_game_test";
            var expectedResult = "MonoPolyGameTest";

            //Act
            var result = NamingConverter.ConvertSnakeCaseToPascalCase(exampleSnakeCase);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests the ConvertSnakeCaseToPascalCase method with an uppercase input.
        /// </summary>
        [TestMethod]
        public void Test_ConvertSnakeCaseToPascalCase_UpperCase()
        {
            //Arrange
            var exampleSnakeCase = "MONO_POLY_GAME_TEST";
            var expectedResult = "MonoPolyGameTest";

            //Act
            var result = NamingConverter.ConvertSnakeCaseToPascalCase(exampleSnakeCase);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests the ConvertSnakeCaseToPascalCase method with a mixed case input.
        /// </summary>
        [TestMethod]
        public void Test_ConvertSnakeCaseToPascalCase_BothCases()
        {
            //Arrange
            var exampleSnakeCase = "MONO_POLY_game_test";
            var expectedResult = "MonoPolyGameTest";

            //Act
            var result = NamingConverter.ConvertSnakeCaseToPascalCase(exampleSnakeCase);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests the ConvertSnakeCaseToPascalCase method with a long input text.
        /// </summary>
        [TestMethod]
        public void Test_ConvertSnakeCaseToPascalCase_LongText()
        {
            //Arrange
            var exampleSnakeCase = "test";
            var expectedResult = new StringBuilder("Test");

            for (int i = 0; i < 100; i++)
            {
                exampleSnakeCase += "_test";
                expectedResult.Append("Test");
            }

            //Act
            var result = NamingConverter.ConvertSnakeCaseToPascalCase(exampleSnakeCase);

            //Assert
            Assert.AreEqual(expectedResult.ToString(), result);
        }
    }
}