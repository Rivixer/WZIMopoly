using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZIMopoly.Models.GameScene;

namespace TestWZIMopoly.Test_Models
{
    [TestClass]
    public class Test_TimerModel
    {
        [TestMethod]
        public void Test_UpdateTime()
        {
            //Arrange
            var timerModel = new TimerModel();
            var expectedTimeSpan = new TimeSpan(123);

            //Act
            timerModel.UpdateTime(new TimeSpan(123), null);

            //Assert
            Assert.AreEqual(new TimeSpan(123).TotalSeconds, timerModel.Time.TotalSeconds);
        }
    }
}
