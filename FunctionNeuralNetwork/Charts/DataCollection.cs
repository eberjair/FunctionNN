using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunctionNeuralNetwork.Charts
{
    public class DataCollection
    {
        #region Attributes
        private List<DataSeries> dataList;
        //Bar definitions
        private BarTypeEnum barType = BarTypeEnum.Vertical;
        //Stem Definitions
        private StemTypeEnum stemType = StemTypeEnum.Vertical;
        //Area definitions
        private AreaTypeEnum areaType = AreaTypeEnum.Vertical;

        private double xMaxCollection;
        private double xMinCollection;
        private double yMaxCollection;
        private double yMinCollection;
        private double xMinLog;
        private double yMinLog;
        private double xMaxAreaStack = double.NaN;
        private double xMinAreaStack = double.NaN;
        private double yMaxAreaStack = double.NaN;
        private double yMinAreaStack = double.NaN;
        private double xLogAreaStack = double.NaN;
        private double yLogAreaStack = double.NaN;
        private double xMaxBarStack = double.NaN;
        private double xMinBarStack = double.NaN;
        private double yMaxBarStack = double.NaN;
        private double yMinBarStack = double.NaN;
        private double xLogBarStack = double.NaN;
        private double yLogBarStack = double.NaN;

        private LogIrregularActionEnum logIrregularAction = LogIrregularActionEnum.NullAndContinue;

        private PointCollection PCollectionStackA_go;
        private PointCollection PCollectionStackB_go;

        #region Attributes of Reduction
        private int gnInt = 1;              //Defines a int for simplyNData, Lang
        private double gnEps = 2;           //Defines a double for RadialDist, PerpendicularDist, ReumanWitkam, DouglasPeucker
        private double gnEsp2 = 3;        //Defines a double for Opheim
        private ReductionEnum geRecutEnum = ReductionEnum.ReumanWitkam;


        #endregion

        #endregion

        #region Properties
        public List<DataSeries> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }
        public BarTypeEnum BarType
        {
            get { return barType; }
            set { barType = value; }
        }
        public StemTypeEnum StemType
        {
            get { return stemType; }
            set { stemType = value; }
        }
        public AreaTypeEnum AreaType
        {
            get { return areaType; }
            set { areaType = value; }
        }

        public double XMaxCollection
        { get { return xMaxCollection; } }
        public double XMinCollection
        { get { return xMinCollection; } }
        public double YMaxCollection
        { get { return yMaxCollection; } }
        public double YMinCollection
        { get { return yMinCollection; } }
        public double XMinLog
        { get { return xMinLog; } }
        public double YMinLog
        { get { return yMinLog; } }



        public LogIrregularActionEnum LogIrregularAction
        {
            get { return logIrregularAction; }
            set { logIrregularAction = value; }
        }
        #endregion

        #region Constructor
        public DataCollection()
        {
            dataList = new List<DataSeries>();
        }
        #endregion

        #region Methods


        #region // --------------------LINE METHODS---------------------------//
        private void PlotLines(List<int> LP, ChartStyle cs)
        {
            int nSeries = LP.Count;
            int j = 0;
            int k = 0;
            foreach (int ind in LP)
            {
                SetName(DataList[ind], nSeries, j);
                DataList[ind].PointsToPlot();
                DataList[ind].AddLinePattern();
                DataList[ind].ValidErrorList();
                double lineLength = DataList[ind].GetErrorLineLegth(cs);

                switch (cs.AxisStyle)
                {
                    case ChartStyle.AxisStyleEnum.Linear:
                        #region Linear
                        for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                        {
                            DataList[ind].LineSeries.Points[i] =
                                cs.Point2D(DataList[ind].LineSeries.Points[i]);
                            if (!cs.IsReductionActionOn)
                            {
                                DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                            }

                        }

                        if (cs.IsReductionActionOn)
                        {
                            #region Reduction Action
                            switch (geRecutEnum)
                            {
                                case ReductionEnum.SimplyNData:
                                    #region SimplyNData
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.SimplyNData(DataList[ind].LineSeries.Points, gnInt);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.RadialDistance:
                                    #region Radial Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.RadialDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.PerpendicularDistance:
                                    #region Perpendicular Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.PerpendicularDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.ReumanWitkam:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.ReumannWitkam(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Opheim:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.Opheim(DataList[ind].LineSeries.Points, gnEps, gnEsp2);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Lang:
                                    #region Lang
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.LangSimply(DataList[ind].LineSeries.Points, gnInt, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.DouglasPeucker:
                                    #region Douglas-Peucker
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.DouglasPeucker(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion
                        }

                        cs.ChartCanvas.Children.Add(DataList[ind].LineSeries);
                        j++;
                        break;
                    #endregion
                    case ChartStyle.AxisStyleEnum.Logarithmic:
                        #region Logarithmic
                        for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                        {
                            Point pt = DataList[ind].LineSeries.Points[i];
                            if (ValidPoint(pt, cs))
                            {
                                Point pt_aux = cs.Point2DLogLog(DataList[ind].LineSeries.Points[i]);
                                DataList[ind].LineSeries.Points[i] = cs.Point2D(pt_aux);
                                if (!cs.IsReductionActionOn)
                                {
                                    DataList[ind].AddErrorLine(cs, i + k, lineLength);
                                    DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i + k, ind);
                                }
                            }
                            else
                            {
                                DataList[ind].LineSeries.Points.RemoveAt(i);
                                i--;
                                k++;
                            }
                        }

                        if (cs.IsReductionActionOn)
                        {
                            #region Reduction Action
                            switch (geRecutEnum)
                            {
                                case ReductionEnum.SimplyNData:
                                    #region SimplyNData
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.SimplyNData(DataList[ind].LineSeries.Points, gnInt);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.RadialDistance:
                                    #region Radial Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.RadialDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.PerpendicularDistance:
                                    #region Perpendicular Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.PerpendicularDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.ReumanWitkam:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.ReumannWitkam(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Opheim:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.Opheim(DataList[ind].LineSeries.Points, gnEps, gnEsp2);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Lang:
                                    #region Lang
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.LangSimply(DataList[ind].LineSeries.Points, gnInt, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.DouglasPeucker:
                                    #region Douglas-Peucker
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.DouglasPeucker(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion
                        }
                        cs.ChartCanvas.Children.Add(DataList[ind].LineSeries);
                        j++;
                        break;
                    #endregion
                    case ChartStyle.AxisStyleEnum.SemiLogX:
                        #region SemilogX
                        for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                        {
                            Point pt = DataList[ind].LineSeries.Points[i];
                            if (ValidPoint(pt, cs))
                            {
                                Point pt_aux = cs.Point2DLogX(DataList[ind].LineSeries.Points[i]);
                                DataList[ind].LineSeries.Points[i] = cs.Point2D(pt_aux);
                                if (!cs.IsReductionActionOn)
                                {
                                    DataList[ind].AddErrorLine(cs, i + k, lineLength);
                                    DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i + k, ind);
                                }
                            }
                            else
                            {
                                DataList[ind].LineSeries.Points.RemoveAt(i);
                                i--;
                                k++;
                            }
                        }
                        if (cs.IsReductionActionOn)
                        {
                            #region Reduction Action
                            switch (geRecutEnum)
                            {
                                case ReductionEnum.SimplyNData:
                                    #region SimplyNData
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.SimplyNData(DataList[ind].LineSeries.Points, gnInt);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.RadialDistance:
                                    #region Radial Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.RadialDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.PerpendicularDistance:
                                    #region Perpendicular Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.PerpendicularDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.ReumanWitkam:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.ReumannWitkam(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Opheim:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.Opheim(DataList[ind].LineSeries.Points, gnEps, gnEsp2);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Lang:
                                    #region Lang
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.LangSimply(DataList[ind].LineSeries.Points, gnInt, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.DouglasPeucker:
                                    #region Douglas-Peucker
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.DouglasPeucker(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion
                        }

                        cs.ChartCanvas.Children.Add(DataList[ind].LineSeries);
                        j++;
                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.SemiLogY:
                        #region SemilogY
                        for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                        {
                            Point pt = DataList[ind].LineSeries.Points[i];
                            if (ValidPoint(pt, cs))
                            {
                                Point pt_aux = cs.Point2DLogY(DataList[ind].LineSeries.Points[i]);
                                DataList[ind].LineSeries.Points[i] = cs.Point2D(pt_aux);
                                if (!cs.IsReductionActionOn)
                                {
                                    DataList[ind].AddErrorLine(cs, i + k, lineLength);
                                    DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i + k, ind);
                                }
                            }
                            else
                            {
                                DataList[ind].LineSeries.Points.RemoveAt(i);
                                i--;
                                k++;
                            }
                        }

                        if (cs.IsReductionActionOn)
                        {
                            #region Reduction Action
                            switch (geRecutEnum)
                            {
                                case ReductionEnum.SimplyNData:
                                    #region SimplyNData
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.SimplyNData(DataList[ind].LineSeries.Points, gnInt);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.RadialDistance:
                                    #region Radial Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.RadialDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.PerpendicularDistance:
                                    #region Perpendicular Distance
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.PerpendicularDist(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.ReumanWitkam:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.ReumannWitkam(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Opheim:
                                    #region Reuman-Witkam
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.Opheim(DataList[ind].LineSeries.Points, gnEps, gnEsp2);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.Lang:
                                    #region Lang
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.LangSimply(DataList[ind].LineSeries.Points, gnInt, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                                case ReductionEnum.DouglasPeucker:
                                    #region Douglas-Peucker
                                    DataList[ind].LineSeries.Points = SimplyPointCollection.DouglasPeucker(DataList[ind].LineSeries.Points, gnEps);
                                    for (int i = 0; i < DataList[ind].LineSeries.Points.Count; i++)
                                    {
                                        DataList[ind].AddErrorLine(cs, i, lineLength);                  //Prueba
                                        DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, DataList[ind].LineSeries.Points[i], i, ind);
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion
                        }
                        cs.ChartCanvas.Children.Add(DataList[ind].LineSeries);
                        j++;
                        #endregion
                        break;
                    default:
                        break;
                }
                k = 0;
            }
        }

        #endregion

        #region //-----------------BAR METHODS-----------------------//
        public enum BarTypeEnum
        {
            Vertical = 0,
            Horizontal = 1,
            VerticalStack = 2,      //Sum of values
            HorizontalStack = 3,    //Sum of values
            VerticalOverlay = 4,
            HorizontalOverlay = 5,
            RoundedVertical = 6 //PBO 310314
        }

        //Dibuja una barra vertical con esquinas redondeadas
        public void Draw1RoundedVerticalBar2(Point pt, ChartStyle cs, DataSeries ds, double width, double y)
        {
            double lnAngle = 7;
            if (pt.Y == 0)
            {
                return;
            }

            double x = pt.X;
            Point pt1 = cs.Point2D(new Point(x - width / 2, y));
            Point pt3 = cs.Point2D(new Point(x + width / 2, y + pt.Y));
            Rect rec = new Rect(pt1, pt3);

            Color loStartColor = ((SolidColorBrush)ds.FillColor).Color;
            Color loEndColor = loStartColor;
            loEndColor.A = (byte)(loStartColor.A / 2);
            LinearGradientBrush linGrBrush1 = new LinearGradientBrush(loStartColor, loEndColor, 90);

            MyVisualHost visualHost = new MyVisualHost();
            visualHost.AddChildren(visualHost.DrawRoundedRectangle(rec, linGrBrush1, lnAngle, lnAngle, 0, 0));
            cs.ChartCanvas.Children.Add(visualHost);
        }

        public void Draw1VerticalBar(Point pt, ChartStyle cs, DataSeries ds, double width, double y)
        {
            Polygon plg = new Polygon();
            BorderBarPattern(plg, ds);

            double x = pt.X;
            plg.Points.Add(cs.Point2D(new Point(x - width / 2, y)));
            plg.Points.Add(cs.Point2D(new Point(x + width / 2, y)));
            plg.Points.Add(cs.Point2D(new Point(x + width / 2, y + pt.Y)));
            plg.Points.Add(cs.Point2D(new Point(x - width / 2, y + pt.Y)));
            cs.ChartCanvas.Children.Add(plg);
        }

        private void DrawVerticalBars(Point pt, ChartStyle cs, DataSeries ds, int nSeries, int n)
        {
            Polygon plg = new Polygon();
            BorderBarPattern(plg, ds);

            double width = 0.7 * cs.XTick;
            double w1 = width / nSeries;
            double w = ds.BarWidth * w1;
            double space = (w1 - w) / 2;
            double x = pt.X - 0.5 * cs.XTick;
            plg.Points.Add(cs.Point2D(new Point(x - width / 2 + space + n * w1, 0)));
            plg.Points.Add(cs.Point2D(new Point(x - width / 2 + space + n * w1 + w, 0)));
            plg.Points.Add(cs.Point2D(new Point(x - width / 2 + space + n * w1 + w, pt.Y)));
            plg.Points.Add(cs.Point2D(new Point(x - width / 2 + space + n * w1, pt.Y)));
            cs.ChartCanvas.Children.Add(plg);
        }
        private void Draw1HorizontalBar(Point pt, ChartStyle cs, DataSeries ds, double width, double x)
        {
            Polygon plg = new Polygon();
            BorderBarPattern(plg, ds);

            double y = pt.Y;
            plg.Points.Add(cs.Point2D(new Point(x, y - width / 2)));
            plg.Points.Add(cs.Point2D(new Point(x, y + width / 2)));
            plg.Points.Add(cs.Point2D(new Point(x + pt.X, y + width / 2)));
            plg.Points.Add(cs.Point2D(new Point(x + pt.X, y - width / 2)));
            cs.ChartCanvas.Children.Add(plg);
        }
        private void DrawHorizontalBars(Point pt, ChartStyle cs, DataSeries ds, int nSeries, int n)
        {
            Polygon plg = new Polygon();
            BorderBarPattern(plg, ds);

            double width = 0.7 * cs.YTick;
            double w1 = width / nSeries;
            double w = ds.BarWidth * w1;
            double space = (w1 - w) / 2;
            double y = pt.Y - 0.5 * cs.YTick;
            plg.Points.Add(cs.Point2D(new Point(0, y - width / 2 + space + n * w1)));
            plg.Points.Add(cs.Point2D(new Point(0, y - width / 2 + space + n * w1 + w)));
            plg.Points.Add(cs.Point2D(new Point(pt.X, y - width / 2 + space + n * w1 + w)));
            plg.Points.Add(cs.Point2D(new Point(pt.X, y - width / 2 + space + n * w1)));
            cs.ChartCanvas.Children.Add(plg);
        }
        private void BorderBarPattern(Polygon plg, DataSeries ds)
        {
            plg.Fill = ds.FillColor;
            plg.Stroke = ds.BorderColor;
            plg.StrokeThickness = ds.BorderThickness;
            plg.Opacity = ds.AreaOpacity;
            switch (ds.LinePattern)
            {
                case DataSeries.LinePatternEnum.Dash:
                    plg.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case DataSeries.LinePatternEnum.Dot:
                    plg.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case DataSeries.LinePatternEnum.DashDot:
                    plg.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case DataSeries.LinePatternEnum.None:
                    plg.Stroke = Brushes.Transparent;
                    break;
            }
        }
        private void PlotBars(List<int> BP, ChartStyle cs)
        {
            int nSeries = BP.Count;
            int firstBarInd = BP[0];
            double width = 1;
            int j = 0;

            if (nSeries == 1)
            {
                #region nSeries == 1
                foreach (int ind in BP)
                {
                    SetName(DataList[ind], nSeries, j);
                    #region Set Bar width
                    if (BarType == BarTypeEnum.Vertical || BarType == BarTypeEnum.VerticalOverlay || BarType == BarTypeEnum.RoundedVertical
                        || BarType == BarTypeEnum.VerticalStack)
                        width = cs.XTick * DataList[ind].BarWidth / 2;
                    else
                        width = cs.YTick * DataList[ind].BarWidth / 2;
                    #endregion

                    for (int i = 0; i < DataList[ind].Datapoints.Count; i++)
                    {
                        Point pt = DataList[ind].Datapoints[i];
                        #region AxisStyle Linear
                        if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear)
                        {
                            if (BarType == BarTypeEnum.Vertical || BarType == BarTypeEnum.VerticalOverlay
                                || BarType == BarTypeEnum.VerticalStack)
                                Draw1VerticalBar(pt, cs, DataList[ind], width, 0);
                            else if (BarType == BarTypeEnum.RoundedVertical)
                                Draw1RoundedVerticalBar2(pt, cs, DataList[ind], width, 0);
                            else
                                Draw1HorizontalBar(pt, cs, DataList[ind], width, 0);
                        }
                        #endregion
                        #region AxisStyle Log or SemiLog
                        else
                        {
                            if (ValidPoint(pt, cs))
                            {
                                switch (cs.AxisStyle)
                                {
                                    case ChartStyle.AxisStyleEnum.Logarithmic:
                                        pt = cs.Point2DLogLog(pt);
                                        break;
                                    case ChartStyle.AxisStyleEnum.SemiLogX:
                                        pt = cs.Point2DLogX(pt);
                                        break;
                                    case ChartStyle.AxisStyleEnum.SemiLogY:
                                        pt = cs.Point2DLogY(pt);
                                        break;
                                }
                                if (BarType == BarTypeEnum.Vertical || BarType == BarTypeEnum.VerticalOverlay
                                    || BarType == BarTypeEnum.VerticalStack)
                                    Draw1VerticalBar(pt, cs, DataList[ind], width, 0);
                                else if (BarType == BarTypeEnum.RoundedVertical)
                                    Draw1RoundedVerticalBar2(pt, cs, DataList[ind], width, 0);
                                else
                                    Draw1HorizontalBar(pt, cs, DataList[ind], width, 0);
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
            else
            {
                #region nSeries > 1
                foreach (int ind in BP)
                {
                    SetName(DataList[ind], nSeries, j);
                    #region Set Bar Width
                    switch (BarType)
                    {
                        case BarTypeEnum.Vertical:
                            break;
                        case BarTypeEnum.Horizontal:
                            break;
                        case BarTypeEnum.VerticalStack:
                            width = cs.XTick * DataList[ind].BarWidth / 2;
                            break;
                        case BarTypeEnum.HorizontalStack:
                            width = cs.YTick * DataList[ind].BarWidth / 2;
                            break;
                        case BarTypeEnum.VerticalOverlay:
                            width = cs.XTick * DataList[ind].BarWidth / 2;
                            width = width / Math.Pow(2, j);
                            break;
                        case BarTypeEnum.HorizontalOverlay:
                            width = cs.YTick * DataList[ind].BarWidth / 2;
                            width = width / Math.Pow(2, j);
                            break;
                    }
                    #endregion

                    for (int i = 0; i < DataList[ind].Datapoints.Count; i++)
                    {
                        Point pt = DataList[ind].Datapoints[i];
                        #region AxisStyle Linear
                        if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear)
                        {
                            switch (BarType)
                            {
                                case BarTypeEnum.Vertical:
                                    DrawVerticalBars(pt, cs, DataList[ind], nSeries, j);
                                    break;
                                case BarTypeEnum.VerticalOverlay:
                                    Draw1VerticalBar(pt, cs, DataList[ind], width, 0);
                                    break;
                                case BarTypeEnum.VerticalStack:
                                    Draw1VerticalBar(DataList[ind].Datapoints[i], cs, DataList[ind], width, 0);
                                    break;
                                case BarTypeEnum.Horizontal:
                                    DrawHorizontalBars(pt, cs, DataList[ind], nSeries, j);
                                    break;
                                case BarTypeEnum.HorizontalOverlay:
                                    Draw1HorizontalBar(pt, cs, DataList[ind], width, 0);
                                    break;
                                case BarTypeEnum.HorizontalStack:
                                    Draw1HorizontalBar(DataList[ind].Datapoints[i], cs, DataList[ind], width, 0);
                                    break;
                                case BarTypeEnum.RoundedVertical:
                                    Draw1RoundedVerticalBar2(DataList[ind].Datapoints[i], cs, DataList[ind], width, 0);
                                    break;
                            }
                        }
                        #endregion
                        #region Axis Style Log or SemiLog
                        else
                        {
                            #region ValidPoint
                            if (ValidPoint(pt, cs))
                            {
                                switch (cs.AxisStyle)
                                {
                                    case ChartStyle.AxisStyleEnum.Logarithmic:
                                        pt = cs.Point2DLogLog(pt);
                                        break;
                                    case ChartStyle.AxisStyleEnum.SemiLogX:
                                        pt = cs.Point2DLogX(pt);
                                        break;
                                    case ChartStyle.AxisStyleEnum.SemiLogY:
                                        pt = cs.Point2DLogY(pt);
                                        break;
                                }
                                #endregion
                                #region DrawBars
                                switch (BarType)
                                {
                                    case BarTypeEnum.Vertical:
                                        DrawVerticalBars(pt, cs, DataList[ind], nSeries, j);
                                        break;
                                    case BarTypeEnum.VerticalOverlay:
                                        Draw1VerticalBar(pt, cs, DataList[ind], width, 0);
                                        break;
                                    case BarTypeEnum.VerticalStack:
                                        Draw1VerticalBar(pt, cs, DataList[ind], width, 0);
                                        break;
                                    case BarTypeEnum.Horizontal:
                                        DrawHorizontalBars(pt, cs, DataList[ind], nSeries, j);
                                        break;
                                    case BarTypeEnum.HorizontalOverlay:
                                        Draw1HorizontalBar(pt, cs, DataList[ind], width, 0);
                                        break;
                                    case BarTypeEnum.HorizontalStack:
                                        Draw1HorizontalBar(pt, cs, DataList[ind], width, 0);
                                        break;
                                    case BarTypeEnum.RoundedVertical:
                                        Draw1RoundedVerticalBar2(pt, cs, DataList[ind], nSeries, j); //?? check
                                        break;
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    j++;
                }
                #endregion
            }
        }

        #endregion


        #region //---------------STEM METHODS---------------------------------------//
        public enum StemTypeEnum
        {
            Vertical = 0,
            Horizontal = 1
        }
        private void PlotStem(List<int> SP, ChartStyle cs)
        {
            int nSeries = SP.Count;
            int j = 0;
            int k = 0;
            foreach (int ind in SP)
            {
                SetName(DataList[ind], nSeries, j);
                Point[] pts = new Point[2];
                for (int i = 0; i < DataList[ind].Datapoints.Count; i++)
                {
                    if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear)
                    {
                        if (StemType == StemTypeEnum.Vertical)
                        {
                            pts[0] = cs.Point2D(new Point(DataList[ind].Datapoints[i].X, 0));
                            pts[1] = cs.Point2D(DataList[ind].Datapoints[i]);
                        }
                        else
                        {
                            pts[0] = cs.Point2D(new Point(0, DataList[ind].Datapoints[i].Y));
                            pts[1] = cs.Point2D(DataList[ind].Datapoints[i]);
                        }
                    }
                    else
                    {
                        Point pt = DataList[ind].Datapoints[i];
                        if (ValidPoint(pt, cs))
                        {
                            Point pt_aux = new Point();
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    pt_aux = cs.Point2DLogLog(pt);
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    pt_aux = cs.Point2DLogX(pt);
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    pt_aux = cs.Point2DLogY(pt);
                                    break;
                            }
                            if (StemType == StemTypeEnum.Vertical)
                            {
                                pts[0] = cs.Point2D(new Point(pt_aux.X, 0));
                                pts[1] = cs.Point2D(pt_aux);
                            }
                            else
                            {
                                pts[0] = cs.Point2D(new Point(0, pt_aux.Y));
                                pts[1] = cs.Point2D(pt_aux);
                            }
                        }
                    }
                    Line line = new Line();
                    line.Stroke = DataList[ind].LineColor;
                    line.StrokeThickness = DataList[ind].LineThickness;
                    line.X1 = pts[0].X;
                    line.Y1 = pts[0].Y;
                    line.X2 = pts[1].X;
                    line.Y2 = pts[1].Y;
                    cs.ChartCanvas.Children.Add(line);
                    DataList[ind].Symbols.AddSymbol(cs.ChartCanvas, pts[1], i + k, ind);
                }
                j++;
                k = 0;
            }
        }
        #endregion


        #region //-------------STAIR METHODS---------------------------------------//
        private void PlotStairs(List<int> StP, ChartStyle cs)
        {
            int nSeries = StP.Count;
            int j = 0;
            foreach (int ind in StP)
            {
                SetName(DataList[ind], nSeries, j);
                List<Point> ptList = new List<Point>();
                Point[] pts = new Point[2];
                DataList[ind].AddLinePattern();
                DataList[ind].PointsToPlot();
                for (int i = 0; i < DataList[ind].LineSeries.Points.Count - 1; i++)
                {
                    if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear)
                    {
                        pts[0] = DataList[ind].LineSeries.Points[i];
                        pts[1] = DataList[ind].LineSeries.Points[i + 1];
                        ptList.Add(pts[0]);
                        ptList.Add(new Point(pts[1].X, pts[0].Y));
                    }
                    else
                    {
                        Point pt1 = DataList[ind].LineSeries.Points[i];
                        Point pt2 = DataList[ind].LineSeries.Points[i + 1];
                        if (ValidPoint(pt1, cs) && ValidPoint(pt2, cs))
                        {
                            switch (cs.AxisStyle)
                            {
                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                    pts[0] = cs.Point2DLogLog(pt1);
                                    pts[1] = cs.Point2DLogLog(pt2);
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                    pts[0] = cs.Point2DLogX(pt1);
                                    pts[1] = cs.Point2DLogX(pt2);
                                    break;
                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                    pts[0] = cs.Point2DLogY(pt1);
                                    pts[1] = cs.Point2DLogY(pt2);
                                    break;
                            }
                            ptList.Add(pts[0]);
                            ptList.Add(new Point(pts[1].X, pts[0].Y));
                        }
                    }
                }
                ptList.Add(new Point(pts[1].X, pts[0].Y));
                DataList[ind].LineSeries.Points.Clear();
                for (int i = 0; i < ptList.Count; i++)
                {
                    DataList[ind].LineSeries.Points.Add(cs.Point2D(ptList[i]));
                }
                cs.ChartCanvas.Children.Add(DataList[ind].LineSeries);
                j++;
            }

        }
        #endregion

        #region //-------------AREA METHODS----------------------------------------//
        public enum AreaTypeEnum
        {
            Vertical = 0,
            Horizontal = 1,
            VerticalStack = 2,        //Sum of values
            HorizontalStack = 3       //Sum of values

        }
        private void PlotArea(List<int> AP, ChartStyle cs)
        {
            int nSeries = AP.Count;
            int j = 0;

            PointCollection PCollA_aux = new PointCollection();
            bool IsPCollA_auxNull = true;

            if (AreaType == AreaTypeEnum.Horizontal || AreaType == AreaTypeEnum.Vertical)
            {
                #region Horizontal or Vertical
                foreach (int ind in AP)
                {
                    SetName(DataList[ind], nSeries, j);
                    Point pt1 = new Point();
                    Point pt2 = new Point();
                    DataList[ind].PointstoArea();
                    DataList[ind].AddAreaPattern();
                    DataList[ind].AreaSeries.FillRule = FillRule.Nonzero;   //-------------prueba

                    int nPoints = DataList[ind].AreaSeries.Points.Count;

                    switch (AreaType)
                    {
                        case AreaTypeEnum.Vertical:
                            #region Vertical Area
                            if (cs.AxisStyle != ChartStyle.AxisStyleEnum.Linear)
                            {
                                for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                {
                                    Point pt = DataList[ind].AreaSeries.Points[i];
                                    if (ValidPoint(pt, cs))
                                    {
                                        #region Mapping Points to Logarithmic System
                                        Point pt_aux = new Point();
                                        switch (cs.AxisStyle)
                                        {
                                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                                pt_aux = cs.Point2DLogLog(pt);
                                                break;
                                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                                pt_aux = cs.Point2DLogX(pt);
                                                break;
                                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                                pt_aux = cs.Point2DLogY(pt);
                                                break;
                                        }
                                        DataList[ind].AreaSeries.Points[i] = pt_aux;
                                        #endregion
                                    }
                                    else
                                    {
                                        #region LogIrregularAction
                                        switch (logIrregularAction)
                                        {
                                            case LogIrregularActionEnum.NullAndContinue:
                                                DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                i--;
                                                break;
                                            case LogIrregularActionEnum.GoesToZero:
                                                Point pt_aux1 = new Point();
                                                pt_aux1 = DataList[ind].AreaSeries.Points[i];

                                                switch (cs.AxisStyle)
                                                {
                                                    case ChartStyle.AxisStyleEnum.Logarithmic:
                                                        if (pt_aux1.Y <= 0 && pt_aux1.X > 0)
                                                        {
                                                            if (DataList[ind].AreaOrigin > 0)
                                                                pt_aux1.Y = DataList[ind].AreaOrigin;
                                                            else
                                                                pt_aux1.Y = 1;
                                                            pt_aux1 = cs.Point2DLogLog(pt_aux1);
                                                            DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                        }
                                                        else
                                                        {
                                                            DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                            i--;
                                                        }
                                                        break;
                                                    case ChartStyle.AxisStyleEnum.SemiLogX:
                                                        DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                        i--;
                                                        break;
                                                    case ChartStyle.AxisStyleEnum.SemiLogY:
                                                        if (DataList[ind].AreaOrigin > 0)
                                                            pt_aux1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                                        else
                                                            pt_aux1.Y = 0;
                                                        DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                        break;
                                                }
                                                break;
                                            case LogIrregularActionEnum.SplitGraph:
                                                break;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            nPoints = DataList[ind].AreaSeries.Points.Count;
                            #region Locking Points
                            if (nPoints != 0)
                            {
                                pt1.X = DataList[ind].AreaSeries.Points[0].X;
                                pt2.X = DataList[ind].AreaSeries.Points[nPoints - 1].X;

                                if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear || cs.AxisStyle == ChartStyle.AxisStyleEnum.SemiLogX)
                                {
                                    pt1.Y = DataList[ind].AreaOrigin;
                                    pt2.Y = DataList[ind].AreaOrigin;
                                }
                                else if (DataList[ind].AreaOrigin > 0)
                                {
                                    pt1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                    pt2.Y = Math.Log10(DataList[ind].AreaOrigin);
                                }
                                else
                                {
                                    pt1.Y = 0;
                                    pt2.Y = 0;
                                }
                            }
                            #endregion
                            #endregion
                            break;

                        case AreaTypeEnum.Horizontal:
                            #region Horizontal Area
                            if (cs.AxisStyle != ChartStyle.AxisStyleEnum.Linear)
                            {
                                for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                {
                                    Point pt = DataList[ind].AreaSeries.Points[i];
                                    if (ValidPoint(pt, cs))
                                    {
                                        #region Maping Points to Logarithmic System
                                        Point pt_aux = new Point();
                                        switch (cs.AxisStyle)
                                        {
                                            case ChartStyle.AxisStyleEnum.Logarithmic:
                                                pt_aux = cs.Point2DLogLog(pt);
                                                break;
                                            case ChartStyle.AxisStyleEnum.SemiLogX:
                                                pt_aux = cs.Point2DLogX(pt);
                                                break;
                                            case ChartStyle.AxisStyleEnum.SemiLogY:
                                                pt_aux = cs.Point2DLogY(pt);
                                                break;
                                        }
                                        DataList[ind].AreaSeries.Points[i] = pt_aux;
                                        #endregion
                                    }
                                    else
                                    {
                                        #region LogIrregularAction
                                        switch (logIrregularAction)
                                        {
                                            case LogIrregularActionEnum.NullAndContinue:
                                                DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                i--;
                                                break;
                                            case LogIrregularActionEnum.GoesToZero:
                                                Point pt_aux1 = new Point();
                                                pt_aux1 = DataList[ind].AreaSeries.Points[i];
                                                switch (cs.AxisStyle)
                                                {
                                                    case ChartStyle.AxisStyleEnum.Logarithmic:
                                                        if (pt_aux1.X <= 0 && pt_aux1.Y > 0)
                                                        {
                                                            if (DataList[ind].AreaOrigin > 0)
                                                                pt_aux1.X = DataList[ind].AreaOrigin;
                                                            else
                                                                pt_aux1.X = 1;
                                                            pt_aux1 = cs.Point2DLogLog(pt_aux1);
                                                            DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                        }
                                                        else
                                                        {
                                                            DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                            i--;
                                                        }
                                                        break;
                                                    case ChartStyle.AxisStyleEnum.SemiLogX:
                                                        if (DataList[ind].AreaOrigin > 0)
                                                            pt_aux1.X = Math.Log10(DataList[ind].AreaOrigin);
                                                        else
                                                            pt_aux1.X = 0;
                                                        DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                        break;
                                                    case ChartStyle.AxisStyleEnum.SemiLogY:
                                                        DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                        i--;
                                                        break;
                                                }
                                                break;
                                            case LogIrregularActionEnum.SplitGraph:
                                                break;
                                        }
                                        #endregion

                                    }
                                }
                            }
                            nPoints = DataList[ind].AreaSeries.Points.Count;
                            #region Locking Points
                            if (nPoints != 0)
                            {
                                pt1.Y = DataList[ind].AreaSeries.Points[0].Y;
                                pt2.Y = DataList[ind].AreaSeries.Points[nPoints - 1].Y;

                                if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear || cs.AxisStyle == ChartStyle.AxisStyleEnum.SemiLogY)
                                {
                                    pt1.X = DataList[ind].AreaOrigin;
                                    pt2.X = DataList[ind].AreaOrigin;
                                }
                                else if (DataList[ind].AreaOrigin > 0)
                                {
                                    pt1.X = Math.Log10(DataList[ind].AreaOrigin);
                                    pt2.X = Math.Log10(DataList[ind].AreaOrigin);
                                }
                                else
                                {
                                    pt1.X = 0;
                                    pt2.X = 0;
                                }
                            }
                            #endregion
                            #endregion
                            break;
                    }
                    DataList[ind].AreaSeries.Points.Insert(0, pt1);
                    DataList[ind].AreaSeries.Points.Add(pt2);
                    nPoints += 2;
                    for (int i = 0; i < nPoints; i++)
                    {
                        DataList[ind].AreaSeries.Points[i] = cs.Point2D(DataList[ind].AreaSeries.Points[i]);
                    }
                    cs.ChartCanvas.Children.Add(DataList[ind].AreaSeries);

                    j++;

                }
                #endregion
            }
            else
            {
                List<int> IndListOrigin;
                switch (AreaType)
                {
                    case AreaTypeEnum.VerticalStack:
                        if (PCollectionStackA_go != null)
                        {
                            int IndCase = 1;
                            foreach (int ind in AP)
                            {
                                if (!PerfectAreaStack(PCollectionStackA_go, DataList[ind].Datapoints, 'x'))
                                {
                                    IndCase = 2;
                                    break;
                                }
                            }

                            foreach (int ind in AP)
                            {
                                Point pt1 = new Point();
                                Point pt2 = new Point();
                                #region AddAreaPattern
                                SetName(DataList[ind], nSeries, j);
                                DataList[ind].PointstoArea();
                                DataList[ind].AddAreaPattern();
                                DataList[ind].AreaSeries.FillRule = FillRule.Nonzero;
                                #endregion

                                switch (IndCase)
                                {
                                    case 1:
                                        #region Get PCollA_aux
                                        if (j == 0 || IsPCollA_auxNull)
                                        {
                                            PCollA_aux = new PointCollection();
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt_aux2 = new Point();
                                                pt_aux2 = DataList[ind].AreaSeries.Points[i];
                                                PCollA_aux.Add(pt_aux2);
                                            }
                                            if (PCollA_aux.Count != 0)
                                                IsPCollA_auxNull = false;
                                            Canvas.SetZIndex(dataList[ind].AreaSeries, nSeries);
                                        }
                                        #endregion
                                        #region Addition earlier data
                                        if (!IsPCollA_auxNull && j != 0)
                                        {
                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_aux = PCollA_aux[i];
                                                Point pt2_aux = DataList[ind].AreaSeries.Points[i];
                                                if (pt1_aux.X == pt2_aux.X)
                                                {
                                                    pt2_aux.Y = pt1_aux.Y + pt2_aux.Y;
                                                    DataList[ind].AreaSeries.Points[i] = pt2_aux;
                                                }
                                            }
                                            Canvas.SetZIndex(DataList[ind].AreaSeries, nSeries - j);

                                            PCollA_aux.Clear();
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt_aux3 = new Point();
                                                pt_aux3 = DataList[ind].AreaSeries.Points[i];
                                                PCollA_aux.Add(pt_aux3);
                                            }

                                        }
                                        #endregion
                                        #region Mapping
                                        if (cs.AxisStyle != ChartStyle.AxisStyleEnum.Linear)
                                        {
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt = DataList[ind].AreaSeries.Points[i];
                                                if (ValidPoint(pt, cs))
                                                {
                                                    #region Mapping Points to Logarithmic System
                                                    Point pt_aux = new Point();
                                                    switch (cs.AxisStyle)
                                                    {
                                                        case ChartStyle.AxisStyleEnum.Logarithmic:
                                                            pt_aux = cs.Point2DLogLog(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogX:
                                                            pt_aux = cs.Point2DLogX(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogY:
                                                            pt_aux = cs.Point2DLogY(pt);
                                                            break;
                                                    }
                                                    DataList[ind].AreaSeries.Points[i] = pt_aux;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region LogIrregularAction
                                                    switch (logIrregularAction)
                                                    {
                                                        case LogIrregularActionEnum.NullAndContinue:
                                                            DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                            i--;
                                                            break;
                                                        case LogIrregularActionEnum.GoesToZero:
                                                            Point pt_aux1 = new Point();
                                                            pt_aux1 = DataList[ind].AreaSeries.Points[i];

                                                            switch (cs.AxisStyle)
                                                            {
                                                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                                                    if (pt_aux1.Y <= 0 && pt_aux1.X > 0)
                                                                    {
                                                                        if (DataList[ind].AreaOrigin > 0)
                                                                            pt_aux1.Y = DataList[ind].AreaOrigin;
                                                                        else
                                                                            pt_aux1.Y = 1;
                                                                        pt_aux1 = cs.Point2DLogLog(pt_aux1);
                                                                        DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    }
                                                                    else
                                                                    {
                                                                        DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                        i--;
                                                                    }
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                                                    DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                    i--;
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                                                    if (DataList[ind].AreaOrigin > 0)
                                                                        pt_aux1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                                                    else
                                                                        pt_aux1.Y = 0;
                                                                    DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    break;
                                                            }
                                                            break;
                                                        case LogIrregularActionEnum.SplitGraph:
                                                            break;
                                                    }
                                                    #endregion
                                                }
                                            }

                                        }
                                        #endregion
                                        break;
                                    case 2:
                                        IndListOrigin = new List<int>();
                                        #region GetPCollA_aux
                                        if (j == 0 || IsPCollA_auxNull)
                                        {
                                            PCollA_aux = new PointCollection();
                                            for (int i = 0; i < PCollectionStackA_go.Count; i++)
                                            {
                                                double x1_ln = PCollectionStackA_go[i].X;
                                                bool isInSerie = false;
                                                for (int k = 0; k < DataList[ind].AreaSeries.Points.Count; k++)
                                                {
                                                    double x2_ln = DataList[ind].AreaSeries.Points[k].X;
                                                    if (x1_ln == x2_ln)
                                                    {
                                                        Point pt1aux_lo = new Point(x1_ln, DataList[ind].AreaSeries.Points[k].Y);
                                                        PCollA_aux.Add(pt1aux_lo);
                                                        isInSerie = true;
                                                        break;
                                                    }
                                                }
                                                if (!isInSerie)
                                                {
                                                    IndListOrigin.Add(i);
                                                    PCollA_aux.Add(new Point(x1_ln, 0));
                                                }
                                            }
                                            IsPCollA_auxNull = false;
                                            DataList[ind].AreaSeries.Points.Clear();

                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_lo = PCollA_aux[i];
                                                DataList[ind].AreaSeries.Points.Add(pt1_lo);
                                            }
                                            //for (int i = 0; i < IndListOrigin.Count; i++)
                                            //{
                                            //    int IndOrg_ln = IndListOrigin[i];
                                            //    Point pt_lo = new Point(PCollA_aux[IndOrg_ln].X,
                                            //        DataList[ind].AreaOrigin);
                                            //    DataList[ind].AreaSeries.Points[IndOrg_ln] = pt_lo;
                                            //}
                                        }
                                        #endregion
                                        #region Adition earlier data
                                        if (!IsPCollA_auxNull && j > 0)
                                        {
                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_lo = PCollA_aux[i];
                                                bool isInSeries = false;
                                                for (int k = 0; k < DataList[ind].AreaSeries.Points.Count; k++)
                                                {
                                                    Point pt2_lo = DataList[ind].AreaSeries.Points[k];
                                                    if (pt1_lo.X == pt2_lo.X)
                                                    {
                                                        isInSeries = true;
                                                        pt1_lo.Y += pt2_lo.Y;
                                                        PCollA_aux[i] = pt1_lo;
                                                    }
                                                }
                                                if (!isInSeries)
                                                    IndListOrigin.Add(i);
                                            }
                                            DataList[ind].AreaSeries.Points.Clear();
                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_lo = PCollA_aux[i];
                                                DataList[ind].AreaSeries.Points.Add(pt1_lo);
                                            }
                                            //for (int i = 0; i < IndListOrigin.Count; i++)
                                            //{
                                            //    int IndOrg_ln = IndListOrigin[i];
                                            //    Point pt_lo = new Point(PCollA_aux[IndOrg_ln].X,
                                            //        DataList[ind].AreaOrigin);
                                            //    DataList[ind].AreaSeries.Points[IndOrg_ln] = pt_lo;
                                            //}
                                        }
                                        #endregion
                                        #region Mapping
                                        if (cs.AxisStyle != ChartStyle.AxisStyleEnum.Linear)
                                        {
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt = DataList[ind].AreaSeries.Points[i];
                                                if (ValidPoint(pt, cs))
                                                {
                                                    #region Mapping Points to Logarithmic System
                                                    Point pt_aux = new Point();
                                                    switch (cs.AxisStyle)
                                                    {
                                                        case ChartStyle.AxisStyleEnum.Logarithmic:
                                                            pt_aux = cs.Point2DLogLog(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogX:
                                                            pt_aux = cs.Point2DLogX(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogY:
                                                            pt_aux = cs.Point2DLogY(pt);
                                                            break;
                                                    }
                                                    DataList[ind].AreaSeries.Points[i] = pt_aux;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region LogIrregularAction
                                                    switch (logIrregularAction)
                                                    {
                                                        case LogIrregularActionEnum.NullAndContinue:
                                                            DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                            i--;
                                                            break;
                                                        case LogIrregularActionEnum.GoesToZero:
                                                            Point pt_aux1 = new Point();
                                                            pt_aux1 = DataList[ind].AreaSeries.Points[i];

                                                            switch (cs.AxisStyle)
                                                            {
                                                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                                                    if (pt_aux1.Y <= 0 && pt_aux1.X > 0)
                                                                    {
                                                                        if (DataList[ind].AreaOrigin > 0)
                                                                            pt_aux1.Y = DataList[ind].AreaOrigin;
                                                                        else
                                                                            pt_aux1.Y = 1;
                                                                        pt_aux1 = cs.Point2DLogLog(pt_aux1);
                                                                        DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    }
                                                                    else
                                                                    {
                                                                        DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                        i--;
                                                                    }
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                                                    DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                    i--;
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                                                    if (DataList[ind].AreaOrigin > 0)
                                                                        pt_aux1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                                                    else
                                                                        pt_aux1.Y = 0;
                                                                    DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    break;
                                                            }
                                                            break;
                                                        case LogIrregularActionEnum.SplitGraph:
                                                            break;
                                                    }
                                                    #endregion
                                                }
                                            }

                                        }
                                        #endregion
                                        break;
                                }
                                Canvas.SetZIndex(DataList[ind].AreaSeries, nSeries - j);
                                int nPoints = DataList[ind].AreaSeries.Points.Count;
                                #region Locking Points
                                if (nPoints != 0)
                                {
                                    pt1.X = DataList[ind].AreaSeries.Points[0].X;
                                    pt2.X = DataList[ind].AreaSeries.Points[nPoints - 1].X;

                                    if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear || cs.AxisStyle == ChartStyle.AxisStyleEnum.SemiLogX)
                                    {
                                        pt1.Y = DataList[ind].AreaOrigin;
                                        pt2.Y = DataList[ind].AreaOrigin;
                                    }
                                    else if (DataList[ind].AreaOrigin > 0)
                                    {
                                        pt1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                        pt2.Y = Math.Log10(DataList[ind].AreaOrigin);
                                    }
                                    else
                                    {
                                        pt1.Y = 0;
                                        pt2.Y = 0;
                                    }
                                }
                                #endregion
                                j++;
                                DataList[ind].AreaSeries.Points.Insert(0, pt1);
                                DataList[ind].AreaSeries.Points.Add(pt2);
                                for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                {
                                    DataList[ind].AreaSeries.Points[i] = cs.Point2D(DataList[ind].AreaSeries.Points[i]);
                                }
                                cs.ChartCanvas.Children.Add(DataList[ind].AreaSeries);
                            }
                        }
                        break;
                    case AreaTypeEnum.HorizontalStack:
                        if (PCollectionStackA_go != null)
                        {
                            int IndCase = 1;
                            foreach (int ind in AP)
                            {
                                if (!PerfectAreaStack(PCollectionStackA_go, DataList[ind].Datapoints, 'y'))
                                {
                                    IndCase = 2;
                                    break;
                                }
                            }

                            foreach (int ind in AP)
                            {
                                Point pt1 = new Point();
                                Point pt2 = new Point();
                                #region AddAreaPattern
                                SetName(DataList[ind], nSeries, j);
                                DataList[ind].PointstoArea();
                                DataList[ind].AddAreaPattern();
                                DataList[ind].AreaSeries.FillRule = FillRule.Nonzero;
                                #endregion

                                switch (IndCase)
                                {
                                    case 1:
                                        #region Get PCollA_aux
                                        if (j == 0 || IsPCollA_auxNull)
                                        {
                                            PCollA_aux = new PointCollection();
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt_aux2 = new Point();
                                                pt_aux2 = DataList[ind].AreaSeries.Points[i];
                                                PCollA_aux.Add(pt_aux2);
                                            }
                                            if (PCollA_aux.Count != 0)
                                                IsPCollA_auxNull = false;
                                            Canvas.SetZIndex(dataList[ind].AreaSeries, nSeries);
                                        }
                                        #endregion
                                        #region Addition earlier data
                                        if (!IsPCollA_auxNull && j != 0)
                                        {
                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_aux = PCollA_aux[i];
                                                Point pt2_aux = DataList[ind].AreaSeries.Points[i];
                                                if (pt1_aux.Y == pt2_aux.Y)
                                                {
                                                    pt2_aux.X = pt1_aux.X + pt2_aux.X;
                                                    DataList[ind].AreaSeries.Points[i] = pt2_aux;
                                                }
                                            }
                                            Canvas.SetZIndex(DataList[ind].AreaSeries, nSeries - j);

                                            PCollA_aux.Clear();
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt_aux3 = new Point();
                                                pt_aux3 = DataList[ind].AreaSeries.Points[i];
                                                PCollA_aux.Add(pt_aux3);
                                            }

                                        }
                                        #endregion
                                        #region Mapping
                                        if (cs.AxisStyle != ChartStyle.AxisStyleEnum.Linear)
                                        {
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt = DataList[ind].AreaSeries.Points[i];
                                                if (ValidPoint(pt, cs))
                                                {
                                                    #region Mapping Points to Logarithmic System
                                                    Point pt_aux = new Point();
                                                    switch (cs.AxisStyle)
                                                    {
                                                        case ChartStyle.AxisStyleEnum.Logarithmic:
                                                            pt_aux = cs.Point2DLogLog(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogX:
                                                            pt_aux = cs.Point2DLogX(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogY:
                                                            pt_aux = cs.Point2DLogY(pt);
                                                            break;
                                                    }
                                                    DataList[ind].AreaSeries.Points[i] = pt_aux;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region LogIrregularAction
                                                    switch (logIrregularAction)
                                                    {
                                                        case LogIrregularActionEnum.NullAndContinue:
                                                            DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                            i--;
                                                            break;
                                                        case LogIrregularActionEnum.GoesToZero:
                                                            Point pt_aux1 = new Point();
                                                            pt_aux1 = DataList[ind].AreaSeries.Points[i];

                                                            switch (cs.AxisStyle)
                                                            {
                                                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                                                    if (pt_aux1.Y <= 0 && pt_aux1.X > 0)
                                                                    {
                                                                        if (DataList[ind].AreaOrigin > 0)
                                                                            pt_aux1.Y = DataList[ind].AreaOrigin;
                                                                        else
                                                                            pt_aux1.Y = 1;
                                                                        pt_aux1 = cs.Point2DLogLog(pt_aux1);
                                                                        DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    }
                                                                    else
                                                                    {
                                                                        DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                        i--;
                                                                    }
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                                                    DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                    i--;
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                                                    if (DataList[ind].AreaOrigin > 0)
                                                                        pt_aux1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                                                    else
                                                                        pt_aux1.Y = 0;
                                                                    DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    break;
                                                            }
                                                            break;
                                                        case LogIrregularActionEnum.SplitGraph:
                                                            break;
                                                    }
                                                    #endregion
                                                }
                                            }

                                        }
                                        #endregion
                                        break;
                                    case 2:
                                        IndListOrigin = new List<int>();
                                        #region GetPCollA_aux
                                        if (j == 0 || IsPCollA_auxNull)
                                        {
                                            PCollA_aux = new PointCollection();
                                            for (int i = 0; i < PCollectionStackA_go.Count; i++)
                                            {
                                                double y1_ln = PCollectionStackA_go[i].Y;
                                                bool isInSerie = false;
                                                for (int k = 0; k < DataList[ind].AreaSeries.Points.Count; k++)
                                                {
                                                    double y2_ln = DataList[ind].AreaSeries.Points[k].Y;
                                                    if (y1_ln == y2_ln)
                                                    {
                                                        Point pt1aux_lo = new Point(DataList[ind].AreaSeries.Points[k].X, y1_ln);
                                                        PCollA_aux.Add(pt1aux_lo);
                                                        isInSerie = true;
                                                        break;
                                                    }
                                                }
                                                if (!isInSerie)
                                                {
                                                    IndListOrigin.Add(i);
                                                    PCollA_aux.Add(new Point(0, y1_ln));
                                                }
                                            }
                                            IsPCollA_auxNull = false;
                                            DataList[ind].AreaSeries.Points.Clear();

                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_lo = PCollA_aux[i];
                                                DataList[ind].AreaSeries.Points.Add(pt1_lo);
                                            }
                                            //for (int i = 0; i < IndListOrigin.Count; i++)
                                            //{
                                            //    int IndOrg_ln = IndListOrigin[i];
                                            //    Point pt_lo = new Point(DataList[ind].AreaOrigin, PCollA_aux[IndOrg_ln].Y);
                                            //    DataList[ind].AreaSeries.Points[IndOrg_ln] = pt_lo;
                                            //}
                                        }
                                        #endregion
                                        #region Adition earlier data
                                        if (!IsPCollA_auxNull && j > 0)
                                        {
                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_lo = PCollA_aux[i];
                                                bool isInSeries = false;
                                                for (int k = 0; k < DataList[ind].AreaSeries.Points.Count; k++)
                                                {
                                                    Point pt2_lo = DataList[ind].AreaSeries.Points[k];
                                                    if (pt1_lo.Y == pt2_lo.Y)
                                                    {
                                                        isInSeries = true;
                                                        pt1_lo.X += pt2_lo.X;
                                                        PCollA_aux[i] = pt1_lo;
                                                    }
                                                }
                                                if (!isInSeries)
                                                    IndListOrigin.Add(i);
                                            }
                                            DataList[ind].AreaSeries.Points.Clear();
                                            for (int i = 0; i < PCollA_aux.Count; i++)
                                            {
                                                Point pt1_lo = PCollA_aux[i];
                                                DataList[ind].AreaSeries.Points.Add(pt1_lo);
                                            }
                                            //for (int i = 0; i < IndListOrigin.Count; i++)
                                            //{
                                            //    int IndOrg_ln = IndListOrigin[i];
                                            //    Point pt_lo = new Point(DataList[ind].AreaOrigin, PCollA_aux[IndOrg_ln].Y);
                                            //    DataList[ind].AreaSeries.Points[IndOrg_ln] = pt_lo;
                                            //}
                                        }
                                        #endregion
                                        #region Mapping
                                        if (cs.AxisStyle != ChartStyle.AxisStyleEnum.Linear)
                                        {
                                            for (int i = 0; i < DataList[ind].AreaSeries.Points.Count; i++)
                                            {
                                                Point pt = DataList[ind].AreaSeries.Points[i];
                                                if (ValidPoint(pt, cs))
                                                {
                                                    #region Mapping Points to Logarithmic System
                                                    Point pt_aux = new Point();
                                                    switch (cs.AxisStyle)
                                                    {
                                                        case ChartStyle.AxisStyleEnum.Logarithmic:
                                                            pt_aux = cs.Point2DLogLog(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogX:
                                                            pt_aux = cs.Point2DLogX(pt);
                                                            break;
                                                        case ChartStyle.AxisStyleEnum.SemiLogY:
                                                            pt_aux = cs.Point2DLogY(pt);
                                                            break;
                                                    }
                                                    DataList[ind].AreaSeries.Points[i] = pt_aux;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region LogIrregularAction
                                                    switch (logIrregularAction)
                                                    {
                                                        case LogIrregularActionEnum.NullAndContinue:
                                                            DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                            i--;
                                                            break;
                                                        case LogIrregularActionEnum.GoesToZero:
                                                            Point pt_aux1 = new Point();
                                                            pt_aux1 = DataList[ind].AreaSeries.Points[i];

                                                            switch (cs.AxisStyle)
                                                            {
                                                                case ChartStyle.AxisStyleEnum.Logarithmic:
                                                                    if (pt_aux1.Y <= 0 && pt_aux1.X > 0)
                                                                    {
                                                                        if (DataList[ind].AreaOrigin > 0)
                                                                            pt_aux1.Y = DataList[ind].AreaOrigin;
                                                                        else
                                                                            pt_aux1.Y = 1;
                                                                        pt_aux1 = cs.Point2DLogLog(pt_aux1);
                                                                        DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    }
                                                                    else
                                                                    {
                                                                        DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                        i--;
                                                                    }
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogX:
                                                                    DataList[ind].AreaSeries.Points.RemoveAt(i);
                                                                    i--;
                                                                    break;
                                                                case ChartStyle.AxisStyleEnum.SemiLogY:
                                                                    if (DataList[ind].AreaOrigin > 0)
                                                                        pt_aux1.Y = Math.Log10(DataList[ind].AreaOrigin);
                                                                    else
                                                                        pt_aux1.Y = 0;
                                                                    DataList[ind].AreaSeries.Points[i] = pt_aux1;
                                                                    break;
                                                            }
                                                            break;
                                                        case LogIrregularActionEnum.SplitGraph:
                                                            break;
                                                    }
                                                    #endregion
                                                }
                                            }

                                        }
                                        #endregion
                                        break;
                                }

                                Canvas.SetZIndex(DataList[ind].AreaSeries, nSeries - j);
                                int nPoints = DataList[ind].AreaSeries.Points.Count;
                                #region Locking Points
                                if (nPoints != 0)
                                {
                                    pt1.Y = DataList[ind].AreaSeries.Points[0].Y;
                                    pt2.Y = DataList[ind].AreaSeries.Points[nPoints - 1].Y;

                                    if (cs.AxisStyle == ChartStyle.AxisStyleEnum.Linear || cs.AxisStyle == ChartStyle.AxisStyleEnum.SemiLogY)
                                    {
                                        pt1.X = DataList[ind].AreaOrigin;
                                        pt2.X = DataList[ind].AreaOrigin;
                                    }
                                    else if (DataList[ind].AreaOrigin > 0)
                                    {
                                        pt1.X = Math.Log10(DataList[ind].AreaOrigin);
                                        pt2.X = Math.Log10(DataList[ind].AreaOrigin);
                                    }
                                    else
                                    {
                                        pt1.X = 0;
                                        pt2.X = 0;
                                    }
                                }
                                #endregion
                                DataList[ind].AreaSeries.Points.Insert(0, pt1);
                                DataList[ind].AreaSeries.Points.Add(pt2);
                                nPoints += 2;
                                for (int i = 0; i < nPoints; i++)
                                {
                                    DataList[ind].AreaSeries.Points[i] = cs.Point2D(DataList[ind].AreaSeries.Points[i]);
                                }
                                cs.ChartCanvas.Children.Add(DataList[ind].AreaSeries);

                                j++;

                            }
                        }
                        break;
                }

            }
        }
        #endregion

        #region //-------------Point METHODS----------------------------------------//

        private void PlotPoints(List<int> lsPP, ChartStyle loCS)
        {
            int lnNSeries = lsPP.Count;
            int j = 0;


            foreach (int lnInd in lsPP)
            {
                SetName(DataList[lnInd], lnNSeries, j);
                PointCollection lsPtCollTrans = new PointCollection();

                switch (loCS.AxisStyle)
                {
                    case ChartStyle.AxisStyleEnum.Linear:
                        #region Linear
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = loCS.Point2D(dataList[lnInd].Datapoints[i]);
                            //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                            lsPtCollTrans.Add(loPt);
                        }
                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.Logarithmic:
                        #region Logarithmic
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = dataList[lnInd].Datapoints[i];
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt = loCS.Point2DLogLog(loPt);
                                loPt = loCS.Point2D(loPt);
                                //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                                lsPtCollTrans.Add(loPt);
                            }

                        }

                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.SemiLogX:
                        #region SemilogX
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = dataList[lnInd].Datapoints[i];
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt = loCS.Point2DLogX(loPt);
                                loPt = loCS.Point2D(loPt);
                                //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                                lsPtCollTrans.Add(loPt);
                            }
                        }

                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.SemiLogY:
                        #region SemilogY
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = dataList[lnInd].Datapoints[i];
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt = loCS.Point2DLogY(loPt);
                                loPt = loCS.Point2D(loPt);
                                //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                                lsPtCollTrans.Add(loPt);
                            }
                        }

                        #endregion
                        break;
                }
                if (loCS.IsReductionActionOn)
                {
                    #region Reduction PointCollection
                    switch (geRecutEnum)
                    {
                        case ReductionEnum.SimplyNData:
                            lsPtCollTrans = SimplyPointCollection.SimplyNData(lsPtCollTrans, gnInt);
                            break;
                        case ReductionEnum.RadialDistance:
                            lsPtCollTrans = SimplyPointCollection.RadialDist(lsPtCollTrans, gnEps);
                            break;
                        case ReductionEnum.PerpendicularDistance:
                            lsPtCollTrans = SimplyPointCollection.PerpendicularDist(lsPtCollTrans, gnEps);
                            break;
                        case ReductionEnum.ReumanWitkam:
                            lsPtCollTrans = SimplyPointCollection.ReumannWitkam(lsPtCollTrans, gnEps);
                            break;
                        case ReductionEnum.Opheim:
                            lsPtCollTrans = SimplyPointCollection.Opheim(lsPtCollTrans, gnEps, gnEsp2);
                            break;
                        case ReductionEnum.Lang:
                            lsPtCollTrans = SimplyPointCollection.LangSimply(lsPtCollTrans, gnInt, gnEps);
                            break;
                        case ReductionEnum.DouglasPeucker:
                            lsPtCollTrans = SimplyPointCollection.DouglasPeucker(lsPtCollTrans, gnEps);
                            break;
                    }
                    #endregion

                }

                int Count = lsPtCollTrans.Count;
                for (int i = 0; i < Count; ++i)
                    dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, lsPtCollTrans[i], i, lnInd);
                j++;
            }
        }

        #endregion

        #region //------------Fast Point Methods----------------------------//

        private void PlotFastPoints(List<int> lsFP, ChartStyle loCS)
        {
            int lnNSeries = lsFP.Count;
            int j = 0;


            foreach (int lnInd in lsFP)
            {
                SetName(DataList[lnInd], lnNSeries, j);
                PointCollection lsPtCollTrans = new PointCollection();

                switch (loCS.AxisStyle)
                {
                    case ChartStyle.AxisStyleEnum.Linear:
                        #region Linear
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = loCS.Point2D(dataList[lnInd].Datapoints[i]);
                            //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                            lsPtCollTrans.Add(loPt);
                        }
                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.Logarithmic:
                        #region Logarithmic
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = dataList[lnInd].Datapoints[i];
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt = loCS.Point2DLogLog(loPt);
                                loPt = loCS.Point2D(loPt);
                                //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                                lsPtCollTrans.Add(loPt);
                            }

                        }

                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.SemiLogX:
                        #region SemilogX
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = dataList[lnInd].Datapoints[i];
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt = loCS.Point2DLogX(loPt);
                                loPt = loCS.Point2D(loPt);
                                //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                                lsPtCollTrans.Add(loPt);
                            }
                        }

                        #endregion
                        break;
                    case ChartStyle.AxisStyleEnum.SemiLogY:
                        #region SemilogY
                        for (int i = 0; i < DataList[lnInd].Datapoints.Count; i++)
                        {
                            Point loPt = dataList[lnInd].Datapoints[i];
                            if (ValidPoint(loPt, loCS))
                            {
                                loPt = loCS.Point2DLogY(loPt);
                                loPt = loCS.Point2D(loPt);
                                //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, loPt, i, lnInd);
                                lsPtCollTrans.Add(loPt);
                            }
                        }

                        #endregion
                        break;
                }
                if (loCS.IsReductionActionOn)
                {
                    #region Reduction PointCollection
                    switch (geRecutEnum)
                    {
                        case ReductionEnum.SimplyNData:
                            lsPtCollTrans = SimplyPointCollection.SimplyNData(lsPtCollTrans, gnInt);
                            break;
                        case ReductionEnum.RadialDistance:
                            lsPtCollTrans = SimplyPointCollection.RadialDist(lsPtCollTrans, gnEps);
                            break;
                        case ReductionEnum.PerpendicularDistance:
                            lsPtCollTrans = SimplyPointCollection.PerpendicularDist(lsPtCollTrans, gnEps);
                            break;
                        case ReductionEnum.ReumanWitkam:
                            lsPtCollTrans = SimplyPointCollection.ReumannWitkam(lsPtCollTrans, gnEps);
                            break;
                        case ReductionEnum.Opheim:
                            lsPtCollTrans = SimplyPointCollection.Opheim(lsPtCollTrans, gnEps, gnEsp2);
                            break;
                        case ReductionEnum.Lang:
                            lsPtCollTrans = SimplyPointCollection.LangSimply(lsPtCollTrans, gnInt, gnEps);
                            break;
                        case ReductionEnum.DouglasPeucker:
                            lsPtCollTrans = SimplyPointCollection.DouglasPeucker(lsPtCollTrans, gnEps);
                            break;
                    }
                    #endregion

                }
                for (int i = 0; i < lsPtCollTrans.Count; i++)
                    //dataList[lnInd].Symbols.AddSymbol(loCS.ChartCanvas, lsPtCollTrans[i], i, lnInd);
                    dataList[lnInd].Symbols.AddFastSymbol(loCS.ChartCanvas, lsPtCollTrans[i]);

                j++;
            }
        }

        #endregion

        #region //-------------Plot Function---------------------------------------//
        public void PlotSeries(ChartStyle cs)
        {
            List<int> BarPlots = new List<int>();
            List<int> LinePlots = new List<int>();
            List<int> StemPlots = new List<int>();
            List<int> StairPlots = new List<int>();
            List<int> AreaPlots = new List<int>();
            List<int> PointPlots = new List<int>();
            List<int> FastPoints = new List<int>();


            foreach (DataSeries ds in DataList)
            {
                switch (ds.PlotStyle)
                {
                    case DataSeries.PlotStyleEnum.FastPoints:
                        FastPoints.Add(DataList.IndexOf(ds));
                        break;
                    case DataSeries.PlotStyleEnum.Points:
                        PointPlots.Add(dataList.IndexOf(ds));
                        break;
                    case DataSeries.PlotStyleEnum.Lines:
                        LinePlots.Add(DataList.IndexOf(ds));
                        break;
                    case DataSeries.PlotStyleEnum.Bars:
                        BarPlots.Add(DataList.IndexOf(ds));
                        break;
                    case DataSeries.PlotStyleEnum.Stairs:
                        StairPlots.Add(DataList.IndexOf(ds));
                        break;
                    case DataSeries.PlotStyleEnum.Stem:
                        StemPlots.Add(DataList.IndexOf(ds));
                        break;
                    case DataSeries.PlotStyleEnum.Area:
                        AreaPlots.Add(DataList.IndexOf(ds));
                        break;
                }

            }
            if (AreaPlots.Count != 0)
                PlotArea(AreaPlots, cs);
            if (BarPlots.Count != 0)
                PlotBars(BarPlots, cs);
            if (LinePlots.Count != 0)
                PlotLines(LinePlots, cs);
            if (StemPlots.Count != 0)
                PlotStem(StemPlots, cs);
            if (StairPlots.Count != 0)
                PlotStairs(StairPlots, cs);
            if (PointPlots.Count != 0)
                PlotPoints(PointPlots, cs);
            if (FastPoints.Count != 0)
                PlotFastPoints(FastPoints, cs);
        }
        #endregion

        #region //-----------GetMaxMin Function-------------------------//
        internal void GetMaxMin()
        {
            if (dataList.Count != 0)
            {
                if (dataList[0].Datapoints.Count != 0)
                {
                    double xmin = dataList[0].Datapoints[0].X;
                    double xmax = dataList[0].Datapoints[0].X;
                    double ymax = dataList[0].Datapoints[0].Y;
                    double ymin = dataList[0].Datapoints[0].Y;

                    double xmin_aux = dataList[0].Datapoints[0].X;
                    double xmax_aux = dataList[0].Datapoints[0].X;
                    double ymax_aux = dataList[0].Datapoints[0].Y;
                    double ymin_aux = dataList[0].Datapoints[0].Y;

                    double xlogmin = 1, xlogmin_aux = 1, ylogmin = 1, ylogmin_aux = 1;
                    bool getx_lb = false, gety_lb = false;
                    int isError = 0;

                    double x = 0, x1 = double.NaN, x2 = double.NaN;
                    double y = 0, y1 = double.NaN, y2 = double.NaN;
                    double error = 0;

                    foreach (DataSeries ds in dataList)
                    {
                        if (ds.IsErrorLines)
                        {
                            ds.ValidErrorList();
                            isError = 1;
                        }
                        for (int i = 0; i < ds.Datapoints.Count; i++)
                        {


                            switch (isError)
                            {
                                case 0:
                                    x = ds.Datapoints[i].X;
                                    y = ds.Datapoints[i].Y;
                                    break;
                                case 1:
                                    error = ds.ErrorToPoint[i];
                                    switch (ds.ErrorType)
                                    {
                                        case DataSeries.ErrorTypeEnum.ErrorX:
                                            x = ds.Datapoints[i].X;
                                            x1 = ds.Datapoints[i].X + error;
                                            x2 = ds.Datapoints[i].X - error;
                                            y = ds.Datapoints[i].Y;
                                            break;
                                        case DataSeries.ErrorTypeEnum.ErrorY:
                                            x = ds.Datapoints[i].X;
                                            y = ds.Datapoints[i].Y;
                                            y1 = ds.Datapoints[i].Y + error;
                                            y2 = ds.Datapoints[i].Y - error;
                                            break;
                                    }
                                    break;
                            }

                            xmax_aux = (x > xmax_aux) ? x : xmax_aux;
                            xmin_aux = (x < xmin_aux) ? x : xmin_aux;
                            ymax_aux = (y > ymax_aux) ? y : ymax_aux;
                            ymin_aux = (y < ymin_aux) ? y : ymin_aux;
                            #region Conditional Extra
                            if (!double.IsNaN(x1))
                            {
                                xmax_aux = (x1 > xmax_aux) ? x1 : xmax_aux;
                                xmin_aux = (x1 < xmin_aux) ? x1 : xmin_aux;
                            }
                            if (!double.IsNaN(x2))
                            {
                                xmax_aux = (x2 > xmax_aux) ? x2 : xmax_aux;
                                xmin_aux = (x2 < xmin_aux) ? x2 : xmin_aux;
                            }
                            if (!double.IsNaN(y1))
                            {
                                ymax_aux = (y1 > ymax_aux) ? y1 : ymax_aux;
                                ymin_aux = (y1 < ymin_aux) ? y1 : ymin_aux;
                            }
                            if (!double.IsNaN(y2))
                            {
                                ymax_aux = (y2 > ymax_aux) ? y2 : ymax_aux;
                                ymin_aux = (y2 < ymin_aux) ? y2 : ymin_aux;
                            }
                            #endregion
                            #region Get LogMin Limits
                            if (!getx_lb && x > 0)
                            {
                                xlogmin = x;
                                xlogmin_aux = x;
                                getx_lb = true;
                            }
                            if (getx_lb)
                            {
                                xlogmin_aux = ((x < xlogmin_aux) && (x > 0)) ? x : xlogmin_aux;
                                xlogmin_aux = ((x1 < xlogmin_aux) && (x1 > 0)) ? x1 : xlogmin_aux;
                                xlogmin_aux = ((x2 < xlogmin_aux) && (x2 > 0)) ? x2 : xlogmin_aux;
                            }
                            if (!gety_lb && y > 0)
                            {
                                ylogmin = y;
                                ylogmin_aux = y;
                                gety_lb = true;
                            }
                            if (gety_lb)
                            {
                                ylogmin_aux = ((y < ylogmin_aux) && (y > 0)) ? y : ylogmin_aux;
                                ylogmin_aux = ((y1 < ylogmin_aux) && (y1 > 0)) ? y1 : ylogmin_aux;
                                ylogmin_aux = ((y2 < ylogmin_aux) && (y2 > 0)) ? y2 : ylogmin_aux;
                            }
                            #endregion
                        }

                        ds.SetMaxMin(xmax_aux, DataSeries.SetXY.XMax);
                        ds.SetMaxMin(xmin_aux, DataSeries.SetXY.XMin);
                        ds.SetMaxMin(ymax_aux, DataSeries.SetXY.YMax);
                        ds.SetMaxMin(ymin_aux, DataSeries.SetXY.YMin);

                        #region Set DataSeries LogMin Limits
                        if (getx_lb)
                            ds.SetMaxMin(xlogmin_aux, DataSeries.SetXY.XLogMin);
                        else
                            ds.SetMaxMin(0, DataSeries.SetXY.XLogMin);
                        if (gety_lb)
                            ds.SetMaxMin(ylogmin_aux, DataSeries.SetXY.YLogMin);
                        else
                            ds.SetMaxMin(0, DataSeries.SetXY.YLogMin);
                        #endregion

                        xmax = (xmax_aux > xmax) ? xmax_aux : xmax;
                        xmin = (xmin_aux < xmin) ? xmin_aux : xmin;
                        ymax = (ymax_aux > ymax) ? ymax_aux : ymax;
                        ymin = (ymin_aux < ymin) ? ymin_aux : ymin;
                        xlogmin = ((xlogmin_aux < xlogmin) && (xlogmin_aux > 0)) ? xlogmin_aux : xlogmin;
                        ylogmin = ((ylogmin_aux < ylogmin) && (ylogmin_aux > 0)) ? ylogmin_aux : ylogmin;

                    }

                    #region Special Case Equal Limits
                    if (xmax == xmin)
                    {
                        if (xmax != 0)
                        {
                            xmax = 1.1 * xmax;
                            xmin = 0.9 * xmin;
                        }
                        else
                        {
                            xmax = 0.5;
                            xmin = -0.5;
                        }
                    }

                    if (ymax == ymin)
                    {
                        if (ymax != 0)
                        {
                            ymax = 1.1 * ymax;
                            ymin = 0.9 * ymin;
                        }
                        else
                        {
                            ymax = 0.5;
                            ymin = -0.5;
                        }
                    }
                    #endregion
                    xMaxCollection = xmax;
                    xMinCollection = xmin;
                    yMaxCollection = ymax;
                    yMinCollection = ymin;
                    xMinLog = xlogmin;
                    yMinLog = ylogmin;
                }
            }
        }

        internal void GetMaxMinStack()
        {
            if (DataList.Count != 0)
            {
                List<int> AreaInd_lo = new List<int>();
                List<int> BarInd_lo = new List<int>();
                List<StPoint> StPListA_lo = new List<StPoint>();
                List<StPoint> StPListB_lo = new List<StPoint>();


                double xMax_ln, yMax_ln, xMin_ln, yMin_ln;
                double xMinLog_ln, yMinLog_ln;


                for (int i = 0; i < DataList.Count; i++)
                {
                    if (DataList[i].PlotStyle == DataSeries.PlotStyleEnum.Area)
                        AreaInd_lo.Add(i);
                    if (DataList[i].PlotStyle == DataSeries.PlotStyleEnum.Bars)
                        BarInd_lo.Add(i);
                }

                if (AreaInd_lo.Count != 0)
                {
                    #region Get StpListA
                    List<StPoint> StPA_aux_lo = new List<StPoint>();
                    foreach (int ind in AreaInd_lo)
                    {
                        for (int i = 0; i < DataList[ind].Datapoints.Count; i++)
                        {
                            Point pt_lo = DataList[ind].Datapoints[i];
                            StPoint stP_lo = new StPoint(pt_lo);
                            StPA_aux_lo.Add(stP_lo);
                        }
                    }

                    double xAux_ln;
                    double yAux_ln;
                    switch (AreaType)
                    {

                        case AreaTypeEnum.VerticalStack:
                            #region Get StPListA for VerticalStack
                            StPA_aux_lo.Sort(CompareStPointX);
                            xAux_ln = StPA_aux_lo[0].Xval;
                            yAux_ln = StPA_aux_lo[0].Yval;

                            for (int i = 1; i < StPA_aux_lo.Count; i++)
                            {
                                if (xAux_ln == StPA_aux_lo[i].Xval)
                                    yAux_ln += StPA_aux_lo[i].Yval;
                                else
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                    xAux_ln = StPA_aux_lo[i].Xval;
                                    yAux_ln = StPA_aux_lo[i].Yval;
                                }
                                if (i == StPA_aux_lo.Count - 1)
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                }
                            }
                            #endregion
                            break;
                        case AreaTypeEnum.HorizontalStack:
                            #region Get StPListA for HorizontalStack
                            StPA_aux_lo.Sort(CompareStPointY);
                            xAux_ln = StPA_aux_lo[0].Xval;
                            yAux_ln = StPA_aux_lo[0].Yval;

                            for (int i = 1; i < StPA_aux_lo.Count; i++)
                            {
                                if (yAux_ln == StPA_aux_lo[i].Yval)
                                    xAux_ln += StPA_aux_lo[i].Xval;
                                else
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                    xAux_ln = StPA_aux_lo[i].Xval;
                                    yAux_ln = StPA_aux_lo[i].Yval;
                                }
                                if (i == StPA_aux_lo.Count - 1)
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                }
                            }
                            #endregion
                            break;
                    }
                    #endregion
                }
                if (BarInd_lo.Count != 0)
                {
                    #region Get StpListB
                    List<StPoint> StPB_aux_lo = new List<StPoint>();
                    foreach (int ind in BarInd_lo)
                    {
                        for (int i = 0; i < DataList[ind].Datapoints.Count; i++)
                        {
                            Point pt_lo = DataList[ind].Datapoints[i];
                            StPoint stP_lo = new StPoint(pt_lo);
                            StPB_aux_lo.Add(stP_lo);
                        }
                    }

                    double xAux_ln;
                    double yAux_ln;

                    switch (BarType)
                    {

                        case BarTypeEnum.VerticalStack:
                            #region Get StPListB for VerticalStack
                            StPB_aux_lo.Sort(CompareStPointX);
                            xAux_ln = StPB_aux_lo[0].Xval;
                            yAux_ln = StPB_aux_lo[0].Yval;

                            for (int i = 1; i < StPB_aux_lo.Count; i++)
                            {
                                if (xAux_ln == StPB_aux_lo[i].Xval)
                                    yAux_ln += StPB_aux_lo[i].Yval;
                                else
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                    xAux_ln = StPB_aux_lo[i].Xval;
                                    yAux_ln = StPB_aux_lo[i].Yval;
                                }
                                if (i == StPB_aux_lo.Count - 1)
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                }
                            }
                            #endregion
                            break;
                        case BarTypeEnum.HorizontalStack:
                            #region Get StPListB for HorizontalStack
                            StPB_aux_lo.Sort(CompareStPointY);
                            xAux_ln = StPB_aux_lo[0].Xval;
                            yAux_ln = StPB_aux_lo[0].Yval;

                            for (int i = 1; i < StPB_aux_lo.Count; i++)
                            {
                                if (yAux_ln == StPB_aux_lo[i].Yval)
                                    xAux_ln += StPB_aux_lo[i].Xval;
                                else
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                    xAux_ln = StPB_aux_lo[i].Xval;
                                    yAux_ln = StPB_aux_lo[i].Yval;
                                }
                                if (i == StPB_aux_lo.Count - 1)
                                {
                                    StPoint stpAux_lo = new StPoint(xAux_ln, yAux_ln);
                                    StPListA_lo.Add(stpAux_lo);
                                }
                            }
                            #endregion
                            break;

                    }
                    #endregion
                }
                if (StPListA_lo.Count != 0)
                {
                    #region Get Max an Min for Area and PCollectionStackA
                    switch (AreaType)
                    {
                        case AreaTypeEnum.VerticalStack:
                            yMin_ln = StPListA_lo[0].Yval;
                            yMax_ln = StPListA_lo[0].Yval;
                            yMinLog_ln = 1;
                            for (int i = 0; i < StPListA_lo.Count; i++)
                            {
                                double y_ln = StPListA_lo[i].Yval;

                                yMin_ln = (y_ln < yMin_ln) ? y_ln : yMin_ln;
                                yMax_ln = (y_ln > yMax_ln) ? y_ln : yMax_ln;
                                if (y_ln > 0)
                                    yMinLog_ln = (y_ln < yMinLog_ln) ? y_ln : yMinLog_ln;
                            }

                            yMinAreaStack = yMin_ln;
                            yMaxAreaStack = yMax_ln;
                            yLogAreaStack = yMinLog_ln;
                            break;
                        case AreaTypeEnum.HorizontalStack:
                            xMin_ln = StPListA_lo[0].Xval;
                            xMax_ln = StPListA_lo[0].Xval;
                            xMinLog_ln = 1;
                            for (int i = 0; i < StPListA_lo.Count; i++)
                            {
                                double x_ln = StPListA_lo[i].Xval;

                                xMin_ln = (x_ln < xMin_ln) ? x_ln : xMin_ln;
                                xMax_ln = (x_ln > xMax_ln) ? x_ln : xMax_ln;
                                if (x_ln > 0)
                                    xMinLog_ln = (x_ln < xMinLog_ln) ? x_ln : xMinLog_ln;
                            }

                            xMinAreaStack = xMin_ln;
                            xMaxAreaStack = xMax_ln;
                            xLogAreaStack = xMinLog_ln;
                            break;
                    }

                    PCollectionStackA_go = new PointCollection();
                    for (int i = 0; i < StPListA_lo.Count; i++)
                    {
                        Point pt_ln = new Point(StPListA_lo[i].Xval, StPListA_lo[i].Yval);
                        PCollectionStackA_go.Add(pt_ln);
                    }

                    #endregion
                }
                if (StPListB_lo.Count != 0)
                {
                    #region Get Max an Min for Bar and PCollectionStackB
                    switch (BarType)
                    {
                        case BarTypeEnum.VerticalStack:
                            yMin_ln = StPListB_lo[0].Yval;
                            yMax_ln = StPListB_lo[0].Yval;
                            yMinLog_ln = 1;
                            for (int i = 0; i < StPListB_lo.Count; i++)
                            {
                                double y_ln = StPListB_lo[i].Yval;

                                yMin_ln = (y_ln < yMin_ln) ? y_ln : yMin_ln;
                                yMax_ln = (y_ln > yMax_ln) ? y_ln : yMax_ln;
                                if (y_ln > 0)
                                    yMinLog_ln = (y_ln < yMinLog_ln) ? y_ln : yMinLog_ln;
                            }

                            yMinBarStack = yMin_ln;
                            yMaxBarStack = yMax_ln;
                            yLogBarStack = yMinLog_ln;
                            break;
                        case BarTypeEnum.HorizontalStack:
                            xMin_ln = StPListB_lo[0].Xval;
                            xMax_ln = StPListB_lo[0].Xval;
                            xMinLog_ln = 1;
                            for (int i = 0; i < StPListB_lo.Count; i++)
                            {
                                double x_ln = StPListB_lo[i].Xval;

                                xMin_ln = (x_ln < xMin_ln) ? x_ln : xMin_ln;
                                xMax_ln = (x_ln > xMax_ln) ? x_ln : xMax_ln;
                                if (x_ln > 0)
                                    xMinLog_ln = (x_ln < xMinLog_ln) ? x_ln : xMinLog_ln;
                            }

                            xMinBarStack = xMin_ln;
                            xMaxBarStack = xMax_ln;
                            xLogBarStack = xMinLog_ln;
                            break;
                    }

                    PCollectionStackB_go = new PointCollection();
                    for (int i = 0; i < StPListB_lo.Count; i++)
                    {
                        Point pt_ln = new Point(StPListB_lo[i].Xval, StPListB_lo[i].Yval);
                        PCollectionStackB_go.Add(pt_ln);
                    }
                    #endregion
                }
                #region Area Comparation
                if (!double.IsNaN(xMaxAreaStack))
                    xMaxCollection = (xMaxAreaStack > xMaxCollection) ? xMaxAreaStack : xMaxCollection;
                if (!double.IsNaN(xMinAreaStack))
                    xMinCollection = (xMinAreaStack < xMinCollection) ? xMinAreaStack : xMinCollection;
                if (!double.IsNaN(xLogAreaStack))
                    xMinLog = (xLogAreaStack < xMinLog) ? xLogAreaStack : xMinLog;
                if (!double.IsNaN(yMaxAreaStack))
                    yMaxCollection = (yMaxAreaStack > yMaxCollection) ? yMaxAreaStack : yMaxCollection;
                if (!double.IsNaN(yMinAreaStack))
                    yMinCollection = (yMinAreaStack < yMinCollection) ? yMinAreaStack : yMinCollection;
                if (!double.IsNaN(yLogAreaStack))
                    yMinLog = (yLogAreaStack < yMinLog) ? yLogAreaStack : yMinLog;
                #endregion

                #region Bar Comparation
                if (!double.IsNaN(xMaxBarStack))
                    xMaxCollection = (xMaxBarStack > xMaxCollection) ? xMaxBarStack : xMaxCollection;
                if (!double.IsNaN(xMinBarStack))
                    xMinCollection = (xMinBarStack < xMinCollection) ? xMinBarStack : xMinCollection;
                if (!double.IsNaN(xLogBarStack))
                    xMinLog = (xLogBarStack < xMinLog) ? xLogBarStack : xMinLog;
                if (!double.IsNaN(yMaxBarStack))
                    yMaxCollection = (yMaxBarStack > yMaxCollection) ? yMaxBarStack : yMaxCollection;
                if (!double.IsNaN(yMinBarStack))
                    yMinCollection = (yMinBarStack < yMinCollection) ? yMinBarStack : yMinCollection;
                if (!double.IsNaN(yLogBarStack))
                    yMinLog = (yLogBarStack < yMinLog) ? yLogBarStack : yMinLog;
                #endregion


            }
        }
        #endregion

        #region Reduction Methods

        /// <summary>
        /// Set reduction method, SimplyNData
        /// </summary>
        /// <param name="leReduct"></param>
        /// <param name="lnRange"></param>
        public void SetReductionMethod(ReductionEnum leReduct, int lnRange)
        {
            geRecutEnum = leReduct;
            gnInt = lnRange;
        }

        /// <summary>
        /// Set reduction method, Radial distance, Perpendicular distance, Reumann-Witkam, Douglas-Peucker 
        /// </summary>
        /// <param name="leReduct"></param>
        /// <param name="lnEps"></param>
        public void SetReductionMethod(ReductionEnum leReduct, double lnEps)
        {
            geRecutEnum = leReduct;
            gnEps = lnEps;
        }

        /// <summary>
        /// Set reduction method, 
        /// </summary>
        /// <param name="leReduct"></param>
        /// <param name="lnRange"></param>
        /// <param name="lnEps"></param>
        public void SetReductionMethod(ReductionEnum leReduct, int lnRange, double lnEps)
        {
            geRecutEnum = leReduct;
            gnInt = lnRange;
            gnEps = lnEps;
        }
        #endregion

        #region Auxiliar Methods
        //private bool ValidPoint(Point pt, ChartStyle cs) PBO 281112
        public bool ValidPoint(Point pt, ChartStyle cs)
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

        private bool EqualElements(List<int> list_lo)
        {
            bool result = true;
            if (list_lo.Count > 1)
            {
                int elem_ln = list_lo[0];
                for (int i = 1; i < list_lo.Count; i++)
                {
                    if (elem_ln != list_lo[i])
                    {
                        result = false;
                    }
                    elem_ln = list_lo[i];
                }
            }
            return result;
        }

        private bool EqualXPoints(List<int> listInd_lo)
        {
            bool result = true;
            int nPoints_ln = 0;
            if (listInd_lo.Count > 1)
            {
                nPoints_ln = DataList[listInd_lo[0]].Datapoints.Count;


                for (int i = 0; i < nPoints_ln; i++)
                {
                    double x_ln = dataList[listInd_lo[0]].Datapoints[i].X;
                    for (int m = 1; m < listInd_lo.Count; m++)
                    {

                        double x1_ln = dataList[listInd_lo[m]].Datapoints[i].X;
                        if (x_ln != x1_ln)
                            result = false;
                    }
                }
            }
            return result;
        }

        private bool PerfectAreaStack(Polygon PL1_lo, Polygon PL2_lo, char axis_lc)
        {
            bool result = false;
            if (PL1_lo.Points.Count != PL2_lo.Points.Count)
                return result;
            switch (axis_lc)
            {
                case 'x':
                    for (int i = 0; i < PL1_lo.Points.Count; i++)
                    {
                        if (PL1_lo.Points[i].X != PL2_lo.Points[i].X)
                            return result;
                    }
                    break;
                case 'y':
                    for (int i = 0; i < PL1_lo.Points.Count; i++)
                    {
                        if (PL1_lo.Points[i].Y != PL2_lo.Points[i].Y)
                            return result;
                    }
                    break;
                default:
                    break;
            }
            result = true;
            return result;
        }

        private bool PerfectAreaStack(PointCollection PC1_lo, PointCollection PC2_lo, char axis_lc)
        {
            bool result = false;
            if (PC1_lo.Count != PC2_lo.Count)
                return result;
            switch (axis_lc)
            {
                case 'x':
                    for (int i = 0; i < PC1_lo.Count; i++)
                    {
                        if (PC1_lo[i].X != PC2_lo[i].X)
                            return result;
                    }
                    break;
                case 'y':
                    for (int i = 0; i < PC2_lo.Count; i++)
                    {
                        if (PC1_lo[i].Y != PC2_lo[i].Y)
                            return result;
                    }
                    break;
            }
            result = true;
            return result;
        }

        #endregion


        #region //-------------SERIES NAMES-----------------------//
        //private void SetName(DataSeries ds, int Nseries, int j) PBO 281112
        public void SetName(DataSeries ds, int Nseries, int j)
        {
            String type = " ";

            switch (ds.PlotStyle)
            {
                case DataSeries.PlotStyleEnum.Lines:
                    break;
                case DataSeries.PlotStyleEnum.Bars:
                    type = "Bar ";
                    break;
                case DataSeries.PlotStyleEnum.Stairs:
                    type = "Stair ";
                    break;
                case DataSeries.PlotStyleEnum.Stem:
                    type = "Stem ";
                    break;
                case DataSeries.PlotStyleEnum.Area:
                    type = "Area";
                    break;
            }

            if (Nseries == 1)
            {
                if (ds.SeriesName == "Default Name")
                    ds.SeriesName = "Data Series " + type;
            }
            else
                if (ds.SeriesName == "Default Name")
                ds.SeriesName = "Data Series " + type + j.ToString();
        }
        #endregion
        #endregion

        #region Other Enumerations
        #region Logarithm Irregularities

        public enum LogIrregularActionEnum
        {
            NullAndContinue = 1,
            GoesToZero = 2,
            SplitGraph = 3
        }

        #endregion

        #region Reduction Enumaration
        public enum ReductionEnum
        {
            SimplyNData,
            RadialDistance,
            PerpendicularDistance,
            ReumanWitkam,
            Opheim,
            Lang,
            DouglasPeucker
        }
        #endregion
        #endregion

        #region Structures
        public struct StPoint
        {
            public double Xval;
            public double Yval;

            public StPoint(double X_ln, double Y_ln)
            {
                Xval = X_ln;
                Yval = Y_ln;
            }

            public StPoint(Point pt_lo)
            {
                Xval = pt_lo.X;
                Yval = pt_lo.Y;
            }
        }

        #region Methods for Structures

        private int CompareStPointX(StPoint StP1_lo, StPoint StP2_lo)
        {
            if (StP1_lo.Xval == StP2_lo.Xval)
                return 0;
            else if (StP1_lo.Xval < StP2_lo.Xval)
                return -1;
            else
                return 1;
        }

        private int CompareStPointY(StPoint StP1_lo, StPoint StP2_lo)
        {
            if (StP1_lo.Yval == StP2_lo.Yval)
                return 0;
            else if (StP1_lo.Yval < StP2_lo.Yval)
                return -1;
            else
                return 1;
        }

        #endregion
        #endregion

    }
}
