using AlgorytmGenetyczny.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

namespace AlgorytmGenetyczny.Controllers
{
    public class PopulationController : Controller
    {
        private static PopulationModel population = new PopulationModel();

        // GET: PopulationController
        public ActionResult Index()
        {
            return View(population);
        }

        // POST: PopulationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PopulationModel populationModel)
        {
            population.Individuals.Clear();
            var random = new Random();
            var l = (int)Math.Ceiling(Math.Log2((populationModel.RangeEnd - populationModel.RangeBeginning) / populationModel.Precision + 1));
            for (int i = 0; i < populationModel.Number; i++)
            {
                var randomNumber = random.NextDouble() * (populationModel.RangeEnd - populationModel.RangeBeginning) + populationModel.RangeBeginning;
                var xReal1 = Math.Abs(randomNumber % populationModel.Precision) < populationModel.Precision / 2
                ? randomNumber - randomNumber % populationModel.Precision
                    : randomNumber + Math.Sign(randomNumber) * populationModel.Precision - randomNumber % populationModel.Precision;
                var xInt1 = (int)Math.Round(1 / (populationModel.RangeEnd - populationModel.RangeBeginning) * (xReal1 - populationModel.RangeBeginning) * (Math.Pow(2, l) - 1));
                var sb = new StringBuilder(Convert.ToString(xInt1, 2));
                sb.Insert(0, new String('0', l - sb.Length));
                var xBin = sb.ToString();
                var xInt2 = Convert.ToInt32(xBin, 2);
                var xReal2 = xInt2 * (populationModel.RangeEnd - populationModel.RangeBeginning) / (Math.Pow(2, l) - 1) + populationModel.RangeBeginning;
                var xReal2Roundend = Math.Abs(xReal2 % populationModel.Precision) < populationModel.Precision / 2
                ? xReal2 - xReal2 % populationModel.Precision
                    : xReal2 + Math.Sign(xReal2) * populationModel.Precision - xReal2 % populationModel.Precision;
                var functionValue = (xReal2Roundend % 1) * (Math.Cos(xReal2Roundend * 20 * Math.PI) - Math.Sin(xReal2Roundend));

                population = populationModel;
                population.Individuals.Add(new IndividualModel()
                {
                    XReal1 = (float)xReal1,
                    XInt1 = xInt1,
                    XBin = xBin,
                    XInt2 = xInt2,
                    XReal2 = (float)xReal2Roundend,
                    FunctionValue = (float)functionValue
                });
            }
            return RedirectToAction(nameof(Index));
        }
        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            population.Individuals.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
