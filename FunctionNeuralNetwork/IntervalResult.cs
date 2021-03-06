using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork
{
    public class IntervalResult
    {
        public bool IsLastInterval { get; private set; }
        public double[] IterationsError { get; private set; }

        public IntervalResult(bool lbLastInterval, double[] lsIterationsError)
        {
            IsLastInterval = lbLastInterval;
            IterationsError = lsIterationsError;
        }
    }
}
