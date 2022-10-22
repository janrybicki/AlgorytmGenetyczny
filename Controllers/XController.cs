using AlgorytmGenetyczny.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AlgorytmGenetyczny.Controllers
{
    public class XController : Controller
    {
        private static IList<XModel> x = new List<XModel>();
        // GET: XController
        public ActionResult Index()
        {
            return View(x);
        }

        // GET: XController/Create 
        public ActionResult Create()
        {
            return View();
        }

        // POST: XController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(XModel xModel)
        {
            var random = new Random();
            var l = (int)Math.Ceiling(Math.Log2((xModel.RangeEnd - xModel.RangeBeginning) / xModel.Precision + 1));
            for (int i = 0; i < xModel.Number; i++)
            {
                var randomNumber = random.NextDouble() * (xModel.RangeEnd - xModel.RangeBeginning) + xModel.RangeBeginning;
                var xReal1 = Math.Abs(randomNumber % xModel.Precision) < xModel.Precision / 2
                    ? randomNumber - randomNumber % xModel.Precision
                    : randomNumber + Math.Sign(randomNumber) * xModel.Precision - randomNumber % xModel.Precision;
                var xInt1 = (int)Math.Round(1 / (xModel.RangeEnd - xModel.RangeBeginning) * (xReal1 - xModel.RangeBeginning) * (Math.Pow(2, l) - 1));
                var sb = new StringBuilder(Convert.ToString(xInt1, 2));
                sb.Insert(0, new String('0', l - sb.Length));
                var xBin = sb.ToString();
                var xInt2 = Convert.ToInt32(xBin, 2);
                var xReal2 = xInt2 * (xModel.RangeEnd - xModel.RangeBeginning) / (Math.Pow(2, l) - 1) + xModel.RangeBeginning;
                var xReal2Roundend = Math.Abs(xReal2 % xModel.Precision) < xModel.Precision / 2
                    ? xReal2 - xReal2 % xModel.Precision
                    : xReal2 + Math.Sign(xReal2) * xModel.Precision - xReal2 % xModel.Precision;
                var functionValue = (xReal2Roundend % 1) * (Math.Cos(xReal2Roundend * 20 * Math.PI) - Math.Sin(xReal2Roundend));

                x.Add(new XModel(xModel)
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
            x.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
