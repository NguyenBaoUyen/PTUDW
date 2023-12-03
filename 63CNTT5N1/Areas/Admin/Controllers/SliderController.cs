using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using UDW.Library;

namespace _63CNTT5N1.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        SlidersDAO slidersDAO = new SlidersDAO();
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        SuppliersDAO suppliersDAO = new SuppliersDAO();
        ProductsDAO productsDAO = new ProductsDAO();

        //////////////////////////////////////////////////////////////////////////////////////
        //INDEX
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(slidersDAO.getList("Index"));
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //DETAILS
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return RedirectToAction("Index");
            }
            Sliders sliders = slidersDAO.getRow(id);
            if (sliders == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return RedirectToAction("Index");
            }
            return View(sliders);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //CREATE
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(slidersDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(slidersDAO.getList("Index"), "Order", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sliders sliders)
        {
            if (ModelState.IsValid)
            {
                //xu ly dong:creatAt
                sliders.CreateAt = DateTime.Now;
                //xu ly tu dong update
                sliders.UpdateAt = DateTime.Now;
                //xu ly tu dong ParenId
               
                /// xu ly order
                if (sliders.Order == null)
                {
                    sliders.Order = 1;
                }
                else
                {
                    sliders.Order += 1;
                }
                //chen dong
                //su li slug
               
                //chen them dong cho DB
                slidersDAO.Insert(sliders);
                //them thong bao cap nhap trang thai thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Tạo mới loại sản phẩm thành công");
                //tro ve trang index
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(slidersDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(slidersDAO.getList("Index"), "Order", "Name");
            return View(sliders);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //EDIT
        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Sliders sliders = slidersDAO.getRow(id);
            if (sliders == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(slidersDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(slidersDAO.getList("Index"), "Order", "Name");
            return View(sliders);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sliders sliders)
        {
            if (ModelState.IsValid)
            {
                //xu li thong tin tu dong

               
                //xu ly tu dong update
                sliders.UpdateAt = DateTime.Now;
                //xu ly tu dong ParenId
                
                /// xu ly order
                if (sliders.Order == null)
                {
                    sliders.Order = 1;
                }
                else
                {
                    sliders.Order += 1;
                }
                //xuli tu dong updateAt
                sliders.UpdateAt = DateTime.Now;
                slidersDAO.Update(sliders);
                //thong tbao thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Cập nhập mẫu tin thành công");
                //tro ve trang index
                return RedirectToAction("Index");

            }
            ViewBag.ListCat = new SelectList(slidersDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(slidersDAO.getList("Index"), "Order", "Name");
            return View(sliders);

        }

        //////////////////////////////////////////////////////////////////////////////////////
        //DELETE
        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Xóa mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            Sliders sliders = slidersDAO.getRow(id);
            if (sliders == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Xóa mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            return View(sliders);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sliders sliders = slidersDAO.getRow(id);
            slidersDAO.Delete(sliders);

            //thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Trash");
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /////status
        /////get adamin
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //truy van dòng có Id = Id yêu cầu
            Sliders sliders = slidersDAO.getRow(id);
            if (sliders == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //truy van Id
            //chuyển đổi trang thái của status tu 1<->2
            sliders.Status = (sliders.Status == 1) ? 2 : 1;
            //cap nhat gia tri UpdateAt 
            sliders.UpdateAt = DateTime.Now;
            //cap nhat lai database
            slidersDAO.Update(sliders);
            //thong bao cap nhat trang thai thanh cong
            TempData["message"] = TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");

            return RedirectToAction("Index");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            //truy van dòng có Id = Id yêu cầu
            Sliders sliders = slidersDAO.getRow(id);
            if (sliders == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            else
            {
                //chuyển đổi trang thái của status tu 1<->2
                sliders.Status = 0;
                //cap nhat gia tri UpdateAt 
                sliders.UpdateAt = DateTime.Now;
                //cap nhat lai database
                slidersDAO.Update(sliders);
                //thong bao cap nhat trang thai thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");

                return RedirectToAction("Index");
            }
            //truy van Id

        }
        //////////////////////////////////////////////////////////////////////////////////////
        //TRash
        // GET: Admin/Category
        public ActionResult Trash()
        {
            return View(slidersDAO.getList("Trash"));
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /////Recover
        /////get adamin
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            //truy van dòng có Id = Id yêu cầu
            Sliders sliders = slidersDAO.getRow(id);
            if (sliders == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            //truy van Id
            //chuyển đổi trang thái của status tu 0->2:khong xuat ban
            sliders.Status = 2;
            //cap nhat gia tri UpdateAt 
            sliders.UpdateAt = DateTime.Now;
            //cap nhat lai database
            slidersDAO.Update(sliders);
            //thong bao phuc hoi du lieu thanh cong
            TempData["message"] = TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");

            return RedirectToAction("Index");
        }
    }
}
