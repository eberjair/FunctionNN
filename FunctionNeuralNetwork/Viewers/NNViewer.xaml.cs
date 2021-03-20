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
using Kitware.VTK;
using FunctionNeuralNetwork.Viewers;

namespace FunctionNeuralNetwork
{

    public enum AxonArray { b3, wj, bj, wij }
    /// <summary>
    /// Interaction logic for NNViewer.xaml
    /// </summary>
    public partial class NNViewer : UserControl
    {
        NeuralNetwork NeuralNetwork;

        //Rendering stuff
        System.Windows.Forms.Panel goPanel;
        System.Windows.Forms.Integration.WindowsFormsHost goWFormsHost;
        vtkRenderWindowInteractor goInteractor;
        vtkRenderer goRenderer;
        vtkRenderWindow goVTKWindow;
        vtkCamera goCamera;
        RenderWindowControl goRenderWindowControl;
        vtkInteractorStyleImage goImageStyle;

        //vtk objects visualization
        List<NeuronRenderer> gsInputNeurons;
        List<NeuronRenderer> gsS1Neurons;
        List<NeuronRenderer> gsS2Neurons;
        NeuronRenderer S3Neuron;

        vtkLookupTable lookupTable;
        vtkScalarBarActor scalarBarActor;
        List<List<AxonRenderer>> gsInputS1Axons;
        List<List<AxonRenderer>> gsS1S2Axons;
        List<AxonRenderer> gsS2S3Axons;


        public bool gbRenderized;
        

        public NNViewer(System.Windows.Forms.Integration.WindowsFormsHost loWFH, NeuralNetwork neuralNetwork)
        {
            InitializeComponent();
            NeuralNetwork = neuralNetwork;
            gsInputNeurons=new List<NeuronRenderer>();
            gsS1Neurons = new List<NeuronRenderer>();
            gsS2Neurons = new List<NeuronRenderer>();
            
            gsInputS1Axons = new List<List<AxonRenderer>>();
            gsS1S2Axons = new List<List<AxonRenderer>>();
            gsS2S3Axons = new List<AxonRenderer>();


            //Interop host control
            goWFormsHost = loWFH;
            goWFormsHost.ClipToBounds = true;
            goWFormsHost.Focusable = false;

            //Panel to host the 3d stuff
            goPanel = new System.Windows.Forms.Panel
            {
                Enabled = true,
                AutoSize = true,
                MinimumSize = new System.Drawing.Size(1, 1),
                Tag = this
            };

            //Interop ocntrol to the usercontrol
            this.Focusable = false;
            this.Content = goWFormsHost;

            goWFormsHost.Child = goPanel; //Panel as hots control's child

            //Creating RenderWindowControl and adding to the panel
            goRenderWindowControl = new RenderWindowControl
            {
                AddTestActors = false,
                Location = new System.Drawing.Point(0, 0),
                Dock = System.Windows.Forms.DockStyle.Fill,
                TabIndex = 1,
                TestText = null
            };
            goPanel.Controls.Add(goRenderWindowControl);

            //VTk stuff
            goInteractor = new vtkRenderWindowInteractor();
            goImageStyle = new vtkInteractorStyleImage();
            goImageStyle.LeftButtonPressEvt += GoImageStyle_LeftButtonPressEvt;
            goImageStyle.LeftButtonReleaseEvt += GoImageStyle_LeftButtonReleaseEvt;
            goInteractor.SetInteractorStyle(goImageStyle);
            goRenderer = vtkRenderer.New();
            goCamera = goRenderer.GetActiveCamera();
            goCamera.ParallelProjectionOn();
            lookupTable = new vtkLookupTable();
            lookupTable.SetRange(-1, 1);
            lookupTable.Build();
            gbRenderized = false;
            goRenderWindowControl.Load += GoRenderWindowControlLoad;
        }

        private void GoImageStyle_LeftButtonReleaseEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            goInteractor.MiddleButtonReleaseEvent();
        }

