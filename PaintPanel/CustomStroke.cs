using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace InkCanvasTest
{
        public class CustomStroke:Stroke
        {

        Action<DrawingContext, Point, Point> draw;
            public CustomStroke(StylusPointCollection pts, Action<DrawingContext, Point, Point> drawact=null)
             : base(pts)
            {
                this.StylusPoints = pts;
               draw = drawact;
            }

            protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
            {
                if (drawingContext == null)
                {
                    throw new ArgumentNullException("drawingContext");
                }
                if (null == drawingAttributes)
                {
                    throw new ArgumentNullException("drawingAttributes");
                }

                if (StylusPoints.Count < 2)
                {
                    return;
                }

            DrawingAttributes originalDa = drawingAttributes.Clone();
            SolidColorBrush brush2 = new SolidColorBrush(originalDa.Color);
            Pen pen = new Pen(brush2, 1);
            brush2.Freeze();
            if (draw != null)
                {
                    draw(drawingContext,(Point)StylusPoints[0], (Point)StylusPoints[StylusPoints.Count - 1]);
                }
                else
                {
                    drawingContext.DrawRectangle(null, pen, new Rect((Point)StylusPoints[0], (Point)StylusPoints[StylusPoints.Count - 1]));
                }
            }
        }
    
}
