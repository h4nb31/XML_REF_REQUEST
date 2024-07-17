using Microsoft.AspNetCore.Mvc;
using REF_XML_REQUEST.Models;
using System.Net.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace REF_XML_REQUEST.Controllers
{
    public class XmlController : Controller
    {

        private readonly ILogger<XmlController>? _logger;
        private readonly ApplicationContext? _db;

        public XmlController(ILogger<XmlController>? logger, ApplicationContext? db)
        {
            _logger = logger;
            _db = db;
        }



        public async Task<IActionResult> XmlResponse()
        {
            string xmlPath = "GetRefFunctions.xml";
            string xmlContent = ReadXmlFile(xmlPath);

            XDocument xmlDoc = XDocument.Parse(xmlContent);

            string xmlString = xmlDoc.ToString();
            string apiUrl = "https://HTTP:9016@195.133.68.161:61037/rk7api/v0/xmlinterface.xml";

            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => true;


            using (HttpClient client = new(handler))
            {
                StringContent content = new(xmlString, Encoding.UTF8, "application/xml");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                return Content(apiUrl, "application/xml");
                //if (response.IsSuccessStatusCode)
                //{
                //    string responseContent = await response.Content.ReadAsStringAsync();
                //    return Content(xmlString, "application/xml");
                //}
                //else
                //{
                //    return Content($"Error: {response.ReasonPhrase}");
                //}
            }
        }

        private string ReadXmlFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string xmlContent = reader.ReadToEnd();

                // Очистка недопустимых символов ?
                return new string(xmlContent.Where(c =>
                XmlConvert.IsXmlChar(c)).ToArray());
            }
        }

    }
}
