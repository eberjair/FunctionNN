using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork
{
    public class NNParameters
    {
        public double[] Mean { get; set; } //Layer 1
        public double[] Dev { get; set; } //Layer 1
        public double[] Bj { get; set; } //Layer 2
        public double[,] Wij { get; set; } //Layer 2
        public double B3 { get; set; } //Layer 3
        public double[] Wj { get; set; } //Layer 3


        //public double[] Mean { get; set; } //Layer 1
        //public double[] Dev { get; set; } //Layer 1
        //public double[] W { get; set; } //Layer 2
        //public double[] R { get; set; } //Layer 4

        public NNParameters()
        {
            Random random = new Random();

            Mean = new double[20] { 0.05, 0.15, 0.25, 0.35, 0.45, 0.55, 0.65, 0.75, 0.85, 0.95, 0.05, 0.15, 0.25, 0.35, 0.45, 0.55, 0.65, 0.75, 0.85, 0.95 };
            Dev = new double[20] { 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2 };
            B3 = random.NextDouble() * 2 - 1;
            Bj = new double[80];
            Wij = new double[20, 80];
            Wj = new double[80];

            for(int j = 0; j<80; j++)
            {
                Bj[j] = random.NextDouble() * 2 - 1;
                Wj[j] = random.NextDouble() * 2 - 1;
                for(int i = 0; i<20; i++)
                {
                    Wij[i, j] = random.NextDouble() * 2 - 1;
                }
            }

            //Mean = new double[10] { 0.1, 0.3, 0.5, 0.7, 0.9, 0.1, 0.3, 0.5, 0.7, 0.9 };
            //Dev = new double[10] { 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2 };
            //W = new double[25] {
            //    0.5, 0.5, 0.5, 0.5, 0.5,
            //    0.5, 0.5, 0.5, 0.5, 0.5,
            //    0.5, 0.5, 0.5, 0.5, 0.5,
            //    0.5, 0.5, 0.5, 0.5, 0.5,
            //    0.5, 0.5, 0.5, 0.5, 0.5 };
            //R = new double[5] { 0.05, 0.2, 0.5, 0.7, 0.95};
        }
    }
}
