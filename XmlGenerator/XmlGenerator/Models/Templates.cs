﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlGenerator.Models
{
    public class Templates
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}