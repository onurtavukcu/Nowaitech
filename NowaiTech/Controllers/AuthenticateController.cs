using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NowaitechDomain.Commands;
using NowaitechDomain.ExcelDbContext;
using NowaitechDomain.Operation;
using NowaitechDomain.Repository.CustomQuery;
using NowaitechShared.AuthenticateOperations;
using NowaitechShared.DTO.InputDTO;
using NowaitechShared.Models.ExcelEntities.Lager;
using NowaitechShared.Models.User;
using System.Configuration;
using System.Text;

namespace NowaiTechAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly EntityDbContext _dbContext;

        public AuthenticateController(EntityDbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = dbContext;           
        }


        [HttpPost]
        public async Task<IActionResult> Authenticate(UserDTO user, CancellationToken cancellationToken)
        {
            var instance = new GetUserCommand(_dbContext, _configuration);

            var token = instance.GetAllUser(user);

            string str = "test";
            string result = str.GetFirstCar();
                        
            return await Task.FromResult<IActionResult>(Ok(token.Result));
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(UserDTO request, CancellationToken cancellationToken)  // Bu endpoint test için. Admin Kullanıcısı statik olarak elle tanımlanmalı.
        {            
            User user = new User();

            var userNameQuery = _dbContext.UserInputs.SingleOrDefault(usr => usr.Username == request.Username);

            if (userNameQuery is not null)
            {
                return BadRequest("User Already Exist");
            }

            if (request.Username is null || request.Password is null)
            {
                return BadRequest("Enter Valid Username or Password!");
            }
            var hashResult = HashPassword.CreateHash(request.Password); //fix returned string

            user.Username = request.Username;
            user.PasswordHash = hashResult;
            user.UserTypeId = 1;
                        
            await _dbContext.UserInputs.AddAsync(user, cancellationToken);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result != 1)
            {
                return BadRequest("User Can No Be Save!");
            }

            return await Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpPost]
        [Route("RegisterRegularUser")]
        public async Task<IActionResult> RegisterRegularUser(UserDTO request, CancellationToken cancellationToken)
        {
            User user = new User();

            var userNameQuery = _dbContext.UserInputs.SingleOrDefault(usr => usr.Username == request.Username);

            //Option<User> userNameQuery = _dbContext.UserInputs.SingleOrDefault(usr => usr.Username == request.Username) is User user1 ? user1 : Option<User>.None;
            //var userNameQuery1 = Prelude.Optional(_dbContext.UserInputs.SingleOrDefault(usr => usr.Username == request.Username));
            //var resdasd = userNameQuery.Some(x => x.Username == "basd");

            var resuls = _dbContext.Set<User>().GetByCustomId(x => x.Username == request.Username);

            if (userNameQuery is not null)
            {
                return BadRequest("User Already Exist");
            }

            if (request.Username.IsNullOrEmpty() || request.Password.IsNullOrEmpty())
            {
                return BadRequest("Enter Valid Username or Password!");
            }
            var hashResult = HashPassword.CreateHash(request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashResult;
            user.UserTypeId = 2;

            _dbContext.UserInputs.AddAsync(user);

            var result = _dbContext.SaveChanges();

            if (result != 1)
            {
                return BadRequest("User Can No Be Save!");
            }

            return await Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpGet]
        [Route("testAuthAdmin")]
        [Authorize(Roles = "1")]
        public ActionResult<string> TestAuthAdmin()
        {
            var userName = User?.Identity?.Name;

            return Ok(userName);
        }

        [HttpGet]
        [Route("testAuthRergular")]
        [Authorize(Roles = "2")]
        public ActionResult<string> TestAuthRegular()
        {
            var userName = User?.Identity?.Name;

            return Ok(userName);
        }
    }
}
