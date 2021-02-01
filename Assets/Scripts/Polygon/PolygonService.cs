using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Forefront class for the server communication.
/// </summary>
///
public struct AuthData
{
    public Vector3 position;
    public Material mat;
    public GameObject prefab;
    public UnityAction action;
}

public class PolygonService : MonoBehaviour
{
    // Server IP address
    [SerializeField]
    private string clusterURL;

    //Polygon API Token
    [SerializeField]
    private string APIKey;


    // WebSocket Client
    private NetworkClient client;
    
    public PolygonMessageSystem polygonMessage;
    [SerializeField]
    private string predefinedMessage;

    private void Awake()
    {
        client = new NetworkClient(clusterURL);
        polygonMessage = new PolygonMessageSystem(this);
        polygonMessage.OnRequestMessage += OnRequestReceived;
    }

    private void Start()
    {
        Connect();
    }

    /// <summary>
    /// Unity method called every frame
    /// </summary>
    private void Update()
    {
        // Check if server send new messages
        var cqueue = client.receiveQueue;
        string msg;
        while (cqueue.TryPeek(out msg))
        {
            // Parse newly received messages
            cqueue.TryDequeue(out msg);
            HandleMessage(msg);
        }
    }
    /// <summary>
    /// Method responsible for handling server messages
    /// </summary>
    /// <param name="msg">Message.</param>
    private void HandleMessage(string msg)
    {
        Debug.Log("Server: " + msg);
        
        msg = msg.Substring(1);
        msg = msg.Remove(msg.Length - 1);

        PolygonMessage message = JsonUtility.FromJson<PolygonMessage>(msg);

        if(message.ev == "status" && message.status == "connected")
        {
            SendAuthRequest();
        }

        Debug.Log(message.ev);
        Debug.Log(message.status);
        Debug.Log(message.message);
    }
    /// <summary>
    /// Call this method to connect to the server
    /// </summary>
    public async void ConnectToServer()
    {
        await client.Connect();
    }
    /// <summary>
    /// Method which sends data through websocket
    /// </summary>
    /// <param name="message">Message.</param>
    public void SendRequest(string message)
    {
        client.Send(message);
    }

    [ContextMenu("Connect")]
    private void Connect()
    {
        ConnectToServer();
    }
    
    [ContextMenu("Send Auth Request")]
    private void SendAuthRequest()
    {
        RequestMessage authRequest = new RequestMessage();
        authRequest.action = "auth";
        authRequest._params = APIKey;
        polygonMessage.SendRequest(authRequest);
    }

    [ContextMenu("Send Predefined Request")]
    private void SendPredefinedRequest()
    {
        polygonMessage.SendPredefinedRequest(predefinedMessage);
    }

    void OnRequestReceived(RequestMessage message)
    {
        Debug.Log(message.action);
        Debug.Log(message._params);
    }
}