using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunctionNeuralNetwork.Charts
{
    public class ChartStyle
    {
        #region Attributes

        //ChartArea definition
        private Canvas chartCanvas;

        #region Axis Attributes
        private bool gbShowXLabel = true;
        private bool gbShowYLabel = true;

        //Limits of graphics.
        private double xmin = 0;
        private double xmax = 10;
        private double ymin = 0;
        private double ymax = 10;
        private bool isAutomaticAxes = false;
        //Autamatic Tick
        private bool isAutomaticTick = false;
        //AxisAlignment
        private AxisAlignmentEnum axisAlignment = AxisAlignmentEnum.XY;
        //AxisStyle
        private AxisStyleEnum axisStyle = AxisStyleEnum.Linear;
        private bool defineLogAxisFlag_gb = true;          //Fix a Logarithmic Axis.
        private bool logWholeRange = true;
        private Brush rectLimitColor_go = Brushes.Black;
        //Show Tick in Axis
        private bool showTickX_gb = true;
        private bool showTickY_gb = true;

        private bool showShortLabelX_gb = true;
        private bool showShortLabelY_gb = true;

        private string gcXTicksFormat = "N";
        private string gcYTicksFormat = "N";

        #endregion

        #region Title and GridLines
        private bool gbShowTitle = true;

        private string title;
        private double titleSize = 12;
        private FontWeight titleWeight = FontWeights.Bold;

        private string base_gs = "10";

        private string xLabel;
        private double xLabelSize = 11;
        private FontWeight xLabelWeight = FontWeights.SemiBold;

        private string yLabel;
        private double yLabelSize = 11;
        private FontWeight yLabelWeight = FontWeights.SemiBold;

        private Canvas textCanvas;
        private bool isXGrid = true;
        private bool isYGrid = true;
        private Brush gridlineColor = Brushes.Black;
        private double xTick = 1;
        private double yTick = 1;

        private double tickLabelSize = 11;
        private double tickLabelSizeY = 11;
        private double tickLabelSizeX = 11;
        private FontWeight tickLabelWeights = FontWeights.Normal;
        private Brush tickLabelColor = Brushes.Black;

        private GridLinePatternEnum gridLinePattern;
        private double leftOffset = 20;
        private double bottomOffset = 20;
        private double rightOffset = 10;
        private double tbHeight_gn = 10;
        private Line gridline = new Line();

        private double gnYTickOrg;
        private double gnXTickOrg;
        #endregion

        #region SecondGrid Attributes
        //SecondGrid
        private LineStyle grid2ndStyle = new LineStyle(Brushes.Gray, 0.75, LineStyle.LinePatternEnum.Dash);
        private bool isX2ndGrid = false;
        private bool isY2ndGrid = false;
        private double x2ndTick = 0.5;
        private double y2ndTick = 0.5;
        private Line grid2ndline = new Line();
        #endregion

        #region Second Y-Axis
        private double y2min = 0;
        private double y2max = 10;
        private double y2Tick = 2;
        private string y2Label = "Y2Label";
        #endregion

        #region Reduction Action Attributes
        private bool gbIsReductionActionOn = false;
        #endregion

        #region Set Manual Offsets
        private bool gbSetManualLeftOffset = false;
        private double gnManualLeftOffset;

        #endregion
        #endregion

        #region Properties
        //PUBLIC DEFINITIONS.
        //ChartArea
        public Canvas ChartCanvas
        {
            get { return chartCanvas; }
            set { chartCanvas = value; }
        }

        #region Axis Properties
        //Limits of graphics.
        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }
        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }
        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }
        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }
        public bool IsAutomaticAxes
        {
            get { return isAutomaticAxes; }
            set { isAutomaticAxes = value; }
        }
        //Automatic Tick
        internal bool IsAutomaticTick
        {
            get { return isAutomaticTick; }
            set { isAutomaticTick = value; }
        }
        //Show Ticks in Axis
        public bool ShowTickX
        {
            get { return showTickX_gb; }
            set { showTickX_gb = value; }
        }
        public bool ShowTickY
        {
            get { return showTickY_gb; }
            set { showTickY_gb = value; }
        }
        //AxisAligment
        public AxisAlignmentEnum AxisAlignment
        {
            get { return axisAlignment; }
            set { axisAlignment = value; }
        }
        //AxisStyle
        public AxisStyleEnum AxisStyle
        {
            get { return axisStyle; }
            set { axisStyle = value; }
        }
        //Fix Logarithmic Axis
        public bool DefineLogAxisFlag
        {
            get { return defineLogAxisFlag_gb; }
            set { defineLogAxisFlag_gb = value; }
        }


        public bool LogWholeRange
        {
            get { return logWholeRange; }
            set { logWholeRange = value; }
        }
        public Brush RectLimitColor
        {
            get { return rectLimitColor_go; }
            set { rectLimitColor_go = value; }
        }
        #endregion
        //Title and Grids
        public bool ShowTitle
        {
            get { return gbShowTitle; }
            set { gbShowTitle = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public double TitleSize
        {
            get { return titleSize; }
            set { titleSize = value; }
        }
        public FontWeight TitleWeigths
        {
            get { return titleWeight; }
            set { titleWeight = value; }
        }

        public string XLabel
        {
            get { return xLabel; }
            set { xLabel = value; }
        }
        public double XLabelSize
        {
            get { return xLabelSize; }
            set { xLabelSize = value; }
        }
        public FontWeight XLabelWeight
        {
            get { return xLabelWeight; }
            set { xLabelWeight = value; }
        }
        public bool ShowXLabel
        {
            get { return gbShowXLabel; }
            set { gbShowXLabel = value; }
        }

        public string YLabel
        {
            get { return yLabel; }
            set { yLabel = value; }
        }
        public double YLabelSize
        {
            get { return yLabelSize; }
            set { yLabelSize = value; }
        }
        public FontWeight YLabelWeight
        {
            get { return yLabelWeight; }
            set { yLabelWeight = value; }
        }
        public bool ShowYLabel
        {
            get { return gbShowYLabel; }
            set { gbShowYLabel = value; }
        }

        public bool ShowShortLabelX
        {
            get { return showShortLabelX_gb; }
            set { showShortLabelX_gb = value; }
        }
        public bool ShowShortLabelY
        {
            get { return showShortLabelY_gb; }
            set { showShortLabelY_gb = value; }
        }

        public string XTicksFormat { set { gcXTicksFormat = value; } }
        public string YTicksFormat { set { gcYTicksFormat = value; } }

        public GridLinePatternEnum GridLinePattern
        {
            get { return gridLinePattern; }
            set { gridLinePattern = value; }
        }

        //Begin: BPO 271112
        public double LeftOffset
        {
            get { return leftOffset; }
            set { leftOffset = value; }
        }

        public double RightOffset
        {
            get { return rightOffset; }
            set { rightOffset = value; }
        }

        public double BottomOffset
        {
            get { return bottomOffset; }
            set { bottomOffset = value; }
        }

        public double TbHeight
        {
            get { return tbHeight_gn; }
            set { tbHeight_gn = value; }
        }

        public Line GridLine
        {
            get { return gridline; }
            set { gridline = value; }
        }
        public string Base_gs
        {
            get { return base_gs; }
            set { base_gs = value; }
        }
        //End: BPO 271112

        public double XTick
        {
            get { return xTick; }
            set { xTick = value; }
        }
        public double YTick
        {
            get { return yTick; }
            set { yTick = value; }
        }

        public double TickLabelSize
        {
            get { return tickLabelSize; }
            set
            {
                tickLabelSize = value;
            }
        }

        public double TickLabelSizeY
        {
            get { return tickLabelSizeY; }
            set { tickLabelSizeY = value; }
        }
        public double TickLabelSizeX
        {
            get { return tickLabelSizeX; }
            set { tickLabelSizeX = value; }
        }
        public FontWeight TickLabelWeight
        {
            get { return tickLabelWeights; }
            set { tickLabelWeights = value; }
        }
        public Brush TickLabelColor
        {
            get { return tickLabelColor; }
            set { tickLabelColor = value; }
        }

        public Brush GridlineColor
        {
            get { return gridlineColor; }
            set { gridlineColor = value; }
        }
        public Canvas TextCanvas
        {
            get { return textCanvas; }
            set { textCanvas = value; }
        }
        public bool IsXGrid
        {
            get { return isXGrid; }
            set { isXGrid = value; }
        }
        public bool IsYGrid
        {
            get { return isYGrid; }
            set { isYGrid = value; }
        }

        //SecondGrid 
        #region SecondGrid Properties
        public LineStyle Grid2ndStyle
        {
            get { return grid2ndStyle; }
            set { grid2ndStyle = value; }
        }
        public bool IsX2ndGrid
        {
            get { return isX2ndGrid; }
            set { isX2ndGrid = value; }
        }
        public bool IsY2ndGrid
        {
            get { return isY2ndGrid; }
            set { isY2ndGrid = value; }
        }
        public double X2ndTick
        {
            get { return x2ndTick; }
            set { x2ndTick = value; }
        }
        public double Y2ndTick
        {
            get { return y2ndTick; }
            set { y2ndTick = value; }
        }
        #endregion

        //Second Y-Axis Limits 
        public double Y2min
        {
            get { return y2min; }
            set { y2min = value; }
        }
        public double Y2max
        {
            get { return y2max; }
            set { y2max = value; }
        }
        public double Y2Tick
        {
            get { return y2Tick; }
            set { y2Tick = value; }
        }
        public string Y2Label
        {
            get { return y2Label; }
            set { y2Label = value; }
        }


        #region Reduction Action Properties
        public bool IsReductionActionOn
        {
            get { return gbIsReductionActionOn; }
            set { gbIsReductionActionOn = value; }
        }
        #endregion

        #region Set Manual Offset Properties

        public bool SetManualLeftOffset
        {
            get { return gbSetManualLeftOffset; }
            set { gbSetManualLeftOffset = value; }
        }

        public double ManualLeftOffset
        {
            get { return gnManualLeftOffset; }
            set { gnManualLeftOffset = value; }
        }
        #endregion
        #endregion

        #region Constructor
        public ChartStyle()
        {
            title = "Title";
            xLabel = "X Axis";
            yLabel = "Y Axis";
        }
        #endregion

        #region Methods

        #region ChartStyle Methods

        public void AddChartStyle2Y(TextBlock tbTitle, TextBlock tbXLabel, TextBlock tbYLabel, TextBlock tbY2Label)
        {
            Point pt = new Point();
            Line tick = new Line();
            double offset = 0;
            double dx, dy;
            TextBlock tb = new TextBlock();
            Size size = new Size();

            //Right Offset
            // Determine right offset:
            for (dy = Y2min; dy <= Y2max; dy += Y2Tick)
            {
                pt = Point2D2Y(new Point(Xmax, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.TextAlignment = TextAlignment.Left;
                tb.Measure(new Size(Double.PositiveInfinity,
                    Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width)
                    offset = size.Width;
            }
            rightOffset = offset + 10;

            //Left Offset
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = Point2D(new Point(Xmin, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.TextAlignment = TextAlignment.Right;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width)
                    offset = size.Width;
            }
            leftOffset = offset + 5;

            Canvas.SetLeft(ChartCanvas, leftOffset + 10);
            Canvas.SetBottom(ChartCanvas, bottomOffset);
            ChartCanvas.Width = TextCanvas.Width - leftOffset - rightOffset;
            ChartCanvas.Height = TextCanvas.Height - bottomOffset - size.Height / 2;
            Rectangle chartRect = new Rectangle();
            chartRect.Stroke = Brushes.Black;
            chartRect.Width = ChartCanvas.Width;
            chartRect.Height = ChartCanvas.Height;
            ChartCanvas.Children.Add(chartRect);
            //Vertical GridLines
            if (IsYGrid == true)
            {
                for (dx = Xmin + XTick; dx < Xmax; dx += XTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = Point2D(new Point(dx, Ymin)).X;
                    gridline.Y1 = Point2D(new Point(dx, Ymin)).Y;
                    gridline.X2 = Point2D(new Point(dx, Ymax)).X;
                    gridline.Y2 = Point2D(new Point(dx, Ymax)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            //Horizontal GridLines
            if (IsXGrid == true)
            {
                for (dy = Ymin + YTick; dy < Ymax; dy += YTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = Point2D(new Point(Xmin, dy)).X;
                    gridline.Y1 = Point2D(new Point(Xmin, dy)).Y;
                    gridline.X2 = Point2D(new Point(xmax, dy)).X;
                    gridline.Y2 = Point2D(new Point(Xmax, dy)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            //Axis X Ticks marks
            for (dx = Xmin; dx <= Xmax; dx += XTick)
            {
                pt = Point2D(new Point(dx, Ymin));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X;
                tick.Y2 = pt.Y - 5;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dx.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, leftOffset + pt.X - size.Width / 2);
                Canvas.SetTop(tb, pt.Y + 2 + size.Height / 2);
            }

            //Axis Y Ticks marks
            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = Point2D(new Point(Xmin, dy));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X + 5;
                tick.Y2 = pt.Y;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetRight(tb, ChartCanvas.Width + 35);
                Canvas.SetTop(tb, pt.Y);
            }

            // Create y2-axis tick marks:
            for (dy = Y2min; dy <= Y2max; dy += Y2Tick)
            {
                pt = Point2D2Y(new Point(Xmax, dy));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X - 5;
                tick.Y2 = pt.Y;
                ChartCanvas.Children.Add(tick);
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(Double.PositiveInfinity,
                Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, ChartCanvas.Width + 30);
                Canvas.SetTop(tb, pt.Y - 1);
            }
            // Add title and labels:
            tbTitle.Text = Title;
            tbXLabel.Text = XLabel;
            tbYLabel.Text = YLabel;
            tbY2Label.Text = Y2Label;
            tbXLabel.Margin = new Thickness(leftOffset - rightOffset + 2, 2, 2, 2);
            tbTitle.Margin = new Thickness(leftOffset - rightOffset + 2, 2, 2, 2);
        }

        public void AddChartStyle(TextBlock tbTitle, TextBlock tbXLabel, TextBlock tbYLabel)
        {
            Point pt = new Point();
            Line tick = new Line();
            double offset = 0;
            double dy;
            TextBlock tb = new TextBlock();
            Size size = new Size();



            #region AutomaticTick == true
            if (IsAutomaticTick == true)
            {
                double optimalXSpacing = 100;
                double optimalYSpacing = 100;

                #region Axis Limits
                DetAxisLimits();
                #endregion

                #region Right Offset
                SetRightOffset();
                #endregion

                #region Determine Ticks

                double xScale = 0.0, yScale = 0.0;
                double xSpacing = 0.0, ySpacing = 0.0;
                //double xTick = 0.0, yTick = 0.0;
                int xStart = 0, xEnd = 1;
                int yStart = 0, yEnd = 1;
                double offset0 = 30;

                while (Math.Abs(offset - offset0) > 1)
                {
                    if (Xmin != Xmax)
                        xScale = (double)(TextCanvas.Width - offset0 - rightOffset - 5) / (double)(Xmax - Xmin);

                    if (Ymin != Ymax)
                        yScale = (double)TextCanvas.Height / (double)(Ymax - Ymin);
                    xSpacing = optimalXSpacing / xScale;

                    if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogY))
                        xTick = OptimalSpacing(xSpacing, true);
                    else
                        xTick = OptimalSpacing(xSpacing, false);

                    ySpacing = optimalYSpacing / yScale;
                    if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                        yTick = OptimalSpacing(ySpacing, true);
                    else
                        yTick = OptimalSpacing(ySpacing, false);
                    xStart = (int)Math.Ceiling(Xmin / xTick);
                    xEnd = (int)Math.Floor(Xmax / xTick);
                    yStart = (int)Math.Ceiling(Ymin / yTick);
                    yEnd = (int)Math.Floor(Ymax / yTick);

                    for (int i = yStart; i <= yEnd; i++)
                    {
                        dy = i * yTick;
                        //pt = Point2D(new Point(Xmin, dy));
                        tb = new TextBlock();
                        tb.FontSize = tickLabelSizeY;
                        tb.FontWeight = tickLabelWeights;
                        if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                            tb.Text = dy.ToString();
                        else
                            tb.Text = base_gs + dy.ToString();
                        tb.TextAlignment = TextAlignment.Right;
                        tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        size = tb.DesiredSize;
                        if (offset < size.Width)
                            offset = size.Width;
                    }
                    if (offset0 > offset)
                        offset0 -= 0.5;
                    else if (offset0 < offset)
                        offset0 += 0.5;
                }
                tbHeight_gn = tb.DesiredSize.Height;
                leftOffset = offset + 15;
                #endregion

                #region Manual Set Offsets

                #region Manual Left Offset
                LeftOffsetManual(yStart, yEnd, yTick, tbYLabel);
                #endregion

                #endregion


                #region Set Offsets
                SetOffsets();
                #endregion

                if (Xmin != Xmax)
                    xScale = (double)ChartCanvas.Width / (double)(Xmax - Xmin);
                if (Ymin != Ymax)
                    yScale = (double)ChartCanvas.Height / (double)(Ymax - Ymin);
                xSpacing = optimalXSpacing / xScale;
                if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogY))
                    xTick = OptimalSpacing(xSpacing, true);
                else
                    xTick = OptimalSpacing(xSpacing, false);
                ySpacing = optimalYSpacing / yScale;
                if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                    yTick = OptimalSpacing(ySpacing, true);
                else
                    yTick = OptimalSpacing(ySpacing, false);
                xStart = (int)Math.Ceiling(Xmin / xTick);
                xEnd = (int)Math.Floor(Xmax / xTick);
                yStart = (int)Math.Ceiling(Ymin / yTick);
                yEnd = (int)Math.Floor(Ymax / yTick);

                #region Set Ticks
                SetTicks();
                #endregion

                #region 1st GridLines
                #region Vertical 1st GridLines
                GetXTickBegin(); // calcula donde debería ir el primer tick con etiqueta del eje X
                VerticalGridLine(isYGrid);
                #endregion

                #region Horizontal 1st GridLines
                GetYTickBegin(); // calcula donde debería ir el primer tick con etiqueta del eje Y
                HorizontalGridLine(isXGrid);
                #endregion
                #endregion

                #region 2nd GridLines
                #region Vertical 2nd GridLines
                Vertical2ndGridLine(isY2ndGrid);
                #endregion

                #region Horizontal 2nd GridLine
                Horizontal2ndGridLine(isX2ndGrid);
                #endregion
                #endregion

                #region Tick Marks
                #region X-Axis Tick Marks
                TickMarksX();
                #endregion

                #region Y-Axis Tick Marks
                TickMarksY();
                #endregion
                #endregion

            }
            #endregion


            #region AutomaticTick == false
            else
            {

                #region Axis Limits
                DetAxisLimits();
                #endregion

                #region Right Offset
                SetRightOffset();
                #endregion

                #region Left Offset
                SetLeftOffset(offset, tbYLabel);
                #endregion

                #region Set Offsets
                SetOffsets();
                #endregion

                #region 2nd GridLines
                #region Vertical 2nd GridLines
                Vertical2ndGridLine(isY2ndGrid);
                #endregion

                #region Horizontal 2nd GridLine
                Horizontal2ndGridLine(isX2ndGrid);
                #endregion
                #endregion

                #region 1st GridLines
                #region Vertical 1st GridLines
                VerticalGridLine(isYGrid);
                #endregion

                #region Horizontal 1st GridLines
                HorizontalGridLine(isXGrid);
                #endregion
                #endregion

                #region Tick Marks
                #region X-Axis Tick Marks
                TickMarksX();
                #endregion

                #region Y-Axis Tick Marks
                TickMarksY();
                #endregion
                #endregion
            }
            #endregion

            #region Add title and labels:
            if (gbShowTitle)
            {
                tbTitle.Text = Title;
                tbTitle.FontSize = titleSize;
                tbTitle.FontWeight = titleWeight;
                tbTitle.Margin = new Thickness(leftOffset + 2, 2, 2, 2);
            }
            else
                tbTitle.Visibility = Visibility.Collapsed;

            if (gbShowXLabel)
            {
                tbXLabel.Text = XLabel;
                tbXLabel.FontSize = xLabelSize;
                tbXLabel.FontWeight = xLabelWeight;
                tbXLabel.Margin = new Thickness(leftOffset + 2, 2, 2, 2);
            }
            else
                tbXLabel.Visibility = Visibility.Collapsed;

            if (gbShowYLabel)
            {
                tbYLabel.Text = YLabel;
                tbYLabel.FontSize = yLabelSize;
                tbYLabel.FontWeight = yLabelWeight;
            }
            else
                tbYLabel.Visibility = Visibility.Collapsed;
            #endregion
        }

        /// <summary>
        /// Calcula los Ticks para el eje X y Y (previamente calculados) al valor de potencia 10 más cercano
        /// </summary>
        private void SetTicks()
        {
            double epsilon = 0.0000000001;

            if (AxisStyle == ChartStyle.AxisStyleEnum.Linear)
            {
                double
                    exponenteX = getExponente(xTick),
                    exponenteX2 = getExponente(xTick * 2),
                    exponenteY = getExponente(yTick),
                    exponenteY2 = getExponente(yTick * 2);

                if (Math.Abs(xTick - Math.Pow(10, exponenteX)) < 2 * Math.Pow(10, exponenteX))
                    xTick = Math.Pow(10, exponenteX);
                else if (exponenteX == exponenteX2)
                    xTick = 2 * Math.Pow(10, exponenteX);
                else
                    xTick = 5 * Math.Pow(10, exponenteX);
                if (Math.Abs(xmax - xmin) / xTick < 3)
                    xTick /= 2;
                if (xTick < epsilon)
                    xTick = epsilon;

                if (Math.Abs(yTick - Math.Pow(10, exponenteY)) < 2 * Math.Pow(10, exponenteY))
                    yTick = Math.Pow(10, exponenteY);
                else if (exponenteY == exponenteY2)
                    yTick = 2 * Math.Pow(10, exponenteY);
                else
                    yTick = 5 * Math.Pow(10, exponenteY);
                if (Math.Abs(ymax - ymin) / yTick < 3)
                    yTick /= 2;
                if (yTick < epsilon)
                    yTick = epsilon;
            }
            else if (AxisStyle == ChartStyle.AxisStyleEnum.SemiLogX)
            {
                double
                   exponenteY = getExponente(yTick),
                   exponenteY2 = getExponente(yTick * 2);

                if (Math.Abs(yTick - Math.Pow(10, exponenteY)) < 2 * Math.Pow(10, exponenteY))
                    yTick = Math.Pow(10, exponenteY);
                else if (exponenteY == exponenteY2)
                    yTick = 2 * Math.Pow(10, exponenteY);
                else
                    yTick = 5 * Math.Pow(10, exponenteY);
                if (Math.Abs(ymax - ymin) / yTick < 3)
                    yTick /= 2;
                if (yTick < epsilon)
                    yTick = epsilon;

                xTick = 1;
            }
            else if (AxisStyle == ChartStyle.AxisStyleEnum.SemiLogY)
            {
                double
                    exponenteX = getExponente(xTick),
                    exponenteX2 = getExponente(xTick * 2);

                if (Math.Abs(xTick - Math.Pow(10, exponenteX)) < 2 * Math.Pow(10, exponenteX))
                    xTick = Math.Pow(10, exponenteX);
                else if (exponenteX == exponenteX2)
                    xTick = 2 * Math.Pow(10, exponenteX);
                else
                    xTick = 5 * Math.Pow(10, exponenteX);
                if (Math.Abs(xmax - xmin) / xTick < 3)
                    xTick /= 2;
                if (xTick < epsilon)
                    xTick = epsilon;

                yTick = 1;
            }

            else if (AxisStyle == ChartStyle.AxisStyleEnum.Logarithmic)
            {
                xTick = 1;
                yTick = 1;
            }
        }

        public void AddLinePattern()
        {
            gridline.Stroke = GridlineColor;
            gridline.StrokeThickness = 1;

            switch (GridLinePattern)
            {
                case GridLinePatternEnum.Dash:
                    gridline.StrokeDashArray =
                        new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case GridLinePatternEnum.Dot:
                    gridline.StrokeDashArray =
                        new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case GridLinePatternEnum.DashDot:
                    gridline.StrokeDashArray =
                        new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
        }

        #endregion

        #region Normalize Functions
        /// <summary>
        /// Basic normalize function
        /// </summary>
        /// <param name="pt">Point to normalize</param>
        /// <returns>Normalize point</returns>
        public Point Point2D(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();

            switch (AxisAlignment)
            {
                case AxisAlignmentEnum.XY:
                    result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
                    result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
                    break;
                case AxisAlignmentEnum.XinY:
                    result.X = ChartCanvas.Width - (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
                    result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
                    break;
                case AxisAlignmentEnum.XinYin:
                    result.X = ChartCanvas.Width - (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
                    result.Y = (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
                    break;
                case AxisAlignmentEnum.XYin:
                    result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
                    result.Y = (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
                    break;
            }
            return result;

        }
        /// <summary>
        /// Normalize Function for Yin Ticks
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point Point2DYin(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);

            return result;

        }
        /// <summary>
        /// Normalize Function for XinYin Ticks
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point Point2DXinYin(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = ChartCanvas.Width - (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);

            return result;

        }
        /// <summary>
        /// Normalize function for Xin ticks
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point Point2DXin(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);

            return result;

        }
        /// <summary>
        /// Normalize Funtion for Y2-Axis
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        /// Hack Need debug
        public Point Point2D2Y(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 270;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 250;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Y2min) * ChartCanvas.Height / (Y2max - Y2min);
            return result;
        }

        public Point DiffPoint2D(Point loDiffPt)
        {
            Point loResult = new Point();
            double lnXSpam = xmax - xmin;
            double lnYSpam = ymax - ymin;


            switch (AxisAlignment)
            {
                case AxisAlignmentEnum.XY:
                    loResult.X = loDiffPt.X * ChartCanvas.Width / lnXSpam;
                    loResult.Y = -loDiffPt.Y * ChartCanvas.Height / lnYSpam;
                    break;
                case AxisAlignmentEnum.XinY:
                    loResult.X = -loDiffPt.X * ChartCanvas.Width / lnXSpam;
                    loResult.Y = -loDiffPt.Y * ChartCanvas.Height / lnYSpam;
                    break;
                case AxisAlignmentEnum.XinYin:
                    loResult.X = -loDiffPt.X * ChartCanvas.Width / lnXSpam;
                    loResult.Y = loDiffPt.Y * ChartCanvas.Height / lnYSpam;
                    break;
                case AxisAlignmentEnum.XYin:
                    loResult.X = loDiffPt.X * ChartCanvas.Width / lnXSpam;
                    loResult.Y = loDiffPt.Y * ChartCanvas.Height / lnYSpam;
                    break;
            }
            return loResult;

        }

        public Point invPoint2D(Point pt_lo)
        {
            Point result = new Point();
            double height_ln = ChartCanvas.Height;
            double width_ln = ChartCanvas.Width;
            double spamX = Xmax - Xmin;
            double spamY = Ymax - Ymin;
            double X_ln = 0, Y_ln = 0;
            switch (AxisAlignment)
            {
                case AxisAlignmentEnum.XY:
                    X_ln = pt_lo.X * spamX / width_ln + Xmin;
                    Y_ln = Ymax - spamY * pt_lo.Y / height_ln;
                    break;
                case AxisAlignmentEnum.XinY:
                    X_ln = ((width_ln - pt_lo.X) * spamX) / width_ln + Xmin;
                    Y_ln = Ymax - spamY * pt_lo.Y / height_ln;
                    break;
                case AxisAlignmentEnum.XinYin:
                    X_ln = ((width_ln - pt_lo.X) * spamX) / width_ln + Xmin;
                    Y_ln = Ymin - (Ymax - spamY * pt_lo.Y) / height_ln;
                    break;
                case AxisAlignmentEnum.XYin:
                    X_ln = pt_lo.X * spamX / width_ln + Xmin;
                    Y_ln = Ymin - (Ymax - spamY * pt_lo.Y) / height_ln;
                    break;
            }

            switch (AxisStyle)
            {
                case AxisStyleEnum.Linear:
                    break;
                case AxisStyleEnum.Logarithmic:
                    X_ln = Math.Pow(10, X_ln);
                    Y_ln = Math.Pow(10, Y_ln);
                    break;
                case AxisStyleEnum.SemiLogX:
                    X_ln = Math.Pow(10, X_ln);
                    break;
                case AxisStyleEnum.SemiLogY:
                    Y_ln = Math.Pow(10, Y_ln);
                    break;
            }

            result.X = X_ln;
            result.Y = Y_ln;
            return result;
        }

        #region Logarithm Normalize Functions
        /// <summary>
        /// Returns a Log10 point
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point Point2DLogLog(Point pt)
        {
            Point result = new Point();
            result.X = Math.Log10(pt.X);
            result.Y = Math.Log10(pt.Y);

            return result;
        }
        /// <summary>
        /// Returns a point with X in Log10
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point Point2DLogX(Point pt)
        {
            Point result = new Point();
            result.X = Math.Log10(pt.X);
            result.Y = pt.Y;

            return result;
        }
        /// <summary>
        /// Returns a point with Y in Log10
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point Point2DLogY(Point pt)
        {
            Point result = new Point();
            result.X = pt.X;
            result.Y = Math.Log10(pt.Y);

            return result;
        }
        /// <summary>
        /// Returns the next power in base 10 of z
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public double NextPow10(double z)
        {
            return Math.Ceiling(Math.Log10(z));
        }
        /// <summary>
        /// Returns the preciding power in base 10 of z
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public double PredPow10(double z)
        {
            return Math.Floor(Math.Log10(z));
        }
        /// <summary>
        /// Determines Optimized or Whole LogAxis Space.
        /// </summary>
        /// <param name="data"> number to get Optimized logarithmic</param>
        /// <param name="sit"> type of optimization</param>
        /// <returns></returns>
        public double LogOptimized(double data, LogAxisSpacingEnum sit)
        {
            bool wholesup = false;
            bool wholeinf = false;
            double z_aux;
            bool signChange = false;

            if (data > 0)
            {

                double z = Math.Log10(data);
                if (z >= 0)
                    z_aux = z;
                else
                {
                    z_aux = -z;
                    signChange = true;
                }

                double fract = z_aux - (int)z_aux;

                double entero = z_aux - fract;

                if (fract != 0)
                {
                    for (double di = 0.1; di < 1; di += 0.1)
                    {
                        if ((fract < di) && (fract > (di - 0.1)))
                        {
                            switch (sit)
                            {
                                case LogAxisSpacingEnum.OptSup:
                                    fract = di;
                                    break;
                                case LogAxisSpacingEnum.OptInf:
                                    fract = di - 0.1;
                                    break;
                                case LogAxisSpacingEnum.WholeSup:
                                    wholesup = true;
                                    break;
                                case LogAxisSpacingEnum.WholeInf:
                                    wholeinf = true;
                                    break;
                                default:
                                    fract = 0;
                                    break;
                            }
                        }
                    }

                }
                else
                {
                    switch (sit)
                    {
                        case LogAxisSpacingEnum.OptSup:
                            fract = 0.1;
                            break;
                        case LogAxisSpacingEnum.OptInf:
                            fract = -0.1;
                            break;
                        default:
                            fract = 0;
                            break;
                    }
                }
                double result = entero + fract;
                if (signChange)
                {
                    z_aux = -z_aux;
                    result = -result;
                }

                if (wholeinf)
                    return Math.Floor(z_aux);
                else if (wholesup)
                    return Math.Ceiling(z_aux);
                else
                    return Math.Round(result, 6);
            }
            else
                return double.NaN;
        }
        #endregion
        #endregion

        #region Setting Functions
        /// <summary>
        /// Fixes max and min for logarithmic axis
        /// </summary>
        public void DetAxisLimits()
        {
            double epsilon = 0.0000000001;

            if (defineLogAxisFlag_gb == true)
            {
                LogAxisSpacingEnum Sup_el = LogAxisSpacingEnum.OptSup;
                LogAxisSpacingEnum Inf_el = LogAxisSpacingEnum.OptInf;

                if (logWholeRange)
                {
                    Sup_el = LogAxisSpacingEnum.WholeSup;
                    Inf_el = LogAxisSpacingEnum.WholeInf;
                }

                switch (axisStyle)
                {
                    case AxisStyleEnum.Linear:
                        if (Math.Abs(xmin - xmax) < epsilon)
                            xmax = xmin + epsilon;
                        if (Math.Abs(ymin - ymax) < epsilon)
                            ymax = ymin + epsilon;
                        break;

                    case AxisStyleEnum.Logarithmic:
                        xmax = (xmax <= 0) ? 1 : LogOptimized(xmax, Sup_el);
                        xmin = (xmin <= 0) ? 0 : LogOptimized(xmin, Inf_el);
                        ymax = (ymax <= 0) ? 1 : LogOptimized(ymax, Sup_el);
                        ymin = (ymin <= 0) ? 0 : LogOptimized(ymin, Inf_el);

                        if (Math.Abs(xmin - xmax) < epsilon)
                            xmax = xmin + 1;
                        if (Math.Abs(ymin - ymax) < epsilon)
                            ymax = ymin + 1;

                        defineLogAxisFlag_gb = false;
                        break;
                    case AxisStyleEnum.SemiLogX:
                        xmax = (xmax <= 0) ? 1 : LogOptimized(xmax, Sup_el);
                        xmin = (xmin <= 0) ? 0 : LogOptimized(xmin, Inf_el);

                        if (Math.Abs(xmin - xmax) < epsilon)
                            xmax = xmin + 1;
                        if (Math.Abs(ymin - ymax) < epsilon)
                            ymax = ymin + epsilon;

                        defineLogAxisFlag_gb = false;
                        break;
                    case AxisStyleEnum.SemiLogY:
                        ymax = (ymax <= 0) ? 1 : LogOptimized(ymax, Sup_el);
                        ymin = (ymin <= 0) ? 0 : LogOptimized(ymin, Inf_el);

                        if (Math.Abs(xmin - xmax) < epsilon)
                            xmax = xmin + epsilon;
                        if (Math.Abs(ymin - ymax) < epsilon)
                            ymax = ymin + 1;

                        defineLogAxisFlag_gb = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Determines the right offset
        /// </summary>
        public void SetRightOffset()
        {
            TextBlock tb_lo = new TextBlock();
            tb_lo.FontSize = tickLabelSizeX;
            tb_lo.FontWeight = tickLabelWeights;
            Size size_lo;

            if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogY))
                tb_lo.Text = xmax.ToString(gcXTicksFormat);
            else
                tb_lo.Text = base_gs + xmax.ToString(gcXTicksFormat);
            tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            size_lo = tb_lo.DesiredSize;
            rightOffset = size_lo.Width / 2 + 4;

        }

        /// <summary>
        /// Determines the left offset
        /// </summary>
        /// <param name="offset_ln">Sets a extra offset</param>
        public void SetLeftOffset(double offset_ln, TextBlock tbYLabel)
        {
            TextBlock tb_lo = new TextBlock();
            Size size_lo;
            double dy;

            if (gbSetManualLeftOffset && !double.IsNaN(gnManualLeftOffset) && !double.IsInfinity(gnManualLeftOffset) && !double.IsNaN(tbYLabel.ActualHeight))
            {
                bool lbReducingFont = true;
                double lnMaxWidth = 0;
                double lnLabelWidth = tbYLabel.ActualHeight;
                double lnAuxTickLabelSize = tickLabelSizeY;
                while (lbReducingFont)
                {
                    for (dy = ymin; dy <= ymax; dy += yTick)
                    {
                        tb_lo = new TextBlock();
                        tb_lo.FontSize = lnAuxTickLabelSize;
                        tb_lo.FontWeight = tickLabelWeights;
                        if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                            tb_lo.Text = dy.ToString();
                        else
                            tb_lo.Text = base_gs + dy.ToString();
                        tb_lo.TextAlignment = TextAlignment.Right;
                        tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        size_lo = tb_lo.DesiredSize;
                        lnMaxWidth = (size_lo.Width > lnMaxWidth) ? size_lo.Width : lnMaxWidth;
                    }
                    if ((lnLabelWidth + lnMaxWidth) <= gnManualLeftOffset)
                    {
                        lbReducingFont = false;
                        leftOffset = gnManualLeftOffset;
                        tickLabelSizeY = lnAuxTickLabelSize;
                    }
                    else
                    {
                        lnAuxTickLabelSize -= 0.5;
                        lnMaxWidth = 0;
                        if (lnAuxTickLabelSize <= 4)
                        {
                            lbReducingFont = false;
                            leftOffset = gnManualLeftOffset;
                            tickLabelSizeY = lnAuxTickLabelSize;
                        }
                    }
                }
            }
            else // automatic
            {
                for (dy = ymin; dy <= ymax; dy += yTick)
                {
                    tb_lo = new TextBlock();
                    tb_lo.FontSize = tickLabelSize;
                    tb_lo.FontWeight = tickLabelWeights;
                    if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                        tb_lo.Text = dy.ToString(gcYTicksFormat);
                    else
                        tb_lo.Text = base_gs + dy.ToString(gcYTicksFormat);
                    tb_lo.TextAlignment = TextAlignment.Right;
                    tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    size_lo = tb_lo.DesiredSize;
                    if (offset_ln < size_lo.Width)
                        offset_ln = size_lo.Width;
                }
                tbHeight_gn = tb_lo.DesiredSize.Height;
                leftOffset = offset_ln + 15;
            }
        }

        public void SetLeftOffset(double offset_ln)
        {
            TextBlock tb_lo = new TextBlock();
            Size size_lo;
            double dy;

            for (dy = ymin; dy <= ymax; dy += yTick)
            {
                tb_lo = new TextBlock();
                tb_lo.FontSize = tickLabelSizeY;
                tb_lo.FontWeight = tickLabelWeights;
                if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                    tb_lo.Text = dy.ToString();
                else
                    tb_lo.Text = base_gs + dy.ToString();
                tb_lo.TextAlignment = TextAlignment.Right;
                tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size_lo = tb_lo.DesiredSize;
                if (offset_ln < size_lo.Width)
                    offset_ln = size_lo.Width;
            }
            tbHeight_gn = tb_lo.DesiredSize.Height;
            leftOffset = offset_ln + 15;
        }

        private void LeftOffsetManual(int yStart, int yEnd, double yTick, TextBlock tbYLabel)
        {
            if (gbSetManualLeftOffset && !double.IsNaN(gnManualLeftOffset) && !double.IsInfinity(gnManualLeftOffset) && !double.IsNaN(tbYLabel.ActualHeight))
            {
                double dy;
                TextBlock tb;
                Size size;
                bool lbReducingFont = true;
                double lnMaxWidth = 0;
                double lnLabelWidth = tbYLabel.ActualHeight;
                double lnAuxTickLabelSize = tickLabelSizeY;
                while (lbReducingFont)
                {
                    for (int i = yStart; i <= yEnd; i++)
                    {
                        dy = i * yTick;
                        tb = new TextBlock();
                        tb.FontSize = lnAuxTickLabelSize;
                        tb.FontWeight = tickLabelWeights;
                        if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                            tb.Text = dy.ToString();
                        else
                            tb.Text = base_gs + dy.ToString();
                        tb.TextAlignment = TextAlignment.Right;
                        tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        size = tb.DesiredSize;
                        lnMaxWidth = (size.Width > lnMaxWidth) ? size.Width : lnMaxWidth;
                    }
                    if ((lnLabelWidth + lnMaxWidth) <= gnManualLeftOffset)
                    {
                        lbReducingFont = false;
                        leftOffset = gnManualLeftOffset;
                        tickLabelSizeY = lnAuxTickLabelSize;
                    }
                    else
                    {
                        lnAuxTickLabelSize -= 0.5;
                        lnMaxWidth = 0;
                        if (lnAuxTickLabelSize <= 4)
                        {
                            lbReducingFont = false;
                            leftOffset = gnManualLeftOffset;
                            tickLabelSizeY = lnAuxTickLabelSize;
                        }
                    }

                }
            }
        }


        /// <summary>
        /// Sets offsets for chartcanvas
        /// </summary>
        public void SetOffsets()
        {
            Rectangle chartRect;
            Canvas.SetLeft(ChartCanvas, leftOffset);
            Canvas.SetBottom(ChartCanvas, bottomOffset);
            ChartCanvas.Width = Math.Abs(TextCanvas.Width - leftOffset - rightOffset);
            ChartCanvas.Height = Math.Abs(TextCanvas.Height - bottomOffset - tbHeight_gn / 2);
            chartRect = new Rectangle();
            chartRect.Stroke = rectLimitColor_go;
            chartRect.Width = ChartCanvas.Width;
            chartRect.Height = ChartCanvas.Height;
            ChartCanvas.Children.Add(chartRect);
        }


        internal bool AxisIndicator()
        {
            return defineLogAxisFlag_gb;
        }
        #endregion

        #region Grids and Marks
        /// <summary>
        /// Draw the vertical 2nd GridLines
        /// </summary>
        /// <param name="flag">IsY2ndGridLine</param>
        public void Vertical2ndGridLine(bool flag)
        {
            if (flag)
            {
                if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogY))
                {
                    x2ndTick = xTick / 4.0;
                    int delta = (int)(Math.Abs(xmin - gnXTickOrg) / XTick) + 1;

                    for (double dX = gnXTickOrg - xTick * delta; dX < xmax; dX += x2ndTick)
                    {
                        grid2ndline = new Line();
                        grid2ndStyle.AddPattern(grid2ndline);
                        grid2ndline.X1 = Point2D(new Point(dX, Ymin)).X;
                        grid2ndline.Y1 = Point2D(new Point(dX, Ymin)).Y;
                        grid2ndline.X2 = Point2D(new Point(dX, Ymax)).X;
                        grid2ndline.Y2 = Point2D(new Point(dX, Ymax)).Y;
                        ChartCanvas.Children.Add(grid2ndline);
                    }
                }
                else
                {
                    double limMin_ln;
                    double limMax_ln;

                    if (logWholeRange)
                    {
                        limMin_ln = Math.Pow(10, xmin);
                        limMax_ln = Math.Pow(10, xmax);
                    }
                    else
                    {
                        limMin_ln = Math.Pow(10, Math.Floor(xmin));
                        limMax_ln = Math.Pow(10, Math.Ceiling(xmax));
                    }
                    for (double n = limMin_ln; n < limMax_ln; n *= 10)
                    {
                        x2ndTick = n;
                        for (double dX = n + x2ndTick; dX < n * 10; dX += x2ndTick)
                        {
                            grid2ndline = new Line();
                            grid2ndStyle.AddPattern(grid2ndline);
                            grid2ndline.X1 = Point2D(Point2DLogX(new Point(dX, Ymin))).X;
                            grid2ndline.Y1 = Point2D(new Point(dX, Ymin)).Y;
                            grid2ndline.X2 = Point2D(Point2DLogX(new Point(dX, Ymax))).X;
                            grid2ndline.Y2 = Point2D(new Point(dX, Ymax)).Y;
                            ChartCanvas.Children.Add(grid2ndline);
                        }
                    }

                }
            }
        }
        /// <summary>
        /// Draw the horizontal 2nd GridLine
        /// </summary>
        /// <param name="flag">IsX2ndGridLine</param>
        public void Horizontal2ndGridLine(bool flag)
        {
            if (flag)
            {
                if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                {
                    y2ndTick = yTick / 4.0;
                    int delta = (int)(Math.Abs(ymin - gnYTickOrg) / YTick) + 1;

                    for (double dY = gnYTickOrg - YTick * delta; dY < ymax; dY += y2ndTick)
                    {
                        grid2ndline = new Line();
                        grid2ndStyle.AddPattern(grid2ndline);
                        grid2ndline.X1 = Point2D(new Point(Xmin, dY)).X;
                        grid2ndline.Y1 = Point2D(new Point(Xmin, dY)).Y;
                        grid2ndline.X2 = Point2D(new Point(Xmax, dY)).X;
                        grid2ndline.Y2 = Point2D(new Point(Xmax, dY)).Y;
                        ChartCanvas.Children.Add(grid2ndline);
                    }
                }
                else
                {
                    double limMin_ln;
                    double limMax_ln;

                    if (logWholeRange)
                    {
                        limMin_ln = Math.Pow(10, ymin);
                        limMax_ln = Math.Pow(10, ymax);
                    }
                    else
                    {
                        limMin_ln = Math.Pow(10, Math.Floor(ymin));
                        limMax_ln = Math.Pow(10, Math.Ceiling(ymax));
                    }

                    for (double n = limMin_ln; n < limMax_ln; n *= 10)
                    {
                        y2ndTick = n;
                        for (double dY = n + y2ndTick; dY < n * 10; dY += y2ndTick)
                        {
                            grid2ndline = new Line();
                            grid2ndStyle.AddPattern(grid2ndline);
                            grid2ndline.X1 = Point2D(new Point(Xmin, dY)).X;
                            grid2ndline.Y1 = Point2D(Point2DLogY(new Point(Xmin, dY))).Y;
                            grid2ndline.X2 = Point2D(new Point(Xmax, dY)).X;
                            grid2ndline.Y2 = Point2D(Point2DLogY(new Point(Xmax, dY))).Y;
                            ChartCanvas.Children.Add(grid2ndline);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Draw principal  Vertical GridLines
        /// </summary>
        /// <param name="flag">IsYGrid</param>
        public void VerticalGridLine(bool flag)
        {
            if (flag)
            {
                int delta = (int)(Math.Abs(xmin - gnXTickOrg) / XTick) + 1;

                for (double dx = gnXTickOrg - xTick * delta; dx < Xmax; dx += XTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = Point2D(new Point(dx, Ymin)).X;
                    gridline.Y1 = Point2D(new Point(dx, Ymin)).Y;
                    gridline.X2 = Point2D(new Point(dx, Ymax)).X;
                    gridline.Y2 = Point2D(new Point(dx, Ymax)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }
        }
        /// <summary>
        /// Draw principal Horizontal GridLines
        /// </summary>
        /// <param name="flag">IsXGrid</param>
        public void HorizontalGridLine(bool flag)
        {
            if (flag)
            {
                int delta = (int)(Math.Abs(ymin - gnYTickOrg) / YTick) + 1;

                for (double dy = gnYTickOrg - YTick * delta; dy <= Ymax; dy += YTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = Point2D(new Point(Xmin, dy)).X;

                    gridline.Y1 = Point2D(new Point(Xmin, dy)).Y;
                    gridline.X2 = Point2D(new Point(Xmax, dy)).X;
                    gridline.Y2 = Point2D(new Point(Xmax, dy)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }
        }
        /// <summary>
        /// Made tick marks and labels in X-Axis
        /// </summary>
        public void TickMarksX()
        {
            if (showTickX_gb)
            {
                Point pt = new Point();
                Line tick = new Line();
                Run run = new Run();
                TextBlock tb_lo = new TextBlock();
                double dxAux_ln;

                Size size_lo;
                int delta = (int)(Math.Abs(xmin - gnXTickOrg) / XTick);

                for (double dx = gnXTickOrg - xTick * delta; dx <= Xmax; dx += xTick)
                {
                    switch (AxisAlignment)
                    {
                        case (AxisAlignmentEnum.XY):
                            pt = Point2D(new Point(dx, Ymin));
                            break;
                        case AxisAlignmentEnum.XinY:
                            pt = Point2D(new Point(dx, Ymin));
                            break;
                        case AxisAlignmentEnum.XYin:
                            pt = Point2DYin(new Point(dx, Ymin));
                            break;
                        case AxisAlignmentEnum.XinYin:
                            pt = Point2DXinYin(new Point(dx, Ymin));
                            break;
                    }
                    tick = new Line();
                    tick.Stroke = Brushes.Black;
                    tick.StrokeThickness = 3;
                    tick.X1 = pt.X;
                    tick.Y1 = pt.Y;
                    tick.X2 = pt.X;
                    tick.Y2 = pt.Y - 5;
                    ChartCanvas.Children.Add(tick);

                    tb_lo = new TextBlock();
                    tb_lo.FontSize = tickLabelSizeX;
                    tb_lo.FontWeight = tickLabelWeights;
                    tb_lo.Background = Brushes.Transparent;
                    tb_lo.Foreground = tickLabelColor;

                    string dxString_ls = dx.ToString(gcXTicksFormat);
                    int nChars = dxString_ls.Length;
                    if (nChars > 6)
                        dxAux_ln = Math.Round(dx, 5);
                    else
                        dxAux_ln = dx;


                    if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogY))
                    {
                        tb_lo.Text = dxAux_ln.ToString((gcXTicksFormat));
                        tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        size_lo = tb_lo.DesiredSize;
                        TextCanvas.Children.Add(tb_lo);
                        Canvas.SetLeft(tb_lo, leftOffset + pt.X - size_lo.Width / 2);
                        Canvas.SetTop(tb_lo, pt.Y + 2 + size_lo.Height / 2);
                    }
                    else
                    {
                        if (!ShowShortLabelX)
                        {
                            tb_lo.Text = Math.Pow(10, dxAux_ln).ToString();
                            tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                            size_lo = tb_lo.DesiredSize;
                            TextCanvas.Children.Add(tb_lo);
                            Canvas.SetLeft(tb_lo, leftOffset + pt.X - size_lo.Width / 2);
                            Canvas.SetTop(tb_lo, pt.Y + 2 + size_lo.Height / 2);
                        }
                        else
                        {
                            run = new Run();
                            run.Text = Convert.ToString(dxAux_ln);
                            run.FontSize = 10;
                            run.FontFamily = new FontFamily("Courier New");
                            run.BaselineAlignment = BaselineAlignment.Superscript;

                            tb_lo.Text = base_gs;
                            tb_lo.Inlines.Add(run);
                            tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                            size_lo = tb_lo.DesiredSize;
                            TextCanvas.Children.Add(tb_lo);

                            Canvas.SetLeft(tb_lo, leftOffset + pt.X - size_lo.Width / 2);
                            Canvas.SetTop(tb_lo, pt.Y + 2 + size_lo.Height / 6 + 1);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Made tick marks and labels in Y-Axis
        /// </summary>
        public void TickMarksY()
        {
            if (showTickY_gb)
            {
                Point pt = new Point();
                Line tick;
                Run run;
                TextBlock tb_lo;
                Size size_lo;
                double dyAux_ln;
                int delta = (int)(Math.Abs(ymin - gnYTickOrg) / YTick);

                for (double dy = gnYTickOrg - YTick * delta; dy <= Ymax; dy += YTick)
                {
                    switch (AxisAlignment)
                    {
                        case (AxisAlignmentEnum.XY):
                            pt = Point2D(new Point(Xmin, dy));
                            break;
                        case AxisAlignmentEnum.XinY:
                            pt = Point2DYin(new Point(Xmin, dy));
                            break;
                        case AxisAlignmentEnum.XYin:
                            pt = Point2D(new Point(Xmin, dy));
                            break;
                        case AxisAlignmentEnum.XinYin:
                            pt = Point2DXin(new Point(Xmin, dy));
                            break;
                    }
                    tick = new Line();
                    tick.Stroke = Brushes.Black;
                    tick.StrokeThickness = 3;
                    tick.X1 = pt.X;
                    tick.Y1 = pt.Y;
                    tick.X2 = pt.X + 5;
                    tick.Y2 = pt.Y;
                    ChartCanvas.Children.Add(tick);

                    tb_lo = new TextBlock();
                    tb_lo.FontSize = tickLabelSizeY;
                    tb_lo.FontWeight = tickLabelWeights;
                    tb_lo.TextAlignment = TextAlignment.Right;
                    tb_lo.Background = Brushes.Transparent;
                    tb_lo.Foreground = tickLabelColor;

                    string dyString_ls = dy.ToString(gcYTicksFormat);
                    int nChars = dyString_ls.Length;
                    if (nChars > 6)
                        dyAux_ln = Math.Round(dy, 6);
                    else
                        dyAux_ln = dy;

                    if ((axisStyle == AxisStyleEnum.Linear) || (axisStyle == AxisStyleEnum.SemiLogX))
                    {
                        tb_lo.Text = dyAux_ln.ToString((gcYTicksFormat));
                        tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                    }
                    else
                    {
                        if (!ShowShortLabelY)
                        {
                            tb_lo.Text = Math.Pow(10, dyAux_ln).ToString();
                            tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        }
                        else
                        {
                            run = new Run();
                            run.Text = Convert.ToString(dyAux_ln);
                            run.FontFamily = new FontFamily("Courier New");
                            run.FontSize = 10;
                            run.Typography.Variants = FontVariants.Subscript;
                            run.BaselineAlignment = BaselineAlignment.Superscript;

                            tb_lo.Text = base_gs;
                            tb_lo.Inlines.Add(run);
                            tb_lo.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                        }
                    }

                    size_lo = tb_lo.DesiredSize;
                    TextCanvas.Children.Add(tb_lo);
                    Canvas.SetRight(tb_lo, ChartCanvas.Width + rightOffset + 3);
                    Canvas.SetTop(tb_lo, pt.Y);
                }
            }
        }

        #endregion

        #region calculos de límites y origen

        /// <summary>
        /// Establece el valor de gnYTickOrg, 
        /// el primer Tick que se va a dibujar y etiquetar en el eje Y
        /// </summary>
        private void GetYTickBegin()
        {
            if (AxisStyle == ChartStyle.AxisStyleEnum.Linear || AxisStyle == ChartStyle.AxisStyleEnum.SemiLogX)
            {
                double Rango = Math.Abs(ymax - ymin);
                int Potencia = getExponente(Rango);
                double LimSup = LimiteInferior(ymin, Potencia);

                gnYTickOrg = LimSup;
            }
            if (AxisStyle == ChartStyle.AxisStyleEnum.Logarithmic || AxisStyle == ChartStyle.AxisStyleEnum.SemiLogY)
            {
                gnYTickOrg = (int)ymin;
            }
        }

        /// <summary>
        /// Establece el valor de gnXTickOrg, 
        /// el primer Tick que se va a dibujar y etiquetar en el eje X
        /// </summary>
        private void GetXTickBegin()
        {
            if (AxisStyle == ChartStyle.AxisStyleEnum.Linear || AxisStyle == ChartStyle.AxisStyleEnum.SemiLogY)
            {
                double Rango = Math.Abs(xmax - xmin);
                int Potencia = getExponente(Rango);
                double LimInf = LimiteInferior(xmin, Potencia);

                gnXTickOrg = LimInf;
            }
            if (AxisStyle == ChartStyle.AxisStyleEnum.Logarithmic || AxisStyle == ChartStyle.AxisStyleEnum.SemiLogX)
            {
                gnXTickOrg = (int)xmin;
            }
        }

        /// <summary>
        /// Obtiene el máximo valor entero n, que satisface la ecuación:
        /// | A*10^n - rango | > 0
        /// con A un valor entero positivo menor a 10.
        /// </summary>
        /// <param name="rango"> Valor entero al que se le aplica el algoritmo </param>
        /// <returns> El valor n que satisface la ecuación </returns>
        internal static int getExponente(double rango)
        {
            if (rango == 0)
                rango = double.Epsilon;
            int exponente = 0; // 10 ^ 0 = 1

            double aux;
            if (rango < 1) // Buscar potencias menores a 1:
            {
                while (true)
                {
                    aux = rango / Math.Pow(10, exponente);

                    if (double.IsInfinity(aux))
                    {
                        return -30;
                        //throw new Exception("Range too small to draw");
                    }

                    if (aux >= 1 && aux < 10)
                        break;
                    exponente--;
                }
            }
            else
            {
                // Buscar potencias mayores a 1:
                while (true)
                {
                    if (rango / Math.Pow(10, exponente) < 10)
                        break;
                    exponente++;
                }
            }

            return exponente;
        }

        /// <summary>
        /// Dado el entero obtenido de getExponente() = n
        /// Redonde Min a la potencia de 10^n más cercana a la derecha
        /// </summary>
        /// <param name="Min"> Valor que se quiere redondear </param>
        /// <param name="Potencia"> El valor obtenido de getExponente() </param>
        /// <returns> El valor del límite inferior que se va a dibujar </returns>
        internal static double LimiteInferior(double Min, double Potencia)
        {
            double p = Math.Pow(10, Potencia);
            Min /= p;

            if (Min < 0)
            {
                Min = -Min;
                // redondear Max a la potencia de 10 más cercana a la izquierda
                Min = Math.Floor(Min);
                return -Min * p;
            }
            else
            {
                // redondear Max a la potencia de 10 más cercana a la derecha
                Min = Math.Ceiling(Min);
                return Min * p;
            }
        }

        /// <summary>
        /// Dado el entero obtenido de getExponente() = n
        /// Redonde Max a la potencia de 10^n más cercana a la izquierda
        /// </summary>
        /// <param name="Max"> Valor que se quiere redondear </param>
        /// <param name="Potencia"> El valor obtenido de getExponente() </param>
        /// <returns> El valor del límite superior que se va a dibujar </returns>
        internal static double LimiteSuperior(double Max, double Potencia)
        {
            double p = Math.Pow(10, Potencia);
            Max /= p;

            if (Max < 0)
            {
                Max = -Max;
                // redondear Max a la potencia de 10 más cercana a la derecha
                Max = Math.Ceiling(Max);
                return -Max * p;
            }
            else
            {
                // redondear Max a la potencia de 10 más cercana a la izquierda
                Max = Math.Floor(Max);
                return Max * p;
            }
        }

        #endregion


        /*This function finds the optimal tick value. When all_lb is false the tick
         * value will be a multiple of 5, this is used for logarithms. 
         * First step is to get the dimension of the number, this means, if it is 
         * in the interval [0.1, 1), or [1, 10), or [10, 100), etc. ...         
         */
        //Function for AutomaticTick
        internal double OptimalSpacing(double original, bool all_lb)
        {
            double org_ln;
            bool flag_lb = true;
            if (original < 0)
            {
                org_ln = -original;
                flag_lb = false;
            }
            else
                org_ln = original;
            double[] da;
            if (all_lb)
            {
                da = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0 };
            }
            else
                da = new double[1] { 5.0 };
            double multiplier = Math.Pow(10,
                                             Math.Floor(Math.Log(org_ln) / Math.Log(10)));
            double dmin = 100 * multiplier;
            double spacing = 0.0;
            double mn = 100;

            foreach (double d in da)
            {
                double delta = Math.Abs(org_ln - d * multiplier);
                if (delta < dmin)
                {
                    dmin = delta;
                    spacing = d * multiplier;
                }
                if (d < mn)
                {
                    mn = d;
                }
            }

            if (Math.Abs(org_ln - 10 * mn * multiplier) < Math.Abs(org_ln - spacing))
                spacing = 10 * mn * multiplier;
            if (flag_lb)
            {
                return spacing;
            }
            else
                return -spacing;

        }

        internal double OptimalSpacing(double original)
        {
            return original;

            double org_ln;
            bool flag_lb = true;
            bool flag1_lb = true;
            if (original < 0)
            {
                org_ln = -original;
                flag_lb = false;
            }
            else
                org_ln = original;
            double[] da;
            da = new double[]
                {   1.0, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9,
                    2.0, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8, 2.9,
                    3.0, 3.1, 3.2, 3.3, 3.4, 3.5, 3.6, 3.7, 3.8, 3.9,
                    4.0, 4.1, 4.2, 4.3, 4.4, 4.5, 4.6, 4.7, 4.8, 4.9,
                    5.0, 5.1, 5.2, 5.3, 5.4, 5.5, 5.6, 5.7, 5.8, 5.9,
                    6.0, 6.1, 6.2, 6.3, 6.4, 6.5, 6.6, 6.7, 6.8, 6.9,
                    7.0, 7.1, 7.2, 7.3, 7.4, 7.5, 7.6, 7.7, 7.8, 7.9,
                    8.0, 8.1, 8.2, 8.3, 8.4, 8.5, 8.6, 8.7, 8.8, 8.9,
                    9.0, 9.1, 9.2, 9.3, 9.4, 9.5, 9.6, 9.7, 9.8, 9.9};

            double multiplier = Math.Pow(10,
                Math.Floor(Math.Log10(org_ln)));
            double dmin = 100 * multiplier;
            double spacing = 0.0;
            double mn = 100;

            int n = da.Length;

            for (int i = 0; i < n; i++)
            {
                double delta = Math.Abs(org_ln - da[i] * multiplier);
                if (delta == 0)
                {
                    spacing = da[i] * multiplier;
                    flag1_lb = false;
                }
                else if (flag1_lb)
                {
                    if (delta < dmin)
                    {
                        dmin = delta;
                        spacing = da[i] * multiplier;
                    }
                    if (da[i] < mn)
                    {
                        mn = da[i];
                    }
                }
            }

            double aux1 = Math.Abs(org_ln - 10 * mn * multiplier);
            double aux2 = Math.Abs(org_ln - spacing);

            if (aux1 < aux2)
                spacing = 10 * mn * multiplier;
            if (flag_lb)
            {
                return spacing;
            }
            else
                return -spacing;

        }

        public void ResetAxisAligment()
        {
            defineLogAxisFlag_gb = true;
        }

        #endregion

        #region Enumerations
        //AxisAlignment List
        public enum AxisAlignmentEnum
        {
            XY = 1,
            XinY = 2,
            XinYin = 3,
            XYin = 4
        }

        public enum GridLinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4
        }

        public enum AxisStyleEnum
        {
            Linear = 1,
            Logarithmic = 2,
            SemiLogX = 3,
            SemiLogY = 4
        }

        /// <summary>
        /// Actions for LogOptimized method
        /// </summary>
        public enum LogAxisSpacingEnum
        {
            OptSup = 1,
            OptInf = 2,
            WholeSup = 3,
            WholeInf = 4
        }
        #endregion
    }
}
