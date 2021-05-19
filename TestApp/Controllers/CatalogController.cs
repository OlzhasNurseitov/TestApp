using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TestApp.Service;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        public ActionResult CatalogList()
        {
            string path = @"C:\Users\Coding\Desktop\reserv\v3 Clearly\TestApp\TestApp\wwwroot\JsonOked.json";
            var webClient = new WebClient();

            webClient.Headers.Add(HttpRequestHeader.AcceptEncoding, "utf-8");

            var json = webClient.DownloadString(path);
            var items = JsonConvert.DeserializeObject<CatalogService>(json);

            return Ok(items);
        }
    }
}
