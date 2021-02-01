using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PolygonMessageSystem : Message
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:LobbyMessaging"/> class.
    /// </summary>
    /// <param name="client">Client.</param>
    public PolygonMessageSystem(PolygonService client) : base(client) { }

    // Register messages
    public const string Register = "register";
    public UnityAction OnConnectedToServer;

    // Echo messages
    public const string Create = "create";
    public UnityAction<RequestMessage> OnRequestMessage;

    /// <summary>
    /// Sends echo message to the server.
    /// </summary>
    /// <param name="request">Request.</param>
    public void SendRequest(RequestMessage request)
    {
        string msg = JsonUtility.ToJson(request);
        Debug.Log(msg);
        msg = msg.Replace("\"_", "\"");
        Debug.Log(msg);
        client.SendRequest(msg);
    }

    /// <summary>
    /// Sends echo message to the server.
    /// </summary>
    /// <param name="request">Request.</param>
    public void SendPredefinedRequest(string request)
    {
        client.SendRequest(request);
    }
}
