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
        public float TranslatedFunctionValue { get; set; }
        public float SurviveProbability { get; set; }
        public float SurviveDistributionFunction { get; set; }

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

        public static int XRealToXInt(double xReal, int binaryLength, double rangeBeginning, double rangeEnd)
        {
            return (int)Math.Round(1 / (rangeEnd - rangeBeginning) * (xReal - rangeBeginning) * (Math.Pow(2, binaryLength) - 1));
        }

        public static string XIntToXBin(int xInt, int binaryLength)
        {
            var sb = new StringBuilder(Convert.ToString(xInt, 2));
            sb.Insert(0, new String('0', binaryLength - sb.Length));
            return sb.ToString();
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
