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
        
        public FunctionDefinition(DelegateFunction functionImplementation, string label)
        {
            FunctionImplementation = functionImplementation;
            Label = label;
        }

        public double Evaluate(double x1, double x2)
        {
            return FunctionImplementation(x1, x2);
        }

    }
}
