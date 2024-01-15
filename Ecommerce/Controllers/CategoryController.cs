using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;

namespace Ecommerce.Controllers
{

    public class CategoryController : Controller
    {
        EcommerceEntities2 db = new EcommerceEntities2();
        // GET: Category
        public ActionResult index()
        {
            
            return View();
    }
        public ActionResult GetCategory()
        {
            return PartialView();
        }
        //httpget

        public JsonResult LoadData()
        {

            var category = db.Category.Select(x => new { ID = x.ID, Name = x.Name});
            return Json(category, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult LoadData()
        //{
        //    List<Category> categories= db.Category.ToList();
        //    return Json(categories,JsonRequestBehavior.AllowGet);
        //}

        public ActionResult create()
        {
            return PartialView(new CategoryVM());
        }

        [HttpPost]
        public ActionResult create(CategoryVM cat )
        {

            


            Category category = new Category();
            category.Name = cat.Name;
            category.Description = cat.Description;
            db.Category.Add(category);
            db.SaveChanges();
            TempData["Message"] = "the new category added successfully";
            return RedirectToAction("index");
           
        }

        public ActionResult delete(int id)
        {
               try
            {
                Category cat = db.Category.Where(n => n.ID == id).FirstOrDefault();
                db.Category.Remove(cat);
                db.SaveChanges();
                TempData["Message"] = " the category deleted successfully";
               
                return RedirectToAction("index");
            }
            catch
            {
                TempData["Message"] = " the category has related data";
                return RedirectToAction("index");
            }
          
        }

        public ActionResult edit(int id)
        
       {
            Category category = db.Category.Where(n => n.ID == id).FirstOrDefault();
            CategoryVM VM = new CategoryVM();
            VM.ID = category.ID;
            VM.Name = category.Name;
            VM.Description = category.Description;
            return PartialView(VM);
        }
        [HttpPost]
        public ActionResult edit(CategoryVM cat )
        {
            Category c = db.Category.Where(n => n.ID ==cat.ID).FirstOrDefault();
            //if (cat != null)
            //{
                c.Name = cat.Name;
                c.Description = cat.Description;
                db.SaveChanges();
                TempData["Message"] = "the category updated successfully";
                //return Json(new { success = true, message = "saved succsesfully" }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("index");
            //}
            //else
            //{
            //    return PartialView();
            //}
        }
        public ActionResult details(int id)
        {
            Category cat = db.Category.Where(n => n.ID == id).FirstOrDefault();
         
            return PartialView(cat);
        }
    }
}

