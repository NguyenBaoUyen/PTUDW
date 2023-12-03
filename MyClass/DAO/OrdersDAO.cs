using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrdersDAO
    {

        //coppy noi dung cua class categories, thay the category = suppliers
        private MyDBContext db = new MyDBContext();

        //SELECT * FROM ...
        public List<Orders> getList()
        {
            return db.Orders.ToList();
        }

        //Index chi voi staus 1,2        
        public List<Orders> getList(string status = "ALL")
        {
            List<Orders> list = null;
            switch (status)
            {
                case "Index":
                    {
                        if (db != null)
                        {
                            list = db.Orders.Where(m => m.Status != 0).ToList();
                        }
                        break;
                    }
                case "Trash":
                    {
                        if (db != null)
                        {
                            list = db.Orders.Where(m => m.Status == 0).ToList();
                        }
                        break;
                    }
                default:
                    {
                        if (db != null)
                        {
                            list = db.Orders.ToList();
                        }
                        break;
                    }
            }
            return list;
        }

        //details
        public Orders getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }

        //tao moi mau tin
        public int Insert(Orders row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }

        //cap nhat mau tin
        public int Update(Orders row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //Xoa mau tin
        public int Delete(Orders row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
