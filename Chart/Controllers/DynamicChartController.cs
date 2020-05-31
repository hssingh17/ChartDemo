using ChartDemo.Integration;
using ChartDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChartDemo.Controllers
{
    public class DynamicChartController : Controller
    {
        private readonly DataIntegration Integration = new DataIntegration();
        public IActionResult Index()
        {
            var model = new ColumnAttributesModel();
            var tables = Integration.GetTableNames();
            if (tables.Status && tables.Result.Count > 0)
            {
                long TableId = tables.Result[0].TableID;
                model = Integration.GetTableAttributes(TableId);
                model.TableNames = tables.Result;
            }

            return View(model);
        }
        [HttpGet]
        public JsonResult GetTableAttributes(long TableId)
        {
            var r = Integration.GetTableAttributes(TableId);
            return Json(r);
        }
        public JsonResult GetFilteredData(long TableId)
        {
            try
            {
                var resp = Integration.GetTableData(TableId);
                if (resp == null || !resp.Status || resp.Result?.Count < 1)
                {
                    var r = new { status = false, data = "Data is not available for this table" };
                    return Json(r);
                }
                resp.Result.RemoveAt(0);
                return Json(resp);
            }
            catch (Exception ex)
            {

                return Json(new { status = false, Data = ex.Message });
            }
        }

    }
}