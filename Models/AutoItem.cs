﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Models
{
    [Table("items")]
    public class AutoItem : ItemCategory
    {
        [Display(Name="Production year")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProductAge { get; set; }

        public int? Mileage { get; set; }
        //public override string ItemType { get ; set; }
    }
}