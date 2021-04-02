﻿using System;
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
using System.Windows.Controls.Primitives;
using FunctionNeuralNetwork.Functions;
using Microsoft.Win32;
using XCD= Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;

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
        List<XCD.DoubleUpDown> gsInvalidWeightDoubleUDs;
        List<InputBase> gsInvalidDomainUDs;
        List<InputBase> gsInvalidLearningOptions;
        bool gbInvalidadTestParameter;

        ProgressWindow progressWindow;
        FunctionViewer FunctionViewer;
        NNViewer NNViewer;
        Random random;
        double[] X1Domain;
        double[] X2Domain;
        double[] YRange;
        double[] gsErrorResults;
        int gnNextResultsIndex;
        
        bool gbDragStarted = false;

        /// <summary>
        /// Subroutine where the possible functions to learn are added.
        /// Add here your own functions.
        /// </summary>
        void AddFunctions()
        {
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.Gaussian, new double[2] { -4, 4 }, new double[2] { -4, 4 }, new double[2] { 0, 1 }, "y=exp(-(x1^2 + x2^2)/8)"));
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinSumAbsX1X2, new double[2] { -4, 4 }, new double[2] { -4, 4 }, new double[2] { -1, 1 }, "y=sin(|x1|+|x2|)"));
            gsFunctionDefinitions.Add(new FunctionDefinition(FunctionsImplementations.SinProductX1X2, new double[2] { -2, 2 }, new double[2] { -2, 2 }, new double[2] { -1, 1 }, "y=sin(x1*x2)"));
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
            random = new Random();
            NeuralNetwork = new NeuralNetwork();
            FunctionViewer = new FunctionViewer(goFuncHost, this);
            NNViewer = new NNViewer(goNNHost, NeuralNetwork);
            gsInvalidWeightDoubleUDs = new List<XCD.DoubleUpDown>();
            gsInvalidDomainUDs = new List<InputBase>();
            gsInvalidLearningOptions = new List<InputBase>();
            gbInvalidadTestParameter = false;

            X1Domain = new double[2] { (double)goX1minIUP.Value , (double)goX1maxIUP.Value};
            X2Domain = new double[2] { (double)goX2minIUP.Value, (double)goX2maxIUP.Value };
            YRange = new double[2] { (double)goYminIUP.Value, (double)goYmaxIUP.Value };

            //Adding functions
            gsFunctionDefinitions = new List<FunctionDefinition>();
            AddFunctions();
            
            for (int i = 0; i < gsFunctionDefinitions.Count; i++)
                goFunctionComboBox.Items.Add(gsFunctionDefinitions[i].Label);
            goFunctionComboBox.SelectedIndex = 0;

            goLearningWorker = new BackgroundWorker { WorkerReportsProgress = true };
            goLearningWorker.DoWork += ExecuteLearning;
            goLearningWorker.RunWorkerCompleted += GoWorker_RunWorkerCompleted;
            goLearningWorker.ProgressChanged += GoLearningWorker_ProgressChanged;

            goTestingWorker = new BackgroundWorker { WorkerReportsProgress = true };
            goTestingWorker.DoWork += GoTestingWorker_DoWork;
            goTestingWorker.RunWorkerCompleted += GoWorker_RunWorkerCompleted;

            goFunctionComboBox.SelectionChanged += GoFunctionComboBox_SelectionChanged;
            goSyncCB.Checked += GoSyncCB_Checked;
            goSyncCB.Unchecked += GoSyncCB_Checked;

            BrushConverter converter = new BrushConverter();
            goX1minIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goX1maxIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goX2minIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goX2maxIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goYminIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goYmaxIUP.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");

            goX1minIUP.ValueChanged += Input_ValueChanged;
            goX1maxIUP.ValueChanged += Input_ValueChanged;
            goX2minIUP.ValueChanged += Input_ValueChanged;
            goX2maxIUP.ValueChanged += Input_ValueChanged;
            goYminIUP.ValueChanged += Input_ValueChanged;
            goYmaxIUP.ValueChanged += Input_ValueChanged;

            goX1minIUP.ValueChanged += DomainValueChanged;
            goX1maxIUP.ValueChanged += DomainValueChanged;
            goX2minIUP.ValueChanged += DomainValueChanged;
            goX2maxIUP.ValueChanged += DomainValueChanged;
            goYminIUP.ValueChanged += DomainValueChanged;
            goYmaxIUP.ValueChanged += DomainValueChanged;


            goX1IntervalsIUD.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goX2IntervalsIUD.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goIterationsUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goConstantUD.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goLearningIntervalVisualizationIUD.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goTestUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");

            goX1IntervalsIUD.ValueChanged += Input_ValueChanged;
            goX2IntervalsIUD.ValueChanged += Input_ValueChanged;
            goGradientFactorComboBox.SelectionChanged += GoGradientFactorComboBox_SelectionChanged;
            goIterationsUpDown.ValueChanged += Input_ValueChanged;
            goConstantUD.ValueChanged += Input_ValueChanged; ;
            goLearningIntervalVisualizationIUD.ValueChanged += Input_ValueChanged;
            goTestUpDown.ValueChanged += Input_ValueChanged;

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PrintWeightsUIElements();
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
            e.Result = lsError;
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
        
        Thumb GetThumb(Slider slider)
        {
            Track track = slider.Template.FindName("PART_Track", slider) as Track;
            return track?.Thumb;
        }

        /// <summary>
        /// Adding sliders and double up down controls.
        /// </summary>
        void PrintWeightsUIElements()
        {
            BrushConverter converter = new BrushConverter();
            goWeightNamesPanel.Children.Add(new Label() {Content="b3", Height=25, VerticalContentAlignment=VerticalAlignment.Center});
            Slider b3Slider = new Slider() { Minimum=-1, Maximum=1 , Height = 25,
                VerticalContentAlignment = VerticalAlignment.Center, Value=NeuralNetwork.B3, SmallChange = 0.01 };
            b3Slider.ValueChanged += WeightSlider_ValueChanged;
            b3Slider.Loaded += Slider_Loaded;
            goWeightSlidersPanel.Children.Add(b3Slider);
            XCD.DoubleUpDown doubleUpDownB3 = new XCD.DoubleUpDown()
            {
                Minimum = -1,
                Maximum = 1,
                Increment = 0.1,
                Value = NeuralNetwork.B3,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 25,
                Tag = "Weight"
            };
            doubleUpDownB3.ValueChanged += DoubleUpDown_ValueChanged;
            doubleUpDownB3.ValueChanged += Input_ValueChanged;
            doubleUpDownB3.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            goWeigghtValuesPanel.Children.Add(doubleUpDownB3);

            for (int j=0; j<80; j++)
            {
                goWeightNamesPanel.Children.Add(new Label() { Content = "wj " + (j + 1), Height = 25, VerticalContentAlignment = VerticalAlignment.Center });
                Slider wj = new Slider() { Minimum = -1, Maximum = 1, Height = 25,
                    VerticalContentAlignment = VerticalAlignment.Center, Value = NeuralNetwork.Wj[j],
                    SmallChange = 0.01
                };
                wj.ValueChanged += WeightSlider_ValueChanged;
                wj.Loaded += Slider_Loaded;
                goWeightSlidersPanel.Children.Add(wj);
                XCD.DoubleUpDown doubleUpDown = new XCD.DoubleUpDown()
                {
                    Minimum = -1,
                    Maximum = 1,
                    Increment = 0.1,
                    Value = NeuralNetwork.Wj[j],
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 25,
                    Tag = "Weight"
                };
                doubleUpDown.ValueChanged += DoubleUpDown_ValueChanged;
                doubleUpDown.ValueChanged += Input_ValueChanged;
                doubleUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                goWeigghtValuesPanel.Children.Add(doubleUpDown);
            }
            
            for(int j=0; j<80; j++)
            {
                goWeightNamesPanel.Children.Add(new Label() { Content = "bj " + (j + 1), Height = 25, VerticalContentAlignment = VerticalAlignment.Center });
                Slider bj = new Slider() { Minimum = -1, Maximum = 1, Height = 25,
                    VerticalContentAlignment = VerticalAlignment.Center, Value = NeuralNetwork.Bj[j],
                    SmallChange = 0.01
                };
                bj.ValueChanged += WeightSlider_ValueChanged;
                bj.Loaded += Slider_Loaded;
                goWeightSlidersPanel.Children.Add(bj);
                XCD.DoubleUpDown doubleUpDown = new XCD.DoubleUpDown()
                {
                    Minimum = -1,
                    Maximum = 1,
                    Increment = 0.1,
                    Value = NeuralNetwork.Bj[j],
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 25,
                    Tag = "Weight"
                };
                doubleUpDown.ValueChanged += DoubleUpDown_ValueChanged;
                doubleUpDown.ValueChanged += Input_ValueChanged;
                doubleUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                goWeigghtValuesPanel.Children.Add(doubleUpDown);
                
            }

            for(int j=0; j<80; j++)
            {
                for(int i=0; i<22; i++)
                {
                    goWeightNamesPanel.Children.Add(new Label() { Content = "wij " + (i + 1) + ", " + (j + 1), Height = 25, VerticalContentAlignment = VerticalAlignment.Center });
                    Slider wij = new Slider() { Minimum = -1, Maximum = 1, Height = 25,
                        VerticalContentAlignment = VerticalAlignment.Center, Value = NeuralNetwork.Wij[i,j],
                        SmallChange = 0.01
                    };
                    wij.ValueChanged += WeightSlider_ValueChanged;
                    wij.Loaded += Slider_Loaded;
                    goWeightSlidersPanel.Children.Add(wij);
                    XCD.DoubleUpDown doubleUpDown = new XCD.DoubleUpDown()
                    {
                        Minimum = -1,
                        Maximum = 1,
                        Increment = 0.1,
                        Value = NeuralNetwork.Wij[i, j],
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 25,
                        Tag = "Weight"
                    };
                    doubleUpDown.ValueChanged += DoubleUpDown_ValueChanged;
                    doubleUpDown.ValueChanged += Input_ValueChanged;
                    doubleUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                    goWeigghtValuesPanel.Children.Add(doubleUpDown);
                }
            }
        }

        private void Input_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string tag = null;
            bool isNull = false;
            BrushConverter converter = new BrushConverter();
            if(sender == goConstantUD)
            {
                tag = (string)goConstantUD.Tag;
                if (isNull = goGradientFactorComboBox.SelectedIndex == 2 && goConstantUD.Value is null)
                    goConstantUD.BorderBrush = Brushes.Red;
                else
                    goConstantUD.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            }
            else if (sender.GetType() == typeof(XCD.DoubleUpDown))
            {
                XCD.DoubleUpDown doubleUpDown = sender as XCD.DoubleUpDown;
                tag = (string)doubleUpDown.Tag;
                if (isNull = doubleUpDown.Value is null)
                    doubleUpDown.BorderBrush = Brushes.Red;
                else
                    doubleUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            }
            else
            {
                XCD.IntegerUpDown integerUpDown = sender as XCD.IntegerUpDown;
                tag = (string)integerUpDown.Tag;
                if (isNull = integerUpDown.Value is null)
                    integerUpDown.BorderBrush = Brushes.Red;
                else
                    integerUpDown.BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
            }

            switch (tag)
            {
                case "Domain":
                    if (isNull)
                        gsInvalidDomainUDs.Add(sender as InputBase);
                    else
                        gsInvalidDomainUDs.Remove(sender as InputBase);
                    break;
                case "Weight":
                    if (isNull)
                        gsInvalidWeightDoubleUDs.Add(sender as XCD.DoubleUpDown);
                    else
                        gsInvalidWeightDoubleUDs.Remove(sender as XCD.DoubleUpDown);
                    break;
                case "Learning":
                    if (isNull)
                        gsInvalidLearningOptions.Add(sender as InputBase);
                    else
                        gsInvalidLearningOptions.Remove(sender as InputBase);
                    break;
                case "Test":
                    gbInvalidadTestParameter = isNull;
                    break;
                default:
                    break;
            }

            bool lbInvalidWeight = gsInvalidWeightDoubleUDs.Count > 0;
            bool lbInvaidDomain = gsInvalidDomainUDs.Count > 0;
            bool lbInvalidLearning = gsInvalidLearningOptions.Count > 0 ;

            goSaveB.IsEnabled = !lbInvalidWeight;
            goRefreshB.IsEnabled = !lbInvaidDomain && !lbInvalidWeight;
            goLearningButton.IsEnabled = !lbInvaidDomain && !lbInvalidLearning && !lbInvalidWeight;
            goTestButton.IsEnabled = !lbInvaidDomain && !gbInvalidadTestParameter && !lbInvalidWeight;

        }

        private void DoubleUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            XCD.DoubleUpDown doubleUpDown = sender as XCD.DoubleUpDown;

            if (doubleUpDown.Value is null)
                return;

            int element = goWeigghtValuesPanel.Children.IndexOf(doubleUpDown);
            Slider slider = goWeightSlidersPanel.Children[element] as Slider;
            slider.ValueChanged -= WeightSlider_ValueChanged;
            slider.Value = (double)doubleUpDown.Value;
            slider.ValueChanged += WeightSlider_ValueChanged;
            if (element == 0)
            {
                NeuralNetwork.B3 = (double)doubleUpDown.Value;
                NNViewer.UpdateAxonColor(AxonArray.b3, -1, -1);
            }
            else if (element < 81)
            {
                NeuralNetwork.Wj[element - 1] = (double)doubleUpDown.Value;
                NNViewer.UpdateAxonColor(AxonArray.wj, -1, element - 1);
            }
            else if (element < 161)
            {
                NeuralNetwork.Bj[element - 81] = (double)doubleUpDown.Value;
                NNViewer.UpdateAxonColor(AxonArray.bj, -1, element - 81);
            }
            else
            {
                element -= 161;
                int j = element / 22;
                int i = element % 22;
                NeuralNetwork.Wij[i, j] = (double)doubleUpDown.Value;
                NNViewer.UpdateAxonColor(AxonArray.wij, i, j);
            }
            VisualizeFunction(RendererEnum.NeuralNetwork);
        }

        private void Slider_Loaded(object sender, RoutedEventArgs e)
        {
            GetThumb(sender as Slider).DragCompleted += Slider_DragCompleted;
            GetThumb(sender as Slider).DragStarted += Slider_DragStarted;
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            gbDragStarted = true;
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            int element = goWeightSlidersPanel.Children.IndexOf((sender as Thumb).TemplatedParent as Slider);
            if(element == 0)
                NNViewer.UpdateAxonColor(AxonArray.b3, -1, -1);
            else if (element < 81)
                NNViewer.UpdateAxonColor(AxonArray.wj, -1, element - 1);
            else if (element < 161)
                NNViewer.UpdateAxonColor(AxonArray.bj, -1, element - 81);
            else
            {
                element -= 161;
                int j = element / 22;
                int i = element % 22;   
                NNViewer.UpdateAxonColor(AxonArray.wij, i, j);
            }
            gbDragStarted = false;
        }

        private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            int element = goWeightSlidersPanel.Children.IndexOf(slider);
            XCD.DoubleUpDown doubleUpDown = goWeigghtValuesPanel.Children[element] as XCD.DoubleUpDown;
            doubleUpDown.ValueChanged -= DoubleUpDown_ValueChanged;
            doubleUpDown.Value = slider.Value;
            doubleUpDown.ValueChanged += DoubleUpDown_ValueChanged;
            if (element == 0)
            {
                NeuralNetwork.B3 = slider.Value;
                if (!gbDragStarted) NNViewer.UpdateAxonColor(AxonArray.b3, -1, -1);
            }
            else if (element < 81)
            {
                NeuralNetwork.Wj[element - 1] = slider.Value;
                if(!gbDragStarted) NNViewer.UpdateAxonColor(AxonArray.wj, -1, element - 1);
            }
            else if (element < 161)
            {
                NeuralNetwork.Bj[element - 81] = slider.Value;
                if(!gbDragStarted) NNViewer.UpdateAxonColor(AxonArray.bj, -1, element - 81);
            }
            else
            {
                element -= 161;
                int j = element / 22;
                int i = element % 22;
                NeuralNetwork.Wij[i, j] = slider.Value;
                if(!gbDragStarted) NNViewer.UpdateAxonColor(AxonArray.wij, i, j);
            }
            VisualizeFunction(RendererEnum.NeuralNetwork);
        }

        void UpdateWeightSlidersAndDoublesUD()
        {
            int lnTotalChildren = goWeightSlidersPanel.Children.Count;
            for(int i = 0; i<lnTotalChildren; i++)
            {
                (goWeightSlidersPanel.Children[i] as Slider).ValueChanged -= WeightSlider_ValueChanged;
                (goWeigghtValuesPanel.Children[i] as XCD.DoubleUpDown).ValueChanged -= DoubleUpDown_ValueChanged;
            }

            int element = 1;
            (goWeigghtValuesPanel.Children[0] as XCD.DoubleUpDown).Value = (goWeightSlidersPanel.Children[0] as Slider).Value = NeuralNetwork.B3;
            
       
            for (int j = 0; j < 80; j++, element++)
            {
                (goWeigghtValuesPanel.Children[element] as XCD.DoubleUpDown).Value = (goWeightSlidersPanel.Children[element] as Slider).Value = NeuralNetwork.Wj[j];
            }

            for (int j = 0; j < 80; j++, element++)
            {
                (goWeigghtValuesPanel.Children[element] as XCD.DoubleUpDown).Value = (goWeightSlidersPanel.Children[element] as Slider).Value = NeuralNetwork.Bj[j];
            }

            for (int j = 0; j < 80; j++)
            {
                for (int i = 0; i < 22; i++, element++)
                {
                    (goWeigghtValuesPanel.Children[element] as XCD.DoubleUpDown).Value = (goWeightSlidersPanel.Children[element] as Slider).Value = NeuralNetwork.Wij[i,j];
                }
            }

            BrushConverter converter = new BrushConverter();
            for (int i = 0; i < lnTotalChildren; i++)
            {
                (goWeightSlidersPanel.Children[i] as Slider).BorderBrush = (Brush)converter.ConvertFromString("#FF569DE5");
                
                (goWeightSlidersPanel.Children[i] as Slider).ValueChanged += WeightSlider_ValueChanged;
                (goWeigghtValuesPanel.Children[i] as XCD.DoubleUpDown).ValueChanged += DoubleUpDown_ValueChanged;
            }

        }

        public void VisualizeFunction(RendererEnum renderer)
        {
            if (gsInvalidDomainUDs.Count > 0 || gsInvalidWeightDoubleUDs.Count > 0)
                return;
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

            GradientFactorType factor = GradientFactorType.n;
            switch(goGradientFactorComboBox.SelectedIndex)
            {
                case 1: factor = GradientFactorType.ln; break;
                case 2: factor = GradientFactorType.constant; break;
            }
            int iterations = (int)goIterationsUpDown.Value;
            int interval = (int)goLearningIntervalVisualizationIUD.Value;
            if (interval == 0) interval = (int)goIterationsUpDown.Value;
            
            if(interval < 10000 && iterations > 50000)
            {
                MessageBoxResult boxResult = MessageBox.Show("The interval between visualizations is relatively short, this may affect the app performance.\nDo you want to continue?", "Learning process", MessageBoxButton.YesNo);
                if (boxResult == MessageBoxResult.No) return;
            }

            progressWindow = new ProgressWindow();
            FunctionDefinition function = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex];
            gsErrorResults = new double[iterations];
            gnNextResultsIndex = 0;
            LearningParameters learnOptions = new LearningParameters(factor, iterations, interval, function, (double)goConstantUD.Value);
            BackgroundArguments learnArguments = new BackgroundArguments(learnOptions, NeuralNetwork);
            goLearningWorker.RunWorkerAsync(argument: learnArguments);
            progressWindow.ShowDialog();
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
            GradientFactorType gradientFactor = (options as LearningParameters).GradientFactorType;
            double lnEta = (options as LearningParameters).GradientFactor; 
            double s3 = 0;
            int lastPercentage = 0;
            double[] lsError = null;
            for (int i=0; i<iterations; i++)
            {
                if(i % interval == 0)
                    lsError = new double[Math.Min(interval, iterations - i)];

                switch (gradientFactor)
                {
                    case GradientFactorType.n: lnEta = 0.1 / (i + 1); break;
                    case GradientFactorType.ln: lnEta = 0.1 / Math.Log(i + 2); break;
                }

                double[] x1 = GenerateX1();
                double[] x2 = GenerateX2();
                double y = functionDefinition.Evaluate(x1[1], x2[1]);
                double yNormalized = NormalizeY(y);
                lock(neuralNetwork)
                {
                    neuralNetwork.LearnMaxDescend(x1[0], x2[0], yNormalized, lnEta, out s3);
                }
                lsError[i%interval] = Math.Abs(s3 - yNormalized);
                int lnPercentage = 100 * i / iterations;
                if((i+1)%interval == 0)
                {
                    lastPercentage = lnPercentage;
                    loWorker.ReportProgress(lnPercentage, lsError);
                }
                else if (lnPercentage > lastPercentage)
                {
                    lastPercentage = lnPercentage;
                    loWorker.ReportProgress(lnPercentage, null);
                }
            }
            e.Result = lsError;
        }

        private void GoLearningWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.UserState != null)
            {
                lock(NeuralNetwork)
                { 
                    VisualizeFunction(RendererEnum.NeuralNetwork);  
                }
                lock(gsErrorResults)
                {
                    double[] lsError = (double[])e.UserState;
                    lsError.CopyTo(gsErrorResults, gnNextResultsIndex);
                    gnNextResultsIndex += lsError.Length;

                    double[] dataToVisualize;
                    if (gnNextResultsIndex > 100)
                    {
                        dataToVisualize = new double[100];
                        int step = (int)(gnNextResultsIndex / 100d);
                        for (int i = 0; i < 100; i++)
                        {
                            for (int j = 0; j < step; j++)
                            {
                                dataToVisualize[i] += gsErrorResults[i * step + j];
                            }
                            dataToVisualize[i] /= (double)step;
                        }
                    }
                    else
                    {
                        dataToVisualize = new double[gnNextResultsIndex];
                        for(int i=0; i<gnNextResultsIndex; i++)
                        {
                            dataToVisualize[i] = gsErrorResults[i];
                        }
                    }
                    ChartModel chartModel = new ChartModel(dataToVisualize);
                    goPlotView.Model = chartModel.PlotModel;
                }
            }
            progressWindow.ProgressChanged(e.ProgressPercentage);
        }

        private void GoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double[] lsErrorsResult = (double[])e.Result;
            if(gnNextResultsIndex < gsErrorResults.Length)
            {
                lsErrorsResult.CopyTo(gsErrorResults, gnNextResultsIndex);
                double[] dataToVisualize;
                if (gsErrorResults.Length > 100)
                {
                    dataToVisualize = new double[100];
                    int step = (int)(gsErrorResults.Length / 100d);
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < step; j++)
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
            UpdateUIWeights();
            progressWindow.AllowClosing();
            progressWindow.Close();
        }

        /// <summary>
        /// Generates random neural network/function input value for X1.
        /// </summary>
        /// <returns>Normalized value and value within the domain</returns>
        double[] GenerateX1()
        {
            double x1min = X1Domain[0];
            double x1max = X1Domain[1];
            double[] values = new double[2] { random.NextDouble(), 0 };
            values[1] = values[0] * (x1max - x1min) + x1min;
            return values;
        }

        /// <summary>
        /// Generates random neural network/function input value for X2.
        /// </summary>
        /// <returns>Normalized value and value within the domain</returns>
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

        /// <summary>
        /// Updates sliders, double-up-down, neural network viewer and neural network viewer.
        /// </summary>
        private void UpdateUIWeights()
        {
            UpdateWeightSlidersAndDoublesUD();
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
            XCD.DoubleUpDown doubleUpDown = sender as XCD.DoubleUpDown;
            if(doubleUpDown.Value is null)
                return;

            BrushConverter converter = new BrushConverter();
            if(!(goX1minIUP.Value is null) && !(goX1maxIUP.Value is null))
            {
                double x1Min = (double)goX1minIUP.Value;
                double x1Max = (double)goX1maxIUP.Value;
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
            }

            if (!(goX2minIUP.Value is null) && !(goX2maxIUP.Value is null))
            {
                double x2Min = (double)goX2minIUP.Value;
                double x2Max = (double)goX2maxIUP.Value;
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
            }

            if (!(goYminIUP.Value is null) && !(goYmaxIUP.Value is null))
            {
                double yMin = (double)goYminIUP.Value;
                double yMax = (double)goYmaxIUP.Value;
                if (yMin >= yMax)
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
            progressWindow = new ProgressWindow() { Title="Testing Neural Network"};
            FunctionDefinition function = gsFunctionDefinitions[goFunctionComboBox.SelectedIndex];
            gsErrorResults = new double[iterations];
            ExecutionParameters testingParameters = new ExecutionParameters(iterations, function);
            BackgroundArguments backgroundArguments = new BackgroundArguments(testingParameters, NeuralNetwork);
            goTestingWorker.RunWorkerAsync(argument: backgroundArguments);
            progressWindow.ShowDialog();
        }

        private void GoGradientFactorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goConstantUD.IsEnabled = goGradientFactorComboBox.SelectedIndex == 2;
            Input_ValueChanged(goConstantUD, null);
        }
    }

}
