using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelBureauet.Models;
using Newtonsoft.Json;


namespace ModelBureauet
{
    public class DataAccessLayer
    {
        private HttpClient client = new HttpClient();

        public DataAccessLayer()
        {
            client.BaseAddress = new Uri("https://localhost:44392/");

        }


        public async Task<bool> CreateModelInDb(Model model)
        {
            var response = await client.PostAsJsonAsync("api/Models", model);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Model>> GetModelsInDb()
        {
            var result = await client.GetStringAsync("api/Models");
            var returnValue = JsonConvert.DeserializeObject<List<Model>>(result);
            return returnValue;
        }


        public async Task<bool> CreateTaskInDb(IncomingTask incomingTask)
        {
            var response = await client.PostAsJsonAsync("api/IncomingTasks", incomingTask);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<IncomingTask>> GetTasksInDb()
        {
            var result = await client.GetStringAsync("api/IncomingTasks");
            var returnValue = JsonConvert.DeserializeObject<List<IncomingTask>>(result);
            return returnValue;
        }

        public async Task<bool> CreateModelTaskInDb(ModeltaskDTO modeltask)
        {
            var response = await client.PostAsJsonAsync("api/Models/createModelIncomingTask", modeltask);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }
    }
}
