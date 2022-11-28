using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Models;
using RepositoryLayer.Service;
using RepositoryLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TBGTestingSD.Controllers
{
    public class DetailController : Controller
    {
        private readonly TBPContext _context;
        public DetailController(TBPContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public JsonResult GetMaterial(int tipe)
        {
            bool Tipe = (tipe == 1)? true : false;
            List<Material> _list = new MaterialService(_context).GetAllByIdType(Tipe);
            var result = _list.Select(c => new { Id = c.Id, Material = c.Material1, Tipe = c.TipeMaterial }).ToList();
            return Json(result);
        }
        public JsonResult GetDetailByIdHeader(int id)
        {
            DetilSpr _obj = new Detail(_context).GetByIdRef(id);
            if(_obj != null)
            {
                var result = new vmDetailSPR();
                var tipeMaterial = new MaterialService(_context).GetById((_obj.Material) ?? default(int)).TipeMaterial;
                result.tipeMaterial = tipeMaterial;
                result.detilSpr = _obj;
                return Json(result);
            }
            return Json(null);
        }
        public JsonResult AddNew(DetilSpr obj)
        {
            try
            {
                string _obj = new Detail(_context).AddNew(obj);
                return Json(_obj);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = ex.Message});
            }
        }
        public JsonResult Update(DetilSpr obj)
        {
            try
            {
                string _obj = new Detail(_context).Update(obj);
                return Json(_obj);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
