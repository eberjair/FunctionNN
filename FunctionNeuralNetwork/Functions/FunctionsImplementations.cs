using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork.Functions
{
    public static class FunctionsImplementations
    {
        public static double SinSumX1X2(double x1, double x2)
        {
            return Math.Sin(x1 + x2);
        }

        public static double Gaussian(double x1, double x2)
        {
            return Math.Exp(-((x1 * x1 + x2 * x2) / (8)));
        }
    }
}
