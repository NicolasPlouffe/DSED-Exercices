using Module08_StatsServiceClient.Models;

namespace Module08_StatsServiceClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/appel")]
public class AppelController : Controller
{
    private readonly List<AppelModel> _appels;

    public AppelController()
    {
        _appels = new List<AppelModel>();
    }

    public IActionResult Index()
    {
        return View();
    }
    
    // POST - Create
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<AppelModel> Post([FromBody] AppelModel appelModel )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Random random = new Random();
        appelModel.AgentId = random.Next(0,100);
        appelModel.AppelId = random.Next(0,1000);
        appelModel.DebutAppel = DateTime.Now;
        _appels.Add(appelModel);
        return CreatedAtAction(nameof(GetById), new { id = appelModel.AppelId }, appelModel);
        
    }
        
    // GET - Read
    [HttpGet("{id}")]
    public ActionResult<AppelModel> GetById(int id)
    {
        var appel = _appels.FirstOrDefault(a =>a.AppelId == id);
        if (appel == null)
        {
            return NotFound();
        }
        return appel;
    }
    // PUT - Update
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult Put(int id, [FromBody] AppelModel pAppelModel)
    {
        if (!ModelState.IsValid || pAppelModel == null)
        {
            return BadRequest();
        }
        var appel = _appels.FirstOrDefault(a =>a.AppelId == id);
        
        if (appel == null)
            {
            return NotFound();
            }
        appel.FinAppel = DateTime.Now;
        
        return NoContent();
    }
    // Delete
    
}