namespace Products.Backend.Models
{
    using Products.Domain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class ProductView : Product
    {
        [Display(Name="Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}