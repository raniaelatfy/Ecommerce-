using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
namespace Ecommerce.Controllers
{
    public class SubCategoryController : Controller
    {

        EcommerceEntities2 db = new EcommerceEntities2();
        // GET: SubCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            return PartialView();
        }
        public JsonResult LoadData()
        {

            var Subcategory = db.SubCategory.Select(x => new { ID = x.ID, Name = x.Name, CategoryName = x.Category.Name });
            return Json(Subcategory, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult LoadData()
        //{
        //    List<SubCategory> subcategory = db.SubCategory.ToList();
        //    return Json(subcategory, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Create()
        {
            ViewBag.category = new SelectList(db.Category.ToList(), "ID", "Name");
            return PartialView(new SubCategoryVM());
        }
            [HttpPost]
        public ActionResult Create(SubCategoryVM vm)
        {

            SubCategory subCategory = new SubCategory();
            subCategory.ID = vm.ID;
            subCategory.Name = vm.Name;
            subCategory.Description = vm.Description;
            subCategory.CategoryFK = vm.CategoryFK;
            db.SubCategory.Add(subCategory);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int id)
        {
            SubCategory subCategory = db.SubCategory.Where(n => n.ID == id).FirstOrDefault();
            SubCategoryVM vm = new SubCategoryVM();
      
            vm.Name = subCategory.Name;
            vm.Description = subCategory.Description;
            vm.CategoryFK = subCategory.CategoryFK;
            ViewBag.subcategory = new SelectList(db.Category.ToList(), "ID", "Name", subCategory.CategoryFK);
            return PartialView(vm);

        }

        [HttpPost]
        public ActionResult Edit(SubCategoryVM vm)
        {
            SubCategory subcategory = db.SubCategory.Where(n => n.ID == vm.ID).FirstOrDefault();
            
            subcategory.Name = vm.Name;
            subcategory.Description = vm.Description;
            subcategory.CategoryFK = vm.CategoryFK;
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(int id)
        {
            SubCategory subcategory = db.SubCategory.Where(n => n.ID == id).FirstOrDefault();
            ViewBag.subcategory = new SelectList(db.Category.ToList(), "ID", "Name", subcategory.CategoryFK);
            return PartialView(subcategory);


        }


        public ActionResult Delete(int id)
        {
            try
            {
                SubCategory subcategory = db.SubCategory.Where(n => n.ID == id).FirstOrDefault();
                db.SubCategory.Remove(subcategory);
                db.SaveChanges();
                return Json(new { success = true, message = "deleted succsesfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                return Json(new { success = false, message = "deleted fail" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}