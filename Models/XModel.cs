using System.ComponentModel;

namespace AlgorytmGenetyczny.Models
{
    public class XModel
    {
        [DisplayName("a")]
        public float RangeBeginning { get; set; }
        [DisplayName("b")]
        public float RangeEnd { get; set; }
        [DisplayName("d")]
        public double Precision { get; set; }
        [DisplayName("N")]
        public int Number { get; set; }
        [DisplayName("xReal")]
        public float XReal1 { get; set; }
        [DisplayName("xInt")]
        public int XInt1 { get; set; }
        [DisplayName("xBin")]
        public string XBin { get; set; }
        [DisplayName("xInt")]
        public int XInt2 { get; set; }
        [DisplayName("xReal")]
        public float XReal2 { get; set; }
        [DisplayName("f(x)")]
        public float FunctionValue { get; set; }

        public XModel(XModel xModel)
        {
            this.RangeBeginning = xModel.RangeBeginning;
            this.RangeEnd = xModel.RangeEnd;
            this.Precision = xModel.Precision;
        }
        public XModel() { }
    }
}
