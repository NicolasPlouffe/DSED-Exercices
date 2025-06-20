namespace DAL;

public class MessageEnveloppe
{
    public string Action { get; set; }
    public string TypeEntite { get; set; }
    public byte[] DataEntiteEncodees { get; set; }
}