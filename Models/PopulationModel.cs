﻿using System.ComponentModel;
using System.Text;

namespace AlgorytmGenetyczny.Models
{
    public class PopulationModel
    {
        [DisplayName("a")]
        public float RangeBeginning { get; set; }
        [DisplayName("b")]
        public float RangeEnd { get; set; }
        [DisplayName("d")]
        public double Precision { get; set; }
        [DisplayName("N")]
        public int Number { get; set; }
        public int BinaryLength { get; set; }
        [DisplayName("pk")]
        public float CrossingProbability { get; set; }
        public List<IndividualModel> Individuals { get; set; }

        public PopulationModel()
        {
            Individuals = new List<IndividualModel>();
        }
        public void CalculateBinaryLengh()
        {
            BinaryLength = (int)Math.Ceiling(Math.Log2((RangeEnd - RangeBeginning) / Precision + 1));
        }
        public void Selection()
        {
            var random = new Random();
            //return random.NextDouble();
            for (int i = 0; i < Number; i++)
            {
                var r = (float)random.NextDouble();
                Individuals[i].R = r;
                foreach (var individual in Individuals)
                {
                    if (r < individual.SurviveDistributionFunction)
                    {
                        individual.IsSurvivor = true;
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
                if (individual.IsSurvivor && random.NextDouble() < CrossingProbability)
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
            for (int i = 0; i < parents.Count(); i += 2)
            {
                var crossingPoint = random.Next(1, BinaryLength);
                parents[i].CrossingPoint = crossingPoint;
                parents[i + 1].CrossingPoint = crossingPoint;
                
                var firstParent = parents[i].XRealToXBin(BinaryLength,RangeBeginning, RangeEnd);
                var secondParent = parents[i+1].XRealToXBin(BinaryLength, RangeBeginning, RangeEnd);
                
                var firstParentStringBuilder = new StringBuilder();
                firstParentStringBuilder.Append(firstParent.Substring(0, crossingPoint)).Append(secondParent.Substring(crossingPoint, BinaryLength - crossingPoint));
                
                var secondParentStringBuilder = new StringBuilder();
                secondParentStringBuilder.Append(secondParent.Substring(0, crossingPoint)).Append(firstParent.Substring(crossingPoint, BinaryLength - crossingPoint));

                parents[i].ChildXBin = firstParentStringBuilder.ToString();
                parents[i + 1].ChildXBin = secondParentStringBuilder.ToString();
            }
        }
    }
}
