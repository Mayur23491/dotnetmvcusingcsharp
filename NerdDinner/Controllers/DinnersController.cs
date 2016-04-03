using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        //
        // GET: /Dinners/
        DinnerRepository repository = new DinnerRepository();
        public ActionResult Index()
        {
            var dinners = repository.FindUpcomingDinners().ToList();
            return View("Index", dinners);
        }

        public ActionResult Details(int id)
        {
            Dinner dinner = repository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");
            else
                return View("Details", dinner);
        }

        public ActionResult Create()
        {
            Dinner dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            return View(dinner);
        }

        [HttpPost]
        public ActionResult Create(FormCollection formValues) 
        {
            Dinner dinner = new Dinner();
            if(TryUpdateModel(dinner)) 
            {
            repository.Add(dinner);
            repository.Save();
            return RedirectToAction("Details", new {id=dinner.DinnerID});
            }
            return View(dinner);
        }

        public ActionResult Edit(int id)
        {
            Dinner dinner = repository.GetDinner(id);
            return View(dinner);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Dinner dinner = repository.GetDinner(id);
            UpdateModel(dinner);
            if (TryUpdateModel(dinner))
            {
                repository.Save();
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            return View(dinner);
        }

        public ActionResult Delete(int id) 
        {
            Dinner dinner = repository.GetDinner(id);
            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }

        [HttpPost]
        public ActionResult Delete(int id, string confirmButton) 
        {
            Dinner dinner = repository.GetDinner(id);
            if (dinner == null)
                return View("NotFound");
            repository.Delete(dinner);
            repository.Save();
            return View("Deleted");
        }
    }
}
