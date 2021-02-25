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
        public LearnMethod LearnMethod { get;  }
        public GradientFactor GradientFactor { get;  }
        public int Iterations { get;  }
        public FunctionDefinition FunctionDefinition { get; }
        

        public LearnOptions(LearnMethod method, GradientFactor factor, int iterations, FunctionDefinition functionDefinition)
        {
            LearnMethod = method;
            GradientFactor = factor;
            Iterations = iterations;
            FunctionDefinition = functionDefinition;
        }
    }
}
