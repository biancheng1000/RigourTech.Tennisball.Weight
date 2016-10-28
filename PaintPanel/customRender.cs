using System;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input;
using System.Windows.Ink;

namespace InkCanvasTest
{
    class CustomDynamicRenderer : DynamicRenderer
    {
        [ThreadStatic]
        static private Brush brush = null;

        [ThreadStatic]
        static private Pen pen = null;

        private Point prevPoint;
        private Point LastPoint;
        bool first;
        Action<DrawingContext, Point, Point> processDraw;

        public CustomDynamicRenderer(Action<DrawingContext,Point,Point> c)
        {
            processDraw = c;
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            // Allocate memory to store the previous point to draw from.
            prevPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            base.OnStylusDown(rawStylusInput);
            
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            first = false;
            base.OnStylusUp(rawStylusInput);
        }

        protected override void OnDraw(DrawingContext drawingContext,
                                       StylusPointCollection stylusPoints,
                                       Geometry geometry, Brush fillBrush)
        {
            
            // Create a new Brush, if necessary.
            //if (brush == null)
            //{
            //    brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 20d);
            //}

            // Create a new Pen, if necessary.
            //if (pen == null)
            //{
            //    pen = new Pen(brush, 2d);
            //}

            if (!first)
            {
                prevPoint=(Point)stylusPoints[0];
                first = true;
            }

            LastPoint = (Point)stylusPoints[stylusPoints.Count - 1];

            processDraw(drawingContext, prevPoint, LastPoint);

            ///drawingContext.DrawRectangle(null, pen,  new Rect(prevPoint, LastPoint));
        }
    }
}
