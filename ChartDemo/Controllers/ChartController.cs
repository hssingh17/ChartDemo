using ChartDemo.Integration;
using ChartDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChartDemo.Controllers
{
    public class ChartController : Controller
    {
        DataIntegration Integration = new DataIntegration();
        public IHostingEnvironment Env { get; }
        public List<ChartObject> Chart
        {
            get
            {
                string path = Env.WebRootPath + @"\Data.json";
                return ChartObject.GetList(path);
            }
        }
        public List<ChartObject> Charts { get; set; }
        PageDataModel dataModel = new PageDataModel();
        public ChartController(IHostingEnvironment env)
        {
            Env = env;
        }
        public IActionResult Area()
        {
            return View();
        }
        public IActionResult Pie()
        {
            return View();
        }
        public IActionResult Scatter()
        {
            return View();
        }
        public IActionResult Dynamic()
        {
            dataModel = new PageDataModel();

            var countryData = Integration.GetDropdownValues(2020);
            if (countryData.Status)
            {
                dataModel.Country = countryData.Result;
            }
            else
            {
                ViewBag.CountryError = countryData.Message;
            }
            var paymenyData = Integration.GetDropdownValues(17055);
            if (paymenyData.Status)
            {
                dataModel.PaymentMethods = paymenyData.Result;
            }
            else
            {
                ViewBag.CountryError = paymenyData.Message;
            }

            return View(dataModel);
        }

        [HttpGet]
        public JsonResult GetChartData()
        {
            string path = Env.WebRootPath + @"\Data.json";

            return Json(ChartObject.GetList(path));
        }

        [HttpPost]
        public JsonResult GetFilteredData(Serarch searchModel)
        {
            try
            {
                var customerData = Integration.GetTableData(17057);
                if (customerData.Status)
                {
                    var cust = customerData.Result
                        .Where(d => (d.PaymentMethod == searchModel.Payment || searchModel.Payment == 0) && (d.Country == searchModel.Country || searchModel.Country == 0) && !string.IsNullOrEmpty(d.Name))
                        .ToList();
                    var summryData = cust.GroupBy(g => new { g.Name })
                                        .Select(s => new DropdownValues { Code = s.Count(), Name = s.Key.Name })
                                        .ToList();
                    var dd = new List<DropdownValues>();
                    for (int d = 0; d < summryData.Count; d++)
                    {
                        dd.Add(new DropdownValues { Code = summryData[d].Code, Name = summryData[d].Name });
                    }
                    return Json(new { status = true, Data = dd });
                }
                return Json(new { status = true, Data = customerData.Message });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public JsonResult GetFilteredData_Old(Serarch searchModel)
        {
            try
            {
                //Charts = Integration.GetTableData(17057).Result;
                if (searchModel.Country == null)
                {
                    var dates = Charts.Where(d => (d.DateCreated >= searchModel.FromDate && d.DateCreated <= searchModel.ToDate) || searchModel.FromDate == null);

                    if (searchModel.Count.Contains("-"))
                    {
                        var cn = searchModel.Count.Split('-');
                        int st = Convert.ToInt32(cn[0]) - 1;
                        int lst = Convert.ToInt32(cn[1]) - 1;
                        if (lst == 0)
                        {
                            lst = Charts.Count;
                            st = lst - st;
                        }
                        dates = dates.Skip(st).Take(lst)
                            .GroupBy(g => new { g.ObjectText2 })
                            .Select(s => new ChartObject { ObjectValue1 = s.Sum(d => d.ObjectValue1), ObjectText2 = s.Key.ObjectText2 });
                    }
                    else
                    {
                        int st = Convert.ToInt32(searchModel.Count);
                        if (st == 0)
                        {
                            st = Charts.Count;
                        }
                        dates = dates.Take(st)
                            .GroupBy(g => new { g.ObjectText2 })
                            .Select(s => new ChartObject { ObjectValue1 = s.Sum(d => d.ObjectValue1), ObjectText2 = s.Key.ObjectText2 });
                    }
                    return Json(new { status = true, Data = dates });
                }
                else
                {
                    var dates = Charts.ToList();
                    var dd = new List<ChartObject>();
                    for (int d = 0; d < dates.Count(); d++)
                    {
                        dd.Add(new ChartObject { ObjectValue1 = dates[d].ObjectValue1, ObjectText2 = dates[d].DateCreated.ToString("dd/MM/yyyy") });
                    }
                    return Json(new { status = true, Data = dd });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }


    public class ChartObject
    {
        public static List<ChartObject> GetList(string path)
        {
            if (File.Exists(path))
            {
                string JsonData = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<ChartObject>>(JsonData);
            }
            return new List<ChartObject> { new ChartObject { ObjectText2 = "Test", ObjectValue1 = 1000 } };
        }
        public long DataObjectID { get; set; }
        public long TableID { get; set; }
        public bool Status { get; set; }
        public bool Inactive { get; set; }
        public DateTime DateCreated { get; set; }
        public string ObjectText2 { get; set; }
        public long ObjectValue1 { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
    }

    public class Serarch
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Count { get; set; }
        public long Country { get; set; }
        public long Payment { get; set; }
    }
}