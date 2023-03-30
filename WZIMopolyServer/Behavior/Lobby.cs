#region Using Statements
using WebSocketSharp;
using WebSocketSharp.Server;
#endregion

namespace WZIMopolyServer
{
    class Lobby : WebSocketBehavior, IDisposable
    {
        /// <summary>
        /// Represents a dictionary of connected clients,<br/>
        /// where the key is the client's session ID
        /// and the value is the <see cref="WebSocketSharp.Net.WebSockets.WebSocketContext"/> class.
        /// </summary>
        private readonly Dictionary<string, WebSocketSharp.Net.WebSockets.WebSocketContext> _clients = new();

        #region Properties
        /// <value>
        /// Represents the absolute request uri path.
        /// </value>
        private string WebSocketPath => Context.RequestUri.AbsolutePath;

        
        /// <value>
        /// Represents the lobby code.
        /// </value>
        private string Code => WebSocketPath[1..];

        /// <value>
        /// Represents the <see cref="WebSocket"/> class.
        /// </value>
        private WebSocket WebSocket => Context.WebSocket;
        #endregion

        /// <summary>
        /// Runs <see cref="WaitForConnection"/> function on a separate thread.
        /// </summary>
        public Lobby() : base()
        {
            var thread = new Thread(new ThreadStart(WaitForConnection));
            thread.Start();
        }

        /// <summary>
        /// Waits for a connection from a client for up to 60 seconds.
        /// If the client doesn't connect within this time, the lobby is removed.
        /// </summary>
        private void WaitForConnection()
        {
            while (DateTime.Now < StartTime.AddSeconds(60))
            {
                if (_clients.Count > 0)
                {
                    return;
                }
            }
            Dispose();
        }

        /// <summary>
        /// Adds the connected client to <see cref="_clients"/>.<br/>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnOpen()
        {
            _clients.Add(ID, Context);
        }

        /// <summary>
        /// Checks if any other client is still connected.
        /// If not, the lobby is removed.<br/>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClose(CloseEventArgs e)
        {
            if (_clients.Count == 0)
            {
                Dispose();
            }
        }

        /// <summary>
        /// Removes the lobby.
        /// </summary>
        public void Dispose()
        {
            WebSocket.Close();
            Server.LobbyCodes.Remove(Code);
        }
    }

}
