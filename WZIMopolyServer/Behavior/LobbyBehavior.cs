﻿using WebSocketSharp;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;
using WZIMopolyNetworkingLibrary;

namespace WZIMopolyServer
{
    /// <summary>
    /// Represents the lobby class.
    /// </summary>
    class Lobby : IDisposable
    {
        /// <summary>
        /// Represents a dictionary of connected clients.
        /// </summary>
        /// <remarks>
        /// The key is the client's session ID
        /// and the value is the <see cref="WebSocketContext"/> class.
        /// </remarks>
        private readonly Dictionary<string, WebSocketContext> _clients = new();

        /// <summary>
        /// The lobby code.
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lobby"/> class.
        /// </summary>
        /// <param name="code">
        /// The lobby code.
        /// </param>
        public Lobby(string code)
        {
            _code = code;
        }

        /// <summary>
        /// Gets the lobby code.
        /// </summary>
        public string Code => _code;

        /// <summary>
        /// Gets a dictionary of connected clients.
        /// </summary>
        public Dictionary<string, WebSocketContext> Clients => _clients.ToDictionary(x => x.Key, x => x.Value);

        /// <summary>
        /// Sends a message to all connected clients.
        /// </summary>
        /// <param name="e">
        /// The <see cref="MessageEventArgs"/> class containing the message.
        /// </param>
        /// <param name="except">
        /// The <see cref="WebSocketContext"/> contexts to exclude from the broadcast.
        /// </param>
        public void SendAll(MessageEventArgs e, params WebSocketContext?[] except)
        {
            foreach (var client in Clients)
            {
                if (except?.Contains(client.Value) ?? false)
                {
                    continue;
                }
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Console.WriteLine($"Sending message to {client.Key}... (attempt {i + 1}/5)");
                        client.Value.WebSocket.Send(e.RawData);
                        Console.WriteLine($"Message to {client.Key} has been sent.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Sending message to {client.Key} failed! ({ex.Message})");
                    }
                }
            }
        }

        /// <summary>
        /// Adds a client to the lobby.
        /// </summary>
        /// <param name="id">
        /// The client's session ID.
        /// </param>
        /// <param name="context">
        /// The <see cref="WebSocketContext"/> class.
        /// </param>
        public void OnConnect(string id, WebSocketContext context)
        {
            _clients.Add(id, context);
        }

        /// <summary>
        /// Removes a client from the lobby.
        /// </summary>
        /// <param name="id">
        /// The client's session ID.
        /// </param>
        public void OnClose(string id)
        {
            _clients.Remove(id);
        }

        /// <summary>
        /// Deletes the lobby.
        /// </summary>
        public void Dispose()
        {
            foreach (var client in Clients)
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        client.Value.WebSocket.Close();
                        Console.WriteLine($"Connection with {client.Key} has been closed.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unable to close connection with {client.Key} (attempt {i + 1}/3) - {ex.Message}");
                    }
                }
            }

            try
            {
                LobbyBehavior.Lobbies.Remove(this);
            }
            catch
            {
                Console.WriteLine($"Lobby {Code} cannot be removed from Lobbies list.");
            }

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Server.LobbyCodes.Remove(Code);
                    Console.WriteLine($"Lobby (code: {Code} has been deleted.");
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Lobby (code: {Code}) cannot be deleted (attempt {i + 1}/3) - {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Represents the lobby behavior.
    /// </summary>
    class LobbyBehavior : WebSocketBehavior
    {
        /// <summary>
        /// The list of active lobbies.
        /// </summary>
        public static readonly List<Lobby> Lobbies = new();

        /// <summary>
        /// Called when the <see cref="WebSocket"/> used
        /// in a session receives a message.
        /// </summary>
        /// <param name="e">
        /// The <see cref="MessageEventArgs"/> class.
        /// </param>
        protected override void OnMessage(MessageEventArgs e)
        {
            string lobbyCode = Context.RequestUri.AbsoluteUri[^6..];
            Lobby? lobby = Lobbies.Find(x => x.Code == lobbyCode);
            lobby?.SendAll(e, Context);
            if ((PacketType)e.RawData[0] == PacketType.Close)
            {
                lobby?.Dispose();
            }
        }

        /// <summary>
        /// Called when the <see cref="WebSocket"/> used
        /// in a session has been established.
        /// </summary>
        /// <remarks>
        /// Adds the client to the lobby.
        /// </remarks>
        protected override void OnOpen()
        {
            string lobbyCode = Context.RequestUri.AbsoluteUri[^6..];
            Console.WriteLine($"New connection on {lobbyCode}: {ID}");
            Lobby? lobby = Lobbies.Find(x => x.Code == lobbyCode);
            if (lobby is null)
            {
                lobby = new Lobby(lobbyCode);
                Lobbies.Add(lobby);
            }
            lobby.OnConnect(ID, Context);
        }

        /// <summary>
        /// Called when the <see cref="WebSocket"/> connection
        /// used in a session has been closed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="CloseEventArgs"/> class.
        /// </param>
        /// <remarks>
        /// Sends a close packet to all connected clients
        /// and removes the lobby from the list of active lobbies.
        /// </remarks>
        protected override void OnClose(CloseEventArgs e)
        {
            string lobbyCode = Context.RequestUri.AbsoluteUri[^6..];
            Lobby? lobby = Lobbies.Find(x => x.Code == lobbyCode);

            if (lobby is not null)
            {
                foreach (var client in lobby.Clients)
                {
                    if (client.Key != ID && client.Value.WebSocket.ReadyState == WebSocketState.Open)
                    {
                        client.Value.WebSocket.Send(new byte[] { (byte)PacketType.Close });
                    }
                }
                lobby.Dispose();
            }
        }
    }
}
