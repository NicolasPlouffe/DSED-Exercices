using M01_Srv_Municipalite;
using M01_Entite;
using M03_Web_Municipalites_REST01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace M03_Web_Municipalites_REST01.Controllers;

[ApiController]
[Route("api/municipalite")]
public class MunicipalitesController : Controller
{
    
    private readonly IDepotMunicipalites _depotMunicipalites;


    public MunicipalitesController(IDepotMunicipalites p_depotMunicipalites)
    {
        _depotMunicipalites = p_depotMunicipalites;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    //GET : api/municipalite
    [HttpGet("liste")]
    [ProducesResponseType(200)]
    public ActionResult<IEnumerable<MunicipaliteModel>> ListerMunicipalites()
    {
        return Ok(_depotMunicipalites.ListerMunicipalitesActives().Select(m => new MunicipaliteModel(m)));
    }

    //GET : api/municipalite/3
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<string> ObtenirMunicipaliteParId(int id)
    {
        var municipalite = _depotMunicipalites.ChercherMunicipaliteParCodeGeographique(id);
        if (municipalite != null)
        {
            return Ok(municipalite);
        }
        return NotFound();
    }
    
    // POST api/municipalite
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<MunicipaliteModel> Post([FromBody] MunicipaliteModel p_municipalite)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        MunicipaliteEntite m_municipalite = p_municipalite.VersEntite();
        _depotMunicipalites.AjouterMunicipalite(m_municipalite);
        
        return CreatedAtAction(nameof(ObtenirMunicipaliteParId),new {id=m_municipalite.CodeGeographique}, new MunicipaliteModel(m_municipalite));
    }
    
    // PUT: api/municipalite/3
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult PUT(int id, [FromBody] MunicipaliteModel p_municipalite)
    {
        if (!ModelState.IsValid || p_municipalite.MunicipaliteId != id)
        {
            return BadRequest();
        }

        var municipaliteExistante = _depotMunicipalites.ChercherMunicipaliteParCodeGeographique(id);

        if (municipaliteExistante == null)
        {
            return NotFound();
        }
       
        _depotMunicipalites.MAJMunicipalite(municipaliteExistante);
        
        return NoContent();
    }
    
    //Delete
    
    [HttpDelete("{p_codeGeographique}")]  // Matches parameter name
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int p_codeGeographique)
    {
        MunicipaliteEntite municipalite = _depotMunicipalites
            .ChercherMunicipaliteParCodeGeographique(p_codeGeographique);
    
        if (municipalite == null)
        {
            return NotFound();  // 404 if not found
        }
    
        _depotMunicipalites.DesactiverMunicipalite(municipalite);
        return NoContent();  // 204 for successful soft-delete
    }

    
    
}