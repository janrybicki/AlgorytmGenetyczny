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
            Population.CalculateBinaryLengh();
            Population.AddNewIndividuals();
            Population.CalculateSurviveChances();

            var elite = Population.Individuals.MaxBy(x => x.FunctionValue);

            Population.Selection();
            Population.Cross();
            Population.Mutation();
            Population.ReplaceRandomIndividualWithElite(elite);
        }

        public void DeletePopulation()
        {
            Population.Individuals.Clear();
        }
    }
}
