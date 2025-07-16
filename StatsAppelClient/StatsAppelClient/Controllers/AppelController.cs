using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StatsAppelClient.Depot;
using StatsAppelClient.Hubs;
using StatsAppelClient.Models; 
using System.Linq;


namespace StatsAppelClient.Controllers
{
    public class AppelController : ControllerBase
    {

        private readonly AppelDepot _appelsRepository;

        private readonly IHubContext<StatsAppelHub> _hubContext;


        public AppelController(AppelDepot p_appelsRepository, IHubContext<StatsAppelHub> hubContext)
        {
            _appelsRepository = p_appelsRepository;
            _hubContext = hubContext;
        }

        

        // POST - Create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AppelModel>> Post([FromBody] AppelModel appelModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Random random = new Random();
            appelModel.AgentId = random.Next(0, 100);
            appelModel.AppelId = random.Next(0, 1000);
            appelModel.PDebutAppel = DateTime.Now;
            _appelsRepository.Appels.Add(appelModel);

            await _hubContext.Clients.All.SendAsync("Connected",
                this._appelsRepository.CalculerDureeMoyenneAppels(),
                this._appelsRepository.CalculerNbrAgentEnLigne(),
                this._appelsRepository.CalculerNbrAppelJourneeCourrante());
            
            return CreatedAtAction(nameof(GetById), new { id = appelModel.AppelId }, appelModel);

        }

        // GET - Read
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<AppelModel> GetById(int id)
        {
            var appel = _appelsRepository.Appels.FirstOrDefault(a => a.AppelId == id);
            if (appel == null)
            {
                return NotFound();
            }
            return Ok(appel);
        }

        [HttpGet("stats/nbrAppel")]
        [ProducesResponseType(200)]
        public ActionResult GetNbrappelCourrant()
        {
            return Ok(_appelsRepository.CalculerNbrAppelJourneeCourrante());
        }

        [HttpGet("stats/dureeMoyenne")]
        [ProducesResponseType(200)]
        public ActionResult GetDureeMoyenne()
        {
            return Ok(_appelsRepository.CalculerDureeMoyenneAppels());
        }

        [HttpGet("stats/NbrAppelJourneeCourrante")]
        [ProducesResponseType(200)]
        public ActionResult GetNbrAppelJourneeCourrante()
        {
            return Ok(_appelsRepository.CalculerNbrAgentEnLigne());
        }

        // PUT - Update
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async IActionResult Put(int id, [FromBody] AppelModel pAppelModel)
        {
            if (!ModelState.IsValid || pAppelModel == null)
            {
                return BadRequest();
            }
            var appel = _appelsRepository.Appels.FirstOrDefault(a => a.AppelId == id);

            if (appel == null)
            {
                return NotFound();
            }
            appel.PFinAppel = DateTime.Now;

            await _hubContext.Clients.All.SendAsync("StatistiquesMAJ", GetStatistics());

            return NoContent();
        }
    }
}
