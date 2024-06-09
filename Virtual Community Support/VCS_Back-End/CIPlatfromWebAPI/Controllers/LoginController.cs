using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {       
        private readonly BALLogin _balLogin;
        ResponseResult result = new ResponseResult();
        public LoginController(BALLogin balLogin)
        {           
            _balLogin = balLogin;
        }
            

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(Login user)
        {
            try
            {                                
                result.Data = _balLogin.LoginUser(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public ResponseResult RegisterUser(User user)
        {
            try
            {
                result.Data = _balLogin.Register(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)    
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("LoginUserDetailById/{id}")]
        public ActionResult<ResponseResult> LoginUserDetailById(int id)
        {
            try
            {
                var result = _balLogin.LoginUserDetailById(id);
                return Ok(new ResponseResult { Data = result, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetUserProfileDetailById/{userId}")]
        public ActionResult<ResponseResult> GetUserProfileDetailById(int userId)
        {
            try
            {
                var result = _balLogin.GetUserProfileDetailById(userId);
                return Ok(new ResponseResult { Data = result, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("LoginUserProfileUpdate")]
        public ResponseResult LoginUserProfileUpdate(UserDetail userDetail)
        {
            try
            {
                result.Data = _balLogin.LoginUserProfileUpdate(userDetail);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("ChangePassword")]
        public ResponseResult ChangePassword(User user)
        {
            try
            {
                result.Data = _balLogin.ChangePassword(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public ActionResult<ResponseResult> GetUserById(int id)
        {
            try
            {
                var result = _balLogin.GetUserById(id);
                return Ok(new ResponseResult{ Data = result, Result= ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public ResponseResult UpdateUser(User user)
        {
            try
            {
                result.Data = _balLogin.UpdateUser(user);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
