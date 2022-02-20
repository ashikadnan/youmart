using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructre.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
            
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }

       
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();

            return result;
        }

         public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
           var result = await ApplySpecification(spec).FirstOrDefaultAsync();
           return result;
        }


        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            var result = await ApplySpecification(spec).ToListAsync();
            return result;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }


}