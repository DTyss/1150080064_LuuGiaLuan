using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luan.Models;
using System.Net;
using System.Web.Mvc;
using System.Globalization;

namespace Luan.Controllers
{
    public class ChiTietBangLuongController : Controller
    {
        Models.LuanEntities _context = new Models.LuanEntities(); // Tạo đối tượng để truy cập dữ liệu

        // GET: HANGHOA
        public ActionResult Index()


        {

            var listOfData = _context.ChiTietBangLuongs.ToList(); // Lấy tất cả dữ liệu từ bảng HANGHOA
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var manvList = _context.NhanViens.Select(x => new SelectListItem { Value = x.MaNV.ToString(), Text = x.MaNV }).ToList();
            ViewBag.MaNVList = new SelectList(manvList, "Value", "Text");
            var matinhluongList = _context.TinhLuongs.Select(x => new SelectListItem { Value = x.MaTinhLuong.ToString(), Text = x.MaTinhLuong }).ToList();
            ViewBag.MaTinhLuongList = new SelectList(matinhluongList, "Value", "Text");
            var mathangList = _context.BangLuongs.Select(x => new SelectListItem { Value = x.Thang.ToString(), Text = x.Thang }).ToList();
            ViewBag.ThangList = new SelectList(mathangList, "Value", "Text");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ChiTietBangLuong model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {




                    var manv = _context.NhanViens.Find(model.MaNV);
                    var matinhluong = _context.TinhLuongs.Find(model.MaTinhLuong);

                    var mathang = _context.BangLuongs.Find(model.Thang);



                    if (manv == null)
                    {
                        ModelState.AddModelError("MaNV", "Mã hoa không hợp lệ.");
                    }
                    if (matinhluong == null)
                    {
                        ModelState.AddModelError("MaTinhLuong", "Mã hoa không hợp lệ.");
                    }
                    if (mathang == null)
                    {
                        ModelState.AddModelError("Thang", "Mã hoa không hợp lệ.");
                    }




                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.ChiTietBangLuongs.Add(model);
                        _context.SaveChanges();

                        ViewBag.Message = "Data inserted successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error occurred: " + ex.Message;
                }
            }

            var manvList = _context.NhanViens.Select(x => new SelectListItem { Value = x.MaNV.ToString(), Text = x.MaNV }).ToList();
            ViewBag.MaNVList = new SelectList(manvList, "Value", "Text");
            var matinhluongList = _context.TinhLuongs.Select(x => new SelectListItem { Value = x.MaTinhLuong.ToString(), Text = x.MaTinhLuong }).ToList();
            ViewBag.MaTinhLuongList = new SelectList(matinhluongList, "Value", "Text");
            var mathangList = _context.BangLuongs.Select(x => new SelectListItem { Value = x.Thang.ToString(), Text = x.Thang }).ToList();
            ViewBag.ThangList = new SelectList(mathangList, "Value", "Text");

            return View(model); // Trả về View với model để hiển thị lại dữ liệu đã nhập
        }
        [HttpGet]
        public ActionResult Delete(string id1)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.ChiTietBangLuong itemToDelete = _context.ChiTietBangLuongs.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1)
        {
            // Tìm phần tử cần xóa trong cơ sở dữ liệu
            Models.ChiTietBangLuong itemToDelete = _context.ChiTietBangLuongs.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.ChiTietBangLuongs.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }
    }
}
