using System.ComponentModel.DataAnnotations;

namespace AOWebApp.ViewModels
{
    public class ItemDetail
    {
        public required Models.Item TheItem { get; set; }
        public int NumberOfReviews { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double AverageRating { get; set; }
    }
}
