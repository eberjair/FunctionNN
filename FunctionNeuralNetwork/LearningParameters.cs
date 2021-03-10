using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionNeuralNetwork.Functions;


namespace FunctionNeuralNetwork
{
    public enum GradientFactor { n, ln, c1, c2 }
    public class LearningParameters : ExecutionParameters
    {
        public GradientFactor GradientFactor { get;  }
        public int CurrentIterations { get; }
        public int Interval { get; }
        
        public LearningParameters(GradientFactor factor, int currentIterations, int iterations, int interval, FunctionDefinition functionDefinition):
            base(iterations, functionDefinition)
        {
            GradientFactor = factor;
            CurrentIterations = currentIterations;
            Interval = interval;
        }
    }
}
