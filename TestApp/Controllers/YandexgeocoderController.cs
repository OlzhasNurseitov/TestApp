using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YandexgeocoderController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get(string key, string address)
        {
            HttpClient httpClient = new HttpClient();
            //c357f10a-53de-4db1-9c89-e3e0e11b6a2b Yandex GeoCoder API
            string request = $"https://geocode-maps.yandex.ru/1.x/?apikey={key}&geocode={address}";

            HttpResponseMessage response = (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);
        }
    }
}
