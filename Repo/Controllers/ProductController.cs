using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repo.Core;
using Repo.Core.Interface;
using System.Security.Cryptography.X509Certificates;

namespace Repo.Controllers
{
    public class ProductController : Controller
    {
       // private readonly IProductRepository _productRepo;
       private UnitOfWork unitOfWork=new UnitOfWork();
        public ProductController(IProductRepository productRepo)
        {
            //_productRepo = productRepo;
        }
        public async Task<IActionResult> Index(Product model)
        {
            // var products = await _productRepo.GetAll();
            var products = await unitOfWork.ProductRepository.GetAll(model);
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id=0)
        {
            if (id == 0)
            {
        return View (new Product());

            }else
            {
                Product product=await unitOfWork.ProductRepository.GetById(id);
           if(product != null)
                {
                    return View(product);
                }
                TempData["error message"]= $"Product details not found with Id:{id}";
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public async Task< IActionResult> CreateOrEdit(Product model) {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        await unitOfWork.ProductRepository.Add(model);
                        TempData["Successful Message"] = "Data added in Product Model";
                        
                    }
                    else
                    {
                        await unitOfWork.ProductRepository.Update(model);
                        TempData["Successful Message"] = "Data Updated in Product Model";

                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error message"] = "Error in model";
                    return View();
                }
            }
            catch(Exception ex){
                TempData["error message"]=ex.Message;
                return View();
            }
       

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = await unitOfWork.ProductRepository.GetById(id);
                if (product != null)
                {
                    await unitOfWork.ProductRepository.Delete(id);
                     TempData["Success Message"] = "Product Deleted Successfuly";
                    return RedirectToAction("Index");
                    

                }
            }
            catch(Exception ex)
            {
                TempData["error message"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["erroe message"] = $"product data not found with Id:{id}";
            return RedirectToAction("Index");


        }
        protected virtual void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
        //[HttpPost,ActionName("Delete")]
        //public async Task<IActionResult> DeleteCofirmed(int id)
        //{
        //    try
        //    {
        //        await _productRepo.Delete(id);
        //        TempData["Success Message"] = "Product Deleted Successfuly";
        //        return RedirectToAction("Index");

        //    }catch(Exception ex) {
        //        TempData["error message"] = ex.Message;
        //        return View();
            
        //    }
        //}
    }
}
