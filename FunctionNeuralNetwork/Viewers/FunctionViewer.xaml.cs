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


namespace FunctionNeuralNetwork
{
    public enum RendererEnum { Function, NeuralNetwork, Both }

    /// <summary>
    /// Interaction logic for FunctionViewer.xaml
    /// </summary>
    public partial class FunctionViewer : UserControl
    {
        MainWindow goParent;

        //Rendering stuff
        System.Windows.Forms.Panel goPanel;
        System.Windows.Forms.Integration.WindowsFormsHost goWFormsHost;
        vtkRenderWindowInteractor goInteractor;
        vtkRenderer goRendererFunction;
        vtkRenderer goRendererNN;
        vtkRenderWindow goVTKWindow;
        vtkCamera goCamera;
        RenderWindowControl goRenderWindowControl;
        vtkInteractorStyleTrackballCamera goTackballStyle;

        //vtk objects visualization
        vtkPoints goPointsFunction;
        vtkPoints goPointsNN;

        vtkStructuredGrid goSGFunction;
        vtkStructuredGrid goSGNN;

        vtkDataSetMapper goMapperFunction;
        vtkDataSetMapper goMapperNN;

        vtkElevationFilter goElevationFilterFunction;
        vtkElevationFilter goElevationFilterNN;

        vtkLookupTable goLUTFunction;
        vtkLookupTable goLUTNN;

        vtkActor goActorFunction;
        vtkActor goActorNN;

        vtkOrientationMarkerWidget goOrientationFunction;
        vtkOrientationMarkerWidget goOrientationNN;


        public bool gbRenderized;
        bool gbSync;

        public bool Synchronized
        {
            get { return gbSync; }
            set
            {
                if (value)
                {
                    goRendererNN.SetActiveCamera(goCamera);
                    goOrientationNN.EnabledOff();


                    goOrientationFunction.SetCurrentRenderer(goRendererFunction);
                    goOrientationFunction.EnabledOn();
                    goOrientationFunction.InteractiveOff();
                }

                else
                {
                    vtkCamera loCamera = new vtkCamera();
                    double[] lsTemp = goCamera.GetClippingRange();
                    loCamera.SetClippingRange(lsTemp[0], lsTemp[1]);
                    lsTemp = goCamera.GetFocalPoint();
                    loCamera.SetFocalPoint(lsTemp[0], lsTemp[1], lsTemp[2]);
                    lsTemp = goCamera.GetPosition();
                    loCamera.SetPosition(lsTemp[0], lsTemp[1], lsTemp[2]);
                    lsTemp = goCamera.GetViewUp();
                    loCamera.SetViewUp(lsTemp[0], lsTemp[1], lsTemp[2]);
                    goRendererNN.SetActiveCamera(loCamera);

                    goOrientationNN.SetCurrentRenderer(goRendererNN);
                    goOrientationNN.EnabledOn();
                    goOrientationNN.InteractiveOff();

                }
                gbSync = value;
                goInteractor.Render();
            }
        }

        public FunctionViewer(System.Windows.Forms.Integration.WindowsFormsHost loWFH, MainWindow parent)
        {
            goParent = parent;
            InitializeComponent();

            //Interop host control
            goWFormsHost = loWFH;
            goWFormsHost.ClipToBounds = true;
            goWFormsHost.Focusable = false;

            //Panel to host the 3d stuff
            goPanel = new System.Windows.Forms.Panel();
            goPanel.Enabled = true;
            goPanel.AutoSize = true;
            goPanel.MinimumSize = new System.Drawing.Size(1, 1);
            goPanel.Tag = this;

            //Interop ocntrol to the usercontrol
            this.Focusable = false;
            this.Content = goWFormsHost;

            goWFormsHost.Child = goPanel; //Panel as hots control's child

            //Creating RenderWindowControl and adding to the panel
            goRenderWindowControl = new RenderWindowControl();
            goRenderWindowControl.AddTestActors = false;
            goRenderWindowControl.Location = new System.Drawing.Point(0, 0);
            goRenderWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            goRenderWindowControl.TabIndex = 1;
            goRenderWindowControl.TestText = null;
            goPanel.Controls.Add(goRenderWindowControl);

            //VTk stuff
            goInteractor = new vtkRenderWindowInteractor();
            goTackballStyle = new vtkInteractorStyleTrackballCamera();
            goInteractor.SetInteractorStyle(goTackballStyle);
            goRendererFunction = vtkRenderer.New();
            goCamera = goRendererFunction.GetActiveCamera();

            goRendererNN = vtkRenderer.New();
            goRendererNN.SetActiveCamera(goCamera);

            gbRenderized = false;
            gbSync = true;
            goRenderWindowControl.Load += GoRenderWindowControlLoad;
        }


        private void GoRenderWindowControlLoad(object sender, EventArgs e)
        {
            if (goRenderWindowControl != null && !gbRenderized)
            {
                goRendererFunction.SetBackground(0.1, 0.1, 0.1);
                goVTKWindow = goRenderWindowControl.RenderWindow;

                goInteractor.SetRenderWindow(goVTKWindow);
                goRendererFunction.SetViewport(0.0, 0.0, 0.499, 1.0);
                goVTKWindow.AddRenderer(goRendererFunction);

                goRendererNN.SetBackground(0.1, 0.1, 0.1);
                goRendererNN.SetViewport(0.501, 0.0, 1, 1);
                goVTKWindow.AddRenderer(goRendererNN);

                goCamera.SetClippingRange(0.1, 10000);

                goInteractor.Initialize();
                InitializeVisualizationObjects();

                gbRenderized = true;
                goParent.VisualizeFunction(RendererEnum.Both);
            }
        }

