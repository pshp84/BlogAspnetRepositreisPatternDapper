using Dapper;
using SampleDotNetRepositoryPatternDapper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotNetRepositoryPatternDapper.Infra
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<User>(sql);
        }

        public async Task AddAsync(User user)
        {
            try
            {
                var sql = "INSERT INTO Users (Name,UserName,Password) VALUES (@Name,@userName,@password)";
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(sql, new { Name = user.Name, userName = user.UserName, password = user.Password });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(User user)
        {
            var sql = "UPDATE Users SET Name = @Name , UserName = @userName , Password = @password WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<User> LoginUser (UserLogin login)
        {
            var sql = "SELECT * FROM Users WHERE UserName = @userName and Password = @password";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { userName = login.UserName , password = login.Password  });
        }

    }
}
