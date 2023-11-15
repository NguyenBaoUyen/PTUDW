﻿using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductsDAO
    {
        //cOPY CLas 
        private MyDBContext db = new MyDBContext();
        //SELECT * FROM
        public List<Products> getList()
        {
            return db.Products.ToList();
        }
        //index select * from cho Index chi voi status 1,2
        public List<Products> getList(string status = "ALL")//status 0,1,2
        {
            List<Products> list = null;
            switch (status)
            {
                case "Index"://1,2
                    {
                        list = db.Products.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash"://0
                    {
                        list = db.Products.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products.ToList();
                        break;
                    }
            }
            return list;
        }
        //detail
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
        //tao moi
        public int Insert(Products row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //cap nhat
        public int Update(Products row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Products row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
