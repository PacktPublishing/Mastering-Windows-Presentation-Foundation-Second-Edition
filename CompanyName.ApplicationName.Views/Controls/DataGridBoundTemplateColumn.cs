using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Represents a System.Windows.Controls.DataGrid column that hosts data bindable template-specified content in its cells.
    /// </summary>
    public class DataGridBoundTemplateColumn : DataGridTemplateColumn
    {
        /// <summary>
        /// The System.Windows.Data.Binding object to set on the generated element from the specified cell.
        /// </summary>
        public Binding Binding { get; set; }

        /// <summary>
        /// Gets an element defined by the System.Windows.Controls.DataGridTemplateColumn.CellTemplate that is bound to the column's System.Windows.Controls.DataGridBoundColumn.Binding property value.
        /// </summary>
        /// <param name="cell">The cell that will contain the generated element.</param>
        /// <param name="dataItem">The data item represented by the row that contains the intended cell.</param>
        /// <returns>A new, read-only element that is bound to the column's System.Windows.Controls.DataGridBoundColumn.Binding property value.</returns>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement element = base.GenerateElement(cell, dataItem);
            element.SetBinding(ContentPresenter.ContentProperty, Binding);
            return element;
        }
    }
}