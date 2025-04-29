namespace M01_Entite.IDepot;

public interface ITransactionBD : IDisposable
{
    void Commit();
    void Rollback();
    void BeginTransaction();
}