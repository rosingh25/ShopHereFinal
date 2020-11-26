using ShopHere.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHere.Controllers
{
    [HandleError]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Order
        
        public ActionResult Index()
        {   
            IEnumerable<Order> AllOrders = _context.Orders.Include("Item").Where(c => c.CustomerName == User.Identity.Name).OrderByDescending(o => o.OrderId).ToList();
            return View(AllOrders);
        }

        
        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllOrders(int? id)
        {
            IEnumerable<Order> AllOrders;
            if (id != null)
            {
                AllOrders = _context.Orders.Include("Item").Where(o => o.OrderId == id && o.Item.AdminName == User.Identity.Name).OrderByDescending(o => o.OrderId).ToList();
                return View(AllOrders);
            }
            AllOrders = _context.Orders.Include("Item").Where(o => o.Item.AdminName == User.Identity.Name).OrderByDescending(o => o.OrderId).ToList();
            return View(AllOrders);
            
        }
        public ActionResult OrderPlaced(Order order)
        {
            return View(order);
        }
        public ActionResult OrderStatusChange(int id)
        {
            Order Order = _context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            Order.IsDelivered = !Order.IsDelivered;
            _context.SaveChanges();
            return RedirectToAction("ViewAllOrders");
        }

        
    }

}