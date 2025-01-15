using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Core.Enums;
using SafeTravelApp.Data;
using SafeTravelApp.Models;
using SafeTravelApp.Security;

namespace SafeTravelApp.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(SafeTravelAppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            if (!EncryptionUtil.IsValidPassword(password, user.Password!))
            {
                return null;
            }
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username) =>
            await context.Users!.FirstOrDefaultAsync(u => u.Username == username || u.Email == username);

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await context.Users!
                .Include(u => u.Details)
                .Where(t => t.Details!.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize, List<Func<User, bool>> predicates)
        {
            int skip = (pageNumber - 1) * pageSize;
            IQueryable<User> query = context.Users!; //toDO: ελεγχος .Skip(skip).Take(pageSize);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }
            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<PaginatedResult<User>> GetAllUsersPagedAsync(int pageNumber, int pageSize, List<Func<User, bool>> predicates)
        {
            var totalRecords = await context.Users!.CountAsync();

            int skip = (pageNumber - 1) * pageSize;
            IQueryable<User> query = context.Users!
               .Skip(skip)
               .Take(pageSize);

            if (predicates != null && predicates.Any())
            {
                query = query.Where(u => predicates.All(predicate => predicate(u)));
            }

            var usersFiltered = await query.ToListAsync();

            return new PaginatedResult<User>
            {
                Data = usersFiltered,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existingUser = await context.Users!.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser is null) return null;
            if (existingUser.Id != id) return null;  //ωστε να μην παει να τροποποιησει καποιος αλλον χρηστη

            context.Users!.Attach(user);
            context.Entry(user).State = EntityState.Modified;
            return existingUser;
        }
               
        
    }
        
}
