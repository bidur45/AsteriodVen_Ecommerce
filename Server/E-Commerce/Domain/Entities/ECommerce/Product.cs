using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ECommerce
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int  Stock  { get; set; }   
        public double Price { get; set; }
        public double? Rating { get; set; }
        public string? Img { get; set; }

    }
}
