using Business_logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly BALCommon _balCommon;
        private readonly ResponseResult result = new ResponseResult();
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CommonController(BALCommon balCommon, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _balCommon = balCommon;
            _environment = environment;
        }

        [HttpGet("CountryList")]
        public ResponseResult CountryList()
        {
            try
            {
                result.Data = _balCommon.CountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("CityList/{countryId}")]
        public ResponseResult CityList(int countryId)
        {
            try
            {
                result.Data = _balCommon.CityList(countryId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("MissionCountryList")]
        public ResponseResult MissionCountryList()
        {
            try
            {
                result.Data = _balCommon.MissionCountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("MissionCityList")]
        public ResponseResult MissionCityList()
        {
            try
            {
                result.Data = _balCommon.MissionCityList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("MissionThemeList")]
        public ResponseResult MissionThemeList()
        {
            try
            {
                result.Data = _balCommon.MissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("MissionSkillList")]
        public ResponseResult MissionSkillList()
        {
            try
            {
                result.Data = _balCommon.MissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("MissionTitleList")]
        public ResponseResult MissionTitleList()
        {
            try
            {
                result.Data = _balCommon.MissionTitleList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        
        [HttpGet("AddMission")]
        public ResponseResult AddMission(Missions mission)
        {
            try
            {
                result.Data = _balCommon.AddMission(mission);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost("AddUserSkill")]
        public ResponseResult AddUserSkill([FromBody] UserDetail userDetail)
        {
            var result = new ResponseResult();
            try
            {
                // Call the BAL method with userId and newSkill
                result.Data = _balCommon.AddUserSkill(userDetail.UserId, userDetail.MySkills);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet("GetUserSkill/{id}")]
        public ResponseResult GetUserSkill(int id)
        {
            try
            {
                result.Data = _balCommon.GetUserSkill(id);
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
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> upload)
        {
            try
            {
                string filePath = "";
                string fullPath = "";
                var files = Request.Form.Files;
                List<string> fileList = new List<string>();
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePath = Path.Combine("UploadMissionImage", "Mission");
                        string fileRootPath = Path.Combine(_environment.WebRootPath, filePath);

                        if (!Directory.Exists(fileRootPath))
                        {
                            Directory.CreateDirectory(fileRootPath);
                        }

                        string name = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);
                        string fullFileName = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                        fullPath = Path.Combine(fileRootPath, fullFileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        fileList.Add(fullPath);
                    }
                }
                return Ok(fileList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
