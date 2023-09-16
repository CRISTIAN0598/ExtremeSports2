using System.ComponentModel.DataAnnotations;

namespace ExtremeSports2.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://shoppingprueba.azurewebsites.net/images/NoImage.jpg"
            : $"https://shoppingcristian.blob.core.windows.net/products/{ImageId}";
    }
}
