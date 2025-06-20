using Entite.IDepot;

namespace Entite.Manipulations;

public class ManipulationCompteBLConsoProd
{
    #region variables 
    #endregion
    
    #region properties
    private readonly ICompteDepot _mCompteDepotSQLServer; 
    private readonly ICompteDepot _mCompteDepotRabbit; 
    #endregion
    
    #region constructors

    public ManipulationCompteBLConsoProd
            (ICompteDepot p_CompteDepotSqlServer,
            ICompteDepot p_CompteDepotRabbit)
    {
        ArgumentNullException.ThrowIfNull(p_CompteDepotSqlServer, nameof(p_CompteDepotSqlServer));
        ArgumentNullException.ThrowIfNull(p_CompteDepotRabbit, nameof(p_CompteDepotRabbit));
        
        this._mCompteDepotSQLServer = p_CompteDepotSqlServer;
        this._mCompteDepotRabbit = p_CompteDepotRabbit;
    }
    #endregion
    
    #region CRUD Methods
    // CREATE Post
    public void AjouterCompteSQLServer(CompteEntite pCompte)
    {
        ArgumentNullException.ThrowIfNull(pCompte, nameof(pCompte));
        this._mCompteDepotSQLServer.CreerCompte(pCompte);
    }
    
    // Read Get
    public void ObtenerCompteSQLServer(Guid p_id)
    {
        this._mCompteDepotSQLServer.ObtenirCompte(p_id);
    }
    // Update Put
    public void ModifierCompteSQLServer(CompteEntite pCompte)
    {
        ArgumentNullException.ThrowIfNull(pCompte, nameof(pCompte));
        this._mCompteDepotSQLServer.MAJCompte(pCompte);
    }
    
    #endregion
    
    #region methods
    #endregion
}
