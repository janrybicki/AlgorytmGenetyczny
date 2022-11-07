using System;
using System.ComponentModel;
using System.Text;

namespace AlgorytmGenetyczny.Models
{
    public class IndividualModel
    {
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
        [DisplayName("g(x)")]
        public float TranslatedFunctionValue { get; set; }
        [DisplayName("p(x)")]
        public float SurviveProbability { get; set; }
        [DisplayName("q(x)")]
        public float SurviveDistributionFunction { get; set; }
        //public bool IsSurvivor { get; set; } = false;
        [DisplayName("r")]
        public float R { get; set; }
        public bool IsParent { get; set; } = false;
        public string ChildXBin { get; set; }
        public int CrossingPoint { get; set; }
        public List<int> MutantBits { get; set; }
        public string MutantBitsWithSeparators { get; set; }
        public string XBinAfterMutation { get; set; }
        public float XRealAfterMutation { get; set; }
        public float FunctionValueAfterMutation { get; set; }
        public IndividualModel()
        {
            MutantBits = new List<int>();
        }

        public static double GenerateRandomXReal(double rangeBeginning, double rangeEnd)
        {
            var random = new Random();
            return random.NextDouble() * (rangeEnd - rangeBeginning) + rangeBeginning;
        }
        public static double Function(double xReal)
        {
            return (xReal % 1) * (Math.Cos(xReal * 20 * Math.PI) - Math.Sin(xReal));
        }
        public static double Round(double xReal, double precision)
        {
            return Math.Abs(xReal % precision) < precision / 2
                ? xReal - xReal % precision
                    : xReal + Math.Sign(xReal) * precision - xReal % precision;
        }
        public string XRealToXBin(int binaryLength, float rangeBeginning, float rangeEnd)
        {
            var xInt = (int)Math.Round(1 / (rangeEnd - rangeBeginning) * (XReal1 - rangeBeginning) * (Math.Pow(2, binaryLength) - 1));
            var xBin = new StringBuilder(Convert.ToString(xInt, 2));
            return xBin.Insert(0, new String('0', binaryLength - xBin.Length)).ToString();
        }
        public static string XRealToXBin(int binaryLength, float rangeBeginning, float rangeEnd, float xReal)//do wywalenia pozniej
        {
            var xInt = (int)Math.Round(1 / (rangeEnd - rangeBeginning) * (xReal - rangeBeginning) * (Math.Pow(2, binaryLength) - 1));
            var xBin = new StringBuilder(Convert.ToString(xInt, 2));
            return xBin.Insert(0, new String('0', binaryLength - xBin.Length)).ToString();
        }
        public float XBinToXReal(string xBin, double rangeBeginning, double rangeEnd, int binaryLength)//rozwazyc zmiane xBin na property XBin
        {
            var xInt = Convert.ToInt32(xBin, 2);
            return (float)(xInt * (rangeEnd - rangeBeginning) / (Math.Pow(2, binaryLength) - 1) + rangeBeginning);
        }
        public static int XRealToXInt(double xReal, int binaryLength, double rangeBeginning, double rangeEnd)
        {
            return (int)Math.Round(1 / (rangeEnd - rangeBeginning) * (xReal - rangeBeginning) * (Math.Pow(2, binaryLength) - 1));
        }

        public static string XIntToXBin(int xInt, int binaryLength)
        {
            var xBin = new StringBuilder(Convert.ToString(xInt, 2));
            xBin.Insert(0, new String('0', binaryLength - xBin.Length));
            return xBin.ToString();
        }
        public static int XBinToXInt(string xBin)
        {
            return Convert.ToInt32(xBin, 2);
        }
        public static double XIntToXReal(int xInt, double rangeBeginning, double rangeEnd, int binaryLength)
        {
            return xInt * (rangeEnd - rangeBeginning) / (Math.Pow(2, binaryLength) - 1) + rangeBeginning;
        }

    }
}
