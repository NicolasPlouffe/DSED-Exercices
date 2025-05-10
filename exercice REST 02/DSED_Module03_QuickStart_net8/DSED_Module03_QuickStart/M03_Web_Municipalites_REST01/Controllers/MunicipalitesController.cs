using M01_Srv_Municipalite;
using M01_Entite;
using M03_Web_Municipalites_REST01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace M03_Web_Municipalites_REST01.Controllers;

[Route("api/[controller]")]
[ApiController]

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
    
    //GET 
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult<IEnumerable<MunicipaliteModel>> Get()
    {
        return Ok(_depotMunicipalites.ListerMunicipalitesActives().Select(m => new MunicipaliteModel(m)));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<string> Get(int id)
    {
        var municipalite = _depotMunicipalites.ChercherMunicipaliteParCodeGeographique(id);
        return municipalite != null ? Ok(municipalite.CodeGeographique) : NotFound();
    }
    
    // POST
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<MunicipaliteModel> Post([FromBody] MunicipaliteModel p_municipalite)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        MunicipaliteEntite m_municipalite = p_municipalite.VersEntite();
        _depotMunicipalites.AjouterMunicipalite(m_municipalite);
        
        return CreatedAtAction(nameof(Get),new {id=m_municipalite.CodeGeographique}, new MunicipaliteModel());

    }
    
    //Delete
    
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Delete(int id) Delete(int p_codeGeographique)
    {
        
        
        MunicipaliteEntite r_municipalite = _depotMunicipalites.ChercherMunicipaliteParCodeGeographique(p_codeGeographique);
        if (r_municipalite == null)
        {
            return NotFound();
        }
        _depotMunicipalites.DesactiverMunicipalite(r_municipalite);
        return NoContent();
    }
    
    
}