using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHere.Models;
using ShopHere.ViewModels;

namespace ShopHere.Controllers
{
    [HandleError]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const byte pageSize = 4;


        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /*
         *  -------Admin Actions--------
         *
         */

        //To View All Items
        public ActionResult ViewAllItems(int? pageNo)
        {
            TempData.Remove("search");

            int pageNumber;
            if (TempData["PageNumber"] == null)
            {
                pageNumber = 1;
                TempData["PageNumber"] = 1;
            }
            else
            {
                if (pageNo == 1)
                {
                    pageNumber = Convert.ToInt32(TempData["PageNumber"]) + 1;
                    TempData["PageNumber"] = pageNumber;
                }
                else if (pageNo == -1)
                {
                    pageNumber = Convert.ToInt32(TempData["PageNumber"]) - 1;
                    TempData["PageNumber"] = pageNumber;
                }
                else
                {
                    pageNumber = 1;
                    TempData["PageNumber"] = pageNumber;
                }
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
                TempData["PageNumber"] = 1;
            }
            pageNumber = (pageNumber - 1) * pageSize;

            IEnumerable<Item> AllItems = _context.Items.Where(c =>c.AdminName == User.Identity.Name).OrderBy(item => item.Id).Skip(pageNumber).Take(pageSize).ToList();

            if (AllItems.Any() == false)
            {

                return View("ProductSearchEndedAdmin");
            }
            TempData.Keep();
            return View(AllItems);

        }
        public ActionResult SearchItemByCustomerAdminIndex(string search)
        {
            if (search == "")
            {
                return RedirectToAction("ViewAllItems");
            }
            TempData.Remove("search");
            return RedirectToAction("SearchItemByCustomerAdmin", new { Search = search });
        }

        public ActionResult SearchItemByCustomerAdmin(string search, int? pageNo)
        {

            if (TempData["search"] != null)
            {
                search = TempData["Search"].ToString();
            }

            int pageNumber;
            if (TempData["PageNumberSearch"] == null)
            {
                 pageNumber = 1;
                TempData["PageNumberSearch"] = 1;
            }
            else
            {
                if (pageNo == 1)
                {
                    pageNumber = Convert.ToInt32(TempData["PageNumberSearch"]) + 1;
                    TempData["PageNumberSearch"] = pageNumber;
                }
                else if (pageNo == -1)
                {
                    pageNumber = Convert.ToInt32(TempData["PageNumberSearch"]) - 1;
                    TempData["PageNumberSearch"] = pageNumber;
                }
                else
                {
                    pageNumber = 1;
                    TempData["PageNumberSearch"] = pageNumber;
                }
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
                TempData["PageNumberSearch"] = 1;
            }
            pageNumber = (pageNumber - 1) * pageSize;

            
            IEnumerable<Item> SearchedItems = _context.Items.Where(c => c.ItemName.Contains(search) && c.AdminName == User.Identity.Name).OrderBy(item => item.Id).Skip(pageNumber).Take(pageSize).ToList();
            if (SearchedItems.Any() == false)
            {

                return View("ProductSearchEnded");
            }
            TempData["search"] = search;
            TempData.Keep();
        
            
            return View(SearchedItems);

        }

        //Adding Item In View
        public ActionResult AddItemInView()
        {
            AddItemViewModel addItemModel = new AddItemViewModel
            {
                Categories = _context.Categories.ToList()
            };   
            
            return View(addItemModel);
        }

       

        //To Add New Item
        public ActionResult AddItemToDb(AddItemViewModel itemViewModel)
        {
            
            if (!ModelState.IsValid)
            {
                itemViewModel.Categories = _context.Categories.ToList();
                return View("AddItemInView", itemViewModel);
            }
            _context.Items.Add(itemViewModel.Item);
            _context.SaveChanges();
            return View(itemViewModel.Item);
        }

        //To Edit Item (Using Edit Button)
        public ActionResult EditItemInView(int id)
        {
            AddItemViewModel addItemModel = new AddItemViewModel
            {
                Item = _context.Items.Include("Category").SingleOrDefault(m => m.Id == id),
                Categories = _context.Categories.ToList()
            };


            
            if (addItemModel.Item == null)
            {
                return View("ProductSearchEnded");
            }
            return View(addItemModel);
        }

        //Edit In Database
        public ActionResult EditItemInDb(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View("EditItemInView", item);
            }

            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return View();

        }

        //Delete Item In Db (Delete Button)
        public ActionResult DeleteItemFromDb(int id)
        {
            Item removing = _context.Items.SingleOrDefault(m => m.Id == id);
            if (removing == null)
            {
                return View("ProductSearchEnded");
            }
            _context.Items.Remove(removing);
            _context.SaveChanges();
            return View();
        }
    }
}