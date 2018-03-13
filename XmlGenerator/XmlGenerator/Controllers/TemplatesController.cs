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
    [Authorize]
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
        public ActionResult Create(TemplateViewModel model)
        {
            

            var dtoObject = new Template();
            dtoObject.TemplateName = model.TemplateName;
            dtoObject.UserName = model.UserName;
            dtoObject.Email = model.Email;
            dtoObject.City = model.City;

           
            _context.Template.Add(dtoObject);
            _context.SaveChanges();


            var xmlDoc = ToXML(model);

            DownloadXML(xmlDoc);

            return null;

        }

        private void DownloadXML(XmlDocument xmlDoc)
        {
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
        }

        private static XmlDocument ToXML(TemplateViewModel dtoObject)
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(dtoObject.GetType());
           
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, dtoObject);
                    xml = sww.ToString(); // Your XML
                }
            }
            Console.WriteLine(xml);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            return xmlDoc;
    }
    }
}