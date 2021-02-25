using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FunctionNeuralNetwork
{
    public class LearnArguments
    {
        public LearnOptions LearnOptions { get; }
        
        public NeuralNetwork NeuralNetwork { get; }
        

        public LearnArguments(LearnOptions learnOptions, NeuralNetwork neuralNetwork)
        {
            LearnOptions = learnOptions;
            NeuralNetwork = neuralNetwork;
        }
    }
}
