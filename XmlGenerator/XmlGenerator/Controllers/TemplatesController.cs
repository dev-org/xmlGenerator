using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using XmlGenerator.Models;

namespace XmlGenerator.Controllers
{
    public class TemplatesController : Controller
    {
        private ApplicationDbContext _context;

        public TemplatesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Templates
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Templates model)
        {
            _context.Templates.Add(model);
            _context.SaveChanges();

            var dtoObject = new Templates();
            dtoObject.TemplateName = model.TemplateName;
            dtoObject.UserName = model.UserName;
            dtoObject.Email = model.Email;
            dtoObject.City = model.City;

            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(dtoObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, dtoObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                
            }

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);

            xmlDoc.WriteTo(writer);
            writer.Flush();
            Response.Clear();
            byte[] byteArray = stream.ToArray();
            Response.AppendHeader("Content-Disposition", "filename=Template.xml");
            Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(byteArray);
            writer.Close();


            return null;


        }

    }
}