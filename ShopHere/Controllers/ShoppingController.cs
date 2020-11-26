using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHere.Models;
using ShopHere.ViewModels;

namespace ShopHere.Controllers
{
    [HandleError]
    public class ShoppingController : Controller
    {
        public const byte pageSize = 4;
        public int pageNumberInit = 1;
        private ApplicationDbContext _context;

        public ShoppingController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Shopping
        [AllowAnonymous]
        public ActionResult Index(int? pageNo,string filter)
        {
            if (filter ==null &&TempData["filter"] != null)
            {
                filter = TempData["filter"].ToString();
            }
            TempData.Remove("pricefilter");
            TempData.Remove("search");
            TempData.Remove("category");
            int pageNumber;
            if(TempData["PageNumber"]==null)
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
                else if(pageNo == -1)
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
            
            if(pageNumber < 1)
            {
                pageNumber = 1;
                TempData["PageNumber"] = 1;
            }
            pageNumber = (pageNumber - 1) * pageSize;
            IEnumerable<Item> AllItems;
            
            if (filter != null)
            {
                if (filter == "1")
                {
                    TempData["filter"] = "1";
                    AllItems = _context.Items.OrderByDescending(item => item.Price).Skip(pageNumber).Take(pageSize).ToList();
                }
                else
                {
                    TempData["filter"] = "2";
                    AllItems = _context.Items.OrderBy(item => item.Price).Skip(pageNumber).Take(pageSize).ToList();
                }
            }
            else
            {
                 AllItems = _context.Items.OrderBy(item => item.Id).Skip(pageNumber).Take(pageSize).ToList();
            }
            
            
             if(AllItems.Any() == false)
            {

                return View("ProductSearchEnded");
            }
            TempData.Keep();
            return View(AllItems);
        }
        public ActionResult ShoppingPage()
        {
            return View();
        }

        /*
         *  -------Customer Actions--------
         *
         */
        [AllowAnonymous]



        public ActionResult SearchItemByCustomerIndex(string search, string Filter)
        {
            if(search == "")
            {
               return RedirectToAction("Index");

            }
            TempData.Remove("search");
            return RedirectToAction("SearchItemByCustomer",new { search = search, filter = Filter });
        }
        [AllowAnonymous]
        public ActionResult SearchItemByCustomer(string search,int? pageNo,string filter)
        {
            if (filter == null && TempData["pricefilter"] != null)
            {
                filter = TempData["pricefilter"].ToString();
            }
            TempData.Remove("filter");

            TempData.Remove("category");
            if (TempData["search"] != null)
            {
                search = TempData["Search"].ToString();
            }

            int pageNumber;
            if (TempData["PageNumberSearch"] == null)
            {
                Debug.WriteLine("EnteredSearch");
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
            IEnumerable<Item> SearchedItems;
            if (filter != null)
            {
                if (filter == "1")
                {
                    TempData["pricefilter"] = "1";
                    SearchedItems = _context.Items.Where(c => c.ItemName.Contains(search)).OrderByDescending(item => item.Price).Skip(pageNumber).Take(pageSize).ToList();
                }
                else
                {
                    TempData["pricefilter"] = "2";
                    SearchedItems = _context.Items.Where(c => c.ItemName.Contains(search)).OrderBy(item => item.Price).Skip(pageNumber).Take(pageSize).ToList();
                }
            }
            else
            {
                SearchedItems = _context.Items.Where(c => c.ItemName.Contains(search)).OrderBy(item => item.Id).Skip(pageNumber).Take(pageSize).ToList();
            }
            if (SearchedItems.Any() == false)
            {

                return View("ProductSearchEnded");
            }
            TempData["search"] = search;
            TempData.Keep();
            return View(SearchedItems);
        }
        [AllowAnonymous]
        public ActionResult CategoryFilter(string category,int? pageNo)
        {
            TempData.Remove("filter");
            TempData.Remove("search");
            if(pageNo == null)
            {
                TempData.Remove("category");
            }
            if (TempData["category"] != null)
            {
                category = TempData["category"].ToString();
            }

            int pageNumber;
            if (TempData["PageNumberFilter"] == null)
            {
                Debug.WriteLine("EnteredSearch");
                pageNumber = 1;
                TempData["PageNumberFilter"] = 1;
            }
            else
            {
                if (pageNo == 1)
                {
                    pageNumber = Convert.ToInt32(TempData["PageNumberFilter"]) + 1;
                    TempData["PageNumberFilter"] = pageNumber;
                }
                else if (pageNo == -1)
                {
                    pageNumber = Convert.ToInt32(TempData["PageNumberFilter"]) - 1;
                    TempData["PageNumberFilter"] = pageNumber;
                }
                else
                {
                    pageNumber = 1;
                    TempData["PageNumberFilter"] = pageNumber;
                }
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
                TempData["PageNumberFilter"] = 1;
            }
            pageNumber = (pageNumber - 1) * pageSize;


            IEnumerable<Item> SearchedItems = _context.Items.Where(c => c.Category.CategoryName== category).OrderBy(item => item.Id).Skip(pageNumber).Take(pageSize).ToList();
            if (SearchedItems.Any() == false)
            {

                return View("ProductSearchEnded");
            }
            TempData["category"] = category;
            TempData.Keep();
            return View(SearchedItems);
        }

        public ActionResult BuyItem(int? Id, int? Quantity)
        {
            if( Id ==null )
            {
                return RedirectToAction("Index");
            }
            if(Quantity == null)
            {
                return View("ItemNotAvailable");
            }
            int id = Id ?? 0;
            Item BuyItem = _context.Items.SingleOrDefault(m => m.Id == id);
            if(BuyItem == null)
            {
                return View("InvalidIdView");
            }
            if(BuyItem.Quantity < Quantity)
            {
                return Content("NotAvailable");
            }
            var CurrentUser =_context.Users.Where(user => user.UserName == User.Identity.Name).FirstOrDefault();
            Order CurrentOrder = new Order
            {
                CustomerName = User.Identity.Name,
                Address = CurrentUser.Address,
                OrderPlacedTime = DateTime.Now,
                ItemId = id,
                IsDelivered = false
            };
            BuyItem.Quantity -= Quantity??0;
            _context.Orders.Add(CurrentOrder);
            _context.SaveChanges();
            return RedirectToAction("OrderPlaced","Order",CurrentOrder);
        }


        
        
    }
}