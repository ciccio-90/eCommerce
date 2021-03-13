using System.Collections.Generic;

namespace eCommerce.Backoffice.Client.Model.Grids
{
    public class GridModel<T>
    {
        public List<GridColumn> Columns { get; set; }
        public List<T> Rows { get; set; }
        public bool UseAnnotations { get; set; } 
    }
}