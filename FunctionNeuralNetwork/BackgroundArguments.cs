using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FunctionNeuralNetwork
{
    public class BackgroundArguments
    {
        public ExecutionParameters ExecutionOptions { get; }
        
        public NeuralNetwork NeuralNetwork { get; }
        

        public BackgroundArguments(ExecutionParameters executionOptions, NeuralNetwork neuralNetwork)
        {
            ExecutionOptions = executionOptions;
            NeuralNetwork = neuralNetwork;
        }
    }
}
