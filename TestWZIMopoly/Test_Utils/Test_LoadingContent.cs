using Microsoft.Xna.Framework.Content;
using System.Reflection;
using WZIMopoly.Models;
using WZIMopoly.GUI;

namespace TestWZIMopoly.Test_Utils
{
    [TestClass]
    public class Test_Load
    {
        private readonly MockContentManager ContentManager = new(new MockContentManagerRoot(), "Content");

        [TestMethod]
        public void Test_Load_Content()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string[] classes =
            {
                "Timer",
                "Dice",
                //"Map",
                //"Mortgage",
                //"Pawn",
                //"PlayerInfo",
                //"Tile",
                //"Upgrade",
            };

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            foreach(var className in classes)
            {
                Type? classType = assembly.GetType("WZIMopoly.GUI.GameScene.GUI" + className);
                Type? modelType = assemblyModel.GetType("WZIMopoly.Models.GameScene." + className + "Model");

                Assert.IsNotNull(classType);
                Assert.IsNotNull(modelType);

                Model? model = Activator.CreateInstance(
                     type: modelType,
                     bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                     binder: null,
                     args: null,
                     culture: null) as Model;

                Assert.IsNotNull(model);

                GUIElement? instance;
                instance = Activator.CreateInstance(
                    type: classType,
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: new object[] { model },
                    culture: null
                ) as GUIElement;

                Assert.IsNotNull(instance);
                instance.Load(ContentManager);
            }
        }
        [TestMethod]
        public void Test_Load_Map()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string className = "WZIMopoly.GUI.GameScene.GUIMap";
            string modelName = "WZIMopoly.Models.GameScene.MapModel";

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            Type? classType = assembly.GetType(className);
            Type? modelType = assemblyModel.GetType(modelName);

            Assert.IsNotNull(classType);
            Assert.IsNotNull(modelType);

            Model? model = Activator.CreateInstance(
                 type: modelType,
                 bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                 binder: null,
                 args: null,
                 culture: null) as Model;

            Assert.IsNotNull(model);

            GUIElement? instance;
            instance = Activator.CreateInstance(
                type: classType,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
            ) as GUIElement;

            Assert.IsNotNull(instance);
            instance.Load(ContentManager);
        }
        [TestMethod]
        public void Test_Load_Mortgage()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string className = "WZIMopoly.GUI.GameScene.GUIMortgage";
            string modelName = "WZIMopoly.Models.GameScene.MortgageModel";

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            Type? classType = assembly.GetType(className);
            Type? modelType = assemblyModel.GetType(modelName);

            Assert.IsNotNull(classType);
            Assert.IsNotNull(modelType);

            Model? model = Activator.CreateInstance(
                 type: modelType,
                 bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                 binder: null,
                 args: null,
                 culture: null) as Model;

            Assert.IsNotNull(model);

            GUIElement? instance;
            instance = Activator.CreateInstance(
                type: classType,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
            ) as GUIElement;

            Assert.IsNotNull(instance);
            instance.Load(ContentManager);
        }
        [TestMethod]
        public void Test_Load_Pawn()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string className = "WZIMopoly.GUI.GameScene.GUIPawn";
            string modelName = "WZIMopoly.Models.GameScene.PawnModel";

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            Type? classType = assembly.GetType(className);
            Type? modelType = assemblyModel.GetType(modelName);

            Assert.IsNotNull(classType);
            Assert.IsNotNull(modelType);

            Model? model = Activator.CreateInstance(
                 type: modelType,
                 bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                 binder: null,
                 args: null,
                 culture: null) as Model;

            Assert.IsNotNull(model);

            GUIElement? instance;
            instance = Activator.CreateInstance(
                type: classType,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
            ) as GUIElement;

            Assert.IsNotNull(instance);
            instance.Load(ContentManager);
        }
        [TestMethod]
        public void Test_Load_PlayerInfo()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string className = "WZIMopoly.GUI.GameScene.GUIPlayerInfo";
            string modelName = "WZIMopoly.Models.GameScene.PlayerInfoModel";

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            Type? classType = assembly.GetType(className);
            Type? modelType = assemblyModel.GetType(modelName);

            Assert.IsNotNull(classType);
            Assert.IsNotNull(modelType);

            Model? model = Activator.CreateInstance(
                 type: modelType,
                 bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                 binder: null,
                 args: null,
                 culture: null) as Model;

            Assert.IsNotNull(model);

            GUIElement? instance;
            instance = Activator.CreateInstance(
                type: classType,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
            ) as GUIElement;

            Assert.IsNotNull(instance);
            instance.Load(ContentManager);
        }
        [TestMethod]
        public void Test_Load_Tile()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string className = "WZIMopoly.GUI.GameScene.GUITile";
            string modelName = "WZIMopoly.Models.GameScene.TileModel";

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            Type? classType = assembly.GetType(className);
            Type? modelType = assemblyModel.GetType(modelName);

            Assert.IsNotNull(classType);
            Assert.IsNotNull(modelType);

            Model? model = Activator.CreateInstance(
                 type: modelType,
                 bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                 binder: null,
                 args: null,
                 culture: null) as Model;

            Assert.IsNotNull(model);

            GUIElement? instance;
            instance = Activator.CreateInstance(
                type: classType,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
            ) as GUIElement;

            Assert.IsNotNull(instance);
            instance.Load(ContentManager);
        }
        [TestMethod]
        public void Test_Load_Upgrade()
        {
            string assemblyPath = @"..\..\..\..\WZIMopoly\bin\DebugWindows\net6.0\WindowsWZIMopoly.dll";
            string className = "WZIMopoly.GUI.GameScene.GUIUpgrade";
            string modelName = "WZIMopoly.Models.GameScene.UpgradeModel";

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Assembly assemblyModel = Assembly.LoadFrom(assemblyPath);

            Type? classType = assembly.GetType(className);
            Type? modelType = assemblyModel.GetType(modelName);

            Assert.IsNotNull(classType);
            Assert.IsNotNull(modelType);

            Model? model = Activator.CreateInstance(
                 type: modelType,
                 bindingAttr: BindingFlags.Instance | BindingFlags.Public,
                 binder: null,
                 args: null,
                 culture: null) as Model;

            Assert.IsNotNull(model);

            GUIElement? instance;
            instance = Activator.CreateInstance(
                type: classType,
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: new object[] { model },
                culture: null
            ) as GUIElement;

            Assert.IsNotNull(instance);
            instance.Load(ContentManager);
        }
        private class MockContentManagerRoot : IServiceProvider
        {
            public object GetService(Type serviceType) => new();
        }

        private class MockContentManager : ContentManager
        {
            public MockContentManager(IServiceProvider serviceProvider, string rootDirectory)
                : base(serviceProvider, rootDirectory) { }

            public override T Load<T>(string assetName)
            {
                try
                {
                    return base.Load<T>(assetName);
                }
                catch (InvalidOperationException e)
                {
                    Assert.AreEqual(e.Message, "No Graphics Device Service");
#pragma warning disable CS8603
                    return default;
#pragma warning restore CS8603
                }
            }
        }
    }
}