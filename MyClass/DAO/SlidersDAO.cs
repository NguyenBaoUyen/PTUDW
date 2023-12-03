using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SlidersDAO
    {

        private MyDBContext db = new MyDBContext();
        //SELECT * FROM
        public List<Sliders> getList()
        {
            return db.Sliders.ToList();
        }
        //index select * from cho Index chi voi status 1,2
        public List<Sliders> getList(string status = "ALL")//status 0,1,2
        {
            List<Sliders> list = null;
            switch (status)
            {
                case "Index"://1,2
                    {
                        list = db.Sliders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash"://0
                    {
                        list = db.Sliders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.  Sliders.ToList();
                        break;
                    }
            }
            return list;
        }
        //detail
        public Sliders getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Sliders.Find(id);
            }
        }
        //tao moi
        public int Insert(Sliders row)
        {
            db.Sliders.Add(row);
            return db.SaveChanges();
        }
        //cap nhat
        public int Update(Sliders row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xoa mau tin
        public int Delete(Sliders row)
        {
            db.Sliders.Remove(row);
            return db.SaveChanges();
        }
    }
}

