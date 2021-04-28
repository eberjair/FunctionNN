using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace FunctionNeuralNetwork.Viewers.Renderers
{
    public abstract class PlanViewSelector
    {
        protected vtkActor goAreaActor;
        protected vtkActor goEdgesActor;

        public abstract void Render(vtkRenderer loRenderer);

        public abstract void Remove(vtkRenderer loRenderer);

        public virtual bool Picked(vtkActor loActorPicked)
        {
            return loActorPicked != null && (loActorPicked == goAreaActor || loActorPicked == goEdgesActor);
        }
    }
}
