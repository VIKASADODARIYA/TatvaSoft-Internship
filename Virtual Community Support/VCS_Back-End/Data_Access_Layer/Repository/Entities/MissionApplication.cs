﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Entities
{
    public class MissionApplication : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public string? MissionTitle { get; set; }
        [NotMapped]
        public string? UserName { get; set; }
        [NotMapped]
        public string? ThemeName { get; set; }
        [NotMapped]
        public string? CityName { get; set; }
    }
}
