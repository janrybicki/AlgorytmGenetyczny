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

            for (int i = 0; i < Population.NumberOfGenerations && Population.Individuals.Select(x => x.XReal).Distinct().Count() > 1; i++)//dodac warunek, ze jak wszystkie sa takie same to konczymy
            {
                Population.CalculateSurviveChances();
                var elite = Population.Individuals.MaxBy(x => x.FunctionValue);

                Population.Selection();
                Population.Cross();
                Population.Mutation();
                Population.ReplaceIndividuals(elite);
            }
            Population.CalculatePercentage();
        }

        public void DeletePopulation()
        {
            Population.Individuals.Clear();
        }
    }
}
