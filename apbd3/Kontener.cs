using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace KonteneryApp;

public abstract class Kontener
{
    protected double masaLadunku { get; set; }
    protected double wysokosc;
    protected double wagaWlasna;
    protected double glebokosc;
    protected double ladownosc;
    protected String serialnumber;
    static private int counter = 1;
    protected String typ ="";

     protected Kontener(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc)
     {
         this.wysokosc = wysokosc;
         this.wagaWlasna = wagaWlasna;
         this.glebokosc = glebokosc;
         this.ladownosc = ladownosc;
         masaLadunku = 0;
     }

     static protected int nextContainer()
    {
        return counter++;
    }

    public void EmptyContainer()
    {
        masaLadunku = 0;
    }

    public abstract void AddToContainer(String product, double mass);


}

//na płyny
class KontenerL : Kontener, IHazardNotifier
{
    String typladunku = "";
    
    public KontenerL(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc, String typladunku) : base(wysokosc, wagaWlasna, glebokosc, ladownosc)
    {
        this.typladunku = typladunku;
        typ = "L";
        serialnumber = "KON-"+typ+"-"+nextContainer().ToString();
    }

    public void Alert()
    {
        Console.WriteLine("Niebezpieczna sytuacja w kontenerze: "+serialnumber);
    }
    
    
    public override void AddToContainer(String product, double mass)
    {
        string pattern = "^nie";
        Regex rg = new Regex(pattern);
        if (rg.IsMatch(typladunku))
        {
            if ((masaLadunku+mass) < (ladownosc*0.5))
            {
                masaLadunku += mass;
            }
            else
            {
                Alert();
                throw new OverfillException("Próbujesz przeładować kontener (max 90%)");
            }
        }
        else
        {
            if ((masaLadunku+mass) < (ladownosc*0.9))
            {
                masaLadunku += mass;
            }
            else
            {
                Alert();
                throw new OverfillException("Próbujesz przeładować kontener (max 90%)");
            }
        }
    }
    
    
}

//na gaz
class KontenerG : Kontener
{
    public KontenerG(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc) : base(wysokosc, wagaWlasna, glebokosc, ladownosc)
    {
        typ = "G";
        serialnumber = "KON-"+typ+"-"+nextContainer().ToString();

    }
    
    public override void AddToContainer(String product, double mass)
    {
       
    }
    
}

//chłodniczy
class KontenerC : Kontener
{
    public KontenerC(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc) : base(wysokosc, wagaWlasna, glebokosc, ladownosc)
    {
        typ = "C";
        serialnumber = "KON-"+typ+"-"+nextContainer().ToString();
    }
    
    public override void AddToContainer(String product, double mass)
    {
        if ((masaLadunku+mass) < ladownosc)
        {
            masaLadunku += mass;
        }
        else
        {
            throw new OverfillException("Próbujesz przeładować kontener");
        }
    }
    
}