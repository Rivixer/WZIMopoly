#region Using Statements
using WebSocketSharp.Server;
#endregion

namespace WZIMopolyServer
{
    internal class Server
    {
        /// <value>
        /// Represents a <c>WebSocket</c> server.
        /// </value>
        internal static WebSocketServer wssv = new("ws://0.0.0.0:8765");

        /// <value>
        /// Represents a list of active lobby codes.
        /// </value>
        internal static List<string> LobbyCodes = new();

        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            wssv.AddWebSocketService<Root>("/");
            wssv.Start();
            Console.WriteLine("Server started. Press any key to stop.");
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}