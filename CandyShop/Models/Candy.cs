using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    
    /*
     This is like the reflection of database table
     */
    public class Candy
    {
        public int CandyId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Decimal Price { get; set; }
        public String ImageUrl { get; set; }
        public String ImageThumbnailUrl { get; set; }
        public bool IsOnSale { get; set; }

        public bool IsOnStock { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }
}
