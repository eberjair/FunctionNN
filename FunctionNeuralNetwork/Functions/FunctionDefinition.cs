using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork.Functions
{
    public delegate double DelegateFunction(double x1, double x2);

    public class FunctionDefinition
    {
        readonly DelegateFunction FunctionHandler;
        public readonly double[] X1Domain;
        public readonly double[] X2Domain;
        private readonly double[] YRange;
        public readonly string Label;
        readonly Random Generator;

        public FunctionDefinition(DelegateFunction functionImplementation, double[] x1Domain, double[] x2Domain, double[] yRange, string label)
        {
            Generator = new Random();
            FunctionHandler = functionImplementation;
            if (x1Domain != null)
                X1Domain = x1Domain;
            else
                X1Domain = new double[2] { 0, 1 };
            if (x2Domain != null)
                X2Domain = x2Domain;
            else
                X2Domain = new double[2] { 0, 1 };
            if (yRange != null)
                YRange = yRange;
            else
                YRange = new double[2] { 0, 1 };

            Label = label;
        }

        public double[] Evaluate(double x1, double x2)
        {
            double[] lsResult = new double[2];
            lsResult[0] = FunctionHandler(x1, x2);
            lsResult[1] = (lsResult[0] - YRange[0]) / (YRange[1] - YRange[0]);
            return lsResult;
        }

        public double[] GenerateX(int lnVariable)
        {
            double[] lsResult = new double[2];
            lsResult[1] = Generator.NextDouble();
            if (lnVariable == 0)
                lsResult[0] = X1Domain[0] + lsResult[1] * (X1Domain[1] - X1Domain[0]);
            else
                lsResult[0] = X2Domain[0] + lsResult[1] * (X2Domain[1] - X2Domain[0]);

            return lsResult;
        }

        
    }
}
