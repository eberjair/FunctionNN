using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;


namespace FunctionNeuralNetwork.Charts
{
    public class DataSeries : ICloneable
    {
        #region Attributes

        #region Data Attributes
        private PointCollection dataPoints = new PointCollection();
        private double xMaxSerie;
        private double xMinSerie;
        private double yMaxSerie;
        private double yMinSerie;
        private double xLogMin;
        private double yLogMin;
        private string serieName = "Default Name";
        private bool isY2Data = false;
        private bool isLegend = true;
        private Symbols symbols;
        private bool isRight = true; //pbo Stiff graph parameter
        #endregion

        #region PlotStyle Attribute
        private PlotStyleEnum plotStyle = PlotStyleEnum.Lines;
        #endregion

        #region Line Attributes
        private Polyline lineSeries = new Polyline();
        private Brush lineColor = Brushes.Blue;
        private double lineThickness = 1;
        private LinePatternEnum linePattern;
        #endregion

        #region Areas Attributes
        private Polygon areaSeries = new Polygon();
        private Brush fillColor = Brushes.Blue;
        private Brush borderColor = Brushes.Black;
        private double borderThickness = 1.0;
        private double barWidth = 0.8;
        private double areaOrigin = 0.0;
        private double areaOpacity = 0.80;
        #endregion

        #region ErrorLines Attributes
        private bool isErrorLines = false;
        private List<double> errorToPoint = new List<double>();
        private Brush errosLineColor = Brushes.Red;
        private double errorLineThickness = 1;
        private LinePatternEnum errorLinePattern = LinePatternEnum.Solid;
        private ErrorTypeEnum errorType = ErrorTypeEnum.ErrorY;
        #endregion

        #endregion

        #region Properties

        #region Data Properties
        public PointCollection Datapoints
        {
            get { return dataPoints; }
            set { dataPoints = value; }
        }
        public double XMaxSerie
        { get { return xMaxSerie; } }
        public double XMinSerie
        { get { return xMinSerie; } }
        public double YMaxSerie
        { get { return yMaxSerie; } }
        public double YMinSerie
        { get { return yMinSerie; } }
        public double XLogMin
        { get { return xLogMin; } }
        public double YLogMin
        { get { return yLogMin; } }
        public string SeriesName
        {
            get { return serieName; }
            set { serieName = value; }
        }
        public bool IsY2Data
        {
            get { return isY2Data; }
            set { isY2Data = value; }
        }
        public bool IsLegend
        {
            get { return isLegend; }
            set { isLegend = value; }
        }
        public Symbols Symbols
        {
            get { return symbols; }
            set { symbols = value; }
        }
        public bool IsRight //pbo
        {
            get { return isRight; }
            set { isRight = value; }
        }
        #endregion

        #region PlotStyle Propertie
        public PlotStyleEnum PlotStyle
        {
            get { return plotStyle; }
            set { plotStyle = value; }
        }
        #endregion

