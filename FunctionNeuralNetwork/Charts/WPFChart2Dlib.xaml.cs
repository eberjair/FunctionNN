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

namespace FunctionNeuralNetwork.Charts
{
    /// <summary>
    /// Interaction logic for WPFChart2Dlib.xaml
    /// </summary>
    public partial class WPFChart2Dlib : UserControl
    {
        #region Attributes
        private ChartStyle cs;
        private DataSeries ds;
        private DataCollection dc;
        private Legend lg;
        private Customs custm;
        private double extendDeltaX = 1;
        private double extendDeltaY = 1;

        #region Color Attributes
        private Brush mainGridColor = Brushes.Transparent;
        private Brush chartCanvasColor = Brushes.White;
        private Brush textCanvasColor = Brushes.White;
        private Brush legendCanvasColor = Brushes.Transparent;
        private Brush titleBackground = Brushes.White;
        private Brush titleForeground = Brushes.Black;
        private Brush xlabelBackground = Brushes.White;
        private Brush xlabelForeground = Brushes.Black;
        private Brush ylabelBackground = Brushes.White;
        private Brush ylabelForeground = Brushes.Black;

        private string gcXTicksFormat = null;
        private string gcYTicksFormat = null;

        private bool gbQuietMode = true;
        private string gcLastMessage = null;

        #endregion Color Attributes

        #endregion Attributes

        #region Constructor
        public WPFChart2Dlib()
        {
            InitializeComponent();
            this.cs = new ChartStyle();
            this.ds = new DataSeries();
            this.dc = new DataCollection();
            this.lg = new Legend();
            this.custm = new Customs();
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            lg.LegendCanvas = legendCanvas;
        }
        #endregion

        #region Events

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReDraw();
        }

        #endregion

