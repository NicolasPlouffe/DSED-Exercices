namespace Entite.IDepot;

public interface ITransactionDB
{
    void Commit();
    void Rollback();
    void BeginTransaction();
}