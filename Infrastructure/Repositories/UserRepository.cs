using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class UserRepository : Domain.Interfaces.IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> AddUserAsync(User user)
        {
            var sql = @"
            INSERT INTO Users (Username, Password, FirstName, LastName, Device, IpAddress, Balance, IsFirstSignIn)
            VALUES (@Username, @Password, @FirstName, @LastName, @Device, @IpAddress, @Balance, @IsFirstSignIn);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _dbConnection.ExecuteScalarAsync<int>(sql, new
            {
                user.Username,
                user.Password,
                user.FirstName,
                user.LastName,
                user.Device,
                user.IpAddress,
                user.Balance,
                user.IsFirstSignIn
            });
        }

        // Get user by username for authentication
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task UpdateUserAsync(User user)
        {
            var sql = @"
                UPDATE Users 
                SET 
                    Password = @Password,
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    Device = @Device, 
                    IpAddress = @IpAddress, 
                    IsFirstSignIn = @IsFirstSignIn, 
                    Balance = @Balance 
                WHERE Username = @Username";

            await _dbConnection.ExecuteAsync(sql, new
            {
                user.Password,
                user.FirstName,
                user.LastName,
                user.Device,
                user.IpAddress,
                user.IsFirstSignIn,
                user.Balance,
                user.Username
            });
        }

    }
}
