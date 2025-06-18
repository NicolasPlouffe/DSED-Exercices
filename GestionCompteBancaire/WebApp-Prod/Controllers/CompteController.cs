using Entite;
using Entite.IDepot;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;
using DAL;
using Entite.Manipulations;


namespace WebApplication1.Controllers;

[ApiController]
[Route("api/compte")]
public class CompteController:Controller
{
    private readonly ManipulationCompteBLProd _manipulationCompteBlProd;
    private readonly ManipulationTransactionBLProd _manipulationTransactionBlProd;
    
    public CompteController(
        ManipulationCompteBLProd manipulationCompteBlProd,
        ManipulationTransactionBLProd manipulationTransactionBlProd
        )
    {
      this._manipulationCompteBlProd = manipulationCompteBlProd;
      this._manipulationTransactionBlProd = manipulationTransactionBlProd;
    }

    public IActionResult Index()
    {
        return View();
    }
    #region POST
    // POST Create
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<CompteModel> Post([FromBody] CompteModel p_compte)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _manipulationCompteBlProd.AjouterCompte(p_compte.VerEntite());
        
        return CreatedAtAction(nameof(ObtenirCompteParId),new{id = p_compte.NumeroCompte}, p_compte);
    }
    
    [HttpPost("{p_compteId}/transactions")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<TransactionModel> PostTransaction(
        [FromRoute] Guid p_compteId,
        [FromBody] TransactionModel p_transaction)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var compteUtilisateur =_manipulationCompteBlProd.ObtenirCompte(p_compteId);

        if (compteUtilisateur == null)
        {
            return NotFound("compte introuvable");
        }
        
        TransactionEntite nouvelleTransaction = p_transaction.VersEntite();
        _manipulationTransactionBlProd.AjouterTransaction(nouvelleTransaction);

        compteUtilisateur.ListTransactions.Add(nouvelleTransaction);
            
        _manipulationCompteBlProd.ModifierCompte(compteUtilisateur);
        
        return CreatedAtAction(
            nameof(ObtenirCompteParId),
            new{id = nouvelleTransaction.TransactionId},
            new TransactionModel(nouvelleTransaction));
    }
    
    #endregion
    
    #region GET
    
    // GET Read
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<CompteEntite> ObtenirCompteParId(Guid p_id)
    {
        var compte = _manipulationCompteBlProd.ObtenirCompte(p_id);
        if (compte != null)
        {
            return Ok(compte);
        }
        return NotFound();
    }

    [HttpGet("{p_compteId}/transactions/{p_transactionId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<TransactionModel> ObtenirTransactionParId(
        [FromRoute] Guid p_compteId,
        [FromRoute] Guid p_transactionId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var compteUtilisateur = _manipulationCompteBlProd.ObtenirCompte(p_compteId);
        if (compteUtilisateur == null)
        {
            return NotFound("compte introuvable");
        }

        var transactionRecherchee = 
                    compteUtilisateur
                    .ListTransactions
                    .FirstOrDefault(t => t.TransactionId == p_transactionId);
        
            if (transactionRecherchee is null)
            {
                return NotFound("transaction introuvable");
            }
        
        return Ok(new TransactionModel(transactionRecherchee));
    }

    #endregion
    
    #region PUT
    
    // PUT Update
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult PUTCompte(Guid p_id, [FromBody] CompteModel p_compte)
    {
        if (!ModelState.IsValid || p_compte.NumeroCompte != p_id)
        {
            return BadRequest();
        }
        
        var compteExistant = _manipulationCompteBlProd.ObtenirCompte(p_id);
        if (compteExistant == null)
        {
            return NotFound();
        }
        
        _manipulationCompteBlProd.ModifierCompte(compteExistant);
        
        return NoContent();
    }
    
    [HttpPut("{p_compteId}/transactions/{p_transactionId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult PUTTransaction(
        [FromRoute] Guid p_compteId,
        [FromRoute] Guid p_transactionId,
        [FromBody] TransactionModel p_transaction)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
            var compteUtilisateur = _manipulationCompteBlProd.ObtenirCompte(p_compteId);
            if (compteUtilisateur == null)
            {
                return NotFound("compte introuvable");
            }

            var transactionRecherchee =
                compteUtilisateur
                    .ListTransactions
                    .FirstOrDefault(t => t.TransactionId == p_transactionId);

            if (transactionRecherchee is null)
            {
                return NotFound("transaction introuvable");
            }

            var transactionModifiee = p_transaction.VersEntite();
            transactionRecherchee.Type = transactionModifiee.Type;
            transactionRecherchee.DateTransaction = transactionModifiee.DateTransaction;
            if (transactionModifiee.Montant <= 0)
            {
                ModelState.AddModelError(nameof(p_transaction.Montant),
                    "Le montant de la transaction doit être positif");
                return BadRequest();
            }

            transactionRecherchee.Montant = transactionModifiee.Montant;

            _manipulationTransactionBlProd.ModifierTransaction(transactionModifiee);

        return NoContent(); 
    }
        
    
    #endregion
    
    
}