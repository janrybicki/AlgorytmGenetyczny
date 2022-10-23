using System.ComponentModel;

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
        public List<IndividualModel> Individuals { get; set; }

        public PopulationModel()
        {
            Individuals = new List<IndividualModel>();
        }
    }
}
