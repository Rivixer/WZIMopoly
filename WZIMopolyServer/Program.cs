using WebSocketSharp.Server;

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
            wssv.AddWebSocketService<RootBehavior>("/");
            wssv.Start();
            string logo = @"
$$\      $$\ $$$$$$$$\ $$$$$$\ $$\      $$\                               $$\           
$$ | $\  $$ |\____$$  |\_$$  _|$$$\    $$$ |                              $$ |          
$$ |$$$\ $$ |    $$  /   $$ |  $$$$\  $$$$ | $$$$$$\   $$$$$$\   $$$$$$\  $$ |$$\   $$\ 
$$ $$ $$\$$ |   $$  /    $$ |  $$\$$\$$ $$ |$$  __$$\ $$  __$$\ $$  __$$\ $$ |$$ |  $$ |
$$$$  _$$$$ |  $$  /     $$ |  $$ \$$$  $$ |$$ /  $$ |$$ /  $$ |$$ /  $$ |$$ |$$ |  $$ |
$$$  / \$$$ | $$  /      $$ |  $$ |\$  /$$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |$$ |  $$ |
$$  /   \$$ |$$$$$$$$\ $$$$$$\ $$ | \_/ $$ |\$$$$$$  |$$$$$$$  |\$$$$$$  |$$ |\$$$$$$$ |
\__/     \__|\________|\______|\__|     \__| \______/ $$  ____/  \______/ \__| \____$$ |
                                                      $$ |                    $$\   $$ |
                                                      $$ |                    \$$$$$$  |
 $$$$$$$\  $$$$$$\   $$$$$$\ $$\    $$\  $$$$$$\   $$$$$$\                     \______/ 
$$  _____|$$  __$$\ $$  __$$\\$$\  $$  |$$  __$$\ $$  __$$\                             
\$$$$$$\  $$$$$$$$ |$$ |  \__|\$$\$$  / $$$$$$$$ |$$ |  \__|                            
 \____$$\ $$   ____|$$ |       \$$$  /  $$   ____|$$ |                                  
$$$$$$$  |\$$$$$$$\ $$ |        \$  /   \$$$$$$$\ $$ |                                  
\_______/  \_______|\__|         \_/     \_______|\__|";
            Console.WriteLine(logo);
            Console.WriteLine("\nServer started. Press any key to stop.");
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}