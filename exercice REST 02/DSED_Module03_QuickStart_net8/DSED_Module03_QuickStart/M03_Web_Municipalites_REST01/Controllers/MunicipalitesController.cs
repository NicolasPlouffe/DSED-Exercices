using Microsoft.AspNetCore.Mvc;

namespace M03_Web_Municipalites_REST01.Controllers;

public class MunicipalitesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}