using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XmlGenerator.Models
{
    public class Template
    {

      //  [XmlIgnore()]
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}