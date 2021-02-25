using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunctionNeuralNetwork.Charts
{
    public delegate void ItemSymbolClickEventHandler(object sender, System.Windows.Input.MouseButtonEventArgs e);
    public class Symbols
    {
        #region Attributes
        private SymbolTypeEnum symbolType;
        private double symbolSize;
        private Brush borderColor;
        private Brush fillColor;
        private double borderThickness;
        //private Ellipse ellipse;
        private Polygon polyg;
        //private int dataInd, serieInd;
        private double gnSymOpacity = 1;
        #endregion

        #region Properties
        public double BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }
        public Brush BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        public Brush FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }
        public double SymbolSize
        {
            get { return symbolSize; }
            set { symbolSize = value; }
        }
        public SymbolTypeEnum SymbolType
        {
            get { return symbolType; }
            set { symbolType = value; }
        }
        public double SymOpacity
        {
            get { return gnSymOpacity; }
            set { gnSymOpacity = value; }
        }
        //public int DataInd
        //{ get { return dataInd; } }
        //public int SerieInd
        //{ get { return serieInd; } }
        #endregion

        #region Constructor
        public Symbols()
        {
            symbolType = SymbolTypeEnum.None;
            symbolSize = 8.0;
            borderColor = Brushes.Black;
            fillColor = Brushes.Black;
            borderThickness = 1.0;
        }


        #endregion

        #region Enumeration
        public enum SymbolTypeEnum
        {
            None = 0,
            Box = 1,
            Circle = 2,
            Cross = 3,
            Diamond = 4,
            Dot = 5,
            InvertedTriangle = 6,
            OpenDiamond = 7,
            OpenInvertedTriangle = 8,
            OpenTriangle = 9,
            Square = 10,
            Star = 11,
            Triangle = 12,
            Plus = 13,
            LineCircle = 14,
            LineTriangle = 15,
            LineBox = 16
            //SolidSquare = 17, Box and SolidSquare are the same symbol
        }
        #endregion

        #region Events
        void ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Ellipse elp = sender as Ellipse;
            IndOrigin Ord1 = (IndOrigin)elp.Tag;

            OnItemSymbolClick(Ord1, e);

        }
        void polyg_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Polygon elp = sender as Polygon;
            IndOrigin Ord1 = (IndOrigin)elp.Tag;

            OnItemSymbolClick(Ord1, e);
        }
        private void OnItemSymbolClick(IndOrigin Ord1, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ItemSymbolClick != null)
            {
                ItemSymbolClick(Ord1, e);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a selected symbol in a canvas
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="pt"></param>
        /// <param name="Data"></param>
        /// <param name="Serie"></param>
        public void AddSymbol(Canvas canvas, Point pt, int Data, int Serie)
        {
            IndOrigin Org = new IndOrigin(Data, Serie);
            polyg = new Polygon();
            polyg.Stroke = BorderColor;
            polyg.StrokeThickness = BorderThickness;
            polyg.Tag = Org;
            polyg.Opacity = gnSymOpacity;
            Ellipse ellipse = new Ellipse();
            ellipse.Tag = Org;
            ellipse.Stroke = BorderColor;
            ellipse.StrokeThickness = BorderThickness;
            ellipse.Opacity = gnSymOpacity;
            Line line = new Line();
            line.Opacity = gnSymOpacity;
            double halfSize = 0.5 * SymbolSize;
            Canvas.SetZIndex(polyg, 5);
            Canvas.SetZIndex(ellipse, 5);

            ellipse.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ellipse_MouseLeftButtonDown);
            polyg.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(polyg_MouseLeftButtonDown);

            #region Draw Symbol
            switch (SymbolType)
            {
                case SymbolTypeEnum.Square:
                    #region Draw Square
                    polyg.Fill = Brushes.White;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                //case SymbolTypeEnum.SolidSquare:
                //    #region Draw solid Square
                //    polyg.Fill = FillColor;
                //    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                //    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                //    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                //    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                //    canvas.Children.Add(polyg);
                //    #endregion
                //    break;
                case SymbolTypeEnum.OpenDiamond:
                    #region Draw OpendDiamond
                    polyg.Fill = Brushes.White;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y));
                    polyg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y));
                    polyg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.Circle:
                    #region Draw Circle
                    ellipse.Fill = Brushes.White;
                    ellipse.Width = SymbolSize;
                    ellipse.Height = SymbolSize;
                    Canvas.SetLeft(ellipse, pt.X - halfSize);
                    Canvas.SetTop(ellipse, pt.Y - halfSize);
                    canvas.Children.Add(ellipse);
                    #endregion
                    break;
                case SymbolTypeEnum.OpenTriangle:
                    #region OpenTriangle
                    polyg.Fill = Brushes.White;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.None:
                    break;
                case SymbolTypeEnum.Cross:
                    #region Draw Cross
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y + halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y - halfSize;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    Canvas.SetZIndex(line, 5);
                    #endregion
                    break;
                case SymbolTypeEnum.Star:
                    #region Draw Star
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y + halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y - halfSize;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    #endregion
                    break;
                case SymbolTypeEnum.OpenInvertedTriangle:
                    #region Draw OpenInvertedTriangle
                    polyg.Fill = Brushes.White;
                    polyg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.Plus:
                    #region Draw Plus
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X - halfSize;
                    line.Y1 = pt.Y;
                    line.X2 = pt.X + halfSize;
                    line.Y2 = pt.Y;
                    canvas.Children.Add(line);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y + halfSize;
                    canvas.Children.Add(line);
                    #endregion
                    break;
                case SymbolTypeEnum.Dot:
                    #region Draw Dot
                    ellipse.Fill = FillColor;
                    ellipse.Width = SymbolSize;
                    ellipse.Height = SymbolSize;
                    Canvas.SetLeft(ellipse, pt.X - halfSize);
                    Canvas.SetTop(ellipse, pt.Y - halfSize);
                    canvas.Children.Add(ellipse);
                    #endregion
                    break;
                case SymbolTypeEnum.Box:
                    #region Draw Box
                    polyg.Fill = FillColor;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.Diamond:
                    #region Diamond
                    polyg.Fill = FillColor;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y));
                    polyg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y));
                    polyg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.InvertedTriangle:
                    #region Draw InvertedTrianlge
                    polyg.Fill = FillColor;
                    polyg.Points.Add(new Point(pt.X, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.Triangle:
                    #region Draw Triangle
                    polyg.Fill = FillColor;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.LineCircle:
                    #region Draw LineCircle
                    ellipse.Fill = FillColor;
                    ellipse.Stroke = BorderColor;
                    ellipse.Width = SymbolSize;
                    ellipse.Height = SymbolSize;
                    Canvas.SetLeft(ellipse, pt.X - halfSize);
                    Canvas.SetTop(ellipse, pt.Y - halfSize);
                    canvas.Children.Add(ellipse);
                    line = new Line();
                    Canvas.SetZIndex(line, 5);
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness + 1;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y - 3 * halfSize;
                    canvas.Children.Add(line);
                    #endregion
                    break;
                case SymbolTypeEnum.LineTriangle:
                    #region Draw LineTriangle
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness + 1;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y - 3 * halfSize;
                    canvas.Children.Add(line);
                    polyg.Fill = FillColor;
                    polyg.Stroke = borderColor;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;
                case SymbolTypeEnum.LineBox:
                    #region Draw LineBox
                    line.Stroke = BorderColor;
                    line.StrokeThickness = BorderThickness + 1;
                    line.Opacity = gnSymOpacity;
                    line.X1 = pt.X;
                    line.Y1 = pt.Y - halfSize;
                    line.X2 = pt.X;
                    line.Y2 = pt.Y - 3 * halfSize;
                    canvas.Children.Add(line);
                    polyg.Fill = FillColor;
                    polyg.Stroke = BorderColor;
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y - halfSize));
                    polyg.Points.Add(new Point(pt.X + halfSize, pt.Y + halfSize));
                    polyg.Points.Add(new Point(pt.X - halfSize, pt.Y + halfSize));
                    canvas.Children.Add(polyg);
                    #endregion
                    break;

            }
            #endregion
        }

        #region Fast Symbol Methods
        public void AddFastSymbol(Canvas loCV, Point loPt)
        {
            double halfSize = 0.5 * SymbolSize;
            //Pen loPen = new Pen(borderColor, borderThickness);
            Path loPath = new Path();
            loPath.Fill = fillColor;
            loPath.Stroke = borderColor;
            loPath.StrokeThickness = borderThickness;

            #region DrawSymbols
            switch (symbolType)
            {
                case SymbolTypeEnum.Box:
                    #region Box

                    RectangleGeometry loRectGeo = new RectangleGeometry();
                    Rect loRect = new Rect(new Point(loPt.X - halfSize, loPt.Y - halfSize), new Point(loPt.X + halfSize, loPt.Y + halfSize));
                    loRectGeo.Rect = loRect;

                    loPath.Data = loRectGeo;

                    #endregion
                    break;
                case SymbolTypeEnum.Circle:
                    #region Circle
                    loPath.Fill = Brushes.Transparent;
                    Rect loRecCir = new Rect(loPt.X - halfSize, loPt.Y - halfSize, symbolSize, symbolSize);
                    EllipseGeometry loEllipGeoCr = new EllipseGeometry(loRecCir);
                    loPath.Data = loEllipGeoCr;
                    #endregion
                    break;
                case SymbolTypeEnum.Cross:
                    #region Cross

                    GeometryGroup loLines = new GeometryGroup();
                    LineGeometry loLine1 = new LineGeometry(
                        new Point(loPt.X - halfSize, loPt.Y + halfSize), new Point(loPt.X + halfSize, loPt.Y - halfSize));
                    LineGeometry loLine2 = new LineGeometry(
                        new Point(loPt.X - halfSize, loPt.Y - halfSize), new Point(loPt.X + halfSize, loPt.Y + halfSize));
                    loLines.Children.Add(loLine1);
                    loLines.Children.Add(loLine2);


                    loPath.Data = loLines;

                    //loCV.Children.Add(loPath);
                    #endregion
                    break;
                case SymbolTypeEnum.Diamond:
                    #region Diamond
                    PathGeometry loPathGeo = new PathGeometry();
                    PathFigure loPathFig = new PathFigure();
                    loPathFig.IsClosed = true;
                    loPathFig.StartPoint = new Point(loPt.X - halfSize, loPt.Y);
                    LineSegment loLinSeg1 = new LineSegment(new Point(loPt.X, loPt.Y - halfSize), true);
                    LineSegment loLinSeg2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y), true);
                    LineSegment loLinSeg3 = new LineSegment(new Point(loPt.X, loPt.Y + halfSize), true);
                    loPathFig.Segments.Add(loLinSeg1);
                    loPathFig.Segments.Add(loLinSeg2);
                    loPathFig.Segments.Add(loLinSeg3);
                    loPathGeo.Figures.Add(loPathFig);
                    loPath.Data = loPathGeo;
                    #endregion
                    break;
                case SymbolTypeEnum.Dot:
                    #region Dot
                    Rect loRecEllip = new Rect(loPt.X - halfSize, loPt.Y - halfSize, symbolSize, symbolSize);
                    EllipseGeometry loEllipGeo = new EllipseGeometry(loRecEllip);
                    loPath.Data = loEllipGeo;
                    #endregion
                    break;
                case SymbolTypeEnum.InvertedTriangle:
                    #region Inverted Triangle
                    PathGeometry loPGeo = new PathGeometry();
                    PathFigure loPFig = new PathFigure();
                    loPFig.IsClosed = true;
                    loPFig.StartPoint = new Point(loPt.X, loPt.Y + halfSize);
                    LineSegment loLinS1 = new LineSegment(new Point(loPt.X - halfSize, loPt.Y - halfSize), true);
                    LineSegment loLinS2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y - halfSize), true);
                    loPFig.Segments.Add(loLinS1);
                    loPFig.Segments.Add(loLinS2);
                    loPGeo.Figures.Add(loPFig);
                    loPath.Data = loPGeo;
                    #endregion
                    break;
                case SymbolTypeEnum.None:
                    break;
                case SymbolTypeEnum.OpenDiamond:
                    #region Open Diamond
                    loPath.Fill = Brushes.Transparent;
                    PathGeometry loOD = new PathGeometry();
                    PathFigure loPFOD = new PathFigure();
                    loPFOD.IsClosed = true;
                    loPFOD.StartPoint = new Point(loPt.X - halfSize, loPt.Y);
                    LineSegment loLSOD1 = new LineSegment(new Point(loPt.X, loPt.Y - halfSize), true);
                    LineSegment loLSOD2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y), true);
                    LineSegment loLSOD3 = new LineSegment(new Point(loPt.X, loPt.Y + halfSize), true);
                    loPFOD.Segments.Add(loLSOD1);
                    loPFOD.Segments.Add(loLSOD2);
                    loPFOD.Segments.Add(loLSOD3);
                    loOD.Figures.Add(loPFOD);
                    loPath.Data = loOD;
                    #endregion
                    break;
                case SymbolTypeEnum.OpenInvertedTriangle:
                    #region Open Inverted Triangle
                    loPath.Fill = Brushes.Transparent;
                    PathGeometry loOIT = new PathGeometry();
                    PathFigure loPFOIT = new PathFigure();
                    loPFOIT.IsClosed = true;
                    loPFOIT.StartPoint = new Point(loPt.X, loPt.Y + halfSize);
                    LineSegment loLSOIT1 = new LineSegment(new Point(loPt.X - halfSize, loPt.Y - halfSize), true);
                    LineSegment loLSOIT2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y - halfSize), true);
                    loPFOIT.Segments.Add(loLSOIT1);
                    loPFOIT.Segments.Add(loLSOIT2);
                    loOIT.Figures.Add(loPFOIT);
                    loPath.Data = loOIT;
                    #endregion
                    break;
                case SymbolTypeEnum.OpenTriangle:
                    #region Open Triangle
                    loPath.Fill = Brushes.Transparent;
                    PathGeometry loPGOT = new PathGeometry();
                    PathFigure loPFOT = new PathFigure();
                    loPFOT.IsClosed = true;
                    loPFOT.StartPoint = new Point(loPt.X - halfSize, loPt.Y + halfSize);
                    LineSegment loLSOT1 = new LineSegment(new Point(loPt.X, loPt.Y - halfSize), true);
                    LineSegment loLSOT2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y + halfSize), true);
                    loPFOT.Segments.Add(loLSOT1);
                    loPFOT.Segments.Add(loLSOT2);
                    loPGOT.Figures.Add(loPFOT);
                    loPath.Data = loPGOT;
                    #endregion
                    break;
                case SymbolTypeEnum.Square:
                    #region Square
                    loPath.Fill = Brushes.Transparent;
                    RectangleGeometry loRectGeoS = new RectangleGeometry();
                    Rect loRectSq = new Rect(new Point(loPt.X - halfSize, loPt.Y - halfSize), new Point(loPt.X + halfSize, loPt.Y + halfSize));
                    loRectGeoS.Rect = loRectSq;

                    loPath.Data = loRectGeoS;
                    #endregion
                    break;
                case SymbolTypeEnum.Star:
                    #region Star
                    GeometryGroup loGGStr = new GeometryGroup();
                    LineGeometry loLGStr1 = new LineGeometry(
                        new Point(loPt.X - halfSize, loPt.Y + halfSize), new Point(loPt.X + halfSize, loPt.Y - halfSize));
                    LineGeometry loLGStr2 = new LineGeometry(
                        new Point(loPt.X - halfSize, loPt.Y - halfSize), new Point(loPt.X + halfSize, loPt.Y + halfSize));
                    LineGeometry loLGStr3 = new LineGeometry(
                        new Point(loPt.X - halfSize, loPt.Y), new Point(loPt.X + halfSize, loPt.Y));
                    LineGeometry loLGStr4 = new LineGeometry(
                        new Point(loPt.X, loPt.Y - halfSize), new Point(loPt.X, loPt.Y + halfSize));
                    loGGStr.Children.Add(loLGStr1);
                    loGGStr.Children.Add(loLGStr2);
                    loGGStr.Children.Add(loLGStr3);
                    loGGStr.Children.Add(loLGStr4);

                    loPath.Data = loGGStr;
                    #endregion
                    break;
                case SymbolTypeEnum.Triangle:
                    #region Triangle
                    PathGeometry loPGT = new PathGeometry();
                    PathFigure loPFT = new PathFigure();
                    loPFT.IsClosed = true;
                    loPFT.StartPoint = new Point(loPt.X - halfSize, loPt.Y + halfSize);
                    LineSegment loLST1 = new LineSegment(new Point(loPt.X, loPt.Y - halfSize), true);
                    LineSegment loLST2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y + halfSize), true);
                    loPFT.Segments.Add(loLST1);
                    loPFT.Segments.Add(loLST2);
                    loPGT.Figures.Add(loPFT);
                    loPath.Data = loPGT;
                    #endregion
                    break;
                case SymbolTypeEnum.Plus:
                    #region Plus
                    GeometryGroup loGGPl = new GeometryGroup();
                    LineGeometry loLGPl1 = new LineGeometry(
                        new Point(loPt.X - halfSize, loPt.Y), new Point(loPt.X + halfSize, loPt.Y));
                    LineGeometry loLGPl2 = new LineGeometry(
                        new Point(loPt.X, loPt.Y - halfSize), new Point(loPt.X, loPt.Y + halfSize));
                    loGGPl.Children.Add(loLGPl1);
                    loGGPl.Children.Add(loLGPl2);


                    loPath.Data = loGGPl;
                    #endregion
                    break;
                case SymbolTypeEnum.LineCircle:
                    #region Line Circle
                    GeometryGroup loGPLC = new GeometryGroup();

                    Rect loRLCr = new Rect(loPt.X - halfSize, loPt.Y - halfSize, symbolSize, symbolSize);
                    EllipseGeometry loEGLCr = new EllipseGeometry(loRLCr);
                    loGPLC.Children.Add(loEGLCr);

                    LineGeometry loLGL = new LineGeometry(
                        new Point(loPt.X, loPt.Y - halfSize), new Point(loPt.X, loPt.Y - 2 * halfSize));

                    loGPLC.Children.Add(loLGL);

                    loPath.Data = loGPLC;
                    #endregion
                    break;
                case SymbolTypeEnum.LineTriangle:
                    #region Line Triangle
                    PathGeometry loPGLT = new PathGeometry();

                    PathFigure loPFTr = new PathFigure();
                    loPFTr.IsClosed = true;
                    loPFTr.StartPoint = new Point(loPt.X - halfSize, loPt.Y + halfSize);
                    LineSegment loLSTr1 = new LineSegment(new Point(loPt.X, loPt.Y - halfSize), true);
                    LineSegment loLSTr2 = new LineSegment(new Point(loPt.X + halfSize, loPt.Y + halfSize), true);
                    loPFTr.Segments.Add(loLSTr1);
                    loPFTr.Segments.Add(loLSTr2);
                    loPGLT.Figures.Add(loPFTr);

                    LineGeometry loLGTr = new LineGeometry(
                       new Point(loPt.X, loPt.Y - halfSize), new Point(loPt.X, loPt.Y - 2 * halfSize));
                    loPGLT.AddGeometry(loLGTr);

                    loPath.Data = loPGLT;
                    #endregion
                    break;
                case SymbolTypeEnum.LineBox:
                    #region Line Box
                    GeometryGroup loGCLB = new GeometryGroup();

                    RectangleGeometry loRGLB = new RectangleGeometry();
                    Rect loRLB = new Rect(new Point(loPt.X - halfSize, loPt.Y - halfSize), new Point(loPt.X + halfSize, loPt.Y + halfSize));
                    loRGLB.Rect = loRLB;
                    loGCLB.Children.Add(loRGLB);

                    LineGeometry loLGLB = new LineGeometry(
                       new Point(loPt.X, loPt.Y - halfSize), new Point(loPt.X, loPt.Y - 2 * halfSize));
                    loGCLB.Children.Add(loLGLB);

                    loPath.Data = loGCLB;
                    #endregion
                    break;
                default:
                    break;
            }

            loCV.Children.Add(loPath);
            #endregion

        }
        #endregion

        #endregion



        public event ItemSymbolClickEventHandler ItemSymbolClick;


    }

    public class IndOrigin
    {
        public int dataInd, serieInd;

        public IndOrigin(int Data, int Serie)
        {
            dataInd = Data;
            serieInd = Serie;

        }
    }

}
