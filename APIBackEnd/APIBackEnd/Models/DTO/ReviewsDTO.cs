﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackEnd.Models.DTO
{
    public class ReviewsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //navigation property
        public Activities Activities { get; set; }
        //public UserNames UserNames { get; set; }

    }
}
