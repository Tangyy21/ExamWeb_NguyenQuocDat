using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamWeb_NguyenQuocDat.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamWeb_NguyenQuocDat.Controllers
{
    public class PhimController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hosting;
        public PhimController(ApplicationDbContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        //Hiển thị danh sách sản phẩm
        public IActionResult Index()
        {
            var phimList = _db.Phims.ToList();
            return View(phimList);
        }
        public IActionResult Add()
        {
            //truyền danh sách thể loại cho View để sinh ra điều khiển DropDownList
            ViewBag.PhimList = _db.Phims.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.TuaDe
            });
            return View();
        }
        //Xử lý thêm sản phẩm
        [HttpPost]
        public IActionResult Add(Phim phim)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
               
                //thêm product vào table Product
                _db.Phims.Add(phim);
                _db.SaveChanges();
                TempData["success"] = "Thêm phim thành công!";
                return RedirectToAction("Index");
            }
            return View();
        }
        //Hiển thị form cập nhật sản phẩm
        public IActionResult Update(int ID)
        {
            var phim = _db.Phims.Find(ID);
            if (phim == null)
            {
                return NotFound();
            }
            //truyền danh sách thể loại cho View để sinh ra điều khiển DropDownList
           
            return View(phim);
        }
        //Xử lý cập nhật sản phẩm
        [HttpPost]
        public IActionResult Update(Phim phim)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                var existingPhim = _db.Phims.Find(phim.ID);
                
                //cập nhật product vào table Product
                existingPhim.TuaDe = phim.TuaDe;
                existingPhim.DienVien = phim.DienVien;
                if (existingPhim.TrongNuoc == true)
                {
                    TempData["TrongNuoc"] = "Trong nước";
                }
                TempData["TrongNuoc"] = "Nước ngoài";
                existingPhim.ThoiLuong = phim.ThoiLuong;
                existingPhim.GiaVe = phim.GiaVe;
                _db.SaveChanges();
                TempData["success"] = "Cập nhật phim thành công!";
                return RedirectToAction("Index");
            }
            return View();
        }
           
        //Hiển thị form xác nhận xóa sản phẩm
        public IActionResult Delete(int ID)
        {
            var phim = _db.Phims.Find(ID);
            if (phim == null)
            {
                return NotFound();
            }
            return View(phim);
        }
        //Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int ID)
        {
            var phim = _db.Phims.Find(ID);
            if (phim == null)
            {
                return NotFound();
            }
            // xoa san pham khoi CSDL
            _db.Phims.Remove(phim);
            _db.SaveChanges();
            TempData["success"] = "Xoá phim thành công!";
            //chuyen den action index
            return RedirectToAction("Index");
        }
    }
}
