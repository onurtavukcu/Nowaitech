using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NowaitechDomain.ExcelDbContext;
using NowaitechShared.AuthenticateOperations;
using NowaitechShared.DTO.InputDTO;
using NowaitechShared.Maybe;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Configuration;

namespace NowaitechDomain.Operation
{
    public class GetUserCommand
    {
        EntityDbContext _dbContext;
        IConfiguration _configuration;

        public GetUserCommand(EntityDbContext dbContext, IConfiguration configuration)
        {
            //if (_dbContext.HasNoValue)
            //{
            //    Console.WriteLine("Db Context Oluşturulamadı.");
            //}

            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> GetAllUser(UserDTO user)
        {
            var userNameQuery = _dbContext
                .UserInputs
                .Where(usr => usr.Username == user.Username)
                .SingleOrDefault();


            if (user.Username.IsNullOrEmpty() || user.Password.IsNullOrEmpty())
            {
                return "Username Or Password is Null";
            }

            if (userNameQuery is null)
            {
                return "User Not Found";
            }

            if (!HashPassword.VerifyPasswordHash(user.Password, userNameQuery.PasswordHash))
            {
                return "Wrong Password";
            }

            var tokenInstance = new CreateToken(_configuration);

            string token = tokenInstance.CreateTokens(userNameQuery);

            if (token is null)
            {
                return "Token Can Not Create";
            }

            return token;
        }
    }
}
