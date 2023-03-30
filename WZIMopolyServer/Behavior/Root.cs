#region Using Statements
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using WZIMopolyNetworkingLibrary;
#endregion

namespace WZIMopolyServer
{
    class Root : WebSocketBehavior
    {
        /// <summary>
        /// Represents the <see cref="Random"/> class instance.
        /// </summary>
        private static readonly Random random = new();

        /// <inheritdoc/>
        protected override void OnOpen()
        {
            Console.WriteLine($"Connection opened on root: {ID}");
        }

        /// <inheritdoc/>
        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine($"Connection closed on root: {ID} ({e.Code})");
        }

        /// <inheritdoc/>
        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            Console.WriteLine($"Connection errored on root: {ID}");
            Console.WriteLine(e.Message);
        }

        /// <inheritdoc/>
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
        /// Creates a new lobby.<br/>
        /// Adds a new <c>WebSocketService</c> to <see cref="Server.wssv"/> with the given code.<br/>
        /// Adds the lobby code to <see cref="Server.LobbyCodes"/> list.
        /// </summary>
        /// <param name="code">
        /// The lobby code.
        /// </param>
        private static void CreateNewLobby(string code)
        {
            Server.wssv.AddWebSocketService<Lobby>($"/{code}");
            Server.LobbyCodes.Add(code);
        }

        /// <summary>
        /// Sends the lobby's code to the client.
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
