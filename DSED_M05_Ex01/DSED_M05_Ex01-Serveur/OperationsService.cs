namespace DSED_M05_Model;

public class OperationsService : IOperationsService
{
    public float Additionner(float p1, float p2)
    {
        if (p1 < 0 || p2 < 0){throw new ArgumentOutOfRangeException("ne peut etre negatif");}
        
        return p1 + p2;
    }

    public float Soustraire(float p1, float p2)
    {
        if (p1 < 0 || p2 < 0){throw new ArgumentOutOfRangeException("ne peut etre negatif");}
        
        return p1 - p2;
    }

    public float Multiplier(float p1, float p2)
    {
        if (p1 < 0 || p2 < 0){throw new ArgumentOutOfRangeException("ne peut etre negatif");}
        
        return p1 * p2;    
    }

    public float Diviser(float p1, float p2)
    {
        if (p1 < 0 || p2 < 0){throw new ArgumentOutOfRangeException("ne peut etre negatif");}
        
        return p1 / p2;    }

    public float RacineCarrer(float p1)
    {
        if (p1 < 0 ){throw new ArgumentOutOfRangeException("ne peut etre negatif");}
        
        float racine = (float)Math.Round(Math.Sqrt(p1),2);
        
        return racine;
    }

    public string Echo(string echo)
    {
        return echo;
    }
}