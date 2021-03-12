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
        BackgroundWorker goLearningWorker;
        BackgroundWorker goTestingWorker;
        List<FunctionDefinition> gsFunctionDefinitions;
        ProgressWindow progressWindow;
        FunctionViewer FunctionViewer;
        NNViewer NNViewer;
        Random random;
        double[] X1Domain;
        double[] X2Domain;
        double[] YRange;
        double[] gsErrorResults;
        int gnNextResultsIndex;

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
            random = new Random();
            NeuralNetwork = new NeuralNetwork();
            FunctionViewer = new FunctionViewer(goFuncHost, this);
            NNViewer = new NNViewer(goNNHost, NeuralNetwork);

            X1Domain = new double[2] { (double)goX1minIUP.Value , (double)goX1maxIUP.Value};
            X2Domain = new double[2] { (double)goX2minIUP.Value, (double)goX2maxIUP.Value };
            YRange = new double[2] { (double)goYminIUP.Value, (double)goYmaxIUP.Value };

            //Adding functions
            gsFunctionDefinitions = new List<FunctionDefinition>();
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.Gaussian, new int[2] {-4, 4 }, new int[2] { -4, 4}, new int[2] {0,1 }, "y=exp(-(x1^2 + x2^2)/8)"));
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinSumAbsX1X2, new int[2] { -4, 4 }, new int[2] { -4, 4 }, new int[2] { -1, 1 }, "y=sin(|x1|+|x2|)"));
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinProductX1X2, new int[2] { -2, 2 }, new int[2] { -2, 2 }, new int[2] { -1, 1 }, "y=sin(x1*x2)"));
            

            for (int i = 0; i < gsFunctionDefinitions.Count; i++)
                goFunctionComboBox.Items.Add(gsFunctionDefinitions[i].Label);
            goFunctionComboBox.SelectedIndex = 0;

            PrintWeights();

            goLearningWorker = new BackgroundWorker { WorkerReportsProgress = true };
            goLearningWorker.DoWork += ExecuteLearning;
            goLearningWorker.RunWorkerCompleted += GoWorker_RunWorkerCompleted;

            goTestingWorker = new BackgroundWorker { WorkerReportsProgress = true };
            goTestingWorker.DoWork += GoTestingWorker_DoWork;
            goTestingWorker.RunWorkerCompleted += GoWorker_RunWorkerCompleted;

            goFunctionComboBox.SelectionChanged += GoFunctionComboBox_SelectionChanged;
            goSyncCB.Checked += GoSyncCB_Checked;
            goSyncCB.Unchecked += GoSyncCB_Checked;
            goX1minIUP.ValueChanged += DomainValueChanged;
            goX1maxIUP.ValueChanged += DomainValueChanged;
            goX2minIUP.ValueChanged += DomainValueChanged;
            goX2maxIUP.ValueChanged += DomainValueChanged;
            goYminIUP.ValueChanged += DomainValueChanged;
            goYmaxIUP.ValueChanged += DomainValueChanged;
        }

        
        private void GoTestingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker loWorker = sender as BackgroundWorker;
            BackgroundArguments arguments = e.Argument as BackgroundArguments;
            ExecutionParameters options = arguments.ExecutionOptions;
            NeuralNetwork neuralNetwork = arguments.NeuralNetwork;
            FunctionDefinition functionDefinition = arguments.ExecutionOptions.FunctionDefinition;
            int iterations = options.Iterations;
            double s3 = 0;
            double[] lsError = new double[iterations];
            int porcentage = 0;
            for (int i = 0;  i < iterations; i++)
            {
                double[] x1 = GenerateX1();
                double[] x2 = GenerateX2();
                double y = functionDefinition.Evaluate(x1[1], x2[1]);
                double yNormalized = NormalizeY(y);
                s3 = neuralNetwork.CalculateS3(x1[0], x2[0]);
                lsError[i] = Math.Abs(s3 - yNormalized);
                int newPorcentage = 100 * i / iterations;
                if ( newPorcentage > porcentage)
                {
                    loWorker.ReportProgress(newPorcentage);
                    porcentage = newPorcentage;
                }
                    
            }
            IntervalResult intervalResult = new IntervalResult(true, lsError);
            e.Result = intervalResult;
        }

        private void GoFunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goX1minIUP.Value = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex].DefaultX1Domain[0];
            goX1maxIUP.Value = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex].DefaultX1Domain[1];
            goX2minIUP.Value = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex].DefaultX2Domain[0];
            goX2maxIUP.Value = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex].DefaultX2Domain[1];
            goYminIUP.Value = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex].DefaultYRange[0];
            goYmaxIUP.Value = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex].DefaultYRange[1];

            VisualizeFunction(RendererEnum.Both);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
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

                if(x1Min >= x1Max || x2Min>=x2Max)
                {
                    MessageBox.Show("Invalid function domain", "Parameters error",MessageBoxButton.OK);
                    return;
                }

                if(renderer == RendererEnum.Both)
                {
                    for (int i = 0; i < NumberX1Values; i++)
                    {
                        for (int j = 0; j < NumberX2Values; j++)
                        {
                            double x1 = x1Min + i * x1Delta;
                            double x2 = x2Min + j * x2Delta;
                            lsFunctionData[i, j] = function.Evaluate(x1 , x2 );
                            lsNNData[i, j] = GetYFromNormalized(NeuralNetwork.CalculateS3(NormalizeX1(x1), NormalizeX2(x2)));
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
                            lsFunctionData[i, j] = function.Evaluate(x1Min + i * x1Delta, x2Min + j * x2Delta);
                            
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
                            lsNNData[i, j] = GetYFromNormalized(NeuralNetwork.CalculateS3(NormalizeX1(x1Min + i * x1Delta), NormalizeX2(x2Min + j * x2Delta)));
                        }
                    }
                    FunctionViewer.VisualizeData(new double[4] { x1Min, x1Max, x2Min, x2Max }, lsNNData, RendererEnum.NeuralNetwork);
                }
                
            }
        }

        private void GoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IntervalResult intervalResult = e.Result as IntervalResult;
            double[] lsIntervalResults = intervalResult.IterationsError;
            
            for(int i = 0; i < lsIntervalResults.Length; i++, gnNextResultsIndex++)
            {
                gsErrorResults[gnNextResultsIndex] = lsIntervalResults[i];
            }
            if(intervalResult.IsLastInterval)
            {
                double[] dataToVisualize;
                if(gsErrorResults.Length > 100)
                {
                    dataToVisualize = new double[100];
                    int step = (int)(gsErrorResults.Length / 100d);
                    for(int i=0; i<100; i++)
                    {
                        for(int j=0; j<step; j++)
                        {
                            dataToVisualize[i] += gsErrorResults[i * step + j];
                        }
                        dataToVisualize[i] /= (double)step;
                    }
                }
                else
                {
                    dataToVisualize = gsErrorResults;
                }
                ChartModel chartModel = new ChartModel(dataToVisualize);
                goPlotView.Model = chartModel.PlotModel;
            }
            progressWindow.AllowClosing();
            progressWindow.Close();
        }

        private void GoLearningButton_Click(object sender, RoutedEventArgs e)
        {
            double x1Min = (double)goX1minIUP.Value;
            double x1Max = (double)goX1maxIUP.Value;
            double x2Min = (double)goX2minIUP.Value;
            double x2Max = (double)goX2maxIUP.Value;
            if (x1Min >= x1Max || x2Min >= x2Max)
            {
                MessageBox.Show("Invalid function domain", "Parameters error", MessageBoxButton.OK);
                return;
            }
            if (goYminIUP.Value >= goYmaxIUP.Value)
            {
                MessageBox.Show("Invalid function range", "Parameters error", MessageBoxButton.OK);
                return;
            }

            GradientFactor factor = GradientFactor.n;;
            switch(goGradientFactorComboBox.SelectedIndex)
            {
                case 1: factor = GradientFactor.ln; break;
                case 2: factor = GradientFactor.c1; break;
                case 3: factor = GradientFactor.c2; break;
            }
            int iterations = (int)goIterationsUpDown.Value;
            int interval = (int)goLearningIntervalVisualizationIUD.Value;
            if (interval == 0) interval = (int)goIterationsUpDown.Value;
            int steps = (int)Math.Ceiling((double)iterations / (double)interval);
            
            if(steps > 10)
            {
                MessageBoxResult boxResult = MessageBox.Show("There will be " + (steps - 1) + " visualizations before finishing the learning process.\nDo you want to continue?", "Learning process", MessageBoxButton.YesNo);
                if (boxResult == MessageBoxResult.No) return;
            }

            progressWindow = new ProgressWindow(goLearningWorker);
            FunctionDefinition function = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex];
            gsErrorResults = new double[iterations];
            gnNextResultsIndex = 0;
            for (int currentIterations = 0; currentIterations< iterations; )
            {
                LearningParameters learnOptions = new LearningParameters(factor, currentIterations, iterations, interval, function);
                BackgroundArguments learnArguments = new BackgroundArguments(learnOptions, NeuralNetwork);
                goLearningWorker.RunWorkerAsync(argument: learnArguments);
                progressWindow.ShowDialog();
                currentIterations += interval;
                UpdateUIWeights();
                progressWindow = new ProgressWindow(goLearningWorker);
                if (currentIterations < iterations)
                    MessageBox.Show("Interval executed", "Learning Process", MessageBoxButton.OK);
            }
        }


        void ExecuteLearning(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker loWorker = sender as BackgroundWorker;
            BackgroundArguments arguments = e.Argument as BackgroundArguments;
            ExecutionParameters options = arguments.ExecutionOptions;
            NeuralNetwork neuralNetwork = arguments.NeuralNetwork;
            FunctionDefinition functionDefinition = arguments.ExecutionOptions.FunctionDefinition;
            int iterations = options.Iterations;
            int interval = (options as LearningParameters).Interval;
            int currentIterarions = (options as LearningParameters).CurrentIterations;
            GradientFactor gradientFactor = (options as LearningParameters).GradientFactor;
            double lnEta = 0.1;
            double s3 = 0;
            double[] lsError = new double[Math.Min(interval, iterations-currentIterarions)];

            for (int i=0; i< interval && currentIterarions<iterations; i++, currentIterarions++)
            {
                switch(gradientFactor)
                {
                    case GradientFactor.c2: lnEta = 0.01; break;
                    case GradientFactor.n: lnEta = 0.1 / (currentIterarions + 1); break;
                    case GradientFactor.ln: lnEta = 0.1 / Math.Log(currentIterarions + 2); break;
                }

                double[] x1 = GenerateX1();
                double[] x2 = GenerateX2();
                double y = functionDefinition.Evaluate(x1[1], x2[1]);
                double yNormalized = NormalizeY(y);

                neuralNetwork.LearnMaxDescend(x1[0], x2[0], yNormalized, lnEta, out s3);
                lsError[i] = Math.Abs(s3 - yNormalized);
                loWorker.ReportProgress(100 * i /interval);
            }
            IntervalResult intervalResult = new IntervalResult(currentIterarions == iterations, lsError);
            e.Result = intervalResult;
        }

        double[] GenerateX1()
        {
            double x1min = X1Domain[0];
            double x1max = X1Domain[1];
            double[] values = new double[2] { random.NextDouble(), 0 };
            values[1] = values[0] * (x1max - x1min) + x1min;
            return values;
        }

        double[] GenerateX2()
        {
            double x2min = X2Domain[0];
            double x2max = X2Domain[1];
            double[] values = new double[2] { random.NextDouble(), 0 };
            values[1] = values[0] * (x2max - x2min) + x2min;
            return values;
        }

        double NormalizeX1(double x1)
        {
            return (x1 - X1Domain[0]) / (X1Domain[1] - X1Domain[0]);
        }

        double NormalizeX2(double x2)
        {
            return (x2 - X2Domain[0]) / (X2Domain[1] - X2Domain[0]);
        }

        double NormalizeY(double y)
        {
            return (y - YRange[0]) / (YRange[1] - YRange[0]);
        }

        double GetYFromNormalized(double yNormalized)
        {
            return yNormalized * (YRange[1] - YRange[0]) + YRange[0];
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

        private void GoRefreshB_Click(object sender, RoutedEventArgs e)
        {
            VisualizeFunction(RendererEnum.Both);
        }

        private void DomainValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            double x1Min = (double)goX1minIUP.Value;
            double x1Max = (double)goX1maxIUP.Value;
            double x2Min = (double)goX2minIUP.Value;
            double x2Max = (double)goX2maxIUP.Value;
            double yMin = (double)goYminIUP.Value;
            double yMax = (double)goYmaxIUP.Value;

            BrushConverter converter = new BrushConverter();
            if (x1Min >= x1Max)
            {
                goX1minIUP.BorderBrush = Brushes.Red;
                goX1maxIUP.BorderBrush = Brushes.Red;
            }
            else
            {
                goX1minIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                goX1maxIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                X1Domain[0] = x1Min;
                X1Domain[1] = x1Max;
            }
                
            if (x2Min >= x2Max)
            {
                goX2minIUP.BorderBrush = Brushes.Red;
                goX2maxIUP.BorderBrush = Brushes.Red;
            }
            else
            {
                goX2minIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                goX2maxIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                X2Domain[0] = x2Min;
                X2Domain[1] = x2Max;
            }

            if(yMin >= yMax)
            {
                goYminIUP.BorderBrush = Brushes.Red;
                goYmaxIUP.BorderBrush = Brushes.Red;
            }
            else
            {
                goYminIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                goYmaxIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                YRange[0] = yMin;
                YRange[1] = yMax;
            }
        }

        private void GoTestButton_Click(object sender, RoutedEventArgs e)
        {
            double x1Min = (double)goX1minIUP.Value;
            double x1Max = (double)goX1maxIUP.Value;
            double x2Min = (double)goX2minIUP.Value;
            double x2Max = (double)goX2maxIUP.Value;
            if (x1Min >= x1Max || x2Min >= x2Max)
            {
                MessageBox.Show("Invalid function domain", "Parameters error", MessageBoxButton.OK);
                return;
            }
            if (goYminIUP.Value >= goYmaxIUP.Value)
            {
                MessageBox.Show("Invalid function range", "Parameters error", MessageBoxButton.OK);
                return;
            }

            int iterations = (int)goTestUpDown.Value;
            gnNextResultsIndex = 0;
            progressWindow = new ProgressWindow(goTestingWorker) { Title="Testing Neural Network"};
            FunctionDefinition function = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex];
            gsErrorResults = new double[iterations];
            ExecutionParameters testingParameters = new ExecutionParameters(iterations, function);
            BackgroundArguments backgroundArguments = new BackgroundArguments(testingParameters, NeuralNetwork);
            goTestingWorker.RunWorkerAsync(argument: backgroundArguments);
            progressWindow.ShowDialog();
        }
    }

}