        #region Methods
        private void AddChart()
        {
            if (dc.DataList.Count != 0)
            {
                #region Automatic Axes
                dc.GetMaxMin();
                dc.GetMaxMinStack();
                if (cs.IsAutomaticAxes == true)
                {
                    if ((dc.XMaxCollection != double.NaN) && (dc.XMaxCollection != double.NegativeInfinity) && (dc.XMaxCollection != double.PositiveInfinity) &&
                        (dc.XMinCollection != double.NaN) && (dc.XMinCollection != double.NegativeInfinity) && (dc.XMinCollection != double.PositiveInfinity) &&
                        (dc.YMaxCollection != double.NaN) && (dc.YMaxCollection != double.NegativeInfinity) && (dc.YMaxCollection != double.PositiveInfinity) &&
                        (dc.YMinCollection != double.NaN) && (dc.YMinCollection != double.NegativeInfinity) && (dc.YMinCollection != double.PositiveInfinity) &&
                        cs.AxisIndicator())
                    {

                        #region NADA
                        switch (cs.AxisStyle)
                        {
                            case ChartStyle.AxisStyleEnum.Linear:
                                cs.Xmax = cs.OptimalSpacing(extendDeltaX * dc.XMaxCollection);
                                cs.Ymax = cs.OptimalSpacing(extendDeltaY * dc.YMaxCollection);
                                if (dc.XMinCollection > 0)
                                    cs.Xmin = cs.OptimalSpacing((2 - extendDeltaX) * dc.XMinCollection);
                                else
                                    cs.Xmin = cs.OptimalSpacing(ExtendDeltaX * dc.XMinCollection);
                                if (dc.YMinCollection > 0)
                                    cs.Ymin = cs.OptimalSpacing((2 - extendDeltaY) * dc.YMinCollection);
                                else
                                    cs.Ymin = cs.OptimalSpacing(extendDeltaY * dc.YMinCollection);
                                if (cs.Ymin == cs.Ymax)
                                {
                                    cs.Ymin = dc.YMinCollection;
                                    cs.Ymax = dc.YMaxCollection;
                                }
                                break;
                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                cs.Xmax = cs.OptimalSpacing(extendDeltaX * dc.XMaxCollection);
                                cs.Ymax = cs.OptimalSpacing(extendDeltaY * dc.YMaxCollection);
                                cs.Xmin = cs.OptimalSpacing((2 - extendDeltaX) * dc.XMinLog);
                                cs.Ymin = cs.OptimalSpacing((2 - extendDeltaY) * dc.YMinLog);
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                cs.Xmax = cs.OptimalSpacing(extendDeltaX * dc.XMaxCollection);
                                cs.Ymax = cs.OptimalSpacing(extendDeltaY * dc.YMaxCollection);
                                cs.Xmin = cs.OptimalSpacing((2 - extendDeltaX) * dc.XMinLog);
                                if (dc.YMinCollection > 0)
                                    cs.Ymin = cs.OptimalSpacing((2 - extendDeltaY) * dc.YMinCollection);
                                else
                                    cs.Ymin = cs.OptimalSpacing(extendDeltaY * dc.YMinCollection);
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                cs.Xmax = cs.OptimalSpacing(extendDeltaX * dc.XMaxCollection);
                                cs.Ymax = cs.OptimalSpacing(extendDeltaY * dc.YMaxCollection);
                                cs.Ymin = cs.OptimalSpacing((2 - extendDeltaY) * dc.YMinLog);
                                if (dc.XMinCollection > 0)
                                    cs.Xmin = cs.OptimalSpacing((2 - extendDeltaX) * dc.XMinCollection);
                                else
                                    cs.Xmin = cs.OptimalSpacing(ExtendDeltaX * dc.XMinCollection);
                                break;
                        }
                        #endregion
                        #region try Automatic Ticks
                        cs.IsAutomaticTick = true;
                        #endregion
                    }
                }
                #endregion

                if (gcXTicksFormat != null) cs.XTicksFormat = gcXTicksFormat;
                if (gcYTicksFormat != null) cs.YTicksFormat = gcYTicksFormat;
                cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);
                dc.PlotSeries(cs);
                lg.AddLegend(chartCanvas, dc);

                custm.PlotCustoms(cs);
            }
            else cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);
        }

        public void ReDraw()
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            legendCanvas.Children.Clear();
            chartCanvas.Children.RemoveRange(1, chartCanvas.Children.Count - 1);
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            try
            {
                AddChart();
            }
            catch
            {
                gcLastMessage = "Range too small to draw";
                if (!gbQuietMode) //CustomMessage.ShowMessage("Information", gcLastMessage, CustomMessageButtons.OK, CustomMessageImage.Information);
                    MessageBox.Show(gcLastMessage, "Information", MessageBoxButton.OK);
            }
        }
        #endregion

        #region Properties



        public ChartStyle ChartStyle
        {
            get { return cs; }
            set { cs = value; }
        }
        public DataCollection DataCollection
        {
            get { return dc; }
            set { dc = value; }
        }
        public DataSeries DataSeries
        {
            get { return ds; }
            set { ds = value; }
        }
        public Legend Legend
        {
            get { return lg; }
            set { lg = value; }
        }
        public Customs Customs
        {
            get { return custm; }
            set { custm = value; }
        }
        public double ExtendDeltaX
        {
            get { return extendDeltaX; }
            set { extendDeltaX = value; }
        }
        public double ExtendDeltaY
        {
            get { return extendDeltaY; }
            set { extendDeltaY = value; }
        }

        #region Color Properties
        public Brush MainGridColor
        {
            get { return mainGridColor; }
            set
            {
                mainGridColor = value;
                grid1.Background = value;
                chartGrid.Background = value;
            }
        }
        public Brush ChartCanvasColor
        {
            get { return chartCanvasColor; }
            set
            {
                chartCanvasColor = value;
                chartCanvas.Background = value;
            }
        }
        public Brush TextCanvasColor
        {
            get { return textCanvasColor; }
            set
            {
                textCanvasColor = value;
                textCanvas.Background = value;
            }
        }
        public Brush LegendCanvasColor
        {
            get { return legendCanvasColor; }
            set
            {
                legendCanvasColor = value;
                legendCanvas.Background = value;
            }
        }
        public Brush TitleBackground
        {
            get { return titleBackground; }
            set
            {
                titleBackground = value;
                tbTitle.Background = value;
            }
        }
        public Brush TitleForeground
        {
            get { return titleForeground; }
            set
            {
                titleForeground = value;
                tbTitle.Foreground = value;
            }
        }
        public Brush XLabelBackground
        {
            get { return xlabelBackground; }
            set
            {
                xlabelBackground = value;
                tbXLabel.Background = value;
            }
        }
        public Brush XLabelForeground
        {
            get { return xlabelForeground; }
            set
            {
                xlabelForeground = value;
                tbXLabel.Foreground = value;
            }
        }
        public Brush YLabelBackground
        {
            get { return ylabelBackground; }
            set
            {
                ylabelBackground = value;
                tbYLabel.Background = value;
                tbY2Label.Background = value;
            }
        }
        public Brush YLabelForeground
        {
            get { return ylabelForeground; }
            set
            {
                ylabelForeground = value;
                tbYLabel.Foreground = value;
                tbY2Label.Foreground = value;
            }
        }
        #endregion


        public string XTicksFormat { set { gcXTicksFormat = value; } }
        public string YTicksFormat { set { gcYTicksFormat = value; } }

        public bool IsQuietMode
        {
            get { return gbQuietMode; }
            set { gbQuietMode = value; }
        }

        public string LastMessage { get { return gcLastMessage; } }

        #endregion

        #region Dependency Properties
        public static DependencyProperty XminProperty =
            DependencyProperty.Register("Xmin", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(0.0,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double Xmin
        {
            get { return (double)GetValue(XminProperty); }
            set
            {
                SetValue(XminProperty, value);
                cs.Xmin = value;
            }
        }

        public static DependencyProperty XmaxProperty =
            DependencyProperty.Register("Xmax", typeof(double),
            typeof(WPFChart2Dlib),
                new FrameworkPropertyMetadata(10.0,
                    new PropertyChangedCallback(OnPropertyChanged)));
        public double Xmax
        {
            get { return (double)GetValue(XmaxProperty); }
            set
            {
                SetValue(XmaxProperty, value);
                cs.Xmax = value;
            }
        }

        public static DependencyProperty YminProperty =
            DependencyProperty.Register("Ymin", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(0.0,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double Ymin
        {
            get { return (double)GetValue(YminProperty); }
            set
            {
                SetValue(YminProperty, value);
                cs.Ymin = value;
            }
        }

        public static DependencyProperty YmaxProperty =
            DependencyProperty.Register("Ymax", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(10.0,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double Ymax
        {
            get { return (double)GetValue(YmaxProperty); }
            set
            {
                SetValue(YmaxProperty, value);
                cs.Ymax = value;
            }
        }

        public static DependencyProperty XTickProperty =
            DependencyProperty.Register("XTick", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(2.0,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double XTick
        {
            get { return (double)GetValue(XTickProperty); }
            set
            {
                SetValue(XTickProperty, value);
                cs.XTick = value;
            }
        }

        public static DependencyProperty YTickProperty =
            DependencyProperty.Register("YTick", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(1.0,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double YTick
        {
            get { return (double)GetValue(YTickProperty); }
            set
            {
                SetValue(YTickProperty, value);
                cs.YTick = value;
            }
        }

        public static DependencyProperty X2ndTickProperty =
            DependencyProperty.Register("X2ndTick", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(1.0,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double X2ndTick
        {
            get { return (double)GetValue(X2ndTickProperty); }
            set
            {
                SetValue(X2ndTickProperty, value);
                cs.X2ndTick = value;
            }
        }

        public static DependencyProperty Y2ndTickProperty =
            DependencyProperty.Register("Y2ndTick", typeof(double),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(0.5,
                new PropertyChangedCallback(OnPropertyChanged)));
        public double Y2ndTick
        {
            get { return (double)GetValue(Y2ndTickProperty); }
            set
            {
                SetValue(Y2ndTickProperty, value);
                cs.Y2ndTick = value;
            }
        }

        public static DependencyProperty IsAutomaticAxesProperty =
            DependencyProperty.Register("IsAutomaticAxes", typeof(bool),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(false,
                new PropertyChangedCallback(OnPropertyChanged)));
        public bool IsAutomaticAxes
        {
            get { return (bool)GetValue(IsAutomaticAxesProperty); }
            set
            {
                SetValue(IsAutomaticAxesProperty, value);
                cs.IsAutomaticAxes = value;
            }
        }

        public static DependencyProperty AxisAlignmentProperty =
            DependencyProperty.Register("AxisAlignment",
            typeof(ChartStyle.AxisAlignmentEnum),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(
                ChartStyle.AxisAlignmentEnum.XY,
                new PropertyChangedCallback(OnPropertyChanged)));
        public ChartStyle.AxisAlignmentEnum AxisAlignment
        {
            get { return (ChartStyle.AxisAlignmentEnum)GetValue(AxisAlignmentProperty); }
            set
            {
                SetValue(AxisAlignmentProperty, value);
                cs.AxisAlignment = value;
            }
        }

        public static DependencyProperty GridLinePatternProperty =
            DependencyProperty.Register("GridLinePattern",
            typeof(ChartStyle.GridLinePatternEnum),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(
                ChartStyle.GridLinePatternEnum.Solid,
                new PropertyChangedCallback(OnPropertyChanged)));
        public ChartStyle.GridLinePatternEnum GridLinePattern
        {
            get { return (ChartStyle.GridLinePatternEnum)GetValue(GridLinePatternProperty); }
            set
            {
                SetValue(GridLinePatternProperty, value);
                cs.GridLinePattern = value;
            }
        }

        public static DependencyProperty GridLineColorProperty =
            DependencyProperty.Register("GridLineColor",
            typeof(Brush), typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(Brushes.Gray,
                new PropertyChangedCallback(OnPropertyChanged)));
        public Brush GridLineColor
        {
            get { return (Brush)GetValue(GridLineColorProperty); }
            set
            {
                SetValue(GridLineColorProperty, value);
                cs.GridlineColor = value;
            }
        }

        public static DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string),
            typeof(WPFChart2Dlib), new FrameworkPropertyMetadata("Title",
                new PropertyChangedCallback(OnPropertyChanged)));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
                cs.Title = value;
            }
        }

        public static DependencyProperty XLabelProperty =
            DependencyProperty.Register("X Axis", typeof(string),
            typeof(WPFChart2Dlib), new FrameworkPropertyMetadata("X Axis",
                new PropertyChangedCallback(OnPropertyChanged)));
        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set
            {
                SetValue(XLabelProperty, value);
                cs.XLabel = value;
            }
        }

        public static DependencyProperty YLabelProperty =
            DependencyProperty.Register("Y Axis", typeof(string),
            typeof(WPFChart2Dlib), new FrameworkPropertyMetadata("Y Axis",
                new PropertyChangedCallback(OnPropertyChanged)));
        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set
            {
                SetValue(YLabelProperty, value);
                cs.YLabel = value;
            }
        }

        public static DependencyProperty IsXGridProperty =
            DependencyProperty.Register("IsXGrid", typeof(bool),
            typeof(WPFChart2Dlib), new FrameworkPropertyMetadata(true,
                new PropertyChangedCallback(OnPropertyChanged)));
        public bool IsXGrid
        {
            get { return (bool)GetValue(IsXGridProperty); }
            set
            {
                SetValue(IsXGridProperty, value);
                cs.IsXGrid = value;
            }
        }

        public static DependencyProperty IsYGridProperty =
            DependencyProperty.Register("IsYGrid", typeof(bool),
            typeof(WPFChart2Dlib), new FrameworkPropertyMetadata(true,
                new PropertyChangedCallback(OnPropertyChanged)));
        public bool IsYGrid
        {
            get { return (bool)GetValue(IsYGridProperty); }
            set
            {
                SetValue(IsYGridProperty, value);
                cs.IsYGrid = value;
            }
        }

        public static DependencyProperty IsLegendProperty =
            DependencyProperty.Register("IsLegend", typeof(bool),
            typeof(WPFChart2Dlib), new FrameworkPropertyMetadata(false,
                new PropertyChangedCallback(OnPropertyChanged)));
        public bool IsLegend
        {
            get { return (bool)GetValue(IsLegendProperty); }
            set
            {
                SetValue(IsLegendProperty, value);
                lg.IsLegend = value;
            }
        }

        public static DependencyProperty LegendPositionProperty =
            DependencyProperty.Register("LegendPosition",
            typeof(Legend.LegendPositionEnum),
            typeof(WPFChart2Dlib),
            new FrameworkPropertyMetadata(Legend.LegendPositionEnum.NorthEast,
                new PropertyChangedCallback(OnPropertyChanged)));
        public Legend.LegendPositionEnum LegendPositon
        {
            get { return (Legend.LegendPositionEnum)GetValue(LegendPositionProperty); }
            set
            {
                SetValue(LegendPositionProperty, value);
                lg.LegendPosition = value;
            }
        }


        private static void OnPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            WPFChart2Dlib lcc = sender as WPFChart2Dlib;
            if (e.Property == XminProperty)
                lcc.Xmin = (double)e.NewValue;
            else if (e.Property == XmaxProperty)
                lcc.Xmax = (double)e.NewValue;
            else if (e.Property == YminProperty)
                lcc.Ymin = (double)e.NewValue;
            else if (e.Property == YmaxProperty)
                lcc.Ymax = (double)e.NewValue;
            else if (e.Property == XTickProperty)
                lcc.XTick = (double)e.NewValue;
            else if (e.Property == YTickProperty)
                lcc.YTick = (double)e.NewValue;
            else if (e.Property == IsAutomaticAxesProperty)
                lcc.IsAutomaticAxes = (bool)e.NewValue;
            else if (e.Property == GridLinePatternProperty)
                lcc.GridLinePattern =
                (ChartStyle.GridLinePatternEnum)e.NewValue;
            else if (e.Property == GridLineColorProperty)
                lcc.GridLineColor = (Brush)e.NewValue;
            else if (e.Property == TitleProperty)
                lcc.Title = (string)e.NewValue;
            else if (e.Property == XLabelProperty)
                lcc.XLabel = (string)e.NewValue;
            else if (e.Property == YLabelProperty)
                lcc.YLabel = (string)e.NewValue;
            else if (e.Property == IsXGridProperty)
                lcc.IsXGrid = (bool)e.NewValue;
            else if (e.Property == IsYGridProperty)
                lcc.IsYGrid = (bool)e.NewValue;
            else if (e.Property == IsLegendProperty)
                lcc.IsLegend = (bool)e.NewValue;
            else if (e.Property == LegendPositionProperty)
                lcc.LegendPositon = (Legend.LegendPositionEnum)e.NewValue;
            else if (e.Property == AxisAlignmentProperty)
                lcc.AxisAlignment =
                    (ChartStyle.AxisAlignmentEnum)e.NewValue;
        }

        #endregion
    }
}
