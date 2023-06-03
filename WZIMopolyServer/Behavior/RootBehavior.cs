using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using WZIMopolyNetworkingLibrary;

namespace WZIMopolyServer
{
    /// <summary>
    /// Represents the root behavior class.
    /// </summary>
    class RootBehavior : WebSocketBehavior
    {
        /// <summary>
        /// Represents the <see cref="Random"/> class instance.
        /// </summary>
        private static readonly Random random = new();

        /// <summary>
        /// Called when the <see cref="WebSocket"/> used
        /// in a session has been established.
        /// </summary>
        /// <remarks>
        /// Adds the client to the lobby.
        /// </remarks>
        protected override void OnOpen()
        {
            Console.WriteLine($"Connection opened on root: {ID}");
        }

        /// <summary>
        /// Called when the <see cref="WebSocket"/> connection
        /// used in a session has been closed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="CloseEventArgs"/> class.
        /// </param>
        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine($"Connection closed on root: {ID} ({e.Code})");
        }

        /// <summary>
        /// Called when the <see cref="WebSocket"/> used
        /// in a session gets an error.
        /// </summary>
        /// <param name="e">
        /// The <see cref="WebSocketSharp.ErrorEventArgs"/> class.
        /// </param>
        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            Console.WriteLine($"Connection errored on root: {ID}");
            Console.WriteLine(e.Message);
        }

        /// <summary>
        /// Called when the <see cref="WebSocket"/> used
        /// in a session receives a message.
        /// </summary>
        /// <param name="e">
        /// The <see cref="MessageEventArgs"/> class.
        /// </param>
        protected override void OnMessage(MessageEventArgs e)
        {
            var type = (PacketType)e.RawData[0];
            if (type == PacketType.NewLobby)
            {
                string code = GenerateLobbyCode();
                CreateNewLobby(code);
                SendLobbyCode(code);
            }
        }

        /// <summary>
        /// Generates a unique 6-digit random lobby code.
        /// </summary>
        /// <returns>
        /// The generated code.
        /// </returns>
        private static string GenerateLobbyCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[6];
            string code;

            do
            {
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                code = new string(stringChars);
            } while (Server.LobbyCodes.Contains(code));

            return code;
        }

        /// <summary>
        /// Creates a new lobby.
        /// </summary>
        /// <param name="code">
        /// The lobby code.
        /// </param>
        /// <remarks>
        /// <para>
        /// Adds a new <c>WebSocketService</c> to <see cref="Server.wssv"/>
        /// with the given code.
        /// </para>
        /// <para>
        /// Adds the lobby code to <see cref="Server.LobbyCodes"/> list.
        /// </para>
        /// </remarks>
        private static void CreateNewLobby(string code)
        {
            Server.wssv.AddWebSocketService<LobbyBehavior>($"/{code}");
            Server.LobbyCodes.Add(code);
        }

        /// <summary>
        /// Sends the lobby code to the client.
        /// </summary>
        /// <param name="code">
        /// The lobby code.
        /// </param>
        private void SendLobbyCode(string code)
        {
            byte[] data = Encoding.ASCII.GetBytes(code);
            Send(data);
        }
    }
}
