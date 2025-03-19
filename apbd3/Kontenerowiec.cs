using System.Text;

namespace KonteneryApp;

public class Kontenerowiec
{
    private static int ID = 1;
    private Kontener[] Konteners;
    public double MaxVelocity;
    public long MaxContainers;
    public double MaxLoad;
    public double CurrentLoad;
    public Kontenerowiec(long maxContainers, double maxVelocity, double maxLoad)
    {
        MaxVelocity = maxVelocity;
        MaxContainers = maxContainers;
        MaxLoad = maxLoad;
        CurrentLoad = 0;
        
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
                CurrentLoad += kontenerLoad;
                return;
            }
        }
        Console.WriteLine("No more space for new containers!");
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"\n-> Kontenerowiec: {getID()}, Max Containers: {MaxContainers}, Max Velocity: {MaxVelocity} kn, Max Load: {MaxLoad} kg");
        
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