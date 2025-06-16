namespace Entite.Manipulations;
using Entite;
using Entite.IDepot;
public class ManipulationCompteBL: IDisposable
{

    #region variables & const
    
    #endregion

    #region Attributes
    private readonly ICompteDepot _mCompteDepot; 
    private readonly ITransactionDB _mTransactionDB;
    #endregion
    
    
    #region properties
    
    #endregion

    #region Constructors

    public ManipulationCompteBL(ICompteDepot p_CompteDepot, ITransactionDB p_TransactionDB)
    {
        if (p_CompteDepot is null) { throw new ArgumentNullException("CompteDepot"); }
        if (p_TransactionDB is null) { throw new ArgumentNullException("TransactionDB"); }
        
        this._mCompteDepot = p_CompteDepot;
        this._mTransactionDB = p_TransactionDB;
        
    }
    #endregion

    #region CRUD Methods
    // Creat Post
    public void AjouterCompte(CompteEntite p_entite)
    {
        this._mCompteDepot.CreerCompte(p_entite);
    }
    
    //Read Get

    public CompteEntite ObtenerCompte(Guid p_id)
    {
        return this._mCompteDepot.ObtenirCompte(p_id);
    }
    
    //Uodate Put

    void ModifierCompte(CompteEntite p_Entite)
    {
        this._mCompteDepot.MAJCompte(p_Entite);
    }
    
    #endregion
    
    #region methods
    
    #endregion

    bool ValidationObjCompte(CompteEntite p_Entite)
    {
        if (p_Entite is null)
        {
            return false;
        }
        return true;
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}