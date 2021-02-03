/// <summary>
/// Base class for message types.
/// </summary>
public class Message
{
    // Reference to the server communication.
    protected PolygonService client;
    /// <summary>
    /// Initializes a new instance of the <see cref="T:BaseMessaging"/> class.
    /// </summary>
    /// <param name="client">Client.</param>
    public Message(PolygonService client)
    {
        this.client = client;
    }
}