using AOWebApp.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.ViewModels
{
    public class ItemSearch
    {
        public string SearchText { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
        public PaginatedList<ViewModels.ItemDetail>? Items { get; set; }
        public string SortOrder { get; set; } = "";
        public int? PageNumber { get; set; } = 1;
    }
}
