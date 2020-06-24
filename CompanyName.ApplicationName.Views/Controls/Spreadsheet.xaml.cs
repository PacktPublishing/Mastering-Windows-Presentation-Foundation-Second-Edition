using CompanyName.ApplicationName.DataModels;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Interaction logic for Spreadsheet.xaml
    /// </summary>
    public partial class Spreadsheet : DataGrid
    {
        private Canvas selectionRectangleCanvas;
        private ScrollViewer scrollViewer;
        private Rectangle selectionRectangle;
        private bool isSelectionRectangleInitialized = false;

        /// <summary>
        /// Initializes a new empty Spreadsheet object with default values.
        /// </summary>
        public Spreadsheet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the System.Windows.Controls.ItemsControl.ItemsSource property changes.
        /// </summary>
        /// <param name="oldValue">The old source.</param>
        /// <param name="newValue">The new source.</param>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (!(newValue is DataRowCollection rows) || rows.Count == 0) return;
            Cell[] cells = rows[0].ItemArray.Cast<Cell>().ToArray();
            Columns.Clear();
            DataTemplate cellTemplate = (DataTemplate)FindResource("CellTemplate");
            for (int i = 0; i < cells.Length; i++)
            {
                DataGridBoundTemplateColumn column = new DataGridBoundTemplateColumn
                {
                    Header = GetColumnName(i + 1),
                    CellTemplate = cellTemplate,
                    Binding = new Binding($"[{i}]"),
                    Width = cells[i].Width
                };
                Columns.Add(column);
            }
        }

        private string GetColumnName(int index)
        {
            if (index <= 26) return ((char)(index + 64)).ToString();
            if (index % 26 == 0) return string.Concat(GetColumnName(index / 26 - 1), "Z");
            return string.Concat(GetColumnName(index / 26), GetColumnName(index % 26));
        }

        /// <summary>
        /// Invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate.
        /// </summary>
        public override void OnApplyTemplate()
        {
            scrollViewer = Template.FindName("DG_ScrollViewer", this) as ScrollViewer;
        }

        private void SpreadsheetScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (selectionRectangleCanvas == null) GetCanvasReference();
            TranslateTransform selectionRectangleCanvasTransform = selectionRectangleCanvas.RenderTransform as TranslateTransform;
            selectionRectangleCanvas.RenderTransform = new TranslateTransform(selectionRectangleCanvasTransform.X - e.HorizontalChange, selectionRectangleCanvasTransform.Y - e.VerticalChange);
        }

        private void GetCanvasReference()
        {
            ControlTemplate scrollViewerControlTemplate = scrollViewer.Template;
            selectionRectangleCanvas = scrollViewerControlTemplate.FindName("SelectionRectangleCanvas", scrollViewer) as Canvas;
            selectionRectangleCanvas.RenderTransform = new TranslateTransform();
        }

        /// <summary>
        /// Raises the System.Windows.Controls.DataGrid.SelectedCellsChanged event.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnSelectedCellsChanged(SelectedCellsChangedEventArgs e)
        {
            // base.OnSelectedCellsChanged(e);
            if (e.AddedCells != null && e.AddedCells.Count == 1)
            {
                DataGridCellInfo cellInfo = e.AddedCells[0];
                if (!cellInfo.IsValid) return;
                FrameworkElement cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                if (cellContent == null) return;
                DataGridCell dataGridCell = (DataGridCell)cellContent.Parent;
                if (dataGridCell == null) return;
                Point relativePoint = dataGridCell.TransformToAncestor(this).Transform(new Point(0, 0));
                Point startPosition = new Point(relativePoint.X - 3, relativePoint.Y - 3);
                Point endPosition = new Point(relativePoint.X + dataGridCell.ActualWidth, relativePoint.Y + dataGridCell.ActualHeight);
                UpdateSelectionRectangle(startPosition, endPosition);
            }
        }

        private void UpdateSelectionRectangle(Point startPosition, Point endPosition)
        {
            TimeSpan duration = TimeSpan.FromMilliseconds(150);
            if (!isSelectionRectangleInitialized) InitializeSelectionRectangle(startPosition, endPosition);
            else
            {
                selectionRectangle.BeginAnimation(WidthProperty, new DoubleAnimation(endPosition.X - startPosition.X, duration), HandoffBehavior.Compose);
                selectionRectangle.BeginAnimation(HeightProperty, new DoubleAnimation(endPosition.Y - startPosition.Y, duration), HandoffBehavior.Compose);
            }
            TranslateTransform translateTransform = selectionRectangle.RenderTransform as TranslateTransform;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(startPosition.X - RowHeaderWidth + scrollViewer.HorizontalOffset, duration), HandoffBehavior.Compose);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(startPosition.Y - ColumnHeaderHeight + scrollViewer.VerticalOffset, duration), HandoffBehavior.Compose);
        }

        private void InitializeSelectionRectangle(Point startPosition, Point endPosition)
        {
            selectionRectangle = new Rectangle();
            selectionRectangle.Width = endPosition.X - startPosition.X;
            selectionRectangle.Height = endPosition.Y - startPosition.Y;
            selectionRectangle.Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70));
            selectionRectangle.StrokeThickness = 2;
            selectionRectangle.RenderTransform = new TranslateTransform();
            Canvas.SetTop(selectionRectangle, 0); // row and column header
            Canvas.SetLeft(selectionRectangle, 0);
            selectionRectangleCanvas.Children.Add(selectionRectangle);
            isSelectionRectangleInitialized = true;
        }
    }
}