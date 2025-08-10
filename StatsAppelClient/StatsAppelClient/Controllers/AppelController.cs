using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StatsAppelClient.Services_BL;
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
        private readonly StatsAppelService _statsAppelService;
        
        public AppelController(
            AppelDepot p_appelsRepository,
            IHubContext<StatsAppelHub> hubContext, 
            StatsAppelService p_statsAppelService)
        {
            _appelsRepository = p_appelsRepository;
            _hubContext = hubContext;
            _statsAppelService = p_statsAppelService;
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
            
            await EnvoyerStatistiquesVersVue();
            
            return CreatedAtAction(nameof(GetById), new { id = appelModel.AppelId }, appelModel);
        }

        // GET - Read
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<AppelModel> GetById(int id)
        {
            var appel = _appelsRepository.Appels.FirstOrDefault(a => a.AppelId == id);
            if (appel is null)
            {
                return NotFound();
            }
            return Ok(appel);
        }

         /*[HttpGet("stats/nbrAppel")]
        [ProducesResponseType(200)]
        public ActionResult GetNbrappelCourrant()
        {
            return Ok(_appelsRepository.CalculerNbrAgentEnLigne());
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
            return Ok(_appelsRepository.CalculerNbrAppelJourneeCourrante());
        }*/

        // PUT - Update
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async  Task <IActionResult> Put(int id, [FromBody] AppelModel pAppelModel)
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

            await EnvoyerStatistiquesVersVue();

            return NoContent();
        }

        private async Task EnvoyerStatistiquesVersVue()
        {
            await _hubContext.Clients.All.SendAsync("MajStats",
                _statsAppelService.CalculerDureeMoyenneAppels(),
                _statsAppelService.CalculerNbrAgentEnLigne(),
                _statsAppelService.CalculerNbrAppelJourneeCourrante()
                );
        }
    }
}
