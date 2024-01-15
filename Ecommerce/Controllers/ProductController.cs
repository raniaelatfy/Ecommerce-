using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Model;
using Ecommerce.Model.ViewModels;
namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        EcommerceEntities2 db = new EcommerceEntities2();
        // GET: Product
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
            var product=db.Product.Select(n => new {
                ID = n.ID,
                Name = n.Name,
                CategoryName = n.Category.Name,
                BrandName=n.Brand.Name,
                Description=n.ShortDescription,
                
                Price = n.Price
            });
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetImages()
        {
            return PartialView();
        }
        public JsonResult LoadImages(int id)
        {
            var productimages = db.ProductImage.Where(n => n.ProductFK == id).Select(n => new
            {

                Images = n.Image,
                ID = n.ID

            }).ToList();
            return Json(productimages, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddEditDetails(string Trigger,int id=0)
        {
            string message;
            bool done;
            if(id==0)
            {
                ViewBag.Add = "Add";
                ViewBag.categories = new SelectList(db.Category.ToList(), "ID", "Name");
                ViewBag.brands = new SelectList(db.Brand.ToList(), "ID", "Name");
                return PartialView(new ProductVM());
            }
            else
            {
                ViewBag.Trigger =Trigger ;
                Product product = db.Product.Find(id);
                List<Brand> brands = db.Brand.ToList();
                List<Category> categories = db.Category.ToList();
                ViewBag.brands = new SelectList(brands, "ID", "Name", product.BrandFK);
                ViewBag.categories = new SelectList(categories, "ID", "Name", product.CategoryFK);
                ProductVM productVM = new ProductVM();
                productVM.ID = product.ID;
                productVM.Name = product.Name;
                productVM.ShortDescription = product.ShortDescription;
                productVM.BrandFK = product.BrandFK;
                productVM.CategoryFK = product.CategoryFK;
                productVM.Description = product.Description;
                productVM.Price = product.Price;
                return PartialView(productVM);
            }
            return Json(new { done = false, message = "faild" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddEditDetails(ProductVM vm)
        {
            string message;
            bool done;
            if (vm.ID == 0)
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    product.ID = vm.ID;
                    product.Name = vm.Name;
                    product.ShortDescription = vm.ShortDescription;
                    product.CategoryFK = vm.CategoryFK;
                    product.BrandFK = vm.BrandFK;
                    product.Description = vm.Description;
                    product.Price = vm.Price;
                  db.Product.Add(product);
                    if (db.SaveChanges() > -1)
                    {
                        message = "Added Successfully!";
                        done = true;
                    }
                    else
                    {
                        message = "Failed to add!";
                        done = false;
                    }
                    return Json(new { done = done, message = message}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Product product = db.Product.Where(n => n.ID == vm.ID).FirstOrDefault();
                product.Name = vm.Name;
                product.ShortDescription = vm.ShortDescription;
                product.CategoryFK = vm.CategoryFK;
                product.BrandFK = vm.BrandFK;
                product.Description = vm.Description;
                product.Price = vm.Price;
                db.SaveChanges();

                return Json(new { done = true, message = "updated done" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }
        public ActionResult Delete(int id)
        {
            string message = "";
            bool done;
            try
            {
                Product product = db.Product.Where(n=>n.ID==id).FirstOrDefault();
                db.Product.Remove(product);
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
        public ActionResult AddImages(int id)
        {

            ViewBag.ID = id;
      
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddImages(FormCollection formCollection)
        {

            string message;
            bool success;
            int id = int.Parse(formCollection["ID"]);
            var files = Request.Files; 
            for (var i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase image = files[i];
                ProductImage product = new ProductImage();
                product.ProductFK = id;
                product.Image = image.FileName;
                image.SaveAs(Server.MapPath($"~/Attach/Product/" + image.FileName));
                db.ProductImage.Add(product);
                db.SaveChanges();

            }
            
         
         
            return Json(new { success = true, message = "Image done" }, JsonRequestBehavior.AllowGet);



        }
        public ActionResult SetMain( int id ,int ProductFK)
        {
            string message = "";
            bool done;
            List<ProductImage> ProductImages = db.ProductImage.Where(n => n.ProductFK==ProductFK && n.IsMine).ToList();
            foreach (var item in ProductImages)
            {
                item.IsMine = false;
            }

            ProductImage productimages = db.ProductImage.Find(id);
            productimages.IsMine = true;
            db.SaveChanges();
            return Json(new { done = true, message = "Done successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteImages(int id)
        {
            string message = "";
            bool done;
            try
            {
                ProductImage productimg = db.ProductImage.Where(n => n.ID == id).FirstOrDefault();
                db.ProductImage.Remove(productimg);
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