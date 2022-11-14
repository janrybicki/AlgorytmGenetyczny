using System;
using System.ComponentModel;
using System.Text;

namespace AlgorytmGenetyczny.Models
{
    public class IndividualModel
    {
        public float XReal { get; set; }

        public float XRealAfterSelection { get; set; }

        public float XRealAfterMutation { get; set; }

        public string XBinAfterSelection { get; set; } = string.Empty;

        public string XBinAfterCrossing { get; set; } = string.Empty;

        public string XBinAfterMutation { get; set; } = string.Empty;

        public float FunctionValue { get; set; }

        public float TranslatedFunctionValue { get; set; }

        public float SurviveProbability { get; set; }

        public float SurviveDistributionFunction { get; set; }

        public float R { get; set; }

        public bool IsParent { get; set; } = false;

        public int CrossingPoint { get; set; }

        public string MutantBits { get; set; } = string.Empty;
        
        public float FunctionValueAfterMutation { get; set; }

        public static float GenerateRandomXReal(float rangeBeginning, float rangeEnd)
        {
            var random = new Random();
            return (float)random.NextDouble() * (rangeEnd - rangeBeginning) + rangeBeginning;
        }
        public static float CalculateFunctionValue(float xReal)
        {
            return (float)((xReal % 1) * (Math.Cos(xReal * 20 * Math.PI) - Math.Sin(xReal)));
        }
        public static float Round(float xReal, double precision)
        {
            return (float)(Math.Abs(xReal % precision) < precision / 2
                ? xReal - xReal % precision
                    : xReal + Math.Sign(xReal) * precision - xReal % precision);
        }
        public string XRealToXBin(int binaryLength, float rangeBeginning, float rangeEnd)
        {
            var xInt = (int)Math.Round(1 / (rangeEnd - rangeBeginning) * (XReal - rangeBeginning) * (Math.Pow(2, binaryLength) - 1));
            var xBin = new StringBuilder(Convert.ToString(xInt, 2));
            return xBin.Insert(0, new String('0', binaryLength - xBin.Length)).ToString();
        }
        public static string XRealToXBin(int binaryLength, float rangeBeginning, float rangeEnd, float xReal)//do wywalenia pozniej
        {
            var xInt = (int)Math.Round(1 / (rangeEnd - rangeBeginning) * (xReal - rangeBeginning) * (Math.Pow(2, binaryLength) - 1));
            var xBin = new StringBuilder(Convert.ToString(xInt, 2));
            return xBin.Insert(0, new String('0', binaryLength - xBin.Length)).ToString();
        }
        public static float XBinToXReal(string xBin, float rangeBeginning, float rangeEnd, int binaryLength)//rozwazyc zmiane xBin na property XBinAfterSelection
        {
            var xInt = Convert.ToInt32(xBin, 2);
            return (float)(xInt * (rangeEnd - rangeBeginning) / (Math.Pow(2, binaryLength) - 1) + rangeBeginning);
        }
    }
}
