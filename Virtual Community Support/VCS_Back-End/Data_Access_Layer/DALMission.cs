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
    public class DALMission
    {
        private readonly AppDbContext _cIDbContext;

        public DALMission(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<Missions> MissionList()
        {
            return _cIDbContext.Missions.Where(x => !x.IsDeleted).ToList();
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

        public List<Missions> ClientSideMissionList(int userId)
        {
            List<Missions> clientSideMissionList = new List<Missions>();
            try
            {
                clientSideMissionList = _cIDbContext.Missions
                    .Where(m => !m.IsDeleted)
                    .OrderBy(m => m.CreatedDate)
                    .Select(m => new Missions
                    {
                        Id = m.Id,
                        CountryId = m.CountryId,
                        CountryName = m.CountryName,
                        CityId = m.CityId,
                        CityName = m.CityName,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationName = m.MissionOrganisationName,
                        MissionOrganisationDetail = m.MissionOrganisationDetail,
                        TotalSheets = m.TotalSheets,
                        RegistrationDeadLine = m.RegistrationDeadLine,
                        MissionThemeId = m.MissionThemeId,
                        MissionThemeName = m.MissionThemeName,
                        MissionImages = !string.IsNullOrEmpty(m.MissionImages) ? 
                    $"/UploadMissionImage/Mission/{m.MissionImages}" : 
                    "/assets/NoImg.png",                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                        MissionDocuments = m.MissionDocuments,
                        MissionSkillId = m.MissionSkillId,
                        MissionSkillName = string.Join(",", m.MissionSkillName),
                        MissionAvailability = m.MissionAvailability,
                        MissionVideoUrl = m.MissionVideoUrl,
                        MissionType = m.MissionType,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId) ? "Applied" : "Apply",
                        MissionApproveStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status == true) ? "Approved" : "Applied",
                        MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1)?"MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1)? "Closed" : "Running",
                        MissionFavouriteStatus = _cIDbContext.MissionFavourites.Any(mf => mf.MissionId == m.Id && mf.UserId == userId) ? "1" : "0",
                        Rating = _cIDbContext.MissionRating.FirstOrDefault(mr => mr.MissionId == mr.Id && mr.UserId == userId).Rating ?? 0

                    }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientSideMissionList;
        }

        public string ApplyMission(MissionApplication missionApplication)
        {
            string result = "";
            try
            {
                using (var transaction = _cIDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var mission = _cIDbContext.Missions.FirstOrDefault(m => m.Id == missionApplication.MissionId && m.IsDeleted == false);
                        if (mission != null)
                        {
                            if (mission.TotalSheets > 0)
                            {
                                var newApplication = new MissionApplication
                                {
                                    MissionId = missionApplication.MissionId,
                                    UserId = missionApplication.UserId,
                                    AppliedDate = DateTime.UtcNow,
                                    Status = false,
                                    CreatedDate = DateTime.UtcNow,
                                    IsDeleted = false,
                                };

                                _cIDbContext.MissionApplication.Add(newApplication);
                                _cIDbContext.SaveChanges();

                                mission.TotalSheets = mission.TotalSheets - 1;
                                _cIDbContext.SaveChanges();

                                result = "Mission Apply Successfully.";
                            }
                            else
                            {
                                result = "Mission Housefull";
                            }
                        }
                        else
                        {
                            result = "Mission Not Found.";
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /*public List<MissionApplication> GetMissionApplicationList()
        {
            return _cIDbContext.MissionApplication.Where(ma => !ma.IsDeleted).ToList();
        }*/
        public List<MissionApplication> GetMissionApplicationList()
        {
            return _cIDbContext.MissionApplication
                .Where(ma => !ma.IsDeleted)
                .Join(
                    _cIDbContext.Missions,
                    ma => ma.MissionId,
                    m => m.Id,
                    (ma, m) => new { ma, m }
                )
                .Join(
                    _cIDbContext.User,
                    combined => combined.ma.UserId,
                    u => u.Id,
                    (combined, u) => new { combined.ma, combined.m, UserName = u.FirstName + " " + u.LastName }
                )
                .Join(
                    _cIDbContext.City,
                    combined => combined.m.CountryId,
                    c => c.Id,
                    (combined, c) => new { combined.ma, combined.m, combined.UserName, CityName = c.CityName }
                )
                .Join(
                    _cIDbContext.MissionTheme,
                    combined => combined.m.Id,
                    mt => mt.Id,
                    (combined, mt) => new MissionApplication
                    {
                        Id = combined.ma.Id,
                        UserId = combined.ma.UserId,
                        MissionId = combined.ma.MissionId,
                        AppliedDate = combined.ma.AppliedDate,
                        IsDeleted = combined.ma.IsDeleted,
                        MissionTitle = combined.m.MissionTitle,
                        UserName = combined.UserName,
                        CityName = combined.CityName,
                        ThemeName = mt.ThemeName,
                        Status = combined.ma.Status
                    }
                ).ToList();
        }


        public string MissionApplicationApprove(int id)
        {
            var result = "";
            try
            {
                var missionApplication = _cIDbContext.MissionApplication.FirstOrDefault(ma => ma.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.Status = true;
                    _cIDbContext.SaveChanges(); 
                    result = "Mission is approved";
                }
                else
                {
                    result = "Mission Application is not found.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public Missions MissionDetailById(int missionId)
        {
            return _cIDbContext.Missions.FirstOrDefault(m => m.Id == missionId);
        }

        public string DeleteMission(int missionId)
        {
            string result = "";
            try
            {
                var missionToDelete = _cIDbContext.Missions.FirstOrDefault(m => m.Id == missionId);

                if (missionToDelete != null)
                {
                    missionToDelete.IsDeleted = true;
                    _cIDbContext.SaveChanges();
                    /*_cIDbContext.Missions.Remove(missionToDelete);
                    _cIDbContext.SaveChanges();*/
                    result = "Mission deleted successfully.";
                }
                else
                {
                    result = "Mission not found.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public string UpdateMission(Missions mission)
        {
            try
            {
                var existingMission = _cIDbContext.Missions.FirstOrDefault(m => m.Id == mission.Id);
                if (existingMission == null)
                {
                    return "Mission not found.";
                }

                existingMission.MissionTitle = mission.MissionTitle;
                existingMission.MissionDescription = mission.MissionDescription;
                existingMission.MissionOrganisationName = mission.MissionOrganisationName;
                existingMission.MissionOrganisationDetail = mission.MissionOrganisationDetail;
                existingMission.CountryId = mission.CountryId;
                existingMission.CityId = mission.CityId;
                existingMission.StartDate = mission.StartDate;
                existingMission.EndDate = mission.EndDate;
                existingMission.MissionType = mission.MissionType;
                existingMission.TotalSheets = mission.TotalSheets;
                existingMission.RegistrationDeadLine = mission.RegistrationDeadLine;
                existingMission.MissionThemeId = mission.MissionThemeId;
                existingMission.MissionSkillId = mission.MissionSkillId;
                existingMission.MissionImages = mission.MissionImages;
                existingMission.MissionDocuments = mission.MissionDocuments;
                existingMission.MissionAvailability = mission.MissionAvailability;
                existingMission.MissionVideoUrl = mission.MissionVideoUrl;
                existingMission.IsDeleted = mission.IsDeleted;

                _cIDbContext.SaveChanges();

                return "Mission updated successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Missions> GetMissionThemeList()
        {
            return _cIDbContext.Missions.Select(c => new Missions{ MissionThemeId = c.MissionThemeId }).ToList();
        }

        public string MissionApplicationDelete(int id)
        {
            string result = "";
            try
            {
                var missionApplication = _cIDbContext.MissionApplication.FirstOrDefault(ma => ma.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.Status = false;
                    missionApplication.IsDeleted = true;
                    _cIDbContext.SaveChanges();
                    result = "Mission application deleted successfully.";
                }   
                else
                {
                    result = "Mission application not found.";
                }
            }
            catch (Exception ex)
            {
                result = $"Error occurred: {ex.Message}";
            }
            return result;
        }

    }
}
