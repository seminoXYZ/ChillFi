using UnityEngine.UI;

public class UIManager
{
    private static UIManager _instance;

    public static UIManager Instance =>
        _instance ?? (_instance = new UIManager());

    private Text _chatText;
    private UIManager()
    {
    }

    public void Initialize(Text chatText)
    {
        this._chatText = chatText;
    }

    public void Refresh()
    {
    }

    public void UpdateChatText(string chatText)
    {
        this._chatText.text = chatText;
    }
}