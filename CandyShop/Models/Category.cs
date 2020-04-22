using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Categoryname { get; set; }
        public string CategoryDescriptory { get; set; }

        public List<Candy> Candies { get; set; }

    }
}
