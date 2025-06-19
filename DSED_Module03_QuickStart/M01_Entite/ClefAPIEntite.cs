namespace M01_Entite;

public class ClefAPIEntite
{
    public Guid CleApIfId { get; set; }

    public ClefAPIEntite(Guid cleApIfId)
    {
        this.CleApIfId = cleApIfId;
    }
   
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override string ToString()
    {
        return $"{this.CleApIfId}";
    }
    
}