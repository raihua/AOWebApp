using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.ViewModels
{
    public class CustomerSearch
    {
        public string SearchText { get; set; } = string.Empty;

        public string Suburb { get; set; } = string.Empty;

        public SelectList SuburbList { get; set; } = new SelectList(Enumerable.Empty<string>());

        public List<Models.Customer> Customers { get; set; } = new List<Models.Customer>();

        public HashSet<string> CustomerNames { get; set; } = new HashSet<string>();
    }
}
