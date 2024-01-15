using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
using Ecommerce.Model.ViewModels;
namespace Ecommerce.Controllers
{
    public class BrandController : Controller
    {

        EcommerceEntities2 db = new EcommerceEntities2();
        // GET: Brand
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
            var subcategory = db.Brand.Select(n => new {
                ID = n.ID,
                Name = n.Name,
                Description=n.Description,
                Image=n.Image
            });
            return Json(subcategory, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEditDetails(string Trigger, int id=0)
        {
            if(Trigger==null)
            {
                ViewBag.Add = "Add";
                 return PartialView(new BrandVM() );
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddEditDetails(BrandVM vm,string Trigger ,HttpPostedFileBase photo)
        {
            string message;
            bool done;
            if (Trigger==null)
            {
                Brand brand = new Brand();
                brand.ID = vm.ID;
                brand.Name = vm.Name;
                brand.Description = vm.Description;

                //photo.SaveAs(Server.MapPath($"~/Attach/photo:FileName"));
                vm.Image = photo.FileName;
                brand.Image = vm.Image;
                db.Brand.Add(brand);
                db.SaveChanges();
                if(db.SaveChanges()>-1)
                {
                    photo.SaveAs(Server.MapPath($"~/Attach/Brand/" + brand.Image));

                    done = true;
                    message = "Added successfully";
                }
                else
                {
                    done = false;
                    message = "failed";
                }
                return Json(new {done=done,message=message },JsonRequestBehavior.AllowGet);
            }
            return View();
        }
    }
}