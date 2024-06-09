using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using System.Data;

namespace Data_Access_Layer
{
    public class DALLogin
    {
        private readonly AppDbContext _cIDbContext;
        public DALLogin(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public User LoginUser(Login user)
        {
            User userObj = new User();
            try
            {
                    var query = from u in _cIDbContext.User
                                where u.EmailAddress == user.EmailAddress && u.IsDeleted == false
                                select new
                                {
                                    u.Id,
                                    u.FirstName,
                                    u.LastName,
                                    u.PhoneNumber,
                                    u.EmailAddress,
                                    u.UserType,
                                    u.Password,
                                    UserImage = u.UserImage
                                };

                    var userData = query.FirstOrDefault();

                    if (userData != null)
                    {
                        if (userData.Password == user.Password)
                        {
                            userObj.Id = userData.Id;
                            userObj.FirstName = userData.FirstName;
                            userObj.LastName = userData.LastName;
                            userObj.PhoneNumber = userData.PhoneNumber;
                            userObj.EmailAddress = userData.EmailAddress;
                            userObj.UserType = userData.UserType;
                            userObj.UserImage = userData.UserImage;
                            userObj.Message = "Login Successfully";
                        }
                        else
                        {
                            userObj.Message = "Incorrect Password.";
                        }
                    }
                    else
                    {
                        userObj.Message = "Email Address Not Found.";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userObj;
        }

        public string Register(User user)
        {
            string result = string.Empty;
            try
            {
                bool emailExists = _cIDbContext.User.Any(u => u.EmailAddress == user.EmailAddress && !u.IsDeleted);
                if (!emailExists)
                {
                    string maxEmployeeIdStr = _cIDbContext.UserDetail.Max(ud => ud.EmployeeId);
                    int maxEmployeeId = 0;
                    if (!string.IsNullOrEmpty(maxEmployeeIdStr))
                    {
                        if (int.TryParse(maxEmployeeIdStr, out int parsedEmployeeId))
                        {
                            maxEmployeeId = parsedEmployeeId;
                        }
                        else
                        {
                            throw new Exception("Error while converting string to int.");
                        }
                    }
                    int newEmployeeId = maxEmployeeId + 1;

                    var newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    _cIDbContext.User.Add(newUser);
                    _cIDbContext.SaveChanges();
                    var newUserDetail = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeeId.ToString(),
                        Department = "IT",
                        Status = true,
                        CreatedDate = DateTime.UtcNow
                    };
                    _cIDbContext.UserDetail.Add(newUserDetail);
                    _cIDbContext.SaveChanges();
                    result = "User Register Successfully";
                }
                else
                {
                    throw new Exception("Email is Already Exist.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }       
        public User LoginUserDetailById(int id)
        {
            try
            {
                var User = _cIDbContext.User.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (User == null)
                {
                    throw new Exception("User profile not found.");
                }
                return User;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user profile.", ex);
            }
        }

        public UserDetail GetUserProfileDetailById(int userId)
        {
            try
            {
                var userDetail = _cIDbContext.UserDetail.FirstOrDefault(x => x.UserId == userId && !x.IsDeleted);
                if (userDetail == null)
                {
                    throw new Exception("User profile not found.");
                }
                return userDetail;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user profile.", ex);
            }
        }

        public string LoginUserProfileUpdate(UserDetail userDetail)
        {
            string result = "";
            try
            {
                var existingUserDetail = _cIDbContext.UserDetail.FirstOrDefault(u => u.Id == userDetail.Id && !u.IsDeleted);
                if (existingUserDetail != null)
                {
                    existingUserDetail.Name = userDetail.Name;
                    existingUserDetail.Surname = userDetail.Surname;
                    existingUserDetail.EmployeeId = userDetail.EmployeeId;
                    existingUserDetail.Manager = userDetail.Manager;
                    existingUserDetail.Title = userDetail.Title;
                    existingUserDetail.Department = userDetail.Department;
                    existingUserDetail.MyProfile = userDetail.MyProfile;
                    existingUserDetail.WhyIVolunteer = userDetail.WhyIVolunteer;
                    existingUserDetail.CountryId = userDetail.CountryId;
                    existingUserDetail.CityId = userDetail.CityId;
                    existingUserDetail.Avilability = userDetail.Avilability;
                    existingUserDetail.LinkdInUrl = userDetail.LinkdInUrl;
                    existingUserDetail.MySkills = userDetail.MySkills;
                    existingUserDetail.UserImage = userDetail.UserImage;
                    existingUserDetail.Status = userDetail.Status;
                    existingUserDetail.ModifiedDate = userDetail.ModifiedDate;
                    _cIDbContext.SaveChanges();
                    result = "Account Update Successfully.";
                }
                else
                {
                    result = "Account Details is not found.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public string ChangePassword(User user)
        {
            var result = "";
            try
            {
                var existingUser = _cIDbContext.User.FirstOrDefault(x => x.Id == user.Id && !x.IsDeleted);
                if (existingUser != null)
                {
                    if (existingUser.Password == user.oldPassword) // Compare with old password
                    {
                        if (user.Password == user.ConfirmPassword) // Ensure new password and confirm password match
                        {
                            existingUser.Password = user.Password;
                            _cIDbContext.SaveChanges();
                            result = "Password updated successfully.";
                        }
                        else
                        {
                            result = "New password and confirm password do not match.";
                        }
                    }
                    else
                    {
                        result = "Old password is incorrect.";
                    }
                }
                else
                {
                    result = "User not found.";
                }
            }
            catch (Exception ex)
            {
                result = "An error occurred while changing the password.";
            }
            return result;
        }

        public string UpdateUser(User user)
        {
            string result = string.Empty;
            try
            {
                var existingUser = _cIDbContext.User.FirstOrDefault(u => u.Id == user.Id && !u.IsDeleted);
                if (existingUser != null)
                {
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.EmailAddress = user.EmailAddress;
                    existingUser.Password = user.Password;
                    existingUser.UserType = user.UserType;
                    existingUser.ModifiedDate = DateTime.UtcNow;

                    var existingUserDetail = _cIDbContext.UserDetail.FirstOrDefault(ud => ud.UserId == user.Id);
                    if (existingUserDetail != null)
                    {
                        existingUserDetail.Name = user.FirstName;
                        existingUserDetail.Surname = user.LastName;
                        existingUserDetail.ModifiedDate = DateTime.UtcNow;
                    }

                    _cIDbContext.SaveChanges();
                    result = "User updated successfully";
                }
                else
                {
                    throw new Exception("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user.", ex);
            }
            return result;
        }

        public  User GetUserById(int id)
        {
            try
            {
                var user = _cIDbContext.User.FirstOrDefault(ud => ud.Id == id);
                if(user == null)
                {
                    throw new Exception("user not found");
                }
                return user;
            }
            catch (Exception ex) 
            {
                throw new Exception();
            }
            
        }

    }
}
