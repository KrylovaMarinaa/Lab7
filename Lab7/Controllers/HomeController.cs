using Lab7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace Lab7.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Доброго утра! " : "Доброго дня! ";
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {

            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guest)
        {
            if (ModelState.IsValid)
            {
                guest.AddGuests(guest);
                return View("Thanks", guest);
            }
            else

                return View();
        }

        
        public ViewResult Guests()
        {
             Guests guests = new Guests();

            return View(guests.GetGuests());
           // return View();

        }

        
        
    }
}