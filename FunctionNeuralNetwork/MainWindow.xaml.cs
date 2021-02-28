using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using FunctionNeuralNetwork.Functions;

namespace FunctionNeuralNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NeuralNetwork NeuralNetwork;
        BackgroundWorker goWorker;
        List<FunctionDefinition> gsFunctionDefinitions;
        ProgressWindow progressWindow;
        FunctionViewer FunctionViewer;

        public MainWindow()
        {
            InitializeComponent();
            FunctionViewer = new FunctionViewer(goFuncHost);
            gsFunctionDefinitions = new List<FunctionDefinition>();
            //gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinSumX1X2, new double[2] { -Math.PI/4, Math.PI/4 }, new double[2] { -Math.PI/4, Math.PI/4 }, new double[2] { -1, 1 }, "sin(x1+x2)"));
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinSumX1X2, new double[2] { -2, 2 }, new double[2] { -2, 2 }, new double[2] { -1, 1 }, "sin(x1+x2)"));
            //Adding functions

            for (int i = 0; i < gsFunctionDefinitions.Count; i++)
                goFunctionComboBox.Items.Add(gsFunctionDefinitions[i].Label);

            goFunctionComboBox.SelectedIndex = 0;
            NeuralNetwork = new NeuralNetwork();
            goWorker = new BackgroundWorker { WorkerReportsProgress = true };
            goWorker.DoWork += ExecuteLearning;
            goWorker.RunWorkerCompleted += GoWorker_RunWorkerCompleted;
        }

        private void GoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //goResultsTextBox.Text = (string)e.Result;
            progressWindow.AllowClosing();
            progressWindow.Close();
        }

        private void GoLearningButton_Click(object sender, RoutedEventArgs e)
        {
            progressWindow = new ProgressWindow(goWorker);
            LearnMethod method = goMethodComboBox.SelectedIndex == 0 ? LearnMethod.MaxDescend : LearnMethod.FletcherReeves;
            GradientFactor factor = GradientFactor.n;;
            switch(goGradientFactorComboBox.SelectedIndex)
            {
                case 1: factor = GradientFactor.ln; break;
                case 2: factor = GradientFactor.c1; break;
                case 3: factor = GradientFactor.c2; break;
            }
            LearnOptions learnOptions = new LearnOptions(method, factor, (int)goIterationsUpDown.Value, gsFunctionDefinitions[goFunctionComboBox.SelectedIndex]);
            LearnArguments learnArguments = new LearnArguments(learnOptions, NeuralNetwork);
            goWorker.RunWorkerAsync(argument: learnArguments);
            progressWindow.ShowDialog();


        }


        void ExecuteLearning(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker loWorker = sender as BackgroundWorker;
            LearnArguments arguments = e.Argument as LearnArguments;
            LearnOptions options = arguments.LearnOptions;
            NeuralNetwork neuralNetwork = arguments.NeuralNetwork;
            FunctionDefinition functionDefinition = arguments.LearnOptions.FunctionDefinition;
            int iterations = options.Iterations;
            LearnMethod learnMethod = options.LearnMethod;
            GradientFactor gradientFactor = options.GradientFactor;


            double lnEta = 0.1;

            List<double[]> lsLastGradients = new List<double[]>()
            {
                new double[5] { 1, 1, 1, 1, 1 }, 
                new double[25] { 1, 1, 1, 1, 1 , 1, 1, 1, 1, 1 , 1, 1, 1, 1, 1 , 1, 1, 1, 1, 1 , 1, 1, 1, 1, 1 },
                new double[10] { 1, 1, 1, 1, 1 , 1, 1, 1, 1, 1 },
                new double[10] { 1, 1, 1, 1, 1 , 1, 1, 1, 1, 1 },
                new double[5] { 0, 0, 0, 0, 0 },
                new double[25] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            double s3 = 0;
            double error;
            string result = "";
            FletcherReevesIteration lastIteration = new FletcherReevesIteration();
            FletcherReevesIteration newIteration = new FletcherReevesIteration();
            for (int i=0; i< iterations; i++)
            {
                switch(gradientFactor)
                {
                    case GradientFactor.c2: lnEta = 0.01; break;
                    case GradientFactor.n: lnEta = 0.1 / (i + 1); break;
                    case GradientFactor.ln: lnEta = 0.1 / Math.Log(i + 2); break;
                }

                double[] x1 = functionDefinition.GenerateX(0);
                double[] x2 = functionDefinition.GenerateX(1);
                double[] y = functionDefinition.Evaluate(x1[0], x2[0]);

                    
                switch (learnMethod)
                {
                    case LearnMethod.FletcherReeves:
                            neuralNetwork.LearnFletcherReeves(x1[1], x2[1], y[1], lnEta, i==0, lastIteration, out newIteration, out s3);
                            lastIteration = newIteration;
                        break;
                    case LearnMethod.MaxDescend:
                            neuralNetwork.LearnMaxDescend(x1[1], x2[1], y[1], lnEta, out s3);
                        break;
                }
                error = Math.Abs(s3 - y[1]);
                if (i < 20 || i > iterations - 20)
                    result = result + "Iteration:" + i + ". Error: " + error + "\n";
                loWorker.ReportProgress(100 * i /iterations);
            }
            e.Result = result;
            
        }


    }

    
}
