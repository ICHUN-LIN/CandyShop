using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;

namespace CandyShop.ViewModels
{
    /*contain all data needed by viewers*/
    public class CandyListViewModel
    {
        public IEnumerable<Candy> Candies { get; set; }
        public string CurrentCatecory { get; set; }
    }
}
