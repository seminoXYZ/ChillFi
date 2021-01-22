public class ShelbyManager
{
    private static ShelbyManager _instance;

    public static ShelbyManager Instance =>
        _instance ?? (_instance = new ShelbyManager());

    private ShelbyManager()
    {
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
    }
}