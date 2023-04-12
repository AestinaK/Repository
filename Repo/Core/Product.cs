using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repo.Core
{
    public class Product: IEntity
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string BookName { get; set; } = null!;
        [Required]
       
        public int Price { get; set; }
        [Required]
        public int Qty { get; set; }
        
    }
}
