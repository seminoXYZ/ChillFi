[System.Serializable]
public struct Orderbook
{
    public string quoteSymbol;
    public string baseSymbol;
    public Orders[] orderBooks;
}