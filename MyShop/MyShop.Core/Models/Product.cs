using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
      
namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        [StringLength(20)]                   //requires System.ComponentModel.DataAnnotations 
        [DisplayName("Product Name")]        //requires System.ComponentModel;
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Category {get; set;}
        public string Image { get; set; }
        
        /* I like to add my own constructor for product as this will allow us to have flexibility in database technologies we can use*/
        public Product()
        {
            this.Id = Guid.NewGuid().ToString(); //Generated ID converted to a string will be stored to an Id.
        }
    }
}
