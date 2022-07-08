using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Weather.ReadModels;

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
                // data as Json format
                var data = await response.Content.ReadAsStringAsync();

                
                JObject json = JObject.Parse(data);
                
                // access 'data' property
                JToken dataToken = json["data"];

                // Create o list of monthly data
                IEnumerable<JToken> year = dataToken.Children();

                // Create and fill monthly into list for chart
                List<Data> dataForChart = new List<Data>();
                foreach (var month in year)
                {
                    dataForChart.Add(new Data((double)month["tavg"]));
                }

                // Time to create a read model
                var rm = new WeatherDataRM(
                    chart: new Chart(
                        caption: "Weathe rin York",
                        subCaption: "Based on 2022 data",
                        numberPrefix: "",
                        theme:"fusion",
                        radarFillColour: "#fff"
                    ),
                    categories: new List<Category> 
                    { 
                        new Category(
                            new List<Label>
                            {
                                new Label("Jan"),
                                new Label("Feb"),
                                new Label("Mar"),
                                new Label("Apr"),
                                new Label("May"),
                                new Label("Jun"),
                                new Label("Jul"),
                                new Label("Aug"),
                                new Label("Sep"),
                                new Label("Oct"),
                                new Label("Nov"),
                                new Label("Dec")
                            }    
                            
                        )
                    },
                    dataSet: new List<DataSet> 
                    { 
                        new DataSet
                        (
                            seriesName: "Average temperature",
                            dataForChart
                        )
                    }
                );
            }

            

            return View();
        }
    }
}
