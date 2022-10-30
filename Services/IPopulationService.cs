using AlgorytmGenetyczny.Models;

namespace AlgorytmGenetyczny.Services
{
    public interface IPopulationService
    {
        PopulationModel Population { get; }
        void CreatePopulation(PopulationModel populationModel);
        void DeletePopulation();
    }
}
