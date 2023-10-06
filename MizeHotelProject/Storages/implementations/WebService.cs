using MizeHotelProject.Storages.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MizeHotelProject.Storages.implementations
{
    public class WebService<T> : IReadableStorage<T> where T : class
    {
        private readonly string _url;

        public WebService(string url)
        {
            _url = url;
        }

        public bool CanWrite => false;

        public async Task<T> GetValue()
        {
            Console.WriteLine("try retirve data from web service");
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(_url);

                if (response.IsSuccessStatusCode)
                {
                    string stringContent = await response.Content.ReadAsStringAsync();
                    var objectContent = JsonSerializer.Deserialize<T>(stringContent);
                    Console.WriteLine(stringContent);
                    Console.WriteLine("retrive data from web service");
                    return objectContent;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }
    }
}
