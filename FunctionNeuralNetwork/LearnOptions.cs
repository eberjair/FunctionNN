using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionNeuralNetwork.Functions;


namespace FunctionNeuralNetwork
{
    public enum LearnMethod { MaxDescend, FletcherReeves }
    public enum GradientFactor { n, ln, c1, c2 }
    public class LearnOptions
    {
        public GradientFactor GradientFactor { get;  }
        public int Iterations { get; }
        public int CurrentIterations { get; }

        public int Interval { get; }
        public FunctionDefinition FunctionDefinition { get; }
        

        public LearnOptions(GradientFactor factor, int currentIterations, int iterations, int interval, FunctionDefinition functionDefinition)
        {
            GradientFactor = factor;
            Iterations = iterations;
            Interval = interval;
            CurrentIterations = currentIterations;
            FunctionDefinition = functionDefinition;
        }
    }
}
