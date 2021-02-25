using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunctionNeuralNetwork.Charts
{
    public class LineStyle
    {
        private Brush lineColor = Brushes.Black;
        private double lineThickness = 1.0;
        private LinePatternEnum linePattern = LinePatternEnum.Solid;

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

        //public LineStyle(){};
        public LineStyle(Brush br, double LTh, LinePatternEnum LPE)
        {
            lineColor = br;
            lineThickness = LTh;
            LinePattern = LPE;
        }

        public void AddPattern<T>(T t) where T : Shape
        {
            t.Stroke = LineColor;
            t.StrokeThickness = LineThickness;

            switch (linePattern)
            {
                case LinePatternEnum.Solid:
                    break;
                case LinePatternEnum.Dash:
                    t.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    t.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    t.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    t.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }

        }

        //public void AddPattern<T>(T t) where T : Line
        //{
        //    t.Stroke = LineColor;
        //    t.StrokeThickness = LineThickness;

        //    switch (linePattern)
        //    {
        //        case LinePatternEnum.Solid:
        //            break;
        //        case LinePatternEnum.Dash:
        //            t.StrokeDashArray = new DoubleCollection(new double[2] { 4, 3 });
        //            break;
        //        case LinePatternEnum.Dot:
        //            t.StrokeDashArray = new DoubleCollection(new double[2] { 1, 2 });
        //            break;
        //        case LinePatternEnum.DashDot:
        //            t.StrokeDashArray = new DoubleCollection(new double[4] { 4, 2, 1, 2 });
        //            break;
        //        case LinePatternEnum.None:
        //            t.Visibility = Visibility.Hidden;
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }
    }
}
