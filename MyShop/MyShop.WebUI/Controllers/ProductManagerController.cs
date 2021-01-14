using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.WebUI;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context; //created an instance of the product repository 

        public ProductManagerController() {  //create Controller to initiliaze public Repository 
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index() {
            List<Product> products = context.Collection().ToList();  //to be able to use
            return View(products);
        }

        //First we ake an ACTION RESULT for CREATE functionality for our product.
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        //We only need HTTP POST for the Second Create Function because we have nothing being made in the first one 
        //that has new product and then returns a view of product .
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        //In case we want to change the ID we will have it in this method for that purpose.
        //We only need HTTP POST for the Second EDIT Function because we have nothing being made in the first one 
        //that has new product and then returns a view of product if found however if not found we return NOT FOUND. 
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);          //object productToEdit will be used in order to allow for the class attributes to be defined.

            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        } 

        [HttpPost]  //We are using POST to send a request that we are deleting via HTTP?
        [ActionName("Delete")]  //Type of action that we are using is Delete to be sent via HTTP
        public ActionResult ConfirmDelete(string Id)
        {
            Product ProductToDelete = context.Find(Id); //Make ProductToDelete object which Finds the Id by the method Find of  the context of ID?
            if(ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id); // Delete the Product from the ID that is selected.
                context.Commit(); //Commit to the repository of the product with ID was deleted?
                return RedirectToAction("Index");
            }
        }



    }
}