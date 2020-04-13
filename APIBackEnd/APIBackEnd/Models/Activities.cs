﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackEnd.Models
{
    public class Activities
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Rate Rate { get; set; }
        public double Rating { get; set; }
        public Location Location { get; set; }
        public string ExternalLink { get; set; }

        //naviggation properties
        public List<Reviews> Reviews = new List<Reviews>();
        public List<TagActivity> TagActivity = new List<TagActivity>();


    }
    public enum Location
    {
        Indoor = 0, 
        Outdoor = 1,
    }
    public enum Rate
    {
        UpVote = 0,
        DownVote = 1,

    }
}