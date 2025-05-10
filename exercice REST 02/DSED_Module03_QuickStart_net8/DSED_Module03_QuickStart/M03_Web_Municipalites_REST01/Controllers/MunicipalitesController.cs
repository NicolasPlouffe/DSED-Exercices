using M01_Srv_Municipalite;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace M03_Web_Municipalites_REST01.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MunicipalitesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    //GET 
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(Donnees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<string> Get(int id)
    {
        var municipalite = Donnees.Where(m => m.municipaliteId == id).SingleOrDefault();
    }
    
    // POST
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]

    public ActionResult Post([FromBody] Municipalite municipalite)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var idMax = Donnee.OrderByDescending(m => m.Id).FirstOrDefault()?.MunicipaliteId ?? 0;
        municipalite.Id = idMax + 1;
        Donnee.Add(municipalite);
        
        return CreatedAtAction(nameof(Get),new {id=municipalite.Id}, municipalite);

    }
    
    
    
}