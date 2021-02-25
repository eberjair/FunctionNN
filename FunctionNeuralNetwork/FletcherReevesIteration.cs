using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork
{
    public class FletcherReevesIteration
    {
        public double DotProduct { get; set; }
        public double RoB3 { get; set; }
        public double[] RoWj { get; set; }
        public double[] RoBj { get; set; }
        public double[,] RoWij { get; set; }
        
    }
}
