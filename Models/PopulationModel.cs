using System.ComponentModel;
using System.Text;

namespace AlgorytmGenetyczny.Models
{
    public class PopulationModel
    {
        [DisplayName("Beginning of range")]
        public float RangeBeginning { get; set; }

        [DisplayName("End of range")]
        public float RangeEnd { get; set; }

        [DisplayName("Precision")]
        public double Precision { get; set; }

        [DisplayName("Number of individuals")]
        public int NumberOfIndividuals { get; set; }

        public int BinaryLength { get; set; }

        [DisplayName("Crossing probability")]
        public float CrossingProbability { get; set; }

        [DisplayName("Mutation Probability")]
        public float MutationProbability { get; set; }

        [DisplayName("Elitism Selection")]
        public bool ElitismSelection { get; set; }
        public List<IndividualModel> Individuals { get; set; }

        public PopulationModel()
        {
            Individuals = new List<IndividualModel>();
        }
        public void CalculateBinaryLengh()
        {
            BinaryLength = (int)Math.Ceiling(Math.Log2((RangeEnd - RangeBeginning) / Precision + 1));
        }
        public void AddNewIndividuals()
        {
            for (int i = 0; i < NumberOfIndividuals; i++)
            {
                var xReal = IndividualModel.GenerateRandomXReal(RangeBeginning, RangeEnd);
                var xRealRounded = IndividualModel.Round(xReal, Precision);
                var functionValue = IndividualModel.CalculateFunctionValue(xRealRounded);

                Individuals.Add(new IndividualModel()
                {
                    XReal = xRealRounded,
                    FunctionValue = functionValue
                });
            }
        }
        public void CalculateSurviveChances()
        {
            var minimalFunctionValue = Individuals.Select(x => x.FunctionValue).Min();

            foreach (var individual in Individuals)
            {
                individual.TranslatedFunctionValue = individual.FunctionValue - minimalFunctionValue + (float)Precision;
            }
            var sumOfTranslatedFunctionValues = Individuals.Select(x => x.TranslatedFunctionValue).Sum();

            for (int i = 0; i < NumberOfIndividuals; i++)
            {
                Individuals[i].SurviveProbability = Individuals[i].TranslatedFunctionValue / sumOfTranslatedFunctionValues;
                if (i == 0)
                {
                    Individuals[i].SurviveDistributionFunction = Individuals[i].SurviveProbability;
                }
                else
                {
                    Individuals[i].SurviveDistributionFunction = Individuals[i].SurviveProbability + Individuals[i - 1].SurviveDistributionFunction;
                }
            }
        }
        public void Selection()
        {
            var random = new Random();
            for (int i = 0; i < NumberOfIndividuals; i++)
            {
                var r = (float)random.NextDouble();
                Individuals[i].R = r;
                foreach (var individual in Individuals)
                {
                    if (r < individual.SurviveDistributionFunction)
                    {
                        Individuals[i].XRealAfterSelection = individual.XReal;
                        break;
                    }
                }
            }
        }
        public void Cross()
        {
            var random = new Random();
            foreach (var individual in Individuals)
            {
                individual.XBinAfterSelection = IndividualModel.XRealToXBin(BinaryLength, RangeBeginning, RangeEnd, individual.XRealAfterSelection);
                
                if (random.NextDouble() < CrossingProbability)
                {
                    individual.IsParent = true;
                }
            }
            var parents = Individuals.Where(x => x.IsParent == true).ToList();

            if (parents.Count() % 2 != 0)
            {
                parents[parents.Count() - 1].IsParent = false;
                parents.RemoveAt(parents.Count - 1);
            }
            Individuals.Where(x => x.IsParent == false).ToList().ForEach(x => x.XBinAfterCrossing = x.XBinAfterSelection);

            for (int i = 0; i < parents.Count(); i += 2)
            {
                var crossingPoint = random.Next(1, BinaryLength);
                parents[i].CrossingPoint = crossingPoint;
                parents[i + 1].CrossingPoint = crossingPoint;

                var firstParent = parents[i].XBinAfterSelection;
                var secondParent = parents[i + 1].XBinAfterSelection;

                var firstParentStringBuilder = new StringBuilder();
                firstParentStringBuilder.Append(firstParent.Substring(0, crossingPoint)).Append(secondParent.Substring(crossingPoint, BinaryLength - crossingPoint));
                
                var secondParentStringBuilder = new StringBuilder();
                secondParentStringBuilder.Append(secondParent.Substring(0, crossingPoint)).Append(firstParent.Substring(crossingPoint, BinaryLength - crossingPoint));

                parents[i].XBinAfterCrossing = firstParentStringBuilder.ToString();
                parents[i + 1].XBinAfterCrossing = secondParentStringBuilder.ToString();
            }
        }
        public void Mutation()
        {
            var random = new Random();
            foreach (var individual in Individuals)
            {
                var sb = new StringBuilder();
                var mutantBits = new List<int>();
                for (int i = 0; i < individual.XBinAfterCrossing.Count(); i++)
                {
                    if(random.NextDouble() < MutationProbability)
                    {
                        sb.Append(individual.XBinAfterCrossing[i] == '0' ? '1' : '0');
                        mutantBits.Add(i);
                    }
                    else
                    {
                        sb.Append(individual.XBinAfterCrossing[i]);
                    }
                }
                individual.XBinAfterMutation = sb.ToString();
                individual.MutantBits = String.Join(", ", mutantBits);

                var xRealAfterMutation = IndividualModel.XBinToXReal(individual.XBinAfterMutation, RangeBeginning, RangeEnd, BinaryLength);
                individual.XRealAfterMutation = IndividualModel.Round(xRealAfterMutation, Precision);
                individual.FunctionValueAfterMutation = IndividualModel.CalculateFunctionValue(individual.XRealAfterMutation);
            }
        }
        public void ReplaceRandomIndividualWithElite(IndividualModel elite)
        {
            if (Individuals.All(x => x.FunctionValueAfterMutation != elite.FunctionValue))
            {
                var random = new Random();
                var randomIndividual = Individuals[random.Next(NumberOfIndividuals)];
                if (randomIndividual.FunctionValueAfterMutation < elite.FunctionValue)
                {
                    randomIndividual.XRealAfterMutation = elite.XReal;
                    randomIndividual.FunctionValueAfterMutation = IndividualModel.CalculateFunctionValue(randomIndividual.XRealAfterMutation);
                }
            }

        }
    }
}
