namespace KonteneryApp;

public class MainClass
{
    public static void Main(String[] args)
    {
        Produkt Rybka = new Produkt("Frozen fish", -10, 20);
        Produkt Rybka2 = new Produkt("Frozen fish", -10, 30);
        KontenerC kontenerC = new KontenerC(10,10,10,40, -10  );
        kontenerC.AddToContainer(Rybka);
        kontenerC.AddToContainer(Rybka2);
    }
}