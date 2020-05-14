using ChartDemo.Integration;
using ChartDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ChartDemo.Controllers
{
    public class ChartController : Controller
    {
        private readonly DataIntegration Integration = new DataIntegration();
        public IHostingEnvironment Env { get; }

        PageDataModel dataModel = new PageDataModel();
        public ChartController(IHostingEnvironment env)
        {
            Env = env;
        }
        #region Static Graph
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
        #endregion
        public IActionResult Dynamic()
        {
            dataModel = new PageDataModel();
            var customerAttri = Integration.GetTableAttributes(17054);
            customerAttri.Result = customerAttri.Result.Where(h => !string.IsNullOrEmpty(h.HeaderName)).ToList();
            return View(customerAttri);
        }

        [HttpPost]
        public JsonResult GetFilteredData(long TableId, string HeaderName)
        {
            try
            {
                if (TableId == 17054) // Customer Table
                {
                    CustomerModel data = Integration.GetCustomerModelTableData(TableId);
                    data.Result = data.Result.Where(d => d.DataObjectID > 0).ToList();
                    if (data.Status)
                    {
                        List<DropdownValues> filterdata = new List<DropdownValues>();

                        Type t = typeof(Customer);

                        var ptype = t.GetProperty(HeaderName)?.PropertyType?.Name;

                        ParameterExpression p = Expression.Parameter(typeof(Customer), "p");

                        if (ptype == "Int64")
                        {
                            var longExpression = Expression.Lambda<Func<Customer, long>>(Expression.Property(p, HeaderName), p);
                            filterdata = data.Result.AsQueryable().GroupBy(longExpression)
                            .Select(s => new DropdownValues { Code = s.Count(), Name = s.Key.ToString() })
                            .ToList();
                        }
                        else if (ptype == "String")
                        {
                            var strExpression = Expression.Lambda<Func<Customer, string>>(Expression.Property(p, HeaderName), p);
                            filterdata = data.Result.AsQueryable().GroupBy(strExpression)
                            .Select(s => new DropdownValues { Code = s.Count(), Name = s.Key.ToString() })
                            .ToList();
                        }
                        else if (ptype == "long")
                        {
                            var intExpression = Expression.Lambda<Func<Customer, int>>(Expression.Property(p, HeaderName), p);
                            filterdata = data.Result.AsQueryable().GroupBy(intExpression)
                            .Select(s => new DropdownValues { Code = s.Count(), Name = s.Key.ToString() })
                            .ToList();
                        }
                        else
                        {
                            return Json(new { status = false, Data = "This property is not mapped!" });
                        }

                        var dd = new List<DropdownValues>();
                        for (int d = 0; d < filterdata.Count; d++)
                        {
                            dd.Add(new DropdownValues { Code = filterdata[d].Code, Name = filterdata[d].Name });
                        }
                        if (HeaderName == "Country")
                        {
                            var q = Integration.GetDropdownValues(2020);
                            if (q.Status)
                            {
                                for (int w = 0; w < dd.Count; w++)
                                {
                                    dd[w].Name = q.Result.Find(e => e.Code.ToString() == dd[w].Name)?.Name;
                                }
                            }
                        }
                        return Json(new { status = true, Data = dd });
                    }
                    return Json(new { status = false, Data = data.Message });
                }
                else if (TableId == 17057) // Contact Table
                {
                    CustomerModel data = Integration.GetContactTableData(TableId);
                    if (data.Status)
                    {
                        var summryData = data.Result.GroupBy(g => new { HeaderName })
                                            .Select(s => new DropdownValues { Code = s.Count(), Name = s.Key.HeaderName })
                                            .ToList();
                        var dd = new List<DropdownValues>();
                        for (int d = 0; d < summryData.Count; d++)
                        {
                            dd.Add(new DropdownValues { Code = summryData[d].Code, Name = summryData[d].Name });
                        }
                        return Json(new { status = true, Data = dd });
                    }
                    return Json(new { status = true, Data = data.Message });
                }
                else if (TableId == 18099) // CRM Contact
                {
                    CustomerModel data = Integration.GetCRMContactTableData(TableId);
                    if (data.Status)
                    {
                        var summryData = data.Result.GroupBy(g => new { HeaderName })
                                            .Select(s => new DropdownValues { Code = s.Count(), Name = s.Key.HeaderName })
                                            .ToList();
                        var dd = new List<DropdownValues>();
                        for (int d = 0; d < summryData.Count; d++)
                        {
                            dd.Add(new DropdownValues { Code = summryData[d].Code, Name = summryData[d].Name });
                        }
                        return Json(new { status = true, Data = dd });
                    }
                    return Json(new { status = true, Data = data.Message });
                }
                return Json(false);
            }
            catch (Exception ex)
            {

                return Json(new { status = false, Data = ex.Message });
            }
        }
    }
}