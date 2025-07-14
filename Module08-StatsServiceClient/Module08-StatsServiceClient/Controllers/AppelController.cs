using Module08_StatsServiceClient.Models;
using Module08_StatsServiceClient.Services;
namespace Module08_StatsServiceClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/appel")]
public class AppelController : Controller
{
private readonly AppelRepository _appelsRepository;
    public AppelController(AppelRepository p_appelsRepository)
    {
        _appelsRepository = p_appelsRepository;
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
        appelModel.PDebutAppel = DateTime.Now;
        _appelsRepository.Appels.Add(appelModel);
        return CreatedAtAction(nameof(GetById), new { id = appelModel.AppelId }, appelModel);
        
    }
        
    // GET - Read
    [HttpGet("{id}")]
    public ActionResult<AppelModel> GetById(int id)
    {
        var appel = _appelsRepository.Appels.FirstOrDefault(a =>a.AppelId == id);
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
        var appel = _appelsRepository.Appels.FirstOrDefault(a =>a.AppelId == id);
        
        if (appel == null)
            {
            return NotFound();
            }
        appel.PFinAppel = DateTime.Now;
        
        return NoContent();
    }
    // Delete
    
}