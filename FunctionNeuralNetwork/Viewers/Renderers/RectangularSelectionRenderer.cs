using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FunctionNeuralNetwork.Viewers.Renderers
{
    public class RectangularSelectionRenderer : PlanViewSelector
    {
        vtkPlaneSource goPlaneSource;
        vtkPolyDataMapper goPlaneMapper;

        vtkExtractEdges goEdegesExtractor;
        vtkTubeFilter goTubeFilter;
        vtkPolyDataMapper goEdgesMapper;

        double[] gsInitialPoint;
        double[] gsEndingPoint;
        double gnRadius;

        public bool RegionDefined { get; private set; }

        public RectangularSelectionRenderer()
        {
            gsInitialPoint = new double[3];
            gsEndingPoint = new double[2];

            goPlaneSource = new vtkPlaneSource();
            goPlaneMapper = vtkPolyDataMapper.New();
            goAreaActor = new vtkActor();

            goPlaneMapper.SetInputConnection(goPlaneSource.GetOutputPort());
            goAreaActor.SetMapper(goPlaneMapper);
            goAreaActor.GetProperty().SetColor(1, 0, 0);
            goAreaActor.GetProperty().SetOpacity(0.1);

            goEdegesExtractor = new vtkExtractEdges();
            goTubeFilter = new vtkTubeFilter();
            goEdgesMapper = vtkPolyDataMapper.New();
            goEdgesActor = new vtkActor();

            goEdegesExtractor.SetInputConnection(goPlaneSource.GetOutputPort());
            goTubeFilter.SetInputConnection(goEdegesExtractor.GetOutputPort());
            goTubeFilter.SetNumberOfSides(8);
            goTubeFilter.SetRadius(10);
            goEdgesMapper.SetInputConnection(goTubeFilter.GetOutputPort());
            goEdgesActor.SetMapper(goEdgesMapper);
            goEdgesActor.GetProperty().SetColor(1, 0, 0);
        }

        override public void Render(vtkRenderer loRenderer)
        {
            loRenderer.AddActor(goAreaActor);
            loRenderer.AddActor(goEdgesActor);
            RegionDefined = false;
        }

        override public void Remove(vtkRenderer loRenderer)
        {
            loRenderer.RemoveActor(goAreaActor);
            loRenderer.RemoveActor(goEdgesActor);
            RegionDefined = false;
        }

        public void SetInitialPoint(double[] lsInitialPoint, double lnRadius)
        {
            gsInitialPoint[0] = lsInitialPoint[0];
            gsInitialPoint[1] = lsInitialPoint[1];
            gsInitialPoint[2] = lsInitialPoint[2];
            goPlaneSource.SetOrigin(gsInitialPoint[0], gsInitialPoint[1], gsInitialPoint[2]);
            gnRadius = lnRadius;
            goTubeFilter.SetRadius(lnRadius);
            RegionDefined = false;
        }

        public void SetEndingPoint(double[] lsEndingPoint)
        {
            gsEndingPoint[0] = lsEndingPoint[0];
            gsEndingPoint[1] = lsEndingPoint[1];
            if(Math.Abs(gsInitialPoint[0]-gsEndingPoint[0]) > gnRadius && Math.Abs(gsInitialPoint[1] - gsEndingPoint[1]) > gnRadius)
            {
                goPlaneSource.SetPoint1(gsEndingPoint[0], gsInitialPoint[1], gsInitialPoint[2]);
                goPlaneSource.SetPoint2(gsInitialPoint[0], gsEndingPoint[1], gsInitialPoint[2]);
                RegionDefined = true;
            }
            else
            {
                goPlaneSource.SetPoint1(gsEndingPoint[0] + gnRadius * 0.5, gsInitialPoint[1], gsInitialPoint[2]);
                goPlaneSource.SetPoint2(gsInitialPoint[0], gsEndingPoint[1] + gnRadius * 0.5, gsInitialPoint[2]);
            }
            
        }

        /// <summary>
        /// Gives the bounds for the rectangular selection
        /// </summary>
        /// <returns>Xmin, Xmax, Ymin, Ymax</returns>
        public double[] GetBounds()
        {
            return new double[]
            {
                Math.Min(gsInitialPoint[0], gsEndingPoint[0]),
                Math.Max(gsInitialPoint[0], gsEndingPoint[0]),
                Math.Min(gsInitialPoint[1], gsEndingPoint[1]),
                Math.Max(gsInitialPoint[1], gsEndingPoint[1])
            };
        }
    }
}