        #region Line Properties
        internal Polyline LineSeries
        {
            get { return lineSeries; }
            set { lineSeries = value; }
        }
        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }
        public double LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }
        public LinePatternEnum LinePattern
        {
            get { return linePattern; }
            set { linePattern = value; }
        }
        #endregion

        #region Areas Properties
        internal Polygon AreaSeries
        {
            get { return areaSeries; }
            set { areaSeries = value; }
        }
        public Brush FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }
        public Brush BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        public double BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }
        public double BarWidth
        {
            get { return barWidth; }
            set { barWidth = value; }
        }
        public double AreaOrigin
        {
            get { return areaOrigin; }
            set { areaOrigin = value; }
        }
        public double AreaOpacity
        {
            get { return areaOpacity; }
            set { areaOpacity = value; }
        }
        #endregion

        #region ErrorLine Properties
        public bool IsErrorLines
        {
            get { return isErrorLines; }
            set { isErrorLines = value; }
        }
        public List<double> ErrorToPoint
        {
            get { return errorToPoint; }
            set
            {
                if (isErrorLines)
                {
                    errorToPoint = value;
                }
            }
        }
        public Brush ErrorLineColor
        {
            get { return errosLineColor; }
            set
            {
                if (isErrorLines)
                {
                    errosLineColor = value;
                }
            }
        }
        public double ErrorLineThickness
        {
            get { return errorLineThickness; }
            set
            {
                if (isErrorLines)
                {
                    errorLineThickness = value;
                }
            }
        }
        public LinePatternEnum ErrorLinePattern
        {
            get { return errorLinePattern; }
            set
            {
                if (isErrorLines)
                {
                    errorLinePattern = value;
                }
            }
        }
        public ErrorTypeEnum ErrorType
        {
            get { return errorType; }
            set
            {
                if (isErrorLines)
                {
                    errorType = value;
                }
            }
        }
        #endregion

        #endregion

        #region Constructor
        public DataSeries()
        {
            symbols = new Symbols();
        }


        #endregion

        #region Methods
        internal void AddLinePattern()
        {
            LineSeries.Stroke = LineColor;
            LineSeries.StrokeThickness = LineThickness;
            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    LineSeries.StrokeDashArray =
                        new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    LineSeries.StrokeDashArray =
                        new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    LineSeries.StrokeDashArray =
                        new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    LineSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        internal void AddAreaPattern()
        {
            AreaSeries.Stroke = BorderColor;
            AreaSeries.StrokeThickness = BorderThickness;
            AreaSeries.Fill = FillColor;
            AreaSeries.Opacity = areaOpacity;

            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    AreaSeries.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    AreaSeries.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    AreaSeries.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    AreaSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        internal void AddErrorPattern(Line errorLine)
        {
            errorLine.Stroke = errosLineColor;
            errorLine.StrokeThickness = errorLineThickness;
            switch (errorLinePattern)
            {
                case LinePatternEnum.Dash:
                    errorLine.StrokeDashArray =
                        new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    errorLine.StrokeDashArray =
                        new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    errorLine.StrokeDashArray =
                        new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    errorLine.Stroke = Brushes.Transparent;
                    break;
            }
        }

        internal void SetMaxMin(double value, SetXY place)
        {
            switch (place)
            {
                case SetXY.XMax:
                    xMaxSerie = value;
                    break;
                case SetXY.XMin:
                    xMinSerie = value;
                    break;
                case SetXY.YMax:
                    yMaxSerie = value;
                    break;
                case SetXY.YMin:
                    yMinSerie = value;
                    break;
                case SetXY.XLogMin:
                    xLogMin = value;
                    break;
                case SetXY.YLogMin:
                    yLogMin = value;
                    break;
                default:
                    break;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal void SavePoints()
        {
            foreach (Point pt in lineSeries.Points)
            {
                dataPoints.Add(pt);
            }
        }

        internal void PointsToPlot()
        {
            lineSeries.Points.Clear();
            foreach (Point pt in dataPoints)
            {
                Point pt_ln = new Point();
                pt_ln.X = pt.X;
                pt_ln.Y = pt.Y;
                lineSeries.Points.Add(pt_ln);
            }
        }
        internal void PointstoArea()
        {
            areaSeries.Points.Clear();
            foreach (Point pt in dataPoints)
            {
                Point pt_ln = new Point();
                pt_ln.X = pt.X;
                pt_ln.Y = pt.Y;
                areaSeries.Points.Add(pt_ln);
            }
        }


        internal void AddErrorLine(ChartStyle cs, int ind, double lineLength)
        {
            if (isErrorLines)
            {
                Point ptData = dataPoints[ind];
                double error = errorToPoint[ind];
                Point ptStart, ptEnd;
                Line errorLine;
                switch (errorType)
                {
                    case ErrorTypeEnum.ErrorX:
                        ptStart = new Point(ptData.X + error, ptData.Y);
                        ptEnd = new Point(ptData.X - error, ptData.Y);
                        #region Validate and Maping
                        switch (cs.AxisStyle)
                        {
                            case ChartStyle.AxisStyleEnum.Linear:
                                ptStart = cs.Point2D(ptStart);
                                ptEnd = cs.Point2D(ptEnd);
                                break;
                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                if (ValidPoint(ptStart, cs))
                                    ptStart = cs.Point2D(cs.Point2DLogLog(ptStart));
                                else
                                    ptStart = cs.Point2D(cs.Point2DLogLog(ptData));
                                if (ValidPoint(ptEnd, cs))
                                    ptEnd = cs.Point2D(cs.Point2DLogLog(ptEnd));
                                else
                                    ptEnd = cs.Point2D(cs.Point2DLogLog(ptData));
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                if (ValidPoint(ptStart, cs))
                                    ptStart = cs.Point2D(cs.Point2DLogX(ptStart));
                                else
                                    ptStart = cs.Point2D(cs.Point2DLogX(ptData));
                                if (ValidPoint(ptEnd, cs))
                                    ptEnd = cs.Point2D(cs.Point2DLogX(ptEnd));
                                else
                                    ptEnd = cs.Point2D(cs.Point2DLogX(ptData));
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                if (ValidPoint(ptStart, cs))
                                    ptStart = cs.Point2D(cs.Point2DLogY(ptStart));
                                else
                                    ptStart = cs.Point2D(cs.Point2DLogY(ptData));
                                if (ValidPoint(ptEnd, cs))
                                    ptEnd = cs.Point2D(cs.Point2DLogY(ptEnd));
                                else
                                    ptEnd = cs.Point2D(cs.Point2DLogY(ptData));
                                break;
                        }
                        #endregion
                        #region Draw ErrorLine
                        errorLine = new Line();
                        AddErrorPattern(errorLine);
                        errorLine.X1 = ptStart.X;
                        errorLine.Y1 = ptStart.Y;
                        errorLine.X2 = ptEnd.X;
                        errorLine.Y2 = ptEnd.Y;
                        cs.ChartCanvas.Children.Add(errorLine);
                        errorLine = new Line();
                        AddErrorPattern(errorLine);
                        errorLine.X1 = ptStart.X;
                        errorLine.Y1 = ptStart.Y - 0.5 * lineLength;
                        errorLine.X2 = ptStart.X;
                        errorLine.Y2 = ptStart.Y + 0.5 * lineLength;
                        cs.ChartCanvas.Children.Add(errorLine);
                        errorLine = new Line();
                        AddErrorPattern(errorLine);
                        errorLine.X1 = ptEnd.X;
                        errorLine.Y1 = ptEnd.Y - 0.5 * lineLength;
                        errorLine.X2 = ptEnd.X;
                        errorLine.Y2 = ptEnd.Y + 0.5 * lineLength;
                        cs.ChartCanvas.Children.Add(errorLine);
                        #endregion
                        break;
                    case ErrorTypeEnum.ErrorY:
                        ptStart = new Point(ptData.X, ptData.Y + error);
                        ptEnd = new Point(ptData.X, ptData.Y - error);
                        #region Validate and Maping
                        switch (cs.AxisStyle)
                        {
                            case ChartStyle.AxisStyleEnum.Linear:
                                ptStart = cs.Point2D(ptStart);
                                ptEnd = cs.Point2D(ptEnd);
                                break;
                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                if (ValidPoint(ptStart, cs))
                                    ptStart = cs.Point2D(cs.Point2DLogLog(ptStart));
                                else
                                    ptStart = cs.Point2D(cs.Point2DLogLog(ptData));
                                if (ValidPoint(ptEnd, cs))
                                    ptEnd = cs.Point2D(cs.Point2DLogLog(ptEnd));
                                else
                                    ptEnd = cs.Point2D(cs.Point2DLogLog(ptData));
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                if (ValidPoint(ptStart, cs))
                                    ptStart = cs.Point2D(cs.Point2DLogX(ptStart));
                                else
                                    ptStart = cs.Point2D(cs.Point2DLogX(ptData));
                                if (ValidPoint(ptEnd, cs))
                                    ptEnd = cs.Point2D(cs.Point2DLogX(ptEnd));
                                else
                                    ptEnd = cs.Point2D(cs.Point2DLogX(ptData));
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                if (ValidPoint(ptStart, cs))
                                    ptStart = cs.Point2D(cs.Point2DLogY(ptStart));
                                else
                                    ptStart = cs.Point2D(cs.Point2DLogY(ptData));
                                if (ValidPoint(ptEnd, cs))
                                    ptEnd = cs.Point2D(cs.Point2DLogY(ptEnd));
                                else
                                    ptEnd = cs.Point2D(cs.Point2DLogY(ptData));
                                break;
                        }
                        #endregion
                        #region Draw ErrorLine
                        errorLine = new Line();
                        AddErrorPattern(errorLine);
                        errorLine.X1 = ptStart.X;
                        errorLine.Y1 = ptStart.Y;
                        errorLine.X2 = ptEnd.X;
                        errorLine.Y2 = ptEnd.Y;
                        cs.ChartCanvas.Children.Add(errorLine);
                        errorLine = new Line();
                        AddErrorPattern(errorLine);
                        errorLine.X1 = ptStart.X - 0.5 * lineLength;
                        errorLine.Y1 = ptStart.Y;
                        errorLine.X2 = ptStart.X + 0.5 * lineLength;
                        errorLine.Y2 = ptStart.Y;
                        cs.ChartCanvas.Children.Add(errorLine);
                        errorLine = new Line();
                        AddErrorPattern(errorLine);
                        errorLine.X1 = ptEnd.X - 0.5 * lineLength;
                        errorLine.Y1 = ptEnd.Y;
                        errorLine.X2 = ptEnd.X + 0.5 * lineLength;
                        errorLine.Y2 = ptEnd.Y;
                        cs.ChartCanvas.Children.Add(errorLine);
                        #endregion
                        break;
                }
            }
        }

        internal void ValidErrorList()
        {
            if (isErrorLines)
            {
                int ndata = dataPoints.Count;
                int nerror = errorToPoint.Count;

                if (ndata > nerror)
                {
                    for (int i = nerror; i < ndata; i++)
                    {
                        errorToPoint.Add(0);
                    }
                }
            }
        }
        internal double GetErrorLineLegth(ChartStyle cs)
        {
            double lineLength = 1;
            int ndata = dataPoints.Count;
            if (ndata >= 2)
            {
                switch (errorType)
                {
                    case ErrorTypeEnum.ErrorX:
                        switch (cs.AxisStyle)
                        {
                            case ChartStyle.AxisStyleEnum.Linear:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).Y -
                                    cs.Point2D(dataPoints[0]).Y) / 3;
                                break;
                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).Y -
                                    cs.Point2D(dataPoints[0]).Y) / 5;
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).Y -
                                    cs.Point2D(dataPoints[0]).Y) / 3;
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).Y -
                                    cs.Point2D(dataPoints[0]).Y) / 5;
                                break;
                        }
                        break;
                    case ErrorTypeEnum.ErrorY:
                        switch (cs.AxisStyle)
                        {
                            case ChartStyle.AxisStyleEnum.Linear:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).X -
                                    cs.Point2D(dataPoints[0]).X) / 3;
                                break;
                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).X -
                                    cs.Point2D(dataPoints[0]).X) / 5;
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).X -
                                    cs.Point2D(dataPoints[0]).X) / 5;
                                break;
                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                lineLength = Math.Abs(cs.Point2D(dataPoints[1]).X -
                                    cs.Point2D(dataPoints[0]).X) / 3;
                                break;
                        }
                        break;
                }
            }
            else
            {
                Point pt = new Point(10, 10);
                lineLength = cs.Point2D(pt).X / 3;
            }
            return lineLength;
        }
        private double DefineError(ChartStyle cs, int ind)
        {
            double error_aux = errorToPoint[ind];
            if (error_aux < 0)
            {
                error_aux = Math.Abs(error_aux);
            }
            switch (ErrorType)
            {
                case DataSeries.ErrorTypeEnum.ErrorX:
                    switch (cs.AxisStyle)
                    {
                        case ChartStyle.AxisStyleEnum.Linear:
                            break;
                        case ChartStyle.AxisStyleEnum.Logarithmic:
                            if (error_aux != 0)
                                error_aux = Math.Log10(error_aux);
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogX:
                            if (error_aux != 0)
                                error_aux = Math.Log10(error_aux);
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogY:
                            break;
                    }
                    break;
                case DataSeries.ErrorTypeEnum.ErrorY:
                    switch (cs.AxisStyle)
                    {
                        case ChartStyle.AxisStyleEnum.Linear:
                            break;
                        case ChartStyle.AxisStyleEnum.Logarithmic:
                            if (error_aux != 0)
                                error_aux = Math.Log10(error_aux);
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogX:
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogY:
                            if (error_aux != 0)
                                error_aux = Math.Log10(error_aux);
                            break;
                    }
                    break;
            }
            return error_aux;
        }
        private bool ValidPoint(Point pt, ChartStyle cs)
        {
            Point ptLog;
            bool valid = true;
            switch (cs.AxisStyle)
            {
                case ChartStyle.AxisStyleEnum.Linear:
                    break;
                case ChartStyle.AxisStyleEnum.Logarithmic:
                    ptLog = cs.Point2DLogLog(pt);
                    if (double.IsInfinity(ptLog.X) || double.IsNaN(ptLog.X) ||
                        double.IsInfinity(ptLog.Y) || double.IsNaN(ptLog.Y))
                        valid = false;
                    else
                        valid = true;
                    break;
                case ChartStyle.AxisStyleEnum.SemiLogX:
                    ptLog = cs.Point2DLogX(pt);
                    if (double.IsInfinity(ptLog.X) || double.IsNaN(ptLog.X))
                        valid = false;
                    else
                        valid = true;
                    break;
                case ChartStyle.AxisStyleEnum.SemiLogY:
                    ptLog = cs.Point2DLogY(pt);
                    if (double.IsInfinity(ptLog.Y) || double.IsNaN(ptLog.Y))
                        valid = false;
                    else
                        valid = true;
                    break;
            }
            return valid;
        }
        #endregion

        #region Enumerations
        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }

        public enum PlotStyleEnum
        {
            Lines = 1,
            Bars = 2,
            Stairs = 3,
            Stem = 4,
            Area = 5,
            Points = 6,
            FastPoints = 7
        }

        public enum SetXY
        {
            XMax = 1,
            XMin = 2,
            YMax = 3,
            YMin = 4,
            XLogMin = 5,
            YLogMin = 6
        }

        public enum ErrorTypeEnum
        {
            ErrorX = 1,
            ErrorY = 2
        }
        #endregion


    }
}
