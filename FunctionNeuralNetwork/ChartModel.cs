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
            InitializeModel();
            ColumnSeries series = new ColumnSeries();
            PlotModel.Series.Add(series);
            for(int  i= 0; i<100;i++)
                series.Items.Add(new ColumnItem(0));
            
        }

        public ChartModel(double[] lsData)
        {
            InitializeModel();
            ColumnSeries series = new ColumnSeries();
            PlotModel.Series.Add(series);
            for(int i=0; i<lsData.Length;i++)
                series.Items.Add(new ColumnItem(lsData[i]));
            
        }

        void InitializeModel()
        {
            PlotModel = new PlotModel { Title = "Last execution/learning error\n(Subsampling 100 averages max)", TitleFontSize = 10 };
            // A ColumnSeries requires a CategoryAxis on the x-axis.
            CategoryAxis errorCategory = new CategoryAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Title = "Iterations sets",
                TitleFontSize = 8,
                GapWidth = -0.1,
                TickStyle = TickStyle.None,
                LabelFormatter = x => null
            };

            PlotModel.Axes.Add(errorCategory);

            LinearAxis yAxis = new LinearAxis
            {
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Minimum = 0,
                Maximum = 1,
                Title = "Mean error",
                TitleFontSize = 8,
                Position = AxisPosition.Left
            };
            PlotModel.Axes.Add(yAxis);
        }
    }
}
