using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace FunctionNeuralNetwork.Charts
{
    public class Legend
    {
        #region Attributes
        private bool isLegend;
        private bool isBorder;
        private Canvas legendCanvas;
        private LegendPositionEnum legendPosition;
        private Brush borderColor = Brushes.Black;
        private Brush fillcolor = Brushes.White;
        private Brush textColor = Brushes.Black;
        //private Brush textBackGround = Brushes.Transparent;
        #endregion


        #region Constructor
        public Legend()
        {
            isLegend = false;
            isBorder = true;
            legendPosition = LegendPositionEnum.NorthEast;
        }
        #endregion

        #region Properties
        public LegendPositionEnum LegendPosition
        {
            get { return legendPosition; }
            set { legendPosition = value; }
        }
        public Canvas LegendCanvas
        {
            get { return legendCanvas; }
            set { legendCanvas = value; }
        }
        public bool IsLegend
        {
            get { return isLegend; }
            set { isLegend = value; }
        }
        public bool IsBorder
        {
            get { return isBorder; }
            set { isBorder = value; }
        }
        public Brush BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        public Brush FillColor
        {
            get { return fillcolor; }
            set { fillcolor = value; }
        }
        public Brush Textcolor
        {
            get { return textColor; }
            set { textColor = value; }
        }
        #endregion

        #region Enumerations
        public enum LegendPositionEnum
        {
            North,
            NorthWest,
            West,
            SouthWest,
            South,
            SouthEast,
            East,
            NorthEast
        }
        #endregion

        #region Methods
        public void AddLegend(Canvas canvas, DataCollection dc)
        {
            TextBlock tb = new TextBlock();
            //Se guardan los nombres de cada serie en legendLabels
            if (dc.DataList.Count < 1 || !IsLegend)
                return;
            int n = 0;
            string[] legendLabels = new string[dc.DataList.Count];
            foreach (DataSeries ds in dc.DataList)
            {
                if (ds.IsLegend)
                {
                    legendLabels[n] = ds.SeriesName;
                    n++;
                }
            }

            //Se mide la longitud máxima del texto y se guarda en legendWidth
            double legendWidth = 0;
            Size size = new Size(0, 0);
            for (int i = 0; i < legendLabels.Length; i++)
            {
                tb = new TextBlock();
                tb.Text = legendLabels[i];
                tb.Measure(new Size(Double.PositiveInfinity,
                    Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (legendWidth < size.Width)
                    legendWidth = size.Width;
            }

            legendWidth += 50;
            legendCanvas.Width = legendWidth + 5;

            double legendHeight = 0;
            foreach (DataSeries ds in dc.DataList)
            {
                if (ds.IsLegend)
                    legendHeight += 17;
            }
            // legendHeight = 17 * dc.DataList.Count;

            double sx = 6;
            double sy = 0;
            double textHeight = size.Height;
            double lineLength = 34;
            Rectangle legendRect = new Rectangle();
            legendRect.Stroke = borderColor;
            legendRect.Fill = fillcolor;
            legendRect.Width = legendWidth;
            legendRect.Height = legendHeight;
            if (IsLegend && IsBorder)
                LegendCanvas.Children.Add(legendRect);
            Canvas.SetZIndex(LegendCanvas, 10);

            n = 1;
            double xText;
            double yText;
            Line line;
            foreach (DataSeries ds in dc.DataList)
            {
                if (ds.IsLegend)
                {

                    tb = new TextBlock();
                    tb.Text = ds.SeriesName;
                    tb.Foreground = textColor;
                    LegendCanvas.Children.Add(tb);

                    switch (ds.PlotStyle)
                    {
                        case DataSeries.PlotStyleEnum.Lines:
                            xText = 2 * sx + lineLength;
                            yText = n * sy + (2 * n - 1) * textHeight / 2;
                            line = new Line();
                            AddLinePattern(line, ds);
                            line.X1 = sx;
                            line.Y1 = yText;
                            line.X2 = sx + lineLength;
                            line.Y2 = yText;
                            LegendCanvas.Children.Add(line);
                            ds.Symbols.AddSymbol(legendCanvas,
                                new Point(0.5 * (line.X2 - line.X1 + ds.Symbols.SymbolSize) + 1, line.Y1), -1, -1);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            n++;
                            break;

                        case DataSeries.PlotStyleEnum.Bars:
                            xText = 2 * sx + lineLength;
                            yText = n * sy + (2 * n - 1) * textHeight / 2;
                            Rectangle rect = new Rectangle();
                            rect.Stroke = ds.BorderColor;
                            rect.StrokeThickness = ds.BorderThickness;
                            rect.Fill = ds.FillColor;
                            rect.Width = 10;
                            rect.Height = 10;
                            Canvas.SetLeft(rect, sx + lineLength / 2 - 15);
                            Canvas.SetTop(rect, yText - 2);
                            LegendCanvas.Children.Add(rect);
                            Canvas.SetTop(tb, yText - size.Height / 2 + 2);
                            Canvas.SetLeft(tb, xText - 15);
                            n++;
                            break;

                        case DataSeries.PlotStyleEnum.Stem:
                            xText = 2 * sx + lineLength;
                            yText = n * sy + (2 * n - 1) * textHeight / 2;
                            line = new Line();
                            line.Stroke = ds.LineColor;
                            line.StrokeThickness = ds.LineThickness;
                            line.X1 = sx;
                            line.Y1 = yText;
                            line.X2 = sx + lineLength;
                            line.Y2 = yText;
                            LegendCanvas.Children.Add(line);
                            ds.Symbols.AddSymbol(legendCanvas,
                                new Point(0.5 * (line.X1 + ds.Symbols.SymbolSize) + 1, line.Y1), -1, -1);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            n++;
                            break;

                        case DataSeries.PlotStyleEnum.Stairs:
                            xText = 2 * sx + lineLength;
                            yText = n * sy + (2 * n - 1) * textHeight / 2;
                            line = new Line();
                            AddLinePattern(line, ds);
                            line.X1 = sx;
                            line.Y1 = yText;
                            line.X2 = sx + lineLength;
                            line.Y2 = yText;
                            LegendCanvas.Children.Add(line);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            n++;
                            break;

                        case DataSeries.PlotStyleEnum.Area:
                            xText = 2 * sx + lineLength;
                            yText = n * sy + (2 * n - 1) * textHeight / 2;
                            rect = new Rectangle();
                            rect.Stroke = ds.BorderColor;
                            rect.StrokeThickness = ds.BorderThickness;
                            rect.Fill = ds.FillColor;
                            rect.Width = 10;
                            rect.Height = 10;
                            Canvas.SetLeft(rect, sx + lineLength / 2 - 15);
                            Canvas.SetTop(rect, yText - 2);
                            LegendCanvas.Children.Add(rect);
                            Canvas.SetTop(tb, yText - size.Height / 2 + 2);
                            Canvas.SetLeft(tb, xText - 15);
                            n++;
                            break;
                    }
                }
            }
            legendCanvas.Width = legendRect.Width;
            legendCanvas.Height = legendRect.Height;
            double offSet = 7.0;

            switch (LegendPosition)
            {
                case LegendPositionEnum.East:
                    Canvas.SetRight(legendCanvas, offSet);
                    Canvas.SetTop(legendCanvas,
                    canvas.Height / 2 - legendRect.Height / 2);
                    break;
                case LegendPositionEnum.NorthEast:
                    Canvas.SetTop(legendCanvas, offSet);
                    Canvas.SetRight(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.North:
                    Canvas.SetTop(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas,
                    canvas.Width / 2 - legendRect.Width / 2);
                    break;
                case LegendPositionEnum.NorthWest:
                    Canvas.SetTop(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.West:
                    Canvas.SetTop(legendCanvas,
                    canvas.Height / 2 - legendRect.Height / 2);
                    Canvas.SetLeft(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.SouthWest:
                    Canvas.SetBottom(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas, offSet);
                    break;
                case LegendPositionEnum.South:
                    Canvas.SetBottom(legendCanvas, offSet);
                    Canvas.SetLeft(legendCanvas,
                    canvas.Width / 2 - legendRect.Width / 2);
                    break;
                case LegendPositionEnum.SouthEast:
                    Canvas.SetBottom(legendCanvas, offSet);
                    Canvas.SetRight(legendCanvas, offSet);
                    break;
            }
        }

        public void AddLinePattern(Line line, DataSeries ds)
        {
            line.Stroke = ds.LineColor;
            line.StrokeThickness = ds.LineThickness;
            switch (ds.LinePattern)
            {
                case DataSeries.LinePatternEnum.Dash:
                    line.StrokeDashArray =
                    new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case DataSeries.LinePatternEnum.Dot:
                    line.StrokeDashArray =
                    new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case DataSeries.LinePatternEnum.DashDot:
                    line.StrokeDashArray =
                    new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case DataSeries.LinePatternEnum.None:
                    line.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public void AddExternLegend(Canvas canvas, Canvas LegendCanvas, DataCollection dc, double width)
        {
            if (dc.DataList.Count < 1)
            {
                return;
            }
            TextBlock tb = new TextBlock();
            List<string> legendLabels = new List<string>();
            int n = dc.DataList.Count;
            int rows = 1;
            int maxItemsRow = 1;
            for (int i = 0; i < n; i++)
            {
                if (dc.DataList[i].IsLegend)
                {
                    legendLabels.Add(dc.DataList[i].SeriesName);
                }
            }
            double legendWidth = 0, legendHeight = 20;
            Size size = new Size();
            for (int i = 0; i < legendLabels.Count; i++)
            {
                tb = new TextBlock();
                tb.Text = legendLabels[i];
                tb.Measure(new Size(Double.PositiveInfinity,
                    Double.PositiveInfinity));
                size = tb.DesiredSize;
                double temp = legendWidth + size.Width + 100;
                if (temp < width)
                {
                    legendWidth += size.Width + 100;
                }
                else
                {
                    legendWidth = 0;
                    legendHeight += 15;
                    rows++;
                }
                if (rows == 2)
                {
                    maxItemsRow = i;
                }
            }
            if (rows > 1)
            {
                legendWidth = width;
            }
            else
            {
                maxItemsRow = legendLabels.Count;
            }

            double sx = 6;
            double sy = 0;
            double textHeight = size.Height;
            double textWidth = 0;
            double lineLength = 34;
            double barLength = 10;
            double xText = 0;
            double yText;
            double AccX = 0;
            int m = 1;
            n = 1;
            Line line;
            Rectangle rect;
            Rectangle legendRect = new Rectangle();
            legendRect.Stroke = Brushes.Black;
            legendRect.Fill = Brushes.White;
            legendRect.Width = legendWidth;
            legendRect.Height = legendHeight;
            if (IsBorder)
                LegendCanvas.Children.Add(legendRect);
            Canvas.SetZIndex(LegendCanvas, 10);
            legendCanvas.Width = legendRect.Width;
            legendCanvas.Height = legendRect.Height;
            canvas.Width = width;
            canvas.Height = legendRect.Height;

            foreach (DataSeries ds in dc.DataList)
            {
                if (ds.IsLegend)
                {
                    tb = new TextBlock();
                    tb.Text = ds.SeriesName;
                    tb.FontWeight = FontWeights.Bold;
                    tb.Measure(new Size(Double.PositiveInfinity,
                        Double.PositiveInfinity));
                    size = tb.DesiredSize;
                    textWidth = size.Width;
                    switch (ds.PlotStyle)
                    {
                        case DataSeries.PlotStyleEnum.Lines:
                            xText = AccX + 2 * sx + lineLength;
                            yText = m * sy + (2 * m - 1) * textHeight / 2;
                            line = new Line();
                            AddLinePattern(line, ds);
                            line.X1 = AccX + sx;
                            line.Y1 = yText;
                            line.X2 = AccX + sx + lineLength;
                            line.Y2 = yText;
                            LegendCanvas.Children.Add(line);
                            ds.Symbols.AddSymbol(LegendCanvas,
                                new Point(0.5 * (2 * AccX + line.X2 - line.X1 + ds.Symbols.SymbolSize) + 1, line.Y1),
                                -1, -1);
                            LegendCanvas.Children.Add(tb);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            break;
                        case DataSeries.PlotStyleEnum.Bars:
                            xText = AccX + 2 * sx + barLength;
                            yText = m * sy + (2 * m - 1) * textHeight / 2;
                            rect = new Rectangle();
                            rect.Stroke = ds.BorderColor;
                            rect.StrokeThickness = ds.BorderThickness;
                            rect.Fill = ds.FillColor;
                            rect.Width = 10;
                            rect.Height = 10;
                            Canvas.SetLeft(rect, AccX + sx + barLength / 2 - 5);
                            Canvas.SetTop(rect, yText - 5);
                            LegendCanvas.Children.Add(rect);
                            LegendCanvas.Children.Add(tb);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            break;
                        case DataSeries.PlotStyleEnum.Stairs:
                            xText = AccX + 2 * sx + lineLength;
                            yText = m * sy + (2 * m - 1) * textHeight / 2;
                            line = new Line();
                            AddLinePattern(line, ds);
                            line.X1 = AccX + sx;
                            line.Y1 = yText;
                            line.X2 = AccX + sx + lineLength;
                            line.Y2 = yText;
                            LegendCanvas.Children.Add(line);
                            LegendCanvas.Children.Add(tb);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            break;
                        case DataSeries.PlotStyleEnum.Stem:
                            xText = AccX + 2 * sx + lineLength;
                            yText = m * sy + (2 * m - 1) * textHeight / 2;
                            line = new Line();
                            line.Stroke = ds.LineColor;
                            line.StrokeThickness = ds.LineThickness;
                            line.X1 = AccX + sx;
                            line.Y1 = yText;
                            line.X2 = AccX + sx + lineLength;
                            line.Y2 = yText;
                            LegendCanvas.Children.Add(line);
                            ds.Symbols.AddSymbol(LegendCanvas,
                                new Point(0.5 * (AccX + line.X1 + ds.Symbols.SymbolSize) + 1, line.Y1),
                                -1, -1);
                            LegendCanvas.Children.Add(tb);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            break;
                        case DataSeries.PlotStyleEnum.Area:
                            xText = AccX + 2 * sx + barLength;
                            yText = m * sy + (2 * m - 1) * textHeight / 2;
                            rect = new Rectangle();
                            rect.Stroke = ds.BorderColor;
                            rect.StrokeThickness = ds.BorderThickness;
                            rect.Fill = ds.FillColor;
                            rect.Width = 10;
                            rect.Height = 10;
                            Canvas.SetLeft(rect, AccX + sx + barLength / 2 - 5);
                            Canvas.SetTop(rect, yText - 5);
                            LegendCanvas.Children.Add(rect);
                            LegendCanvas.Children.Add(tb);
                            Canvas.SetTop(tb, yText - size.Height / 2);
                            Canvas.SetLeft(tb, xText);
                            break;
                    }

                    if ((n < maxItemsRow) && (AccX < width - 100))
                    {
                        n++;
                        AccX = xText + textWidth;
                    }
                    else
                    {
                        m++;
                        n = 1;
                        AccX = 0;
                    }

                }
            }
            Canvas.SetTop(LegendCanvas,
                    canvas.Height / 2 - legendRect.Height / 2);
            Canvas.SetLeft(LegendCanvas, 0);
        }
        #endregion
    }
}
