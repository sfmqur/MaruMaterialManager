using MaruMaterialManager.Model;
using MaruMaterialManager.Model.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaruMaterialManager.Services
{
    public class PartService
    {
        private readonly PartsDbContext _context;

        public PartService(PartsDbContext context)
        {
            _context = context;
        }

        // Get all parts
        public async Task<List<Part>> GetPartsAsync()
        {
            return await _context.Parts
                .Include(p => p.Category)
                .Include(p => p.PreferredSupplier)
                .ToListAsync();
        }

        // Get a single part by ID
        public async Task<Part?> GetPartAsync(int id)
        {
            return await _context.Parts
                .Include(p => p.Category)
                .Include(p => p.PreferredSupplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Create a new part
        public async Task<Part> CreatePartAsync(Part part)
        {
            part.CreatedAt = DateTime.UtcNow;
            _context.Parts.Add(part);
            await _context.SaveChangesAsync();
            return part;
        }

        // Update an existing part
        public async Task<bool> UpdatePartAsync(Part part)
        {
            _context.Entry(part).State = EntityState.Modified;
            part.UpdatedAt = DateTime.UtcNow;
            
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PartExists(part.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        // Delete a part
        public async Task<bool> DeletePartAsync(int id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null)
            {
                return false;
            }

            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if part exists
        private async Task<bool> PartExists(int id)
        {
            return await _context.Parts.AnyAsync(e => e.Id == id);
        }

        // Get all categories for dropdown
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        // Get all suppliers for dropdown
        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
