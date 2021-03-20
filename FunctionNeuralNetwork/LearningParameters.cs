using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionNeuralNetwork.Functions;


namespace FunctionNeuralNetwork
{
    public enum GradientFactorType { n, ln, constant}
    public class LearningParameters : ExecutionParameters
    {
        public GradientFactorType GradientFactorType { get;  }
        public int Interval { get; }
        public double GradientFactor;

        public LearningParameters(GradientFactorType factorType, int iterations, int interval, FunctionDefinition functionDefinition, double gradientFactor):
            base(iterations, functionDefinition)
        {
            GradientFactorType = factorType;
            Interval = interval;
            GradientFactor = gradientFactor;
        }
    }
}
