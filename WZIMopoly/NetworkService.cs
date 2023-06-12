using System;
using System.IO;
using System.Xml;
using WebSocket4Net;

namespace WZIMopoly
{
    /// <summary>
    /// Represents the type of connection.
    /// </summary>
    internal enum ConnectionType
    {
        /// <summary>
        /// The connection is not established.
        /// </summary>
        None,

        /// <summary>
        /// The connection is established to the server root.
        /// </summary>
        Root,

        /// <summary>
        /// The connection is established to the server lobby.
        /// </summary>
        Lobby,

        /// <summary>
        /// The connection is being established to the server root.
        /// </summary>
        ConnectingToRoot,

        /// <summary>
        /// The connection is being established to the server lobby.
        /// </summary>
        ConnectingToLobby,
    }

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
        /// Gets the current connection.
        /// </summary>
        public static WebSocket Connection { get; private set; }

        /// <summary>
        /// Gets the type of the current connection.
        /// </summary>
        public static ConnectionType Type { get; private set; }

        /// <summary>
        /// Gets the address of the server root.
        /// </summary>
        private static string WSAddress => _wsAddress ?? LoadAddress();

        /// <summary>
        /// Closes the current connection.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="Connection"/> to null
        /// and <see cref="Type"/> to <see cref="ConnectionType.None"/>.
        /// </remarks>
        internal static void CloseCurrentConnection()
        {
            Connection.Close();
            Type = ConnectionType.None;
            Connection = null;
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
        /// While connecting, <see cref="Type"/> will be set to
        /// <see cref="ConnectionType.ConnectingToRoot"/>.
        /// </para>
        /// <para>
        /// After connecting, <see cref="Type"/> will be set to
        /// <see cref="ConnectionType.Root"/>.
        /// </para>
        /// <para>
        /// If the connection cannot be established,
        /// <see cref="Connection"/> will be set to null
        /// and <see cref="Type"/> will be set
        /// to <see cref="ConnectionType.None"/>.
        /// </para>
        /// </remarks>
        internal static void SwitchToRoot()
        {
            if (Connection != null)
            {
                CloseCurrentConnection();
            }
            Type = ConnectionType.ConnectingToRoot;
            if (Connect(WSAddress))
            {
                Type = ConnectionType.Root;
            }
        }

        /// <summary>
        /// Switches to the server lobby.
        /// </summary>
        /// <param name="code">
        /// The code of the lobby to connect to.
        /// </param>
        /// <remarks>
        /// <para>
        /// While connecting, <see cref="Type"/> will be set to
        /// <see cref="ConnectionType.ConnectingToLobby"/>.
        /// </para>
        /// <para>
        /// After connecting, <see cref="Type"/> will be set to
        /// <see cref="ConnectionType.Lobby"/>.
        /// </para>
        /// <para>
        /// If the connection cannot be established,
        /// <see cref="Connection"/> will be set to null
        /// and <see cref="Type"/> will be set
        /// to <see cref="ConnectionType.None"/>.
        /// </para>
        /// </remarks>
        internal static void SwitchToLobby(string code)
        {
            if (Connection != null)
            {
                CloseCurrentConnection();
            }
            Type = ConnectionType.ConnectingToLobby;
            if (Connect($"{WSAddress}/{code}"))
            {
                Type = ConnectionType.Lobby;
            }
        }

        /// <summary>
        /// Connects to the websocket server.
        /// </summary>
        /// <param name="address">
        /// The address of the server to connect to.
        /// </param>
        /// <returns>
        /// True if the connection was established successfully,
        /// otherwise false.
        /// </returns>
        /// <remarks>
        /// If the connection cannot be established,
        /// <see cref="Connection"/> will be set to null
        /// and <see cref="Type"/> will be set
        /// to <see cref="ConnectionType.None"/>.
        /// </remarks>
        private static bool Connect(string address)
        {
            Connection = new WebSocket(address);
            Connection.Open();
            var now = DateTime.Now;
            while (true)
            {
                if (Connection is null || DateTime.Now > now.AddSeconds(3))
                {
                    Connection = null;
                    Type = ConnectionType.None;
                    return false;
                }
                if (Connection is not null && Connection.State == WebSocketState.Open)
                {
                    return true;
                }
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
#if RELEASE
            try
            {
#endif
                var xml = new XmlDocument();
#if WINDOWS
                string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Properties", "Config.xml");
                xml.Load(xmlFilePath);
#elif LINUX
                xml.Load("WZIMopoly/Properties/Config.xml");
#endif
                XmlNode addressNode = xml.DocumentElement.SelectSingleNode("/Config/WebSocketAddress");
                _wsAddress = $"ws://{addressNode.InnerText}";
                return _wsAddress;
#if RELEASE
            }
            catch (Exception)
            {
                Connection = null;
                return null;
            }
#endif
        }
    }
}
