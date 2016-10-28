using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkCanvasTest
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:InkCanvasTest"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:InkCanvasTest;assembly=InkCanvasTest"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:customInkCanvas/>
    ///
    /// </summary>
    public class CustomInkCanvas:InkCanvas
    {
        static CustomInkCanvas()
        {
           // DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomInkCanvas), new FrameworkPropertyMetadata(typeof(CustomInkCanvas)));
        }

        /// <summary>
        /// 最终画板的可以画的图形类型
        /// </summary>
       


        #region 函数
        private void DrawRectangle(DrawingContext drawingContext, Point p1, Point p2)
        {
            drawingContext.DrawRectangle(null, new Pen(StrokeBrush, DrawStrokeThiness), new Rect(p1, p2));
        }

        private void DrawLine(DrawingContext drawingContext, Point p1, Point p2)
        {
            drawingContext.DrawLine(new Pen(StrokeBrush, DrawStrokeThiness), p1, p2);
        }

        private void DrawEllipse(DrawingContext drawingContext, Point p1, Point p2)
        {

            Vector v = p2 - p1;
            Point pcenter = p1 + v / 2;
            double radiusX = v.X/2;
            double radiusY = v.Y / 2;
            drawingContext.DrawEllipse(null, new Pen(StrokeBrush, DrawStrokeThiness), pcenter,radiusX,radiusY);
        }

        #endregion


        #region properties

        CustomDynamicRenderer rectangleRender;
        CustomDynamicRenderer lineRender;
        CustomDynamicRenderer ellipseRender;
        DynamicRenderer commonRender;
        #region DrawType
        public static DependencyProperty DrawTypeProperty = DependencyProperty.Register("DrawType", typeof(DrawTypeEum), typeof(CustomInkCanvas), new PropertyMetadata(DrawTypeEum.Ink,DrawTypeChanged));

        public DrawTypeEum DrawType
        {
            get
            {
                return (DrawTypeEum)GetValue(DrawTypeProperty);
            }
            set
            {
                SetValue(DrawTypeProperty, value);
            }
        }


        private static void DrawTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomInkCanvas o = d as CustomInkCanvas;
            
            switch ((DrawTypeEum)e.NewValue)
            {
                case DrawTypeEum.Ink:
                    o.DynamicRenderer = o.CommonRender;
                    break;
                case DrawTypeEum.Line:
                    o.DynamicRenderer = o.LineRender;
                    break;
                case DrawTypeEum.Rectangle:
                    o.DynamicRenderer = o.RectangleRender;
                    break;
                case DrawTypeEum.Ellipse:
                    o.DynamicRenderer = o.ElipseRender;
                    break;
            }
        }

        #endregion

        #region StrokeBrush
        public static DependencyProperty StrokeBrushProperty = DependencyProperty.Register("StrokeBrush", typeof(Brush), typeof(CustomInkCanvas), new PropertyMetadata(Brushes.Red,StrokeBrushChanged));

        private static void StrokeBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomInkCanvas p = d as CustomInkCanvas;
            DrawingAttributes at = (DrawingAttributes)d.GetValue(CustomInkCanvas.DefaultDrawingAttributesProperty);
            at.Color = ((SolidColorBrush)e.NewValue).Color;
        }

        public Brush StrokeBrush
        {
            get
            {
                return (Brush)GetValue(StrokeBrushProperty);
            }
            set
            {
                SetValue(StrokeBrushProperty, value);
            }
        }
        #endregion


        #region DrawStrokeThiness
        public static DependencyProperty DrawStrokeThinessProperty=DependencyProperty.Register("DrawStrokeThiness", typeof(double),typeof(CustomInkCanvas),new PropertyMetadata(1.0d));

        public double DrawStrokeThiness
        {
            get
            {
                return (double)GetValue(DrawStrokeThinessProperty);
            }

            set
            {
                SetValue(DrawStrokeThinessProperty, value);
            }
        }

        #endregion

        #region RectangleRender
        internal CustomDynamicRenderer RectangleRender
        {
            get
            {
                if (rectangleRender == null)
                {
                    return new CustomDynamicRenderer(DrawRectangle);
                }
                return rectangleRender;
            }

            set
            {
                rectangleRender = value;
            }
        }
        #endregion
        #region LineRender

        internal CustomDynamicRenderer LineRender
        {
            get
            {
                if (lineRender == null)
                {
                    lineRender = new CustomDynamicRenderer(DrawLine);
                }
                return lineRender;
            }

            set
            {
                lineRender = value;
            }
        }
        #endregion

        #region ElipseRender

        internal CustomDynamicRenderer ElipseRender
        {
            get
            {
                if (ellipseRender == null)
                {
                    ellipseRender = new CustomDynamicRenderer(DrawEllipse);
                }
                return ellipseRender;
            }

            set
            {
                ellipseRender = value;
            }
        }


        #endregion

        #region CommonRender
        public DynamicRenderer CommonRender
        {
            get
            {
                if (commonRender == null)
                {
                    commonRender = new DynamicRenderer();
                }
                return commonRender;
            }

            set
            {
                commonRender = value;
            }
        }
        #endregion
        #endregion


        public CustomInkCanvas() : base()
        {
            // Use the custom dynamic renderer on the
            // custom InkCanvas.
            this.DynamicRenderer =CommonRender;
           
        }


       
        private void CustomInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            // Remove the original stroke and add a custom stroke.
            this.Strokes.Remove(e.Stroke);
            Stroke st = new Stroke(e.Stroke.StylusPoints);
            //根据当前的类型判断使用画笔
            switch (DrawType)
            {
                case DrawTypeEum.Ink:
                  
                    break;
                case DrawTypeEum.Line:

                    st = new CustomStroke(e.Stroke.StylusPoints,DrawLine);
                    break;
                case DrawTypeEum.Rectangle:
       
                    st = new CustomStroke(e.Stroke.StylusPoints,DrawRectangle);
                    break;
                case DrawTypeEum.Ellipse:
                 
                    st = new CustomStroke(e.Stroke.StylusPoints, DrawEllipse);
                    break;
            }

            if (!this.Strokes.Contains(st))
            {
                this.Strokes.Add(st);
            }

            // Pass the custom stroke to base class' OnStrokeCollected method.
            InkCanvasStrokeCollectedEventArgs args =
                new InkCanvasStrokeCollectedEventArgs(st);
            base.OnStrokeCollected(args);

        }
    }

    public enum DrawTypeEum
    {
        Ink,
        Line,
        Rectangle,
        Ellipse
    }
}
