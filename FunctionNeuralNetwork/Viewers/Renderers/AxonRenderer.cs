using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FunctionNeuralNetwork.Viewers
{
    public class AxonRenderer
    {
        vtkLineSource lineSource;
        vtkPolyDataMapper mapper;
        vtkActor actor;
        vtkRenderer renderer;

        public AxonRenderer(double[] shaftTipPosition)
        {
            lineSource = new vtkLineSource();
            mapper = vtkPolyDataMapper.New();
            actor = new vtkActor();

            lineSource.SetPoint1(shaftTipPosition[0], shaftTipPosition[1], 0);
            lineSource.SetPoint2(shaftTipPosition[2], shaftTipPosition[3], 0);
            mapper.SetInputConnection(lineSource.GetOutputPort());
            actor.SetMapper(mapper);

        }

        public void Render(vtkRenderer loRenderer)
        {
            renderer = loRenderer;
            renderer.AddActor(actor);
        }

        public void Remove()
        {
            if (renderer != null)
                renderer.RemoveActor(actor);
        }

        public void SetColor(double r, double g, double b)
        {
            actor.GetProperty().SetColor(r, g, b);
        }
    }
}
