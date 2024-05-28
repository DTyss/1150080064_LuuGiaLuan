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
    public class NhanVienController : Controller
    {
        Models.LuanEntities _context = new Models.LuanEntities(); // Tạo đối tượng để truy cập dữ liệu

        // GET: HANGHOA
        public ActionResult Index()


        {

            var listOfData = _context.NhanViens.ToList(); // Lấy tất cả dữ liệu từ bảng HANGHOA
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var mapbList = _context.PhongBans.Select(x => new SelectListItem { Value = x.MaPB.ToString(), Text = x.MaPB }).ToList();
            ViewBag.MaPBList = new SelectList(mapbList, "Value", "Text");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.NhanVien model)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                try
                {




                    var mapb = _context.PhongBans.Find(model.MaPB);


                    if (mapb == null)
                    {
                        ModelState.AddModelError("MaCaSy", "Mã hoa không hợp lệ.");
                    }




                    if (ModelState.IsValid) // Kiểm tra xem sau khi kiểm tra mã hoa và mã khu vực, ModelState có còn hợp lệ không
                    {
                        // Thêm model vào cơ sở dữ liệu
                        _context.NhanViens.Add(model);
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

            var mapbList = _context.PhongBans.Select(x => new SelectListItem { Value = x.MaPB.ToString(), Text = x.MaPB }).ToList();
            ViewBag.MaPBList = new SelectList(mapbList, "Value", "Text");

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
            Models.NhanVien itemToDelete = _context.NhanViens.Find(id1);

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
            Models.NhanVien itemToDelete = _context.NhanViens.Find(id1);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            _context.NhanViens.Remove(itemToDelete); // Xóa phần tử
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index");
        }
    }
}
