using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BALMission
    {
        private readonly DALMission _dalMission;

        public BALMission(DALMission dalMission)
        {
            _dalMission = dalMission;
        }

        public List<Missions> MissionList()
        {
            return _dalMission.MissionList();
        }
        public string AddMission(Missions mission)
        {
            return _dalMission.AddMission(mission);
        }
        public string DeleteMission(int missionId)
        {
            return _dalMission.DeleteMission(missionId);
        }
        public string UpdateMission(Missions mission)
        {
            return _dalMission.UpdateMission(mission);
        }
        public Missions MissionDetailById(int missionId)
        {
            return _dalMission.MissionDetailById(missionId);
        }
        public List<Missions> GetMissionThemeList()
        {
            return _dalMission.GetMissionThemeList();
        }
        public List<Missions> ClientSideMissionList(int userId)
        {
            return _dalMission.ClientSideMissionList(userId);
        }
        public string ApplyMission(MissionApplication missionApplication)
        {
            return _dalMission.ApplyMission(missionApplication);
        }
        public List<MissionApplication> GetMissionApplicationList()
        {
            return _dalMission.GetMissionApplicationList();
        }
        public string MissionApplicationApprove(int id)
        {
            return _dalMission.MissionApplicationApprove(id);
        }
        public string MissionApplicationDelete(int id)
        {
            return _dalMission.MissionApplicationDelete(id);
        }
    }
}
