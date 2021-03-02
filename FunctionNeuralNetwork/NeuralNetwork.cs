using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork
{
    public class NeuralNetwork
    {
        //Version 2
        double[] S1;
        double[] S2;
        double S3;
        double X1;
        double X2;

        double[] X;
        double[] S;
        public double[] Bj { get; private set; }
        public double[,] Wij { get; private set; }
        public double B3 { get; private set; }
        public double[] Wj { get; private set; }

        double[] BjG;
        double[,] WijG;
        double B3G;
        double[] WjG;


        public NNParameters Parameters { get; private set; }

        public NeuralNetwork()
        {
            Parameters = new NNParameters();

            //Version2
            S1 = new double[20];
            S2 = new double[80];

            X = Parameters.Mean;
            S = Parameters.Dev;
            Bj = Parameters.Bj;
            Wij = Parameters.Wij;
            B3 = Parameters.B3;
            Wj = Parameters.Wj;

            BjG = new double[80];
            WijG = new double[20, 80];
            WjG = new double[80];

            
        }

        public void SetParameters(NNParameters parameters)
        {
            //Version 2
            Parameters = parameters;
            X = Parameters.Mean;
            S = Parameters.Dev;
            Bj = Parameters.Bj;
            Wij = Parameters.Wij;
            B3 = Parameters.B3;
            Wj = Parameters.Wj;
        }

        double GaussFunction(double x, double mean, double dev)
        {
            return Math.Exp(-(x - mean) * (x - mean) / (2 * dev * dev));
        }

        double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        private double CalculateS3(double x1, double x2)
        {
            X1 = x1;
            X2 = x2;
            
            //Version 2
            //Layer 1
            for(int i=0; i<10; i++)
            {
                S1[i] = GaussFunction(x1, X[i], S[i]);
                S1[i + 10] = GaussFunction(x2, X[i + 10], S[i + 10]);
            }

            //Layer 2 & Layer 3
            double z3 = B3;
            for (int j=0; j<80; j++)
            {
                double z = Bj[j];
                for(int i=0; i<20; i++)
                    z += Wij[i, j] * S1[i];
                S2[j] = Sigmoid(z);
                z3 += Wj[j] * S2[j];
            }
            S3 = Sigmoid(z3);
            return S3;

            
        }

        public void LearnMaxDescend(double x1, double x2, double Y, double lnGradientFactor, out double s3)
        {
            double lnN = lnGradientFactor;
            s3 = CalculateS3(x1, x2);
            
            CalculateGradiants(Y);

            B3 += -lnN * B3G;
            if (B3 > 1) B3 = 1;
            if (B3 < -1) B3 = -1;
            for(int j=0; j<80; j++)
            {
                Wj[j] += - lnN * WjG[j];
                if (Wj[j] > 1) Wj[j] = 1;
                if (Wj[j] < -1) Wj[j] = -1;
                Bj[j] += -lnN * BjG[j];
                if (Bj[j] > 1) Bj[j] = 1;
                if (Bj[j] < -1) Bj[j] = -1;
                for (int i=0; i<20;i++)
                {
                    Wij[i, j] += -lnN * WijG[i, j];
                    if (Wij[i,j] > 1) Wij[i,j] = 1;
                    if (Wij[i,j] < -1) Wij[i, j] = -1;
                }
            }
            
        }


        public void LearnFletcherReeves(double x1, double x2, double Y, double lnGradientFactor, bool lbFirstIteration, FletcherReevesIteration lastIteration, out FletcherReevesIteration newIteration, out double s3)
        {
            double lnN = lnGradientFactor;
            s3 = CalculateS3(x1, x2);
            CalculateGradiants(Y);

            double RoB3 = 0;
            double[] RoWj = new double[80];
            double[] RoBj = new double[80];
            double[,] RoWij = new double[20, 80];
            double[] RoX = new double[20];
            double[] RoS = new double[20];
            newIteration = new FletcherReevesIteration() { DotProduct = 0 };

            if (lbFirstIteration)
            {
                RoB3 = -B3G;
                B3 += lnN * RoB3;
                newIteration.DotProduct += B3G * B3G;
                
                for (int j = 0; j < 80; j++)
                {
                    RoWj[j] = -WjG[j];
                    Wj[j] += lnN * RoWj[j];
                    newIteration.DotProduct += WjG[j] * WjG[j];
                    
                    RoBj[j] = -BjG[j];
                    Bj[j] += lnN * RoBj[j];
                    newIteration.DotProduct += BjG[j] * BjG[j];
                    
                    for (int i = 0; i < 20; i++)
                    {
                        RoWij[i, j] = -WijG[i, j];
                        Wij[i, j] += lnN * RoWij[i, j];
                        newIteration.DotProduct += WijG[i, j] * WijG[i, j];
                    }
                }

            }
            else
            {
                newIteration.DotProduct += B3G * B3G;
                for (int j = 0; j < 80; j++)
                {
                    newIteration.DotProduct += WjG[j] * WjG[j];
                    newIteration.DotProduct += BjG[j] * BjG[j];

                    for (int i = 0; i < 20; i++)
                    {
                        newIteration.DotProduct += WijG[i, j] * WijG[i, j];
                    }
                }

                double betha = newIteration.DotProduct / lastIteration.DotProduct;

                RoB3 = -B3G + betha * lastIteration.RoB3;
                B3 += lnN * RoB3;
                for (int j = 0; j < 80; j++)
                {
                    RoWj[j] = -WjG[j] + betha * lastIteration.RoWj[j];
                    Wj[j] += lnN * RoWj[j];

                    RoBj[j] = -BjG[j] + betha * lastIteration.RoBj[j];
                    Bj[j] += lnN * RoBj[j];
                    
                    for (int i = 0; i < 20; i++)
                    {
                        RoWij[i, j] = -WijG[i, j] + betha *lastIteration.RoWij[i,j];
                        Wij[i, j] += lnN * RoWij[i, j];
                    }
                }

                
            }

            newIteration.RoB3 = RoB3;
            newIteration.RoBj = RoBj;
            newIteration.RoWij = RoWij;
            newIteration.RoWj = RoWj;
        }

        void CalculateGradiants(double Y)
        {
            //Version 2
            double dEdS3 = S3 - Y;
            double dS3dz3 = S3 * (1 - S3);
            B3G = dEdS3 * dS3dz3;
            
            double[] dS2dzj = new double[80];
            for(int j=0; j<80; j++)
            {
                WjG[j] = B3G * S2[j];
                dS2dzj[j] = S2[j] * (1 - S2[j]);
                BjG[j] = B3G * Wj[j] * dS2dzj[j];
                for(int i= 0; i<20; i++)
                {
                    WijG[i, j] = BjG[j] * S1[i];
                }
            }
                
        }



    }
}
