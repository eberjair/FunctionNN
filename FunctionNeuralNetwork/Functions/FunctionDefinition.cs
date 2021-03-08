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
        public int[] DefaultX1Domain { get; private set; }
        public int[] DefaultX2Domain { get; private set; }
        public int[] DefaultYRange { get; private set; }

        public FunctionDefinition(DelegateFunction functionImplementation, int[] defaultX1, int[] defaultX2, int[] defaultY ,string label)
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
