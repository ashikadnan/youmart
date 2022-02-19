using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructre.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            var productBrand = await _context.ProductBrands.ToListAsync();
            return productBrand;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType).FirstOrDefaultAsync(n=>n.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var product = await _context.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType).ToListAsync();
            return product;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductsTypeAync()
        {
            var productType = await _context.ProductTypes.ToListAsync();
            return productType;
        }
    }
}