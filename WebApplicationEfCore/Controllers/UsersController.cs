using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using WebApplicationEfCore.Models;
using WebApplicationEfCore.Models.DataModel;
using WebApplicationEfCore.Models.ViewModel;

namespace WebApplicationEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public UsersController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("v1/get-connection")]
        public async Task<IActionResult> getConnection()
        {
            try
            {
                var isconnection = await this._dbContext.Database.CanConnectAsync();
                if (isconnection)
                {
                    return await Task.FromResult(Ok(new { isconnection = isconnection }));
                }
                else
                {
                    return await Task.FromResult(BadRequest());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("v1/get-user/{id}")]
        public async Task<IActionResult> getUser(int id)
        {
            Users users = new Users();
            try
            {
                users = await _dbContext.users.FirstOrDefaultAsync(x => x.Id == id);

            }
            catch (Exception ex)
            {
                await Task.FromResult(BadRequest(ex.StackTrace));
            }

            return await Task.FromResult(Ok(users));
        }
        [HttpPost("v1/insert-user")]
        public async Task<IActionResult> InsertUser([FromBody] User userModel)
        {
            var result = new ActionResultModel();
            try
            {
                var user = new Users() {

                    CreatedDate = userModel.CreatedDate,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    DepartmentId = userModel.DepartmentId,
                    IsActive = userModel.IsActive
                    , UpdatedDate = DateTime.Now
                };

                _dbContext.Add(user);
               await _dbContext.SaveChangesAsync();

                result = new ActionResultModel()
                {
                    code = "200",
                    message_code = "Status200Ok",
                    message = "insert data success!",
                };
            }
            catch (Exception ex)
            {
                result = new ActionResultModel()
                {
                    code = "400",
                    message_code = "Status400BadRequest",
                    message = ex.Message,

                };
            }
            return await Task.FromResult(Ok(result));
        }
        [HttpPut("v1/update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] Users userModel)
        {
            Users users = new Users();
            try
            {
                users = await _dbContext.users.FirstOrDefaultAsync(x => x.Id == userModel.Id);

                if (users != null)
                {
                    users.FirstName = userModel.FirstName;
                    users.LastName = userModel.LastName;
                    users.UpdatedDate = DateTime.Now;
                    users.IsActive = userModel.IsActive;
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromResult(BadRequest(ex.StackTrace));
            }

            return await Task.FromResult(Ok(users));
        }
        [HttpDelete("v1/delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var users = await _dbContext.users.FirstOrDefaultAsync(x => x.Id == id);

                if (users != null)
                {
                    _dbContext.Remove(users);

                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                await Task.FromResult(BadRequest(ex.StackTrace));
            }
            return Ok($"delete user id {id} success");
        }
    }
}
