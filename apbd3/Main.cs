namespace KonteneryApp;

public class MainClass
{
    public static void Main(String[] args)
    {
        Produkt Rybka = new Produkt("Frozen fish", -10, 20);
        Produkt Rybka2 = new Produkt("Frozen fish", -10, 10);
        
        //Stworzenie kontenera danego typu
        KontenerC kontenerC = new KontenerC(10,10,10,40, -10  );
        
        //Załadowanie ładunku do danego kontenera
        kontenerC.AddToContainer(Rybka);
        kontenerC.AddToContainer(Rybka2);
        
        //Wypisanie informacji o danym kontenerze
        Console.WriteLine(kontenerC.ToString());
        
        Produkt hel = new Produkt("hel", 20, 20);
        KontenerG kontenerG = new KontenerG(10, 10, 10, 50, 2.5);
        kontenerG.AddToContainer(hel);
        
        Produkt mleko = new Produkt("mleko", -5, 50);
        Produkt kwas = new Produkt("H2SO4", 1, 10);
        KontenerL kontenerL1 = new KontenerL(10, 20, 10, 60, "bezpieczny");
        KontenerL kontenerL2 = new KontenerL(10, 15, 10, 20, "niebezpieczny");
        kontenerL1.AddToContainer(mleko);
        kontenerL2.AddToContainer(kwas);
        List<Kontener> kontenerLs = new List<Kontener>();
        kontenerLs.Add(kontenerL1);
        kontenerLs.Add(kontenerL2);
        
        
        Kontenerowiec kontenerowiec = new Kontenerowiec(8, 10, 2);
        
        //Załadowanie kontenera na statek
        kontenerowiec.AddKontener(kontenerC);
        kontenerowiec.AddKontener(kontenerG);
        
        //Załadowanie listy kontenerów na statek
        kontenerowiec.AddKontenersList(kontenerLs);

        //Wypisanie informacji o danym statku i jego ładunku
        Console.WriteLine(kontenerowiec.ToString());

        //Usunięcie kontenera ze statku
        kontenerowiec.RemoveKontener(kontenerL1);
        
        Console.WriteLine(kontenerowiec.ToString());

        //Rozładowanie kontenera
       kontenerC.EmptyContainer();
       
       Console.WriteLine(kontenerowiec.ToString());

       
       Produkt Wodór = new Produkt("Wodór", -30, 100);
       Produkt Azot = new Produkt("Azot", -130, 30);

       KontenerG kontenerG2 = new KontenerG(10, 30, 10, 180, 1.2);
       kontenerG2.AddToContainer(Wodór);
       kontenerG2.AddToContainer(Azot);
       
       kontenerowiec.AddKontener(kontenerG2);
       
       Console.WriteLine(kontenerowiec.ToString());
       
       Produkt Wodór2 = new Produkt("Wodór", -30, 120);
       Produkt Azot2 = new Produkt("Azot", -130, 60);

       KontenerG kontenerG3 = new KontenerG(10, 30, 10, 180, 1.2);
       kontenerG3.AddToContainer(Wodór2);
       kontenerG3.AddToContainer(Azot2);
       
       //Zastąpienie kontenera na statku o danym numerze innym kontenerem
       kontenerowiec.ReplaceKontener(2, kontenerG3);
       
       Console.WriteLine(kontenerowiec.ToString());
       
       //Przeniesienie kontenera między dwoma statkami
       Kontenerowiec kontenerowiec2 = new Kontenerowiec(3, 30, 0.5);
       kontenerowiec.MoveKontener(kontenerL2, kontenerowiec2);
       Console.WriteLine(kontenerowiec.ToString());
       Console.WriteLine(kontenerowiec2.ToString());
       
       
       
       








    }
}