using System.ComponentModel;

namespace AlgorytmGenetyczny.Models
{
    public class XModel
    {
        [DisplayName("a")]
        public double RangeBeginning { get; set; }
        [DisplayName("b")]
        public double RangeEnd { get; set; }
        [DisplayName("d")]
        public double Accuracy { get; set; }
        [DisplayName("N")]
        public int Number { get; set; }
        [DisplayName("xReal")]
        public double XReal1 { get; set; }
        [DisplayName("xInt")]
        public int XInt1 { get; set; }
        [DisplayName("xBin")]
        public string XBin { get; set; }
        [DisplayName("xInt")]
        public int XInt2 { get; set; }
        [DisplayName("xReal")]
        public double XReal2 { get; set; }
        [DisplayName("f(x)")]
        public double FunctionValue { get; set; }

        public XModel(XModel xModel)
        {
            this.RangeBeginning = xModel.RangeBeginning;
            this.RangeEnd = xModel.RangeEnd;
            this.Accuracy = xModel.Accuracy;
        }
        public XModel()
        {

        }
    }
}
