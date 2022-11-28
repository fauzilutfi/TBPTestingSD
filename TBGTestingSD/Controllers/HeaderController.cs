using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Models;
using RepositoryLayer.Service;
using RepositoryLayer.ViewModels;
using System;
using System.Collections.Generic;

namespace TBGTestingSD.Controllers
{
    public class HeaderController : Controller
    {
        private readonly TBPContext _context;
        public HeaderController(TBPContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll(filterHeaderSPR filter)
        {
            List<HeaderSpr> _list = new Header(_context).GetAll(filter);
            return Json(_list);
        }
        public JsonResult GetById(string id)
        {
            HeaderSpr _obj = new Header(_context).GetById(Convert.ToInt32(id));
            return Json(_obj);
        }
        public JsonResult Remove(string id)
        {
            string result = new Header(_context).Remove(Convert.ToInt32(id));
            return Json(result);
        }
        public JsonResult Save(HeaderSpr obj)
        {
            string result = new Header(_context).Save(obj);
            return Json(result);
        }
        public JsonResult GetPenyetujuan()
        {
            var result = new Header(_context).GetDataPenyetujuan();
            return Json(result);
        }
    }
}
