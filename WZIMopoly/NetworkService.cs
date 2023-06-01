using System.Xml;
using WebSocketSharp;

namespace WZIMopoly
{
    /// <summary>
    /// Provides methods for connecting to the server.
    /// </summary>
    internal static class NetworkService
    {
        /// <summary>
        /// The address of the server root.
        /// </summary>
        private static string _wsAddress;

        /// <summary>
        /// Gets the address of the server root.
        /// </summary>
        private static string WSAddress => _wsAddress ?? LoadAddress();

        /// <summary>
        /// Connects to the server root.
        /// </summary>
        /// <remarks>
        /// If the connection cannot be established,
        /// <see cref="WZIMopoly.Network"/> will be set to null.
        /// </remarks>
        internal static void ConnectToRoot()
        {
            WZIMopoly.Network = new WebSocket(WSAddress);
            Connect();
        }

        /// <summary>
        /// Closes the current connection.
        /// </summary>
        internal static void CloseCurrentConnection()
        {
            WZIMopoly.Network.Close();
        }

        /// <summary>
        /// Connects to the server lobby.
        /// </summary>
        /// <param name="code">
        /// The code of the lobby to connect to.
        /// </param>
        internal static void ConnectToLobby(string code)
        {
            WZIMopoly.Network = new WebSocket($"{WSAddress}/{code}");
            Connect();
        }

        /// <summary>
        /// Switches to the server root.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If other connections are open,
        /// it will be closed before switching.
        /// </para>
        /// <para>
        /// If the connection cannot be established,
        /// <see cref="WZIMopoly.Network"/> will be set to null.
        /// </para>
        /// </remarks>
        internal static void SwitchToRoot()
        {
            if (WZIMopoly.Network != null)
            {
                CloseCurrentConnection();
            }
            ConnectToRoot();
        }

        /// <summary>
        /// Switches to the server lobby.
        /// </summary>
        /// <param name="code">
        /// The code of the lobby to connect to.
        /// </param>
        /// <remarks>
        /// <para>
        /// If other connections are open,
        /// it will be closed before switching.
        /// </para>
        /// <para>
        /// If the connection cannot be established,
        /// <see cref="WZIMopoly.Network"/> will be set to null.
        /// </para>
        /// </remarks>
        internal static void SwitchToLobby(string code)
        {
            if (WZIMopoly.Network != null)
            {
                CloseCurrentConnection();
            }
            ConnectToLobby(code);
        }

        /// <summary>
        /// Connects to the server initialized in <see cref="WZIMopoly.Network"/>.
        /// </summary>
        /// <remarks>
        /// If the connection cannot be established,
        /// <see cref="WZIMopoly.Network"/> will be set to null.
        /// </remarks>
        private static void Connect()
        {
            WZIMopoly.Network.Connect();
            if (!WZIMopoly.Network.IsAlive)
            {
                WZIMopoly.Network = null;
            }
        }

        /// <summary>
        /// Loads the address of the server from the config file.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="_wsAddress"/> to the address of the server.
        /// </remarks>
        /// <returns>
        /// The address of the server.
        /// </returns>
        private static string LoadAddress()
        {
            var xml = new XmlDocument();
#if WINDOWS
            xml.Load("../../../Properties/Config.xml");
#elif LINUX
            xml.Load("WZIMopoly/Properties/Config.xml");
#endif
            XmlNode addressNode = xml.DocumentElement.SelectSingleNode("/Config/WebSocketAddress");
            _wsAddress = $"ws://{addressNode.InnerText}";
            return _wsAddress;
        }
    }
}
