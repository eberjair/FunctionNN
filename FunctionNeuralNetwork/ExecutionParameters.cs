using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionNeuralNetwork.Functions;

namespace FunctionNeuralNetwork
{
    public class ExecutionParameters
    {
        public int Iterations { get; protected set; }
        public FunctionDefinition FunctionDefinition { get; protected set; }

        public ExecutionParameters(int iterations, FunctionDefinition functionDefinition)
        {
            Iterations = iterations;
            FunctionDefinition = functionDefinition;
        }
    }
}
