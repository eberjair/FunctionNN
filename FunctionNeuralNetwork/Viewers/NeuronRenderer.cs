using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FunctionNeuralNetwork.Viewers
{
    public class NeuronRenderer
    {
        vtkSphereSource sphereSource;
        vtkPolyDataMapper mapper;
        vtkActor actor;
        vtkRenderer renderer;

        public NeuronRenderer(double[] position)
        {
            sphereSource = new vtkSphereSource();
            sphereSource.SetRadius(1);
            mapper = vtkPolyDataMapper.New();
            actor = new vtkActor();

            mapper.SetInputConnection(sphereSource.GetOutputPort());
            actor.SetMapper(mapper);
            actor.SetPosition(position[0], position[1], position[2]);
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
            renderer = null;
        }
    }
}