        void InitializeVisualizationObjects()
        {
            //For the points
            goPointsFunction = new vtkPoints();
            goPointsNN = new vtkPoints();
            goSGFunction = new vtkStructuredGrid();
            goSGNN = new vtkStructuredGrid();
            goElevationFilterFunction = new vtkElevationFilter();
            goElevationFilterNN = new vtkElevationFilter();
            goMapperFunction = new vtkDataSetMapper();
            goMapperNN = new vtkDataSetMapper();
            goLUTFunction = new vtkLookupTable();
            goLUTNN = new vtkLookupTable();
            goActorFunction = new vtkActor();
            goActorNN = new vtkActor();

            //goActorFunction.GetProperty().SetPointSize(2);
            //goActorNN.GetProperty().SetPointSize(2);

            goSGFunction.SetPoints(goPointsFunction);
            goSGNN.SetPoints(goPointsNN);

            goElevationFilterFunction.SetInput(goSGFunction);
            goElevationFilterNN.SetInput(goSGNN);

            goMapperFunction.SetInput(goElevationFilterFunction.GetOutput());
            goMapperNN.SetInput(goElevationFilterNN.GetOutput());

            goMapperFunction.SetLookupTable(goLUTFunction);
            goMapperNN.SetLookupTable(goLUTNN);

            goActorFunction.SetMapper(goMapperFunction);
            goActorNN.SetMapper(goMapperNN);

            goRendererFunction.AddActor(goActorFunction);
            goRendererNN.AddActor(goActorNN);


            //For the axis
            vtkAxesActor loAxesActorFunction = new vtkAxesActor();
            vtkAxesActor loAxesActorNN = new vtkAxesActor();

            loAxesActorFunction.SetXAxisLabelText("X1");
            loAxesActorFunction.SetYAxisLabelText("X2");
            loAxesActorFunction.SetZAxisLabelText("Y");
            loAxesActorNN.SetXAxisLabelText("X1");
            loAxesActorNN.SetYAxisLabelText("X2");
            loAxesActorNN.SetZAxisLabelText("Y");

            goOrientationFunction = new vtkOrientationMarkerWidget();
            goOrientationNN = new vtkOrientationMarkerWidget();
                        
            goOrientationFunction.SetOrientationMarker(loAxesActorFunction);
            goOrientationNN.SetOrientationMarker(loAxesActorNN);

            goOrientationFunction.SetInteractor(goInteractor);
            goOrientationNN.SetInteractor(goInteractor);

            goOrientationFunction.SetViewport(0, 0, .3, .3);
            goOrientationNN.SetViewport(0.5, 0, 0.8, .3);

            goOrientationFunction.SetEnabled(1);

            goOrientationFunction.InteractiveOff();

            vtkTextActor functionTitle = new vtkTextActor();
            functionTitle.SetInput("Function To Learn");
            functionTitle.SetTextScaleModeToViewport();
            functionTitle.GetProperty().SetColor(0.9, 0.9, 0.9);
            goRendererFunction.AddActor(functionTitle);

            vtkTextActor neuralNetworkTitle = new vtkTextActor();
            neuralNetworkTitle.SetInput("Neural Network Calculations");
            neuralNetworkTitle.SetTextScaleModeToViewport();
            neuralNetworkTitle.GetProperty().SetColor(0.9, 0.9, 0.9);
            goRendererNN.AddActor(neuralNetworkTitle);
        }

        public void VisualizeData(double[] bounds, double[,] data, RendererEnum rendererToUse)
        {
            int x1dim = data.GetLength(0);
            int x2dim = data.GetLength(1);
            double deltaX1 = (bounds[1] - bounds[0]) / x1dim;
            double deltaX2 = (bounds[3] - bounds[2]) / x2dim;
            vtkPoints points = new vtkPoints();
            points.Allocate(x1dim * x2dim, 1000);

            if (rendererToUse == RendererEnum.Function)
            {
                goSGFunction.SetDimensions(x1dim, x2dim, 1);
                goSGFunction.SetPoints(points);
                goPointsFunction = points;
            }
            else
            {
                goSGNN.SetDimensions(x1dim, x2dim, 1);
                goSGNN.SetPoints(points);
                goPointsNN = points;
            }
            
            for(int j=0; j<x2dim;j++)
            {
                int offset = j * x1dim;
                double x2coord = bounds[2] + j * deltaX2;
                for(int i=0; i< x1dim; i++)
                {
                    points.InsertPoint(i + offset, bounds[0] + i * deltaX1, x2coord, data[i, j]);
                }
            }
            if (rendererToUse == RendererEnum.Function)
            {
                goRendererFunction.ResetCamera();
                if(Synchronized)
                    goRendererNN.ResetCamera();
            }
            else
            {
                goRendererNN.ResetCamera();
                if(Synchronized)
                    goRendererFunction.ResetCamera();
            }
                
            
            goInteractor.Render();
        }

    }
}
