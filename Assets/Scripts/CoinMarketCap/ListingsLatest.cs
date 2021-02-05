[System.Serializable]
public struct ListingsLatest
{
    public Listings listings;
}

[System.Serializable]
public struct Listings
{
    public Status status;
    public ListingData[] data;
}

[System.Serializable]
public struct Status
{

}

[System.Serializable]
public struct ListingData
{
    public string symbol;
    public ListingQuote quote;
}

[System.Serializable]
public struct ListingQuote
{
    public USD USD;
}

[System.Serializable]
public struct USD
{
    public float price;
    public float volume_24h;
    public float percent_change_1h;
    public float percent_change_24h;
    public float percent_change_7d;
    public float percent_change_30d;
    public float market_cap;
}
