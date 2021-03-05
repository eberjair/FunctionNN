﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        }

        public void Save(string lcPath)
        {
            StreamWriter streamWriter = new StreamWriter(lcPath, false);
            for (int j = 0; j < 80; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    
                    streamWriter.WriteLine(Wij[i, j]);
                }
            }
            for (int j = 0; j < 80; j++)
            {
                streamWriter.WriteLine(Bj[j]);
            }
            for (int j = 0; j < 80; j++)
            {
                streamWriter.WriteLine(Wj[j]);
            }
            streamWriter.WriteLine(B3);
            streamWriter.Close();
        }

        public bool Load(string lcPath)
        {
            try
            {
                double[,] posWij = new double[20, 80];
                double[] posBj = new double[80];
                double[] posWj = new double[80];
                double posB3;
                using (StreamReader streamReader = new StreamReader(lcPath))
                {
                    for (int j = 0; j < 80; j++)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Double.TryParse(streamReader.ReadLine(), out posWij[i, j]);
                        }
                    }
                    for (int j = 0; j < 80; j++)
                    {
                        Double.TryParse(streamReader.ReadLine(), out posBj[j]);
                    }
                    for (int j = 0; j < 80; j++)
                    {
                        Double.TryParse(streamReader.ReadLine(), out posWj[j]);
                    }
                    Double.TryParse(streamReader.ReadLine(), out posB3);
                    streamReader.Close();
                }
                Wij = posWij;
                Bj = posBj;
                Wj = posWj;
                B3 = posB3;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
