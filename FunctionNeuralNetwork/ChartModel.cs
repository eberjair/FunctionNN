using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace FunctionNeuralNetwork
{
    public class ChartModel
    {
        public PlotModel PlotModel { get; private set; }
        public ChartModel()
        {
            PlotModel = new PlotModel { Title = "ColumnSeries" };
            // A ColumnSeries requires a CategoryAxis on the x-axis.
            PlotModel.Axes.Add(new CategoryAxis());
            var series = new ColumnSeries();
            PlotModel.Series.Add(series);
            for(int  i= 0; i<50;i++)
            {
                series.Items.Add(new ColumnItem(1 - (i * .01)));
            }

        }
    }
}