        private void GoImageStyle_LeftButtonPressEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            goInteractor.MiddleButtonPressEvent();
        }

        private void GoRenderWindowControlLoad(object sender, EventArgs e)
        {
            if (goRenderWindowControl != null && !gbRenderized)
            {
                goRenderer.SetBackground(0.1, 0.1, 0.1);
                goVTKWindow = goRenderWindowControl.RenderWindow;
                goInteractor.SetRenderWindow(goVTKWindow);
                
                goVTKWindow.AddRenderer(goRenderer);

                goInteractor.Initialize();
                InitializeVisualizationObjects();
                goRenderer.ResetCamera();

                gbRenderized = true;
            }
        }

        void InitializeVisualizationObjects()
        {
            NeuronRenderer X1 = new NeuronRenderer(new double[] { 0, 40, 0 });
            X1.Render(goRenderer);
            gsInputNeurons.Add(X1);
            NeuronRenderer X2 = new NeuronRenderer(new double[] { 0, 120, 0 });
            X2.Render(goRenderer);
            gsInputNeurons.Add(X2);

            for(int i = 0; i<23;i++)
            {
                NeuronRenderer S1Neuron = new NeuronRenderer(new double[] { 40, 4 + 8 * i, 0 });
                S1Neuron.Render(goRenderer);
                gsS1Neurons.Add(S1Neuron);
            }
            
            

            for (int i = 0; i < 81; i++)
            {
                NeuronRenderer S2Neuron = new NeuronRenderer(new double[] { 80, 1 + 2 * i, 0 });
                S2Neuron.Render(goRenderer);
                gsS2Neurons.Add(S2Neuron);
            }

            S3Neuron = new NeuronRenderer(new double[] { 120, 80, 0 });
            S3Neuron.Render(goRenderer);

            
            scalarBarActor = new vtkScalarBarActor();
            scalarBarActor.SetLookupTable(lookupTable);
            scalarBarActor.SetTitle("Weights");
            scalarBarActor.GetPositionCoordinate().SetCoordinateSystemToNormalizedViewport();
            scalarBarActor.GetPositionCoordinate().SetValue(0.2, 0.05);
            scalarBarActor.SetOrientationToHorizontal();
            scalarBarActor.SetWidth(0.6);
            scalarBarActor.SetHeight(0.1);
            goRenderer.AddActor(scalarBarActor);

            for(int i=0; i<2; i++)
            {
                List<AxonRenderer> axons = new List<AxonRenderer>();
                gsInputS1Axons.Add(axons);
                for(int j = 0; j<11; j++)
                {
                    AxonRenderer axon = new AxonRenderer(new double[] { 1, 40 + 80 * i, 39, 4 + 8 * j + 88 * i });
                    axon.Render(goRenderer);
                    axons.Add(axon);
                }
            }

            for(int i=0; i<23; i++)
            {
                List<AxonRenderer> axons = new List<AxonRenderer>();
                gsS1S2Axons.Add(axons);
                for(int j=0; j<80;j++)
                {
                    AxonRenderer axon = new AxonRenderer(new double[] { 41, 4 + 8 * i, 79, 1 + 2 * j });
                    axon.Render(goRenderer);
                    axons.Add(axon);
                    double[] color;
                    color = i<22? lookupTable.GetColor(NeuralNetwork.Wij[i, j]) : lookupTable.GetColor(NeuralNetwork.Bj[j]);
                    axon.SetColor(color[0], color[1], color[2]);
                }
            }

            for(int i=0; i<81;i++)
            {
                AxonRenderer axon = new AxonRenderer(new double[] { 81, 1+2*i, 119, 80 });
                axon.Render(goRenderer);
                gsS2S3Axons.Add(axon);
                double[] color = i<80? lookupTable.GetColor(NeuralNetwork.Wj[i]): lookupTable.GetColor(NeuralNetwork.B3);
                axon.SetColor(color[0], color[1], color[2]);
            }

            vtkTextActor viewerTitle = new vtkTextActor();
            viewerTitle.SetInput("Neural Network Viewer");
            viewerTitle.SetTextScaleModeToViewport();
            viewerTitle.GetProperty().SetColor(0.9, 0.9, 0.9);
            goRenderer.AddActor(viewerTitle);
        }

        public void UpdateAxonsColor()
        {
            for(int i=0; i<23;i++)
            {
                for(int j=0; j<80; j++)
                {
                    double[] color = i<22 ? lookupTable.GetColor(NeuralNetwork.Wij[i, j]) : lookupTable.GetColor(NeuralNetwork.Bj[j]);
                    gsS1S2Axons[i][j].SetColor(color[0], color[1], color[2]);
                }
            }

            for(int j=0; j<81; j++)
            {
                double[] color = j<80? lookupTable.GetColor(NeuralNetwork.Wj[j]) : lookupTable.GetColor(NeuralNetwork.B3);
                gsS2S3Axons[j].SetColor(color[0], color[1], color[2]);
            }
            goInteractor.Render();
        }

        public void UpdateAxonColor(AxonArray array, int i, int j)
        {
            double[] color;
            switch (array)
            {
                case AxonArray.b3:
                    color = lookupTable.GetColor(NeuralNetwork.B3);
                    gsS2S3Axons[80].SetColor(color[0], color[1], color[2]);
                    break;
                case AxonArray.bj:
                    color = lookupTable.GetColor(NeuralNetwork.Bj[j]);
                    gsS1S2Axons[22][j].SetColor(color[0], color[1], color[2]);
                    break;
                case AxonArray.wij:
                    color = lookupTable.GetColor(NeuralNetwork.Wij[i, j]);
                    gsS1S2Axons[i][j].SetColor(color[0], color[1], color[2]);
                    break;
                case AxonArray.wj:
                    color = lookupTable.GetColor(NeuralNetwork.Wj[j]);
                    gsS2S3Axons[j].SetColor(color[0], color[1], color[2]);
                    break;
            }
            goInteractor.Render();
        }
    }
}
