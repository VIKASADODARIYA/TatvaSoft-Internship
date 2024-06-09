using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BALCommon
    {
        private readonly DALCommon _dalCommon;

        public BALCommon(DALCommon dalCommon)
        {
            _dalCommon = dalCommon;
        }
        public List<Country> CountryList()
        {
            return _dalCommon.CountryList();
        }
        public List<City> CityList(int countryId)
        {
            // Call the corresponding method from DALCountry
            return _dalCommon.CityList(countryId);
        }
        public List<Country> MissionCountryList()
        {
            return _dalCommon.MissionCountryList();
        }
        public List<City> MissionCityList()
        {
            return _dalCommon.MissionCityList();
        }
        public List<MissionTheme> MissionThemeList()
        {
            return _dalCommon.MissionThemeList();
        }
        public List<MissionSkill> MissionSkillList()
        {
            return _dalCommon.MissionSkillList();
        }
        public List<Missions> MissionTitleList()
        {
            return _dalCommon.MissionTitleList();
        }
        public string AddMission(Missions mission)
        {
            return _dalCommon.AddMission(mission);
        }
        public string AddUserSkill(int userId, string newSkill)
        {
            return _dalCommon.AddUserSkill(userId, newSkill);
        }

        public List<string> GetUserSkill(int userId)
        {
            return  _dalCommon.GetUserSkill(userId);
        }

    }
}
