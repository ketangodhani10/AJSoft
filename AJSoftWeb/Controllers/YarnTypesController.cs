using AJSoftBAL;
using AJSoftEntity;
using AJSoftWeb.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AJSoftWeb.Controllers
{
    public class YarnTypesController : BaseController
    {
        // GET: YarnTypes
        public ActionResult Index()
        {
            return View();
        }

        public string GetGridData(GridSettings grid)
        {
            try
            {
                return new YarnTypesBL().GetGridData(grid);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}