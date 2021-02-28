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
            PlotModel = new PlotModel { Title = "Last execution/learning error\n(Subsampling 100 iterations max)", TitleFontSize = 10 };
            // A ColumnSeries requires a CategoryAxis on the x-axis.
            CategoryAxis errorCategory = new CategoryAxis();
            errorCategory.IsPanEnabled = false;
            errorCategory.IsZoomEnabled = false;
            errorCategory.Title = "Iterations";
            errorCategory.TitleFontSize = 8;
            errorCategory.GapWidth = -0.1;
            errorCategory.TickStyle = TickStyle.None;
            errorCategory.LabelFormatter = x => null;

            PlotModel.Axes.Add(errorCategory);

            LinearAxis yAxis = new LinearAxis();
            yAxis.IsPanEnabled = false;
            yAxis.IsZoomEnabled = false;
            yAxis.Minimum = 0;
            yAxis.Maximum = 1;
            yAxis.Title = "Error";
            yAxis.TitleFontSize = 8;
            yAxis.Position = AxisPosition.Left;
            PlotModel.Axes.Add(yAxis);
        }
    }
}
