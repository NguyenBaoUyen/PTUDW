using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
using MyClass.DAO;
using UDW.Library;
using System.IO;

namespace _63CNTT5N1.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductsDAO productsDAO = new ProductsDAO();
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        SuppliersDAO suppliersDAO = new SuppliersDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productsDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại sản phẩm");
               return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại sản phẩm");
                return RedirectToAction("Index");
            }
            return View(products);
        }


        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.LisCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.LisSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CatID,Name,Supplier,Slug,Detail,Img,Price,PriceSale,Qty,MetaDesc,MetaKey,CreateBy,CreateAt,UpdateBy,UpdateAt,Status")] Products products)
        {
            if (ModelState.IsValid)
            {
                products.CreateAt = DateTime.Now;
                //xu ly tu dong: UpdateAt
                products.UpdateAt = DateTime.Now;
                products.CreateBy = Convert.ToInt32(Session["UserID"]);
                products.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //xu ly tu dong: Slug
                products.Slug = XString.Str_Slug(products.Name);

                ///xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = products.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        products.Img = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/product";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                 ////luu vao db
                 productsDAO.Insert(products);
                TempData["message"] = new XMessage("success", "Tạo mới sản phẩm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.LisCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.LisSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");
            return View(products);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại sản phẩm");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại sản phẩm");
                return RedirectToAction("Index");
            }
            return View(products);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Products products)
        {
            if (ModelState.IsValid)
            {
                //xử lý tự động cho các trường: Slug, CreateAt/By, UpdateAt/By, Order
                //Xử lý tự động: UpdateAt
                products.UpdateAt = DateTime.Now;
                //Xử lý tự động: Order
                
                //Xử lý tự động: Slug
                products.Slug = XString.Str_Slug(products.Name);

                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                string PathDir = "~/Public/img/product";
                if (img.ContentLength != 0)
                {
                    //Xu ly cho muc xoa hinh anh
                    if (products.Img != null)
                    {
                        string DelPath = Path.Combine(Server.MapPath(PathDir), products.Img);
                        System.IO.File.Delete(DelPath);
                    }

                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = products.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        products.Img = imgName;
                        //upload hinh
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }

                }//ket thuc phan upload hinh anh


                //Cập nhật mẫu tin vào DB
                productsDAO.Update(products);
                //Thông báo tạo mẫu tin thành công 
                TempData["message"] = new XMessage("success", "Cập nhật sản phẩm thành công");
                return RedirectToAction("Index");
            }
            return View(products);
            
        }



        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại sản phẩm");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tồn tại sản phẩm");
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = productsDAO.getRow(id);
           productsDAO.Delete(products);
            //Thông báo tạo mẫu tin thành công 
            TempData["message"] = new XMessage("success", "Cập nhật sản phẩm thành công");
            return RedirectToAction("Index");
           
        }

        //phat sinh them mot so action moi: Status, Trash, DelTrash, Undo
        //STATUS
        //// GET: Admin/Category/Status/5
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }

            //truy van dòng có Id = Id yêu cầu
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                //chuyển đổi trang thái của status tu 1<->2
                products.Status = (products.Status == 1) ? 2 : 1;
                //cap nhat gia tri UpdateAt 
                products.UpdateAt = DateTime.Now;
                //cap nhat lai database
                productsDAO.Update(products);
                //thong bao cap nhat trang thai thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
                return RedirectToAction("Index");
            }

        }

        //DELTRASH
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
                return RedirectToAction("Index");
            }

            //truy van dòng có Id = Id yêu cầu
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
                return RedirectToAction("Index");
            }
            else
            {
                //chuyển đổi trang thái của status tu 1,2 -> 0: không hiển thị ở index
                products.Status = 0;
                //cap nhat gia tri UpdateAt 
                products.UpdateAt = DateTime.Now;
                //cap nhat lai database
                productsDAO.Update(products);
                //thong bao cap nhat trang thai thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Xóa sản phẩm thành công");
                return RedirectToAction("Index");
            }
        }

        //TRASH
        // GET: Admin/Trash
        public ActionResult Trash()
        {
            return View(suppliersDAO.getList("Trash"));
        }

        ////RECOVER
        //// GET: Admin/Category/Recover/5
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Phục hồi sản phẩm thất bại");
                return RedirectToAction("Index");
            }

            //truy van dòng có Id = Id yêu cầu
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //thong bao that bai 
                TempData["message"] = new XMessage("danger", "Phục hồi sản phẩm thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                //chuyển đổi trang thái của status tu 0 -> 2: không xuất bản
                products.Status = 2;
                //cap nhat gia tri UpdateAt 
                products.UpdateAt = DateTime.Now;
                //cap nhat lai database
                productsDAO.Update(products);
                //thong bao phuc hoi du lieu thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Phục hồi sản phẩm thành công");
                return RedirectToAction("Index");
            }

        }
    }
}
