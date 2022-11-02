using AlgorytmGenetyczny.Models;
using System.Text;

namespace AlgorytmGenetyczny.Services
{
    public class PopulationService : IPopulationService
    {
        public PopulationModel Population { get; private set; }
        public PopulationService()
        {
            Population = new PopulationModel();
        }
        public void CreatePopulation(PopulationModel populationModel)
        {
            Population = populationModel;
            var l = (int)Math.Ceiling(Math.Log2((populationModel.RangeEnd - populationModel.RangeBeginning) / populationModel.Precision + 1));
            for (int i = 0; i < populationModel.Number; i++)
            {
                var xReal1 = IndividualModel.GenerateRandomXReal(populationModel.RangeBeginning, populationModel.RangeEnd);
                var xReal1Rounded = IndividualModel.Round(xReal1, populationModel.Precision);
                var xInt1 = IndividualModel.XRealToXInt(xReal1Rounded, l, populationModel.RangeBeginning, populationModel.RangeEnd);
                var xBin = IndividualModel.XIntToXBin(xInt1, l);
                var xInt2 = IndividualModel.XBinToXInt(xBin);
                var xReal2 = IndividualModel.XIntToXReal(xInt2, populationModel.RangeBeginning, populationModel.RangeEnd, l);
                var xReal2Roundend = IndividualModel.Round(xReal2, populationModel.Precision);
                var functionValue = IndividualModel.Function(xReal2Roundend);

                Population.Individuals.Add(new IndividualModel()
                {
                    XReal1 = (float)xReal1Rounded,
                    XInt1 = xInt1,
                    XBin = xBin,
                    XInt2 = xInt2,
                    XReal2 = (float)xReal2Roundend,
                    FunctionValue = (float)functionValue
                });
            }
            var minimalFunctionValue = Population.Individuals.Select(x => x.FunctionValue).Min();

            foreach (var individual in Population.Individuals)
            {
                individual.TranslatedFunctionValue = individual.FunctionValue - minimalFunctionValue + (float)Population.Precision;
            }
            var sumOfTranslatedFunctionValues = Population.Individuals.Select(x => x.TranslatedFunctionValue).Sum();

            for (int i = 0; i < Population.Individuals.Count(); i++)
            {
                Population.Individuals[i].SurviveProbability = Population.Individuals[i].TranslatedFunctionValue / sumOfTranslatedFunctionValues;
                if (i == 0)
                {
                    Population.Individuals[i].SurviveDistributionFunction = Population.Individuals[i].SurviveProbability;
                }
                else
                {
                    Population.Individuals[i].SurviveDistributionFunction = Population.Individuals[i].SurviveProbability + Population.Individuals[i-1].SurviveDistributionFunction;
                }
            }
        }

        public void DeletePopulation()
        {
            Population.Individuals.Clear();
        }
    }
}
