﻿using System;
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
    public class CategoryController : Controller
    {
        CategoriesDAO categoriesDAO = new CategoriesDAO();

        //////////////////////////////////////////////////////////////////////////////////////
        //INDEX
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoriesDAO.getList("Index"));
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
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //CREATE
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"),"Id","Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //xu ly dong:creatAt
                categories.CreateAt = DateTime.Now;
                //xu ly tu dong update
                categories.UpdateAt = DateTime.Now;
                //xu ly tu dong ParenId
                if (categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                /// xu ly order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order +=1;
                }
                //chen dong
                //su li slug
                categories.Slug = XString.Str_Slug(categories.Name);
                //chen them dong cho DB
                categoriesDAO.Insert(categories);
                //them thong bao cap nhap trang thai thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Tạo mới loại sản phẩm thành công");
                //tro ve trang index
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
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
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //xu li thong tin tu dong
                
                categories.Slug = XString.Str_Slug(categories.Name);
                //xu ly tu dong update
                categories.UpdateAt = DateTime.Now;
                //xu ly tu dong ParenId
                if (categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                /// xu ly order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                //xuli tu dong updateAt
                categories.UpdateAt= DateTime.Now;
                categoriesDAO.Update(categories);
                //thong tbao thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Cập nhập mẫu tin thành công");
                //tro ve trang index
                return RedirectToAction("Index");
                
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
            
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
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Xóa mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = categoriesDAO.getRow(id);
            categoriesDAO.Delete(categories);

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
                TempData["message"] = new XMessage("danger","Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //truy van dòng có Id = Id yêu cầu
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //truy van Id
            //chuyển đổi trang thái của status tu 1<->2
            categories.Status = (categories.Status == 1) ? 2 : 1;
            //cap nhat gia tri UpdateAt 
            categories.UpdateAt = DateTime.Now;
            //cap nhat lai database
            categoriesDAO.Update(categories);
            //thong bao cap nhat trang thai thanh cong
            TempData["message"] =TempData["message"]=new XMessage("success", "Cập nhật trạng thái thành công");

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
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            else {
                //chuyển đổi trang thái của status tu 1<->2
                categories.Status = 0;
            //cap nhat gia tri UpdateAt 
            categories.UpdateAt = DateTime.Now;
            //cap nhat lai database
            categoriesDAO.Update(categories);
            //thong bao cap nhat trang thai thanh cong
            TempData["message"] = TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");

            return RedirectToAction("Index");}
            //truy van Id
           
        }
        //////////////////////////////////////////////////////////////////////////////////////
        //TRash
        // GET: Admin/Category
        public ActionResult Trash()
        {
            return View(categoriesDAO.getList("Trash"));
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
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            //truy van Id
            //chuyển đổi trang thái của status tu 0->2:khong xuat ban
            categories.Status = 2;
            //cap nhat gia tri UpdateAt 
            categories.UpdateAt = DateTime.Now;
            //cap nhat lai database
            categoriesDAO.Update(categories);
            //thong bao phuc hoi du lieu thanh cong
            TempData["message"] = TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");

            return RedirectToAction("Index");
        }
    }
}
