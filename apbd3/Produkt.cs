namespace KonteneryApp;

public class Produkt
{
    public string Name;
    public double Temperature;
    public double Mass;

    public Produkt(string name, double temperature, double mass)
    {
        Name = name;
        Temperature = temperature;
        Mass = mass;
    }
}