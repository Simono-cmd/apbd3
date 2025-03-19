namespace KonteneryApp;

public class MainClass
{
    public static void Main(String[] args)
    {
        Produkt Rybka = new Produkt("Frozen fish", -10, 20);
        Produkt Rybka2 = new Produkt("Frozen fish", -10, 10);
        KontenerC kontenerC = new KontenerC(10,10,10,40, -10  );
        kontenerC.AddToContainer(Rybka);
        kontenerC.AddToContainer(Rybka2);
        
        Produkt hel = new Produkt("hel", 20, 20);
        KontenerG kontenerG = new KontenerG(10, 10, 10, 50, 2.5);
        kontenerG.AddToContainer(hel);

        
        Kontenerowiec kontenerowiec = new Kontenerowiec(10, 10, 500);
        kontenerowiec.AddKontener(kontenerC);
        kontenerowiec.AddKontener(kontenerG);
        Console.WriteLine(kontenerowiec.ToString());
    }
}