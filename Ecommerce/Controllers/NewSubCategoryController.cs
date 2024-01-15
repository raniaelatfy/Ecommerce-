using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
namespace Ecommerce.Controllers
{
    public class NewSubCategoryController : Controller
    {
        EcommerceEntities2 db = new EcommerceEntities2();
        // GET: NewSubCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            return PartialView();
        }

        public JsonResult LoadData()
        {
            var subcategory = db.SubCategory.Select(n => new { 
            ID=n.ID,
            Name=n.Name,
            CategoryName=n.Category.Name
            });
            return Json(subcategory, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAll()
        {
            return View(db.SubCategory.ToList());
        }
        public ActionResult GetById(int id)
        {
            SubCategory subCategory = db.SubCategory.Where(n => n.ID == id).FirstOrDefault();
            return View(subCategory);
        }


        //add&edit&details-action
        public ActionResult AddEditDetails(string Trigger, int id=0)
        {
            if(Trigger==null)
            {
                ViewBag.Add = "Add";
                ViewBag.category = new SelectList(db.Category.ToList(), "ID", "Name");
                return PartialView(new SubCategoryVM());
            }

            if(Trigger=="Edit")
            {
                ViewBag.Edit = "Edit";
                SubCategory subCategory = db.SubCategory.Where(n => n.ID == id).FirstOrDefault();
                SubCategoryVM vm = new SubCategoryVM();
                vm.ID = subCategory.ID;
                vm.Name = subCategory.Name;
                vm.Description = subCategory.Description;
                vm.CategoryFK = subCategory.CategoryFK;
                ViewBag.category = new SelectList(db.Category.ToList(), "ID", "Name", subCategory.CategoryFK);
                return PartialView(vm);
            }

            if(Trigger=="Details")
            {
                ViewBag.Details = "Details";
                SubCategory subCategory = db.SubCategory.Where(n => n.ID == id).FirstOrDefault();
                SubCategoryVM vm = new SubCategoryVM();
                vm.ID = subCategory.ID;
                vm.Name = subCategory.Name;
                vm.Description = subCategory.Description;
                vm.CategoryFK = subCategory.CategoryFK;
                ViewBag.category = new SelectList(db.Category.ToList(), "ID", "Name", subCategory.CategoryFK);
                return PartialView(vm);
            }


            return Json(new { success = false, message = "wrong" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEditDetails(SubCategoryVM vm,string Trigger)
        {
           
            if(Trigger==null)
            {
                SubCategory subCategory = new SubCategory();
                subCategory.ID = vm.ID;
                subCategory.Name = vm.Name;
                subCategory.Description = vm.Description;
                subCategory.CategoryFK = vm.CategoryFK;
                db.SubCategory.Add(subCategory);
                db.SaveChanges();
                return Json(new {success=true,message="creation done" },JsonRequestBehavior.AllowGet);
            }
            if(Trigger=="Edit")
            {
                SubCategory subcategory = db.SubCategory.Where(n => n.ID == vm.ID).FirstOrDefault();

                subcategory.Name = vm.Name;
                subcategory.Description = vm.Description;
                subcategory.CategoryFK = vm.CategoryFK;
                db.SaveChanges();
                return Json(new { success = true, message = "Update  done" },JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "wrong" }, JsonRequestBehavior.AllowGet);
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