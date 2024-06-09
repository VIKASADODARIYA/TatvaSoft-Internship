using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class DALCommon
    {
        private readonly AppDbContext _cIDbContext;

        public DALCommon(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<Country> CountryList()
        {
            /*return _cIDbContext.Country.ToList();*/
            return _cIDbContext.Country.Include(c => c.Cities).ToList();
        }
        public List<City> CityList(int countryId)
        {
            // Return the list of cities for a specific country
            return _cIDbContext.City.Where(c => c.CountryId == countryId).ToList();
        }
        public List<Country> MissionCountryList()
        {
            /*return _cIDbContext.Country.ToList();*/
            return _cIDbContext.Country.Include(c => c.Cities).ToList();
        }
        public List<City> MissionCityList()
        {
            return _cIDbContext.City.ToList();
        }
        public List<MissionTheme> MissionThemeList()
        {
            return _cIDbContext.MissionTheme.ToList();
        }
        public List<MissionSkill> MissionSkillList()
        {
            return _cIDbContext.MissionSkill.ToList();
        }
        public List<Missions> MissionTitleList()
        {
            return _cIDbContext.Missions.Select(c => new Missions { MissionTitle = c.MissionTitle }).ToList();
        }
        public string AddMission(Missions mission)
        {
            string result = "";
            try
            {
                _cIDbContext.Missions.Add(mission);
                _cIDbContext.SaveChanges();
                result = "Mission added Successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public string AddUserSkill(int userId, string MySkills)
        {
            string result = "";
            try
            {
                var user = _cIDbContext.UserDetail.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.MySkills = MySkills;
                    _cIDbContext.SaveChanges();
                    result = "User Skill added Successfully.";
                }
                else
                {
                    result = "User not found.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<string> GetUserSkill(int id)
        {
            var userDetail = _cIDbContext.UserDetail.FirstOrDefault(x => x.UserId == id);

            if (userDetail != null && !string.IsNullOrEmpty(userDetail.MySkills))
            {
                return userDetail.MySkills.Split(',').ToList();
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
