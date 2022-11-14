using AlgorytmGenetyczny.Models;
using System.Text;

namespace AlgorytmGenetyczny.Services
{
    public class PopulationService : IPopulationService
    {
        public PopulationModel Population { get; private set; }
        public PopulationService()
        {
            Population = new PopulationModel();//sprawdzic czy konieczne
        }
        public void CreatePopulation(PopulationModel populationModel)
        {
            Population = populationModel;
            Population.CalculateBinaryLengh();
            Population.AddNewIndividuals();
            Population.CalculateSurviveChances();
            Population.Selection();
            Population.Cross();
            Population.Mutation();
            foreach (var individual in Population.Individuals)
            {
                var xRealAfterMutation = IndividualModel.XBinToXReal(individual.XBinAfterMutation, Population.RangeBeginning, Population.RangeEnd, Population.BinaryLength);
                individual.XRealAfterMutation = IndividualModel.Round(xRealAfterMutation, Population.Precision);
                individual.FunctionValueAfterMutation = IndividualModel.CalculateFunctionValue(individual.XRealAfterMutation);
            }
        }

        public void DeletePopulation()
        {
            Population.Individuals.Clear();
        }
    }
}
