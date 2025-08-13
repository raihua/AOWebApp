using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.ViewModels
{
    public class ItemSearch
    {
        public string SearchText { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
        public List<Models.Item>? Items { get; set; }

    }
}
