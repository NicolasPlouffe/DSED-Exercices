using Module08_StatsServiceClient.Models;

namespace Module08_StatsServiceClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/appel")]
public class AppelController : Controller
{
    private readonly List<Appel> _appelsCourrant;
    private readonly List<Appel> _appelsTerminee;

    public AppelController()
    {
        _appelsCourrant = new List<Appel>();
        _appelsTerminee = new List<Appel>();
    }

    public IActionResult Index()
    {
        return View();
    }
    
    // POST - Create
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<Appel> Post([FromBody] Appel appel )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Random random = new Random();
        appel.AgentId = random.Next(0,100);
        appel.AppelId = random.Next(0,1000);
        appel.DebutAppel = DateTime.Now;
        _appelsCourrant.Add(appel);
        return CreatedAtAction(nameof(GetById), new { id = appel.AppelId }, appel);
        
    }
        
    // GET - Read
    [HttpGet("{id}")]
    public ActionResult<Appel> GetById(int id)
    {
        var appel = _appelsCourrant.FirstOrDefault(a =>a.AppelId == id);
        if (appel == null)
        {
            return NotFound();
        }
        return appel;
    }
    // PUT - Update
    // Delete
    
}