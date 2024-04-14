using System.Linq.Expressions;
using EmployeeGraphql.API.DbContext;
using EmployeeGraphql.API.Models;
using HotChocolate.Data;

namespace EmployeeGraphql.API.Repositories
{
    public class EmployeeRepository : GenericRepository<BaseEmployee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context, MyUserContext myUserContext) : base(context, myUserContext)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaseEmployee>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<IEnumerable<BaseEmployee>> GetAsync(Expression<Func<BaseEmployee, bool>> predicate)
        {
            return await base.GetAsync(predicate);
        }

        public async Task<BaseEmployee> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }


        public async Task<BaseEmployee> AddAsync(BaseEmployee employee)
        {
            return await base.AddAsync(employee);
        }

        public async Task<BaseEmployee> UpdateAsync(BaseEmployee employee)
        {
            return await base.UpdateAsync(employee);
        }

        public async Task<BaseEmployee> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        public async Task<IQueryable<IEmployee>> GetAllEmployeesFilteredAsync()
        {
            return await Task.FromResult(_context.Employees);
        }
    }
}