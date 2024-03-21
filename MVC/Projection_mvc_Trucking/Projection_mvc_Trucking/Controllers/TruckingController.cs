
using Projection_mvc_Trucking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projection_mvc_Trucking.Controllers
{
    public class TruckingController : Controller
    {
        private readonly TruckingContext _db = new TruckingContext(); 
        
        #region(Drivers_mthds)
        public ActionResult IndexDrivers()
        {
            var Drivers = _db.Driver.ToList();
            return View(Drivers);
        }

        [HttpGet]
        public ActionResult AddDriver()
        {
            return View();
        }


        [HttpPost]
        [Route(Name = "AddDriver")]
        public ActionResult AddDriver(Driver driver)
        {
            _db.Driver.Add(driver);
            _db.SaveChanges();
            return RedirectToAction("IndexDrivers");
        }

        [HttpGet]
        public ActionResult DeleteDriver(int ID)//delete from orders
        {            
            Driver driver = _db.Driver.Find(ID);
            IEnumerable<Order> orders = _db.Order;
            orders = orders.Where(order => { return (order.Driver.First().ID == ID) || (order.Driver.Last().ID == ID); });
            if (orders.Count() > 0)
            {
                _db.Order.RemoveRange(orders);
            }            
            _db.Driver.Remove(driver);           
            _db.SaveChanges();
            return RedirectToAction("IndexDrivers");

        }


        [HttpGet]
        public ActionResult EditDriver(int id = 0)
        {
            Driver driver = _db.Driver.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }


        [HttpPost]
        public ActionResult EditDriver(Driver driver)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(driver).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("IndexDrivers");
            }
            return View(driver);
        }

        
        #endregion

        #region(Routes_mthds)

        public ActionResult IndexRoutes()
        {
            return View(_db.Route.ToList());
        }


        [HttpGet]
        public ActionResult AddRoute()
        {
            return View();
        }


        [HttpPost]
        [Route(Name = "AddRoute")]
        public ActionResult AddRoute(Route route)
        {
            _db.Route.Add(route);
            _db.SaveChanges();
            return RedirectToAction("IndexRoutes");
        }

        [HttpGet]
        public ActionResult DeleteRoute(int ID)
        {
            Route route = _db.Route.Find(ID);
            _db.Route.Remove(route);
            _db.SaveChanges();
            return RedirectToAction("IndexRoutes");

        }


        [HttpGet]
        public ActionResult EditRoute(int id = 0)
        {
            Route route = _db.Route.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }


        [HttpPost]
        public ActionResult EditRoute(Route route)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(route).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("IndexRoutes");
            }
            return View(route);
        }



        #endregion

        #region(Orders)
        
        public ActionResult IndexOrders()
        {
            List<Order> orders = _db.Order.ToList();
            uint Sum = 0;
            for(int i = 0; i < orders.Count; i++)
            {
                orders[i].SetPayment();
                foreach(var driverPay in orders[i].Bonus_Reward)
                {
                    Sum += driverPay.Value;
                }                
            }
            ViewBag.AllPaymentsSum = Sum;
            return View(orders);
        }

        [HttpGet]
        public ActionResult AddOrder()
        {           
            ViewBag.Routes = new SelectList(_db.Route, "ID", "Name");             
            ViewBag.Drivers = new MultiSelectList(_db.Driver, "ID", "LastName");
            OrderViewModel orderView = new OrderViewModel();
            return View(orderView);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderViewModel orderView)
        {
            ViewBag.Routes = new SelectList(_db.Route, "ID", "Name");            
            ViewBag.Drivers = new MultiSelectList(_db.Driver, "ID", "LastName");

            if (ModelState.IsValid)
            {
                foreach(int DriverId in orderView.SelectedDriversIDs)
                {
                    orderView.Order.Driver.Add(_db.Driver.Find(DriverId));
                }
                orderView.Order.Route = _db.Route.Find(orderView.Order.RouteID);
                orderView.Order.SetPayment();
                _db.Order.Add(orderView.Order);
                _db.SaveChanges();
                return RedirectToAction("IndexOrders");
            }
            else
            {
                return View(orderView);
            }
        }
       

        [HttpGet]
        public ActionResult DeleteOrder(int ID)
        {
            Order order = _db.Order.Find(ID);
            _db.Order.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("IndexOrders");

        }


        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            ViewBag.Routes = new SelectList(_db.Route, "ID", "Name");
            ViewBag.Drivers = new SelectList(_db.Driver, "ID", "LastName");
            OrderViewModel orderView = new OrderViewModel();
            Order order = _db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            orderView.Order = order;
                        
            return View(orderView);
        }


        [HttpPost]
        public ActionResult EditOrder(OrderViewModel orderView)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(orderView.Order).State = EntityState.Modified;
                orderView.Order.SetPayment();
                _db.SaveChanges();
                return RedirectToAction("IndexOrders");
            }
            return View(orderView);
        }

        public ActionResult DetailsOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }           
            ViewBag.Drivers = order.Driver;
            order.SetPayment();
            return View(order);

        }




        #endregion

    }
}