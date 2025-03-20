using System.Text;

namespace KonteneryApp;

public class Kontenerowiec
{
    private static int ID = 0;
    private Kontener[] Konteners;
    public double MaxVelocity;
    public long MaxContainers;
    public double MaxLoad;
    public double CurrentLoad
    {
        get
        {
            double sum = 0;
            foreach (var kontener in Konteners)
            {
                if (kontener != null)
                {
                    sum += kontener.GetTotalWeight(); 
                }
            }
            return sum;
        }
        set{}
    }
    public Kontenerowiec(long maxContainers, double maxVelocity, double maxLoad)
    {
        MaxVelocity = maxVelocity;
        MaxContainers = maxContainers;
        MaxLoad = maxLoad*1000;
        getID();
        Konteners = new Kontener[maxContainers];
    }

    private static int getID()
    {
        return ID++;
    }
    
    public void AddKontener(Kontener kontener)
    {
        double kontenerLoad = kontener.GetTotalWeight();

        if (kontenerLoad + CurrentLoad > MaxLoad)
        {
            throw new OverfillException($"Próbujesz przepełnić kontenerowiec nr {ID.ToString()}");
        }
        for (int i = 0; i < Konteners.Length; i++)
        {
            if (Konteners[i] == null)
            {
                Konteners[i] = kontener;
                return;
            }
        }
        Console.WriteLine("No more space for new containers!");
    }
    
    public void AddKontenersList(List<Kontener> kontenersList)
    {
        double totalWeightToAdd = 0;
        

        if (totalWeightToAdd + CurrentLoad > MaxLoad)
        {
            throw new OverfillException($"Próbujesz przepełnić kontenerowiec nr {ID.ToString()}");
        }

        foreach (var kontener in kontenersList)
        {
            for (int i = 0; i < Konteners.Length; i++)
            {
                if (Konteners[i] == null)
                {
                    Konteners[i] = kontener;
                    break;
                }
            }
        }
    }

    public bool RemoveKontener(Kontener kontener)
    {
        for (int i = 0; i < Konteners.Length; i++)
        {
            if (Konteners[i] == kontener)
            {
                Konteners[i] = null;
                Console.WriteLine($"Kontener {kontener.SerialNumber} został usunięty ze statku {ID.ToString()}");
                return true;
            }
        }
        Console.WriteLine("Nie ma tego kontenera na tym statku");
        return false;
    }

    public void MoveKontener(Kontener kontener, Kontenerowiec destination)
    {
        if (RemoveKontener(kontener))
        {
            try
            {
                destination.AddKontener(kontener);  
                Console.WriteLine($"Przeniesiono kontener {kontener}");
            }
            catch (OverfillException ex)
            {
                AddKontener(kontener);
                Console.WriteLine($"Błąd: {ex.Message}. Kontener nie został przeniesiony.");
            }
        }
        else
        {
            Console.WriteLine("Kontener nie został znaleziony na statku.");
        }
    }
    
    public bool ReplaceKontener(int index, Kontener newKontener)
    {
        if (index < 0 || index >= Konteners.Length)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenera.");
            return false;
        }
        
        if (Konteners[index] == null)
        {
            Console.WriteLine("Brak kontenera na podanym indeksie.");
            return false;
        }
        
        double oldKontenerWeight = Konteners[index].GetTotalWeight();
        
        if (CurrentLoad - oldKontenerWeight + newKontener.GetTotalWeight() > MaxLoad)
        {
            Console.WriteLine("Próba przekroczenia maksymalnej wagi kontenerowca.");
            return false;
        }
        
        Konteners[index] = newKontener;
        

        Console.WriteLine($"Kontener o indeksie {index} został zastąpiony nowym kontenerem {newKontener.SerialNumber}");
        return true;
    }
    
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"\n-> Kontenerowiec: {Kontenerowiec.ID.ToString()}, Max Containers: {MaxContainers}, Max Velocity: {MaxVelocity} kn, Max Load: {MaxLoad/1000} tons, Current Load: {CurrentLoad}");
        
        sb.AppendLine("List of Containers:");
        foreach (var kontener in Konteners)
        {
            if (kontener != null)
            {
                sb.AppendLine('\t' + kontener.ToString());
            }
            else
            {
                sb.AppendLine("\t-- Empty slot --");
            }
        }
        return sb.ToString();
    }
    
}