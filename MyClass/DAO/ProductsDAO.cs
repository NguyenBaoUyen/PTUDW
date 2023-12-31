﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Model;

namespace MyClass.DAO
{
    public class ProductsDAO
    {
        //coppy noi dung cua class categories, thay the category = suppliers
        private MyDBContext db = new MyDBContext();

        //SELECT * FROM ...
        public List<Products> getList()
        {
            return db.Products.ToList();
        }

        //Index chi voi staus 1,2        
        public List<Products> getList(string status = "ALL")
        {
            List<Products> list = null;
            switch (status)
            {
                case "Index":
                    {
                        if (db != null)
                        {
                            list = db.Products.Where(m => m.Status != 0).ToList();
                        }
                        break;
                    }
                case "Trash":
                    {
                        if (db != null)
                        {
                            list = db.Products.Where(m => m.Status == 0).ToList();
                        }
                        break;
                    }
                default:
                    {
                        if (db != null)
                        {
                            list = db.Products.ToList();
                        }
                        break;
                    }
            }
            return list;
        }

        //details
        public Products getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }

        //tao moi mau tin
        public int Insert(Products row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }

        //cap nhat mau tin
        public int Update(Products row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //Xoa mau tin
        public int Delete(Products row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}