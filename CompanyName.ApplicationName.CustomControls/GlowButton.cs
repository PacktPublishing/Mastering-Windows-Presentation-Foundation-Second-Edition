using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CompanyName.ApplicationName.CustomControls.Enums;

namespace CompanyName.ApplicationName.CustomControls
{
    /// <summary>
    /// Represents a UI button with a customizable coloured glow effect.
    /// </summary>
    [TemplatePart(Name = "PART_Root", Type = typeof(Grid))]
    public class GlowButton : ButtonBase
    {
        private RadialGradientBrush glowBrush = null;

        /// <summary>
        /// Specifies the default style key for this class.
        /// </summary>
        static GlowButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GlowButton), new FrameworkPropertyMetadata(typeof(GlowButton)));
        }

        /// <summary>
        /// Gets or sets the GlowMode enumeration of the GlowButton object.
        /// </summary>
        public GlowMode GlowMode { get; set; } = GlowMode.FullCenterMovement;

        /// <summary>
        /// Represents the Color value to display in the GlowButton object.
        /// </summary>
        public static readonly DependencyProperty GlowColorProperty = DependencyProperty.Register(nameof(GlowColor), typeof(Color), typeof(GlowButton), new PropertyMetadata(Color.FromArgb(121, 71, 0, 255), OnGlowColorChanged));

        /// <summary>
        /// Gets or sets the Color value to display in the GlowButton object.
        /// </summary>
        public Color GlowColor
        {
            get { return (Color)GetValue(GlowColorProperty); }
            set { SetValue(GlowColorProperty, value); }
        }
        
        private static void OnGlowColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ((GlowButton)dependencyObject).SetGlowColor((Color)e.NewValue);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            Grid rootGrid = GetTemplateChild("PART_Root") as Grid;
            if (rootGrid != null)
            {
                rootGrid.MouseMove += Grid_MouseMove;
                glowBrush = (RadialGradientBrush)rootGrid.FindResource("GlowBrush");
                SetGlowColor(GlowColor);
            }
        }

        private void SetGlowColor(Color value)
        {
            GlowColor = Color.FromArgb(121, value.R, value.G, value.B);
            if (glowBrush != null)
            {
                GradientStop gradientStop = glowBrush.GradientStops[2];
                gradientStop.Color = GlowColor;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            if (grid.IsMouseOver && glowBrush != null)
            {
                Point mousePosition = e.GetPosition(grid);
                double x = mousePosition.X / ActualWidth;
                double y = GlowMode != GlowMode.HorizontalCenterMovement ? mousePosition.Y / ActualHeight : glowBrush.Center.Y;
                glowBrush.Center = new Point(x, y);
                if (GlowMode == GlowMode.HorizontalCenterMovement) glowBrush.GradientOrigin = new Point(x, glowBrush.GradientOrigin.Y);
                else if (GlowMode == GlowMode.FullCenterMovement) glowBrush.GradientOrigin = new Point(x, y);
            }
        }
    }
}