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
using System.Threading;
using System.ComponentModel;
using FunctionNeuralNetwork.Functions;
using Microsoft.Win32;

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
        NNViewer NNViewer;

        public MainWindow()
        {
            InitializeComponent();
            NeuralNetwork = new NeuralNetwork();
            FunctionViewer = new FunctionViewer(goFuncHost, this);
            NNViewer = new NNViewer(goNNHost, NeuralNetwork);

            gsFunctionDefinitions = new List<FunctionDefinition>();
            //gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinSumX1X2, new double[2] { -Math.PI/4, Math.PI/4 }, new double[2] { -Math.PI/4, Math.PI/4 }, new double[2] { -1, 1 }, "sin(x1+x2)"));
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinSumX1X2, new double[2] { -2, 2 }, new double[2] { -2, 2 }, new double[2] { -1, 1 }, "sin(x1+x2)"));
            //Adding functions

            for (int i = 0; i < gsFunctionDefinitions.Count; i++)
                goFunctionComboBox.Items.Add(gsFunctionDefinitions[i].Label);
            goFunctionComboBox.SelectedIndex = 0;

            PrintWeights();

            goWorker = new BackgroundWorker { WorkerReportsProgress = true };
            goWorker.DoWork += ExecuteLearning;
            goWorker.RunWorkerCompleted += GoWorker_RunWorkerCompleted;

            goSyncCB.Checked += GoSyncCB_Checked;
            goSyncCB.Unchecked += GoSyncCB_Checked;
        }

        private void GoSyncCB_Checked(object sender, RoutedEventArgs e)
        {
            FunctionViewer.Synchronized = (bool)goSyncCB.IsChecked;
        }

        void PrintWeights()
        {
            goWeightsTB.Text = "";
            
            for(int j=0; j<80; j++)
            {
                for(int i=0; i<20; i++)
                {
                    goWeightsTB.Text += "wij"+ i +", "+ j + ":    " + NeuralNetwork.Wij[i,j] + "\n";
                }
            }
            for (int j = 0; j < 80; j++)
            {
                goWeightsTB.Text += "bj" + j + ":    " + NeuralNetwork.Bj[j] + "\n";
            }
            for (int j=0; j<80;j++)
            {
                goWeightsTB.Text += "wj" + j + ":    " + NeuralNetwork.Wj[j] + "\n";
            }
            goWeightsTB.Text += "b3:    " + NeuralNetwork.B3 + "\n";

        }

        public void VisualizeFunction(RendererEnum renderer)
        {
            if (gsFunctionDefinitions.Count > 0)
            {
                FunctionDefinition function = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex];
                int NumberX1Values = (int)goX1IntervalsIUD.Value + 1;
                int NumberX2Values = (int)goX2IntervalsIUD.Value + 1;
                double x1Min = (double)goX1minIUP.Value;
                double x1Max = (double)goX1maxIUP.Value;
                double x2Min = (double)goX2minIUP.Value;
                double x2Max = (double)goX2maxIUP.Value;
                double x1Delta = (x1Max - x1Min) / (double)goX1IntervalsIUD.Value;
                double x2Delta = (x2Max - x2Min) / (double)goX2IntervalsIUD.Value;
                double[,] lsFunctionData = new double[NumberX1Values, NumberX2Values];
                double[,] lsNNData = new double[NumberX1Values, NumberX2Values];

                if(renderer == RendererEnum.Both)
                {
                    for (int i = 0; i < NumberX1Values; i++)
                    {
                        for (int j = 0; j < NumberX2Values; j++)
                        {
                            lsFunctionData[i, j] = function.Evaluate(x1Min + i * x1Delta, x2Min + j * x2Delta)[0];
                            lsNNData[i, j] = NeuralNetwork.CalculateS3(x1Min + i * x1Delta, x2Min + j * x2Delta);
                        }
                    }
                    FunctionViewer.VisualizeData(new double[4] { x1Min, x1Max, x2Min, x2Max }, lsFunctionData, RendererEnum.Function);
                    FunctionViewer.VisualizeData(new double[4] { x1Min, x1Max, x2Min, x2Max }, lsNNData, RendererEnum.NeuralNetwork);
                }
                else if(renderer == RendererEnum.Function)
                {
                    for (int i = 0; i < NumberX1Values; i++)
                    {
                        for (int j = 0; j < NumberX2Values; j++)
                        {
                            lsFunctionData[i, j] = function.Evaluate(x1Min + i * x1Delta, x2Min + j * x2Delta)[0];
                            
                        }
                    }
                    FunctionViewer.VisualizeData(new double[4] { x1Min, x1Max, x2Min, x2Max }, lsFunctionData, RendererEnum.Function);
                }
                else
                {
                    for (int i = 0; i < NumberX1Values; i++)
                    {
                        for (int j = 0; j < NumberX2Values; j++)
                        {
                            lsNNData[i, j] = NeuralNetwork.CalculateS3(x1Min + i * x1Delta, x2Min + j * x2Delta);
                        }
                    }
                    FunctionViewer.VisualizeData(new double[4] { x1Min, x1Max, x2Min, x2Max }, lsNNData, RendererEnum.NeuralNetwork);
                }
                
            }
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
            LearnMethod method = LearnMethod.MaxDescend;
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

        private void GoRandomizeB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This action will generate new random weights. Do you want to continue?", "Randomize weights", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                NNParameters parameters = new NNParameters();
                NeuralNetwork.SetParameters(parameters);
                UpdateUIWeights();
            }
        }

        private void UpdateUIWeights()
        {
            PrintWeights();
            VisualizeFunction(RendererEnum.NeuralNetwork);
            NNViewer.UpdateAxonsColor();
        }

        private void GoSaveB_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Neural Network Weights (*.nnf)|*.nnf",
                FileName = "weights.nnf"
            };
            
            if((bool) saveFileDialog.ShowDialog(this))
            {
                NeuralNetwork.SaveParameter(saveFileDialog.FileName);
            }
        }

        private void GoLoadB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Neural Network Weights (*.nnf)|*.nnf"
            };

            if((bool)openFileDialog.ShowDialog(this))
            {
                NNParameters parameters = new NNParameters();
                if(parameters.Load(openFileDialog.FileName))
                {
                    NeuralNetwork.SetParameters(parameters);
                    UpdateUIWeights();
                    MessageBox.Show("Weights correctly loaded", "Loading weights", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("The file has incorrect format", "Loading weights", MessageBoxButton.OK);
                }
            }
        }
    }

}
