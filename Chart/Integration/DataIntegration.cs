﻿using ChartDemo.Models;
using ChartDemo.Models.Entity;
using System;
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

        public ResultModel<TableNames> GetTableNames()
        {
            string path = "/api/FieldDefinition/GetTableNames";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ResultModel<TableNames>>().Result;
            }
            return null;
        }
        public ColumnAttributesModel GetTableAttributes(long TableId)
        {
            string path = $"/api/FieldDefinition/GetColumnAttributes?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ColumnAttributesModel>().Result;
            }
            return null;
        }
        public ResultModel<object> GetTableData(long TableId)
        {
            string path = $"/api/DataObject/getViewdata?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ResultModel<object>>().Result;
            }
            return null;
        }

        public DropdownModel GetDropdownValues(long TableId)
        {
            string path = $"/api/DataObject/getViewdata?tableID={TableId}";
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
        public CustomerModel GetCustomerModelTableData(long TableId)
        {
            string path = $"/api/DataObject/getViewdata?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<CustomerModel>().Result;
            }
            return null;
        }
        public ContactModel GetContactTableData(long TableId)
        {
            string path = $"/api/DataObject/getViewdata?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ContactModel>().Result;
            }
            return null;
        }
        public CustomerModel GetCRMContactTableData(long TableId)
        {
            string path = $"/api/DataObject/getViewdata?tableID={TableId}";
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<CustomerModel>().Result;
            }
            return null;
        }
    }
}