using System.Text.RegularExpressions;

namespace KonteneryApp;

public abstract class Kontener
{

    public double MasaLadunku { get; set; }
    public double Wysokosc { get; set; }
    public double WagaWlasna { get; set; }
    public double Glebokosc { get; set; }
    public double Ladownosc { get; set; }
    public string SerialNumber { get; protected set; } 
    public string Typ { get; protected set; } 

    private static int counter = 1;

    protected Kontener(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc)
    {
        this.Wysokosc = wysokosc;
        this.WagaWlasna = wagaWlasna;
        this.Glebokosc = glebokosc;
        this.Ladownosc = ladownosc;
        this.MasaLadunku = 0;
    }

    protected static int NextContainer()
    {
        return counter++;
    }

    public double GetTotalWeight()
    {
        return WagaWlasna + MasaLadunku;
    }
    public void EmptyContainer()
    {
        this.MasaLadunku = 0;
    }

    public abstract void AddToContainer(Produkt produkt);
    
    public virtual string ToString()
    {
        return $"SerialNumber: {SerialNumber}, Typ: {Typ}, Ladownosc: {Ladownosc} kg, Masa własna: {WagaWlasna}, Masa Ładunku: {MasaLadunku} kg";
    }
}

// 🔹 Kontener na płyny
class KontenerL : Kontener, IHazardNotifier
{
    public string TypLadunku { get; set; }

    public KontenerL(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc, string typLadunku) 
        : base(wysokosc, wagaWlasna, glebokosc, ladownosc)
    {
        this.TypLadunku = typLadunku;
        this.Typ = "L";
        this.SerialNumber = "KON-" + Typ + "-" + NextContainer();
    }

    public void Alert()
    {
        Console.WriteLine("Niebezpieczna sytuacja w kontenerze: " + SerialNumber);
    }

    public override void AddToContainer(Produkt produkt)
    {
        string pattern = "^nie.*";
        Regex rg = new Regex(pattern);
        if (rg.IsMatch(TypLadunku))
        {
            if ((MasaLadunku + produkt.Mass) <= (Ladownosc * 0.5))
            {
                MasaLadunku += produkt.Mass;
            }
            else
            {
                Alert();
                throw new OverfillException("Próbujesz przeładować kontener (max 50%)");
            }
        }
        else
        {
            if ((MasaLadunku + produkt.Mass) <= (Ladownosc * 0.9))
            {
                MasaLadunku += produkt.Mass;
            }
            else
            {
                Alert();
                throw new OverfillException("Próbujesz przeładować kontener (max 90%)");
            }
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Typ Ładunku: {TypLadunku}";
    }
}

// 🔹 Kontener na gaz
class KontenerG : Kontener, IHazardNotifier
{
    public double Cisnienie { get; set; }

    public KontenerG(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc, double cisnienie) 
        : base(wysokosc, wagaWlasna, glebokosc, ladownosc)
    {
        this.Cisnienie = cisnienie;
        this.Typ = "G";
        this.SerialNumber = "KON-" + Typ + "-" + NextContainer();
    }

    public void Alert()
    {
        Console.WriteLine("Niebezpieczna sytuacja w kontenerze: " + SerialNumber);
    }

    public new void EmptyContainer()
    {
        this.MasaLadunku *= 0.05;
    }

    public override void AddToContainer(Produkt produkt)
    {
        if ((MasaLadunku + produkt.Mass) <= Ladownosc)
        {
            MasaLadunku += produkt.Mass;
        }
        else
        {
            throw new OverfillException("Próbujesz przeładować kontener");
        }
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}, Ciśnienie: {Cisnienie} atm";
    }
}

//chłodniczy
class KontenerC : Kontener
{
    public Produkt Produkt { get; private set; }
    public double Temperatura { get; set; }
    public KontenerC(double wysokosc, double wagaWlasna, double glebokosc, double ladownosc, double temperatura) 
        : base(wysokosc, wagaWlasna, glebokosc, ladownosc)
    {
        this.Temperatura = temperatura;
        this.Typ = "C";
        this.SerialNumber = "KON-" + Typ + "-" + NextContainer().ToString();
    }

    public override void AddToContainer(Produkt produkt)
    {
        if (Produkt == null)
        {
            Produkt = produkt;
        }
        if (!produkt.Name.Equals(Produkt.Name, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Kontener nie może przechowywać różnych typów produktów");
        }
        if (produkt.Temperature < Temperatura)
        {
            throw new InvalidOperationException("Temperatura w kontenerze jest za wysoka");
        }
        if ((MasaLadunku + produkt.Mass) <= Ladownosc)
        {
            MasaLadunku += produkt.Mass;
        }
        else
        {
            throw new OverfillException("Próbujesz przeładować kontener");
        }
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}, Produkt: {Produkt.Name}, Temperatura: {Temperatura}";
    }
}


