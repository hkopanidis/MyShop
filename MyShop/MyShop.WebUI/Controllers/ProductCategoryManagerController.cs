using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();

        }

        // GET: ProductCategoryManager

        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();

            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory ProductCategory = new ProductCategory();
            return View(ProductCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory ProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductCategory);
            }
            else
            {
                context.Insert(ProductCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory ProductCategory = context.Find(Id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory ProductCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(ProductCategory);
                }
                productCategoryToEdit.Category = ProductCategory.Category;
                
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            context.Delete(Id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}