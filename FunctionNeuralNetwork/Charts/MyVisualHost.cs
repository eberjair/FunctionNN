using System;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace FunctionNeuralNetwork.Charts
{
    public class MyVisualHost : FrameworkElement
    {

        // Create a collection of child visual objects.
        private VisualCollection children;

        public MyVisualHost()
        {
            children = new VisualCollection(this);
        }

        public void AddChildren(DrawingVisual loDrawVis)
        {
            children.Add(loDrawVis);
        }

        //public DrawingVisual DrawRoundedRectangle_ensayos(Rect rect, LinearGradientBrush brush, double tla, double tra, double bla, double bra)
        //{
        //    DrawingVisual drawingVisual = new DrawingVisual();

        //    // Retrieve the DrawingContext in order to create new drawing content.
        //    DrawingContext drawingContext = drawingVisual.RenderOpen();

        //    Pen pen = new Pen();
        //    pen.Brush = brush;
        //    pen.Thickness = 2;

        //    double l = rect.Width / 4.0;

        //    //CornerRadius cornerRadius = new CornerRadius(l,l, bra, bla);
        //    CornerRadius cornerRadius = new CornerRadius(tla, tra, bra, bla);
        //    if (cornerRadius.TopLeft > l) cornerRadius.TopLeft = l;
        //    if (cornerRadius.TopRight > l) cornerRadius.TopRight = l;
        //    if (cornerRadius.BottomLeft > l) cornerRadius.BottomLeft = l;
        //    if (cornerRadius.BottomRight > l) cornerRadius.BottomRight = l;

        //    var geometry = new StreamGeometry();                

        //    using (var context = geometry.Open())
        //    {
        //        bool isStroked = pen != null;
        //        const bool isSmoothJoin = false;

        //        Point lvBegFig = rect.TopLeft + new Vector(0, cornerRadius.TopLeft);

        //        context.BeginFigure(lvBegFig, brush != null, true);

        //        Point lpArc1;
        //        if (cornerRadius.TopLeft > l)
        //        {
        //            lpArc1 = new Point(rect.TopLeft.X + l, rect.TopLeft.Y);
        //        }
        //        else
        //        {
        //            lpArc1 = new Point(rect.TopLeft.X + cornerRadius.TopLeft, rect.TopLeft.Y);
        //        }

        //        Size lsz1 = new Size(cornerRadius.TopLeft, cornerRadius.TopLeft);

        //        context.ArcTo(lpArc1, lsz1, 90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

        //        Vector lvFstVert = new Vector(cornerRadius.TopRight, 0);

        //        context.LineTo(rect.TopRight - lvFstVert, isStroked, isSmoothJoin);

        //        Point lpArc2;
        //        if(cornerRadius.TopRight>l){
        //            lpArc2 = new Point(rect.TopRight.X, rect.TopRight.Y + l);
        //        }
        //        else {
        //            lpArc2 = new Point(rect.TopRight.X, rect.TopRight.Y + cornerRadius.TopRight);
        //        }

        //        Size lsz2 = new Size(cornerRadius.TopRight, cornerRadius.TopRight);
        //        context.ArcTo(lpArc2, lsz2, 90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

        //        Vector lvSndVert = new Vector(0, cornerRadius.BottomRight);

        //        context.LineTo(rect.BottomRight - lvSndVert, isStroked, isSmoothJoin);



        //        context.ArcTo(new Point(rect.BottomRight.X - cornerRadius.BottomRight, rect.BottomRight.Y),
        //            new Size(cornerRadius.BottomRight, cornerRadius.BottomRight),
        //            90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
        //        context.LineTo(rect.BottomLeft + new Vector(cornerRadius.BottomLeft, 0), isStroked, isSmoothJoin);
        //        context.ArcTo(new Point(rect.BottomLeft.X, rect.BottomLeft.Y - cornerRadius.BottomLeft),
        //            new Size(cornerRadius.BottomLeft, cornerRadius.BottomLeft),
        //            90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

        //        context.Close();
        //    }
        //    drawingContext.DrawGeometry(brush, pen, geometry);
        //    // Persist the drawing content.
        //    drawingContext.Close();

        //    return drawingVisual;
        //}

        public DrawingVisual DrawRoundedRectangle(Rect rect, LinearGradientBrush brush, double lnRadiusTopLeft, double lnRadiusTopRight, double lnRadiusBottomLeft, double lnRadiusBottomRight)
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            Pen pen = new Pen();
            pen.Brush = brush;
            pen.Thickness = 2;
            CornerRadius cornerRadius = new CornerRadius(lnRadiusTopLeft, lnRadiusTopRight, lnRadiusBottomRight, lnRadiusBottomLeft);
            double l = rect.Width / 4.0;

            if (lnRadiusTopLeft > l) cornerRadius.TopLeft = l;
            if (lnRadiusTopRight > l) cornerRadius.TopRight = l;
            if (lnRadiusBottomLeft > l) cornerRadius.BottomLeft = l;
            if (lnRadiusBottomRight > l) cornerRadius.BottomRight = l;

            var geometry = new StreamGeometry();
            using (var context = geometry.Open())
            {
                bool isStroked = pen != null;
                const bool isSmoothJoin = false;
                //const bool isSmoothJoin = true;

                context.BeginFigure(rect.TopLeft + new Vector(0, cornerRadius.TopLeft), brush != null, true);
                context.ArcTo(new Point(rect.TopLeft.X + cornerRadius.TopLeft, rect.TopLeft.Y),
                    new Size(cornerRadius.TopLeft, cornerRadius.TopLeft),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                context.LineTo(rect.TopRight - new Vector(cornerRadius.TopRight, 0), isStroked, isSmoothJoin);
                context.ArcTo(new Point(rect.TopRight.X, rect.TopRight.Y + cornerRadius.TopRight),
                    new Size(cornerRadius.TopRight, cornerRadius.TopRight),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                context.LineTo(rect.BottomRight - new Vector(0, cornerRadius.BottomRight), isStroked, isSmoothJoin);
                context.ArcTo(new Point(rect.BottomRight.X - cornerRadius.BottomRight, rect.BottomRight.Y),
                    new Size(cornerRadius.BottomRight, cornerRadius.BottomRight),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                context.LineTo(rect.BottomLeft + new Vector(cornerRadius.BottomLeft, 0), isStroked, isSmoothJoin);
                context.ArcTo(new Point(rect.BottomLeft.X, rect.BottomLeft.Y - cornerRadius.BottomLeft),
                    new Size(cornerRadius.BottomLeft, cornerRadius.BottomLeft),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

                context.Close();
            }
            drawingContext.DrawGeometry(brush, pen, geometry);
            // Persist the drawing content.
            drawingContext.Close();

            return drawingVisual;
        }

        // Create a DrawingVisual that contains a rectangle.
        private DrawingVisual CreateDrawingVisualRectangle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create a rectangle and draw it in the DrawingContext.
            Rect rect = new Rect(new Point(160, 100), new Size(320, 80));
            drawingContext.DrawRectangle(Brushes.LightBlue, (Pen)null, rect);

            // Persist the drawing content.
            drawingContext.Close();

            return drawingVisual;
        }

        // Create a DrawingVisual that contains an ellipse.
        private DrawingVisual CreateDrawingVisualEllipses()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawEllipse(Brushes.Maroon, null, new Point(430, 136), 20, 20);
            drawingContext.Close();

            return drawingVisual;
        }

        // Create a DrawingVisual that contains text.
        private DrawingVisual CreateDrawingVisualText(string text, Point origen)
        {
            // Create an instance of a DrawingVisual.
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext from the DrawingVisual.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Draw a formatted text string into the DrawingContext.
            drawingContext.DrawText(
               new FormattedText(text,
                  CultureInfo.GetCultureInfo("en-us"),
                  FlowDirection.LeftToRight,
                  new Typeface("Verdana"),
                  36, System.Windows.Media.Brushes.Black),
                  origen);

            // Close the DrawingContext to persist changes to the DrawingVisual.
            drawingContext.Close();

            return drawingVisual;
        }

        //// Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return children[index];
        }
    }

}
