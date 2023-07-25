using Microsoft.EntityFrameworkCore;
using NLayer.Core.Model;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _dbContext.Categories.Include(x => x.Products).Where(x => x.ID == categoryId).SingleOrDefaultAsync(); 
        }
    }
}
