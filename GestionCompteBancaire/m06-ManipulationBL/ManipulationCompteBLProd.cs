using RabbitMQ.Client;

namespace ManipulationsBL;
using Entite;
using Entite.IDepot;
using DAL;
public class ManipulationCompteBLProd
{

    #region variables & const
    
    #endregion

    #region Attributes
    private readonly ICompteDepot _mCompteDepotSQLServer; 
    private readonly ICompteDepot _mCompteDepotRabbit; 
    
    #endregion
    
    #region properties
    
    #endregion

    #region Constructors

    public ManipulationCompteBLProd(ICompteDepot pCompteDepotSqlServer,ICompteDepot pCompteDepotRabbit)
    {
        ArgumentNullException.ThrowIfNull(pCompteDepotSqlServer, nameof(pCompteDepotSqlServer));
        ArgumentNullException.ThrowIfNull(pCompteDepotRabbit, nameof(pCompteDepotRabbit));
        
        
        this._mCompteDepotSQLServer = pCompteDepotSqlServer;
        this._mCompteDepotRabbit = pCompteDepotRabbit;        
    }
    #endregion

    #region CRUD Methods
    // Creat Post
    public void AjouterCompte(CompteEntite p_entite)
    {
        this._mCompteDepotRabbit.CreerCompte(p_entite);
    }
    
    //Read Get

    public CompteEntite ObtenirCompte(Guid p_id)
    {
        return this._mCompteDepotSQLServer.ObtenirCompte(p_id);
    }
    
    //Uodate Put

    public void ModifierCompte(CompteEntite p_Entite)
    {
        this._mCompteDepotRabbit.MAJCompte(p_Entite);
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
}