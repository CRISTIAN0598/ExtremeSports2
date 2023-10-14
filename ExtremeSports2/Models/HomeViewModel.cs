using ExtremeSports2.Data.Entities;
using ExtremeSports2.Common;

namespace ExtremeSports2.Models
{
    public class HomeViewModel
    {
        public PaginatedList<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
        public float Quantity { get; set; }
    }
}
