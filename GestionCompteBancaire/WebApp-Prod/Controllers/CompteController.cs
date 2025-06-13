using Entite;
using Entite.IDepot;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/compte")]
public class CompteController:Controller
{
    private readonly ICompteDepot _compteDepot;
    private readonly ITransactionDepot _transactionDepot;

    public CompteController(
        ICompteDepot compteDepot, 
        ITransactionDepot transactionDepot)
    {
        this._compteDepot = compteDepot;
        this._transactionDepot = transactionDepot;
    }
    
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
        CompteEntite m_compte = p_compte.VerEntite();
        _compteDepot.CreerCompte(m_compte);
        
        return CreatedAtAction(nameof(ObtenirCompteParId),new{id = m_compte.NumeroCompte}, new CompteModel(m_compte));
    }
    // GET Read
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<CompteEntite> ObtenirCompteParId(Guid p_id)
    {
        var compte = _compteDepot.ObtenirCompte(p_id);
        if (compte != null)
        {
            return Ok(compte);
        }
        return NotFound();
    }
    // PUT Update
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult PUT(Guid p_id, [FromBody] CompteModel p_compte)
    {
        if (!ModelState.IsValid || p_compte.NumeroCompte != p_id)
        {
            return BadRequest();
        }
        
        var compteExistant = _compteDepot.ObtenirCompte(p_id);
        if (compteExistant == null)
        {
            return NotFound();
        }
        
        _compteDepot.MAJCompte(compteExistant);
        return NoContent();
    }
    
    
}