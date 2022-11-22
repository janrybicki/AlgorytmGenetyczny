using AlgorytmGenetyczny.Models;
using AlgorytmGenetyczny.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

namespace AlgorytmGenetyczny.Controllers
{
    public class PopulationController : Controller
    {
        private readonly IPopulationService _populationService;
        public PopulationController(IPopulationService populationService)
        {
            _populationService = populationService;
        }

        // GET: PopulationController
        public ActionResult Index()
        {
            return View(_populationService.Population);
        }

        // POST: PopulationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PopulationModel populationModel)
        {
            _populationService.CreatePopulation(populationModel);
            return RedirectToAction(nameof(Index));
        }
        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            _populationService.DeletePopulation();
            return RedirectToAction(nameof(Index));
        }
    }
}
