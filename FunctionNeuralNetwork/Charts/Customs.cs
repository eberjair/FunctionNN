using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace FunctionNeuralNetwork.Charts
{
    public class Customs
    {
        #region Attributes
        private List<CustomLine> cLineList_go;
        private List<CustomArea> cAreaList_go;
        private List<CustomTag> gsTagList;
        private List<CustomCircles> gsCirclesList;
        private List<CustomFigures> gsFigureList;
        #endregion

        #region Properties
        public List<CustomLine> CLineList
        {
            get { return cLineList_go; }
            set { cLineList_go = value; }
        }
        public List<CustomArea> CAreaList
        {
            get { return cAreaList_go; }
            set { cAreaList_go = value; }
        }
        public List<CustomTag> TagList
        {
            get { return gsTagList; }
            set { gsTagList = value; }
        }
        public List<CustomCircles> CirclesList
        {
            get { return gsCirclesList; }
            set { gsCirclesList = value; }
        }
        public List<CustomFigures> FigureList
        {
            get { return gsFigureList; }
            set { gsFigureList = value; }
        }
        #endregion

        #region Constructor
        public Customs()
        {
            cLineList_go = new List<CustomLine>();
            cAreaList_go = new List<CustomArea>();
            gsTagList = new List<CustomTag>();
            gsCirclesList = new List<CustomCircles>();
            gsFigureList = new List<CustomFigures>();
        }
        #endregion

        #region Methods
        public void PlotCustoms(ChartStyle cs)
        {
            if (cLineList_go.Count != 0)
                PlotLines(cs);
            if (cAreaList_go.Count != 0)
                PlotAreas(cs);
            if (gsTagList.Count != 0)
                PlotTags(cs);
            if (gsCirclesList.Count != 0)
                PlotCircles(cs);
            if (gsFigureList.Count != 0)
                PlotFigures(cs);
        }

        private void PlotLines(ChartStyle cs)
        {
            foreach (CustomLine CLine in cLineList_go)
            {
                CLine.cline_go.Points.Clear();
                CLine.AddCustomlinePattern();
                Point pt1_lo = new Point();
                Point pt2_lo = new Point();
                bool isValidPt = false;
                switch (CLine.ClineType)
                {
                    case CustomTypeEnum.Vertical:
                        #region Vertical Custom Line
                        if (!double.IsNaN(CLine.XPosition))
                        {
                            pt1_lo = new Point(CLine.XPosition, cs.Ymin);
                            pt2_lo = new Point(CLine.XPosition, cs.Ymax);
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPt = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    isValidPt = !double.IsNaN(Math.Log10(CLine.XPosition));
                                    if (isValidPt)
                                    {
                                        pt1_lo = cs.Point2DLogX(pt1_lo);
                                        pt2_lo = cs.Point2DLogX(pt2_lo);
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    isValidPt = !double.IsNaN(Math.Log10(CLine.XPosition));
                                    if (isValidPt)
                                    {
                                        pt1_lo = cs.Point2DLogX(pt1_lo);
                                        pt2_lo = cs.Point2DLogX(pt2_lo);
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    isValidPt = true;
                                    break;
                            }

                        }
                        #endregion
                        break;
                    case CustomTypeEnum.Horizontal:
                        #region Horizontal Custom Line
                        if (!double.IsNaN(CLine.YPosition))
                        {
                            pt1_lo = new Point(cs.Xmin, CLine.YPosition);
                            pt2_lo = new Point(cs.Xmax, CLine.YPosition);
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPt = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    isValidPt = !double.IsNaN(Math.Log10(CLine.YPosition));
                                    if (isValidPt)
                                    {
                                        pt1_lo = cs.Point2DLogY(pt1_lo);
                                        pt2_lo = cs.Point2DLogY(pt2_lo);
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    isValidPt = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    isValidPt = !double.IsNaN(Math.Log10(CLine.YPosition));
                                    if (isValidPt)
                                    {
                                        pt1_lo = cs.Point2DLogY(pt1_lo);
                                        pt2_lo = cs.Point2DLogY(pt2_lo);
                                    }
                                    break;
                            }
                        }
                        #endregion
                        break;
                    case CustomTypeEnum.Custom:
                        #region Custom
                        #region Slope and Yintercep
                        if (!double.IsNaN(CLine.SlopeCL) && !double.IsNaN(CLine.YIntercept))
                        {
                            double ys_ln = CLine.SlopeCL * cs.Xmin + CLine.YIntercept;
                            double yf_ln = CLine.SlopeCL * cs.Xmax + CLine.YIntercept;
                            pt1_lo = new Point(cs.Xmin, ys_ln);
                            pt2_lo = new Point(cs.Xmax, yf_ln);
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPt = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    break;
                            }

                        }
                        #endregion
                        #region Points 
                        if (!double.IsNaN(CLine.xMinCl) && !double.IsNaN(CLine.xMaxCl)
                            && !double.IsNaN(CLine.yMinCl) && !double.IsNaN(CLine.yMaxCl))
                        {
                            pt1_lo = new Point(CLine.xMinCl, CLine.yMinCl);
                            pt2_lo = new Point(CLine.xMaxCl, CLine.yMaxCl);
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPt = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    if (ValidPoint(pt1_lo, cs) && ValidPoint(pt2_lo, cs))
                                    {
                                        pt1_lo = cs.Point2DLogLog(pt1_lo);
                                        pt2_lo = cs.Point2DLogLog(pt2_lo);
                                        isValidPt = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    if (ValidPoint(pt1_lo, cs) && ValidPoint(pt2_lo, cs))
                                    {
                                        pt1_lo = cs.Point2DLogX(pt1_lo);
                                        pt2_lo = cs.Point2DLogX(pt2_lo);
                                        isValidPt = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    if (ValidPoint(pt1_lo, cs) && ValidPoint(pt2_lo, cs))
                                    {
                                        pt1_lo = cs.Point2DLogY(pt1_lo);
                                        pt2_lo = cs.Point2DLogY(pt2_lo);
                                        isValidPt = true;
                                    }
                                    break;
                            }
                        }
                        #endregion
                        break;
                        #endregion
                }

                if (isValidPt)
                {
                    pt1_lo = cs.Point2D(pt1_lo);
                    pt2_lo = cs.Point2D(pt2_lo);
                    CLine.cline_go.Points.Add(pt1_lo);
                    CLine.cline_go.Points.Add(pt2_lo);
                    cs.ChartCanvas.Children.Add(CLine.cline_go);
                }
            }
        }

        private void PlotAreas(ChartStyle cs)
        {
            foreach (CustomArea CArea in cAreaList_go)
            {
                CArea.cArea_go.Points.Clear();
                CArea.AddCustomAreaPattern();
                Point pt11_lo = new Point();
                Point pt12_lo = new Point();
                Point pt21_lo = new Point();
                Point pt22_lo = new Point();
                bool isValidPts = false;
                switch (CArea.CAreaType)
                {
                    case CustomTypeEnum.Vertical:
                        #region Vertical Area
                        if (!double.IsNaN(CArea.XStart) && !double.IsNaN(CArea.XEnd))
                        {
                            pt11_lo = new Point(CArea.XStart, cs.Ymax);
                            pt12_lo = new Point(CArea.XEnd, cs.Ymax);
                            pt22_lo = new Point(CArea.XEnd, cs.Ymin);
                            pt21_lo = new Point(CArea.XStart, cs.Ymin);
                            double xs_ln;
                            double xe_ln;
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPts = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    xs_ln = Math.Log10(CArea.XStart);
                                    xe_ln = Math.Log10(CArea.XEnd);
                                    if (ValidDouble(xs_ln) && ValidDouble(xe_ln))
                                    {
                                        pt11_lo.X = xs_ln;
                                        pt12_lo.X = xe_ln;
                                        pt22_lo.X = xe_ln;
                                        pt21_lo.X = xs_ln;
                                        isValidPts = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    xs_ln = Math.Log10(CArea.XStart);
                                    xe_ln = Math.Log10(CArea.XEnd);
                                    if (ValidDouble(xs_ln) && ValidDouble(xe_ln))
                                    {
                                        pt11_lo.X = xs_ln;
                                        pt12_lo.X = xe_ln;
                                        pt22_lo.X = xe_ln;
                                        pt21_lo.X = xs_ln;
                                        isValidPts = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    isValidPts = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        break;
                    case CustomTypeEnum.Horizontal:
                        #region Horizontal Area
                        if (ValidDouble(CArea.YStart) && ValidDouble(CArea.YEnd))
                        {
                            pt11_lo = new Point(cs.Xmin, CArea.YEnd);
                            pt12_lo = new Point(cs.Xmax, CArea.YEnd);
                            pt22_lo = new Point(cs.Xmax, CArea.YStart);
                            pt21_lo = new Point(cs.Xmin, CArea.YStart);
                            double ys_ln;
                            double ye_ln;
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPts = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    ys_ln = Math.Log10(CArea.YStart);
                                    ye_ln = Math.Log10(CArea.YEnd);
                                    if (ValidDouble(ys_ln) && ValidDouble(ye_ln))
                                    {
                                        pt11_lo.Y = ye_ln;
                                        pt12_lo.Y = ye_ln;
                                        pt22_lo.Y = ys_ln;
                                        pt21_lo.Y = ys_ln;
                                        isValidPts = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    isValidPts = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    ys_ln = Math.Log10(CArea.YStart);
                                    ye_ln = Math.Log10(CArea.YEnd);
                                    if (ValidDouble(ys_ln) && ValidDouble(ye_ln))
                                    {
                                        pt11_lo.Y = ye_ln;
                                        pt12_lo.Y = ye_ln;
                                        pt22_lo.Y = ys_ln;
                                        pt21_lo.Y = ys_ln;
                                        isValidPts = true;
                                    }
                                    break;
                            }
                        }
                        #endregion
                        break;
                    case CustomTypeEnum.Custom:
                        #region Custom Area
                        if (CArea.StartPoint != null && CArea.EndPoint != null)
                        {
                            pt11_lo = CArea.StartPoint;
                            pt22_lo = CArea.EndPoint;
                            pt12_lo = new Point(pt22_lo.X, pt11_lo.Y);
                            pt21_lo = new Point(pt11_lo.X, pt22_lo.Y);
                            double xs_ln;
                            double xe_ln;
                            double ys_ln;
                            double ye_ln;
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Linear:
                                    isValidPts = true;
                                    break;
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    xs_ln = Math.Log10(pt11_lo.X);
                                    xe_ln = Math.Log10(pt22_lo.X);
                                    ye_ln = Math.Log10(pt11_lo.Y);
                                    ys_ln = Math.Log10(pt22_lo.Y);
                                    if (ValidDouble(xs_ln) && ValidDouble(xe_ln) && ValidDouble(ys_ln)
                                        && ValidDouble(ye_ln))
                                    {
                                        pt11_lo = new Point(xs_ln, ye_ln);
                                        pt12_lo = new Point(xe_ln, ye_ln);
                                        pt22_lo = new Point(xe_ln, ys_ln);
                                        pt21_lo = new Point(xs_ln, ys_ln);
                                        isValidPts = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    xs_ln = Math.Log10(pt11_lo.X);
                                    xe_ln = Math.Log10(pt22_lo.X);
                                    if (ValidDouble(xs_ln) && ValidDouble(xe_ln))
                                    {
                                        pt11_lo.X = xs_ln;
                                        pt12_lo.X = xe_ln;
                                        pt22_lo.X = xe_ln;
                                        pt21_lo.X = xs_ln;
                                        isValidPts = true;
                                    }
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    ye_ln = Math.Log10(pt11_lo.Y);
                                    ys_ln = Math.Log10(pt22_lo.Y);
                                    if (ValidDouble(ye_ln) && ValidDouble(ys_ln))
                                    {
                                        pt11_lo.Y = ye_ln;
                                        pt12_lo.Y = ye_ln;
                                        pt22_lo.Y = ys_ln;
                                        pt21_lo.Y = ys_ln;
                                        isValidPts = true;
                                    }
                                    break;

                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }

                if (isValidPts)
                {
                    pt11_lo = cs.Point2D(pt11_lo);
                    pt12_lo = cs.Point2D(pt12_lo);
                    pt22_lo = cs.Point2D(pt22_lo);
                    pt21_lo = cs.Point2D(pt21_lo);
                    CArea.cArea_go.Points.Add(pt11_lo);
                    CArea.cArea_go.Points.Add(pt12_lo);
                    CArea.cArea_go.Points.Add(pt22_lo);
                    CArea.cArea_go.Points.Add(pt21_lo);
                    cs.ChartCanvas.Children.Add(CArea.cArea_go);
                }
            }
        }

        private void PlotTags(ChartStyle loCS)
        {
            foreach (CustomTag loCT in gsTagList)
            {
                if (!double.IsNaN(loCT.PoitnLoc.X) && !double.IsNaN(loCT.PoitnLoc.Y))
                {
                    Point loPt = new Point();
                    bool lbIsValid = false;

                    #region Define Point
                    switch (loCS.AxisStyle)
                    {
                        case ChartStyle.AxisStyleEnum.Linear:
                            loPt = loCT.PoitnLoc;
                            lbIsValid = true;
                            break;
                        case ChartStyle.AxisStyleEnum.Logarithmic:
                            if (ValidPoint(loCT.PoitnLoc, loCS))
                            {
                                loPt = loCS.Point2DLogLog(loCT.PoitnLoc);
                                lbIsValid = true;
                            }
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogX:
                            if (ValidPoint(loCT.PoitnLoc, loCS))
                            {
                                loPt = loCS.Point2DLogX(loCT.PoitnLoc);
                                lbIsValid = true;
                            }
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogY:
                            if (ValidPoint(loCT.PoitnLoc, loCS))
                            {
                                loPt = loCS.Point2DLogY(loCT.PoitnLoc);
                                lbIsValid = true;
                            }
                            break;
                    }
                    #endregion

                    #region Add TextBlock
                    if (lbIsValid)
                    {
                        loPt = loCS.Point2D(loPt);
                        TextBlock loTB = new TextBlock();
                        loCT.AddCustomeTagPattern(loTB);
                        Canvas.SetTop(loTB, loPt.Y);
                        Canvas.SetLeft(loTB, loPt.X);
                        loCS.ChartCanvas.Children.Add(loTB);
                    }
                    #endregion
                }
            }
        }

        private void PlotCircles(ChartStyle loCS)
        {
            foreach (CustomCircles loCC in gsCirclesList)
            {
                Point loPtLoc = new Point((loCC.PointCenter.X - loCC.RadX), (loCC.PointCenter.Y + loCC.RadY));
                loPtLoc = loCS.Point2D(loPtLoc);
                Point loPtX1 = new Point(loCC.PointCenter.X - loCC.RadX, loCC.PointCenter.Y);
                Point loPtX2 = new Point(loCC.PointCenter.X + loCC.RadX, loCC.PointCenter.Y);
                Point loPtY1 = new Point(loCC.PointCenter.X, loCC.PointCenter.Y + loCC.RadY);
                Point loPtY2 = new Point(loCC.PointCenter.X, loCC.PointCenter.Y - loCC.RadY);



                loPtX1 = loCS.Point2D(loPtX1);
                loPtX2 = loCS.Point2D(loPtX2);
                loPtY1 = loCS.Point2D(loPtY1);
                loPtY2 = loCS.Point2D(loPtY2);

                double lnWidth = Math.Abs(loPtX2.X - loPtX1.X);
                double lnHeight = Math.Abs(loPtY2.Y - loPtY1.Y);


                Ellipse loEllip = new Ellipse();
                loCC.AddCustomCirclePattern(loEllip);

                loEllip.Width = lnWidth;
                loEllip.Height = lnHeight;
                Canvas.SetTop(loEllip, loPtLoc.Y);
                Canvas.SetLeft(loEllip, loPtLoc.X);
                Canvas.SetZIndex(loEllip, -2);
                loCS.ChartCanvas.Children.Add(loEllip);

            }
        }

        private void PlotFigures(ChartStyle loCS)
        {
            foreach (CustomFigures loCF in gsFigureList)
            {
                Polygon loPolg = new Polygon();
                loCF.AddCustomFigurePattern(loPolg);

                foreach (Point loPt in loCF.Points)
                {
                    Point loPt_aux = new Point();
                    bool lbIsValid = false;

                    #region Define Point
                    switch (loCS.AxisStyle)
                    {
                        case ChartStyle.AxisStyleEnum.Linear:
                            loPt_aux = loPt;
                            lbIsValid = true;
                            break;
                        case ChartStyle.AxisStyleEnum.Logarithmic:
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt_aux = loCS.Point2DLogLog(loPt);
                                lbIsValid = true;
                            }
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogX:
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt_aux = loCS.Point2DLogX(loPt);
                                lbIsValid = true;
                            }
                            break;
                        case ChartStyle.AxisStyleEnum.SemiLogY:
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt_aux = loCS.Point2DLogY(loPt);
                                lbIsValid = true;
                            }
                            break;
                    }
                    #endregion

                    #region AddPoints
                    if (lbIsValid)
                    {
                        loPt_aux = loCS.Point2D(loPt);
                        loPolg.Points.Add(loPt_aux);
                    }
                    #endregion
                }
                if (loPolg.Points.Count >= 3)
                {
                    Canvas.SetZIndex(loPolg, -2);
                    loCS.ChartCanvas.Children.Add(loPolg);
                }
            }
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

        public bool ValidDouble(double d_ln)
        {
            if (double.IsNaN(d_ln))
                return false;
            if (double.IsInfinity(d_ln))
                return false;
            return true;
        }

        public void Clear()
        {
            cLineList_go.Clear();
            cAreaList_go.Clear();
            gsTagList.Clear();
            gsCirclesList.Clear();
            gsFigureList.Clear();
        }

        public void Add(CustomLine CLine_lo)
        {
            cLineList_go.Add(CLine_lo);
        }
        public void Add(CustomArea CArea_lo)
        {
            cAreaList_go.Add(CArea_lo);
        }
        public void Add(CustomTag loCTag)
        {
            gsTagList.Add(loCTag);
        }
        public void Add(params CustomTag[] lsCTag)
        {
            foreach (CustomTag loCT in lsCTag)
                gsTagList.Add(loCT);
        }

        public void Add(CustomCircles loCCirc)
        {
            gsCirclesList.Add(loCCirc);
        }
        public void Add(CustomFigures loCFig)
        {
            gsFigureList.Add(loCFig);
        }

        static public DoubleCollection GetPattern(CustomPatternEnum leCustPatt)
        {
            DoubleCollection loDoubColl;
            switch (leCustPatt)
            {
                case CustomPatternEnum.Solid:
                    loDoubColl = null;
                    break;
                case CustomPatternEnum.Dash:
                    loDoubColl = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case CustomPatternEnum.Dot:
                    loDoubColl = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case CustomPatternEnum.DashDot:
                    loDoubColl = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                default:
                    loDoubColl = null;
                    break;
            }
            return loDoubColl;

        }
        #endregion
    }

    public class CustomLine
    {
        #region Attributes
        internal Polyline cline_go;
        private Brush clineColor_go = Brushes.Red;
        private double clineThickness_gn = 2;
        private CustomPatternEnum clinePattern_ge = CustomPatternEnum.Solid;
        private CustomTypeEnum clineType_ge = CustomTypeEnum.Horizontal;
        private double xPosition_gn = double.NaN;
        private double yPosition_gn = double.NaN;
        private double xMincl_gn = double.NaN;
        private double xMaxcl_gn = double.NaN;
        private double yMincl_gn = double.NaN;
        private double yMaxcl_gn = double.NaN;
        private double yIntercept_gn = double.NaN;
        private double slopeCl_gn = double.NaN;
        #endregion


        #region Properties
        public Brush ClineColor
        {
            get { return clineColor_go; }
            set { clineColor_go = value; }
        }
        public double ClineThickness
        {
            get { return clineThickness_gn; }
            set { clineThickness_gn = value; }
        }
        public CustomPatternEnum ClinePattern
        {
            get { return clinePattern_ge; }
            set { clinePattern_ge = value; }
        }
        public CustomTypeEnum ClineType
        {
            get { return clineType_ge; }
            set { clineType_ge = value; }
        }
        public double XPosition
        {
            get { return xPosition_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Vertical)
                    xPosition_gn = value;
            }
        }
        public double YPosition
        {
            get { return yPosition_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Horizontal)
                    yPosition_gn = value;
            }
        }
        public double xMinCl
        {
            get { return xMincl_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Custom)
                    xMincl_gn = value;
            }
        }
        public double xMaxCl
        {
            get { return xMaxcl_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Custom)
                    xMaxcl_gn = value;
            }
        }
        public double yMinCl
        {
            get { return yMincl_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Custom)
                    yMincl_gn = value;
            }
        }
        public double yMaxCl
        {
            get { return yMaxcl_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Custom)
                    yMaxcl_gn = value;
            }
        }
        public double YIntercept
        {
            get { return yIntercept_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Custom)
                    yIntercept_gn = value;
            }
        }
        public double SlopeCL
        {
            get { return slopeCl_gn; }
            set
            {
                if (clineType_ge == CustomTypeEnum.Custom)
                    slopeCl_gn = value;
            }
        }
        #endregion

        #region Constructor
        public CustomLine()
        {
            cline_go = new Polyline();
        }
        #endregion

        #region Methods

        internal void AddCustomlinePattern()
        {
            cline_go.Stroke = clineColor_go;
            cline_go.StrokeThickness = clineThickness_gn;
            if (clinePattern_ge != CustomPatternEnum.Solid)
                cline_go.StrokeDashArray = Customs.GetPattern(clinePattern_ge);
        }
        internal void AddCustomLinePoints(Point pt1_lo, Point pt2_lo)
        {
            if (pt1_lo != null && pt2_lo != null)
            {
                cline_go.Points.Add(pt1_lo);
                cline_go.Points.Add(pt2_lo);
            }
        }
        #endregion
    }

    public class CustomArea
    {
        #region Attributes
        internal Polygon cArea_go;
        private Brush fillColor_go = Brushes.Beige;
        private Brush borderColor_go = Brushes.Transparent;
        private double borderThickness_gn = 2;
        private CustomTypeEnum cAreaType_ge = CustomTypeEnum.Vertical;
        private CustomPatternEnum borderPattern_ge = CustomPatternEnum.Solid;
        private double xStart_gn = double.NaN;
        private double xEnd_gn = double.NaN;
        private double yStart_gn = double.NaN;
        private double yEnd_gn = double.NaN;
        private Point startPoint_go;
        private Point endPoint_go;
        private double cAreaWidth_gn = double.NaN;
        private double cAreaHeight_gn = double.NaN;
        private double cAreaOpacity_gn = 0.5;
        #endregion

        #region Properties
        public Brush FillColor
        {
            get { return fillColor_go; }
            set { fillColor_go = value; }
        }
        public Brush BorderColor
        {
            get { return borderColor_go; }
            set { borderColor_go = value; }
        }
        public double BorderThickness
        {
            get { return borderThickness_gn; }
            set { borderThickness_gn = value; }
        }
        public CustomTypeEnum CAreaType
        {
            get { return cAreaType_ge; }
            set { cAreaType_ge = value; }
        }
        public CustomPatternEnum BorderPattern
        {
            get { return borderPattern_ge; }
            set { borderPattern_ge = value; }
        }
        public double XStart
        {
            get { return xStart_gn; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Vertical)
                    xStart_gn = value;
            }
        }
        public double XEnd
        {
            get { return xEnd_gn; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Vertical)
                    xEnd_gn = value;
            }
        }
        public double YStart
        {
            get { return yStart_gn; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Horizontal)
                    yStart_gn = value;
            }
        }
        public double YEnd
        {
            get { return yEnd_gn; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Horizontal)
                    yEnd_gn = value;
            }
        }
        public Point StartPoint
        {
            get { return startPoint_go; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Custom)
                    startPoint_go = value;
            }
        }
        public Point EndPoint
        {
            get { return endPoint_go; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Custom && startPoint_go != null)
                    endPoint_go = value;
            }
        }
        public double CAreaWidth
        {
            get { return cAreaWidth_gn; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Custom && startPoint_go != null)
                    cAreaWidth_gn = value;
            }
        }
        public double CAreaHeight
        {
            get { return cAreaHeight_gn; }
            set
            {
                if (cAreaType_ge == CustomTypeEnum.Custom && startPoint_go != null)
                    cAreaHeight_gn = value;
            }
        }
        public double CAreaOpacity
        {
            get { return cAreaOpacity_gn; }
            set { cAreaOpacity_gn = value; }
        }
        #endregion

        #region Constructor
        public CustomArea()
        {
            cArea_go = new Polygon();
        }
        #endregion

        #region Methods
        internal void AddCustomAreaPattern()
        {

            cArea_go.Fill = fillColor_go;
            cArea_go.Stroke = borderColor_go;
            cArea_go.StrokeThickness = borderThickness_gn;
            cArea_go.Opacity = cAreaOpacity_gn;
            if (borderPattern_ge != CustomPatternEnum.Solid)
                cArea_go.StrokeDashArray = Customs.GetPattern(borderPattern_ge);
        }
        #endregion

    }

    public class CustomTag
    {

        #region Attributes
        //private TextBlock goTextB;
        private string gsText = "null";
        private Point goPointLoc;

        private Brush goBack = Brushes.Transparent;
        private Brush goColorFont = Brushes.Black;
        private FontFamily goFontF = new FontFamily("Tahoma");
        private double gnFSieze = 12;
        private FontStyle goFStyle = FontStyles.Normal;
        private FontWeight goFWeight = FontWeights.Bold;
        private int gnZIndex = 0;
        #endregion

        #region Properties
        //public TextBlock TextB
        //{
        //    get { return goTextB; }
        //    set { goTextB = value; }
        //}
        public string Text
        {
            get { return gsText; }
            set { gsText = value; }
        }
        public Point PoitnLoc
        {
            get { return goPointLoc; }
            set { goPointLoc = value; }
        }

        public Brush BackColor
        {
            get { return goBack; }
            set { goBack = value; }
        }
        public Brush ColorFont
        {
            get { return goColorFont; }
            set { goColorFont = value; }
        }
        public FontFamily FontFam
        {
            get { return goFontF; }
            set { goFontF = value; }
        }
        public double FSieze
        {
            get { return gnFSieze; }
            set { gnFSieze = value; }
        }
        public FontStyle FStyle
        {
            get { return goFStyle; }
            set { goFStyle = value; }
        }
        public FontWeight FWeight
        {
            get { return goFWeight; }
            set { goFWeight = value; }
        }
        public int ZIdex
        {
            get { return gnZIndex; }
            set { gnZIndex = value; }
        }

        #endregion

        #region Constructor
        public CustomTag()
        {
            //goTextB = new TextBlock();
            goPointLoc = new Point(0, 0);
        }

        public CustomTag(string lsText, Point loPt)
        {

            //goTextB = new TextBlock();
            gsText = lsText;
            goPointLoc = loPt;
        }

        #endregion

        #region Methods
        internal void AddCustomeTagPattern(TextBlock loTB)
        {
            loTB.Text = gsText;
            loTB.Background = goBack;
            loTB.Foreground = goColorFont;
            loTB.FontFamily = goFontF;
            loTB.FontSize = gnFSieze;
            loTB.FontStyle = goFStyle;
            loTB.FontWeight = goFWeight;
            Canvas.SetZIndex(loTB, gnZIndex);
        }
        #endregion
    }

    public class CustomCircles
    {

        #region Attributes 
        //private Ellipse goEllip;
        private Brush goFillColor = Brushes.Transparent;
        private Brush goBorderColor = Brushes.DarkGray;
        private CustomPatternEnum geBorderPattern = CustomPatternEnum.Dash;
        private double gnBorderThk = 2;
        private double gnOpacity = 1;
        private Point goPtCenter;
        private double gnRadX;
        private double gnRadY;
        #endregion

        #region Properties
        //public Ellipse Circle
        //{
        //    get { return goEllip; }
        //    set { goEllip = value; }
        //}
        public Brush FillColor
        {
            get { return goFillColor; }
            set { goFillColor = value; }
        }
        public Brush BorderColor
        {
            get { return goBorderColor; }
            set { goBorderColor = value; }
        }
        public CustomPatternEnum BorderPattern
        {
            get { return geBorderPattern; }
            set { geBorderPattern = value; }
        }
        public double Borderthickness
        {
            get { return gnBorderThk; }
            set { gnBorderThk = value; }
        }
        public double Opacity
        {
            get { return gnOpacity; }
            set { gnOpacity = value; }
        }
        public Point PointCenter
        {
            get { return goPtCenter; }
            set { goPtCenter = value; }
        }
        public double RadX
        {
            get { return gnRadX; }
            set { gnRadX = value; }
        }
        public double RadY
        {
            get { return gnRadY; }
            set { gnRadY = value; }
        }
        #endregion

        #region Constructor
        public CustomCircles()
        {
            //goEllip = new Ellipse();
            goPtCenter = new Point(0, 0);
            gnRadX = 1;
            gnRadY = 1;
        }

        public CustomCircles(Point loPtLoc, double lnRadX, double lnRadY)
        {
            //goEllip = new Ellipse();
            //goEllip.Fill = Brushes.Green;
            goPtCenter = loPtLoc;
            gnRadX = lnRadX;
            gnRadY = lnRadY;
        }

        public CustomCircles(Point loPtLoc, double lnRad)
        {
            //goEllip = new Ellipse();
            //goEllip.Fill = Brushes.Green;
            goPtCenter = loPtLoc;
            gnRadX = lnRad;
            gnRadY = lnRad;
        }
        #endregion

        #region Methods
        internal void AddCustomCirclePattern(Ellipse loEllip)
        {
            loEllip.Fill = goFillColor;
            loEllip.Stroke = goBorderColor;
            loEllip.StrokeThickness = gnBorderThk;
            loEllip.Opacity = gnOpacity;
            if (geBorderPattern != CustomPatternEnum.Solid)
                loEllip.StrokeDashArray = Customs.GetPattern(geBorderPattern);
        }
        #endregion
    }

    public class CustomFigures
    {
        #region Attributes
        private PointCollection gsPoints;
        private Brush goFillColor = Brushes.Aqua;
        private Brush goBorderColor = Brushes.DarkBlue;
        private double gnThickBorder = 1;
        private double gnOpacity = 0.5;
        private CustomPatternEnum geBorderPattern = CustomPatternEnum.Solid;
        #endregion

        #region Properties
        public PointCollection Points
        {
            get { return gsPoints; }
            set { gsPoints = value; }
        }

        public Brush FillColor
        {
            get { return goFillColor; }
            set { goFillColor = value; }
        }

        public Brush BorderColor
        {
            get { return goBorderColor; }
            set { goBorderColor = value; }
        }

        public double BorderThickness
        {
            get { return gnThickBorder; }
            set { gnThickBorder = value; }
        }

        public double Opacity
        {
            get { return gnOpacity; }
            set { gnOpacity = value; }

        }
        public CustomPatternEnum BorderPattern
        {
            get { return geBorderPattern; }
            set { geBorderPattern = value; }
        }
        #endregion

        #region Constructor

        public CustomFigures()
        {
            gsPoints = new PointCollection();
        }

        public CustomFigures(PointCollection loPtCol)
        {
            gsPoints = loPtCol;
        }

        public CustomFigures(params Point[] lsPtList)
        {
            gsPoints = new PointCollection();
            if (lsPtList.Length >= 3)
                foreach (Point loPt in lsPtList)
                    gsPoints.Add(loPt);
        }
        #endregion

        #region Methods
        internal void AddCustomFigurePattern(Polygon loPol)
        {
            loPol.Fill = goFillColor;
            loPol.Stroke = goBorderColor;
            loPol.StrokeThickness = gnThickBorder;
            loPol.Opacity = gnOpacity;
            if (geBorderPattern != CustomPatternEnum.Solid)
                loPol.StrokeDashArray = Customs.GetPattern(geBorderPattern);

        }
        #endregion
    }


    #region Enumerations
    public enum CustomTypeEnum
    {
        Vertical = 1,
        Horizontal = 2,
        Custom = 3
    }

    public enum CustomPatternEnum
    {
        Solid = 1,
        Dash = 2,
        Dot = 3,
        DashDot = 4
    }
    #endregion

}
