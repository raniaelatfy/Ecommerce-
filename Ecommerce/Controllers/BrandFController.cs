using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
namespace Ecommerce.Controllers
{
    public class BrandFController : Controller
    {
        EcommerceEntities2 db = new EcommerceEntities2();
        // GET: BrandF
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
            var brands = db.Brand.Select(n => new {
                ID = n.ID,
                Name = n.Name,
                
            });
          
            return Json(brands, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddEditDetails(string Trigger, int id = 0)
        {
            string message;
            bool success;
            if (Trigger == null)
            {
                ViewBag.Add = "Add";
                return PartialView();

            }
             if (Trigger == "Edit" )
            {
                ViewBag.Edit = "Edit";
                Brand brand = db.Brand.Where(n => n.ID == id).FirstOrDefault();
                Brand brands = new Brand();
                brands.ID = brand.ID;
                brands.Name = brand.Name;
                brands.Description = brand.Description;
                brands.Image = brand.Image;
                return PartialView(brands);
            }
            if (Trigger == "Details")
            {
                ViewBag.Details = "Details";
                Brand brand = db.Brand.Where(n => n.ID == id).FirstOrDefault();
                Brand brands = new Brand();
                brands.ID = brand.ID;
                brands.Name = brand.Name;
                brands.Description = brand.Description;
                brands.Image = brand.Image;
                return PartialView(brands);
            }
            else
            {
                return Json(success=false,message="wrong operation",JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AddEditDetails(FormCollection formcollection)
        {
            string message;
            bool success;

            var files = Request.Files;
            HttpPostedFileBase image = files.Get("Image");
          
          
            
            if (formcollection["ID"]== "")
            {
                Brand brands = new Brand();
                brands.Name = formcollection["Name"];
                brands.Description = formcollection["Description"];

                brands.Image = image.FileName;
                image.SaveAs(Server.MapPath($"~/Attach/Brand/" + image.FileName));
                db.Brand.Add(brands);
                db.SaveChanges();
                return Json(new { success = true, message = "creation done" }, JsonRequestBehavior.AllowGet);
            }
            
            else
                {
                var x = int.Parse(formcollection["ID"]);

                Brand brand = db.Brand.Where(n =>n.ID == x).FirstOrDefault();

                    brand.Name= formcollection["Name"];
                    brand.Description= formcollection["Description"] ;

                if (image.ContentLength != 0)
                {
                    image = files.Get("Image");

                    image.SaveAs(Server.MapPath($"~/Attach/Brand/" + image.FileName));
                    brand.Image = image.FileName;
                }
                //if (DB.SaveChanges() > -1)
                //    if (formcollection["Image"] != null)
                //    {
                //        formcollection["Image"] = image.FileName;
                //         brand.Image= formcollection["Image"] ;
                //        //brands.Image = image.FileName;
                //        image.SaveAs(Server.MapPath($"~/Attach/Brand/" + image.FileName));
                //    }
                //    else
                //    {
                //    formcollection["Image"] = image.FileName;
                // brand.Image=db.Brand.Where(n => n.ID == x).Select(n => n.Image).FirstOrDefault();
                //    //brand.Image = image.FileName;
                //    //brands.Image = image.FileName;
                //    image.SaveAs(Server.MapPath($"~/Attach/Brand/" + brand.Image));
                //}
                db.SaveChanges();
                    return Json(new { success = true, message = "Update  done" }, JsonRequestBehavior.AllowGet);

                }
            
           

                return Json(new { success = false, message = "wrong" }, JsonRequestBehavior.AllowGet);

            

        }
        public ActionResult Delete(int id)
        {
            string message = "";
            bool done;
            try
            {
                Brand brand = db.Brand.Where(n => n.ID == id).FirstOrDefault();
                db.Brand.Remove(brand);
                db.SaveChanges();


                return Json(new { done = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "this brand has related data..";
                done = false;
                return Json(new { done = done, message = message }, JsonRequestBehavior.AllowGet);

            }
        }
        
    }
}