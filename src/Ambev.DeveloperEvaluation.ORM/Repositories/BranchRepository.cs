using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DefaultContext _context;

        public BranchRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken)
        {
            await _context.Branches.AddAsync(branch, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return branch;
        }

        public async Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Branches.ToListAsync(cancellationToken);
        }

        public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync(cancellationToken);
            return branch;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var branch = await GetByIdAsync(id, cancellationToken);
            if (branch == null) return false;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
