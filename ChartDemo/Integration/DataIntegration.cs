using ChartDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ChartDemo.Integration
{
    public class DataIntegration
    {
        readonly Uri APIBaseUrl = new Uri("http://apihd.azurewebsites.net/");

        readonly HttpClient client;

        public DataIntegration()
        {
            client = new HttpClient { BaseAddress = APIBaseUrl };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<TableNames> GetTableNames()
        {
            string path = "/api/FieldDefinition/GetTableNames";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<TableNames>>().Result;
            }
            return null;
        }

        public DropdownModel GetDropdownValues(long TableId)
        {
            string path = $"/api/DataObject/getLatestDataJSR?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;
            DropdownModel countryModel = new DropdownModel();
            if (response.IsSuccessStatusCode)
            {
                countryModel = response.Content.ReadAsAsync<DropdownModel>().Result;
                countryModel.Result = countryModel.Result
                                      .Where(d => !string.IsNullOrEmpty(d.Name) && d.Code > 0)
                                      .GroupBy(g => new { g.Code, Name = g.Name.ToUpperInvariant() })
                                      .Select(s => new DropdownValues { Code = s.Key.Code, Name = s.Key.Name })
                                      .ToList();
            }
            return countryModel;
        }
        public ContactModel GetTableData(long TableId)
        {
            string path = $"/api/DataObject/getLatestDataJSR?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ContactModel>().Result;
            }
            return null;
        }
    }
}
