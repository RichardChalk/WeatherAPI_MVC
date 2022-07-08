using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace Weather.Controllers
{
    public class WeatherController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Make request to api
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://meteostat.p.rapidapi.com/stations/monthly?station=10637&start=2020-01-01&end=2020-12-31"),
                Headers =
                {
                    { "X-RapidAPI-Key", "7306dc1932msh2d45ff9ccce401ap193e40jsn0c2d7d3d328b" },
                    { "X-RapidAPI-Host", "meteostat.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }



            return View();
        }
    }
}
