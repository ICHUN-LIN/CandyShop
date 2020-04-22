using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;
using Microsoft.EntityFrameworkCore; // use DbContext for EF Core

namespace CandyShop.Models
{
    public class CandyRepository : ICandyRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appDbContext"></param>
        public CandyRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        
        public IEnumerable<Candy> GetAllCandy 
        {
            /*what is include*/
            get { return _appDbContext.Candies.Include(c=>c.Category);  }
        }

        public IEnumerable<Candy> GetCandyOnSale
        {
            /*what is include*/
            get { return _appDbContext.Candies.Include(c => c.Category).Where(p =>p.IsOnSale); }
        }

        public Candy GetCandyById(int candyId)
        {
            return GetAllCandy.FirstOrDefault(c => c.CandyId == candyId);
        }
    }
}
