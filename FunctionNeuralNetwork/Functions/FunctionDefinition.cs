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
        public DelegateFunction FunctionImplementation;
        public readonly string Label;
        public double[] DefaultX1Domain { get; private set; }
        public double[] DefaultX2Domain { get; private set; }
        public double[] DefaultYRange { get; private set; }

        public FunctionDefinition(DelegateFunction functionImplementation, double[] defaultX1, double[] defaultX2, double[] defaultY ,string label)
        {
            FunctionImplementation = functionImplementation;
            DefaultX1Domain = defaultX1;
            DefaultX2Domain = defaultX2;
            DefaultYRange = defaultY;
            Label = label;
        }

        public double Evaluate(double x1, double x2)
        {
            return FunctionImplementation(x1, x2);
        }

    }
}
