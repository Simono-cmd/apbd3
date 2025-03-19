namespace KonteneryApp;

public class MainClass
{
    public static void Main(String[] args)
    {
        Produkt produkt = new Produkt("Hel", 30, 1);
        KontenerG kontenerG = new KontenerG(1, 1, 1, 10, 1);
        kontenerG.AddToContainer(produkt);
        kontenerG.EmptyContainer();
        Console.WriteLine(kontenerG.MasaLadunku);
    }
}