using Data_Access_Layer;
using Data_Access_Layer.JWTService;
using Data_Access_Layer.Repository.Entities;

namespace Business_logic_Layer
{
    public class BALLogin
    {
        private readonly DALLogin _dalLogin;
        private readonly JwtService _jwtService;
        ResponseResult result = new ResponseResult();

        public BALLogin(DALLogin dalLogin, JwtService jwtService)
        {
            _dalLogin = dalLogin;
            _jwtService = jwtService;
        }

        public ResponseResult LoginUser(Login login)
        {
            try
            {
                User userObj = _dalLogin.LoginUser(login);

                if (userObj != null)
                {
                    if (userObj.Message == "Login Successfully")
                    {
                        result.Message = userObj.Message;
                        result.Result = ResponseStatus.Success;
                        result.Data = _jwtService.GenerateToken(
                            userObj.Id.ToString(),
                            userObj.FirstName,
                            userObj.LastName,
                            userObj.PhoneNumber,
                            userObj.EmailAddress,
                            userObj.UserType,
                            userObj.UserImage
                        );
                    }
                    else
                    {
                        result.Message = userObj.Message;
                        result.Result = ResponseStatus.Error;
                    }
                }
                else
                {
                    result.Message = "Error in Login";
                    result.Result = ResponseStatus.Error;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public string Register(User user)
        {
            return _dalLogin.Register(user);
        }

        public User LoginUserDetailById(int id)
        {
            return _dalLogin.LoginUserDetailById(id);
        }
        public UserDetail GetUserProfileDetailById(int userId)
        {
            return _dalLogin.GetUserProfileDetailById(userId);
        }

        public string LoginUserProfileUpdate(UserDetail userDetail)
        {
            return _dalLogin.LoginUserProfileUpdate(userDetail);
        }

        public string ChangePassword(User user)
        {
            return _dalLogin.ChangePassword(user);
        }

        public string UpdateUser(User user)
        {
            return _dalLogin.UpdateUser(user);
        }

        public User GetUserById(int id)
        {
            return _dalLogin.GetUserById(id);
        }
    }
}