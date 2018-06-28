using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WL.Core.WebApp.Controllers
{
    public class ModelController : Controller
    {
        private static List<ModelEntity> Data = new List<ModelEntity>() {
            new ModelEntity(){ ModelID= 1, ModelName = "模块1", ModelRemark="模块1", IsEnable=true},
            new ModelEntity(){ ModelID= 2, ModelName = "模块2", ModelRemark="模块2", IsEnable=true},
            new ModelEntity(){ ModelID= 3, ModelName = "模块3", ModelRemark="模块3", IsEnable=true},
            new ModelEntity(){ ModelID= 4, ModelName = "模块4", ModelRemark="模块4", IsEnable=true},
            new ModelEntity(){ ModelID= 5, ModelName = "模块5", ModelRemark="模块5", IsEnable=true},
            new ModelEntity(){ ModelID= 6, ModelName = "模块6", ModelRemark="模块6", IsEnable=true},
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Insert(ModelEntity model)
        {
            var result = model.Success();
            return Json(result);
        }

        public IActionResult Update(ModelEntity model)
        {
            var result = model.Success();
            return Json(result);
        }

        public IActionResult Del(ModelEntity model)
        {
            return Json(1);
        }

        public IActionResult Enable(ModelEntity model)
        {
            return Json(1);
        }

        public IActionResult AllList()
        {
            var result = Data.PageForList(1, 40).Success();
            return Json(result);
        }

        public IActionResult PageList(ModelEntity model)
        {
            return Json(1);
        }

    }

    public class ModelEntity
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string ModelRemark { get; set; }
        public bool IsEnable { get; set; }
    }
}
