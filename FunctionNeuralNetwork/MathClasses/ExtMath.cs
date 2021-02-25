using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionNeuralNetwork.MathClasses
{
    public class ExtMath
    {
        static public double DotProduct(double[] v1, double[] v2)
        {
            int size = v1.Length != v2.Length? Math.Min(v1.Length, v2.Length) : v1.Length;
            double result = 0;
            for (int i = 0; i < size; i++)
                result += v1[i] * v2[i];
            return result;
        }

        static public double[] ScalarProduct(double scalar, double[] v)
        {
            int size = v.Length;
            double[] result = new double[size];
            for (int i = 0; i < size; i++)
                result[i] = scalar * v[i];
            return result;
        }

        static public double[] SumVectors(double[] v1, double[] v2)
        {
            int size = v1.Length != v2.Length ? Math.Min(v1.Length, v2.Length) : v1.Length;
            double[] result = new double[size];
            for(int i=0; i<size; i++)
                result[i] = v1[i] + v2[i];
            return result;
        }
    }
}
