using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Online_Farmers_Training.Models;
using PagedList;

namespace Online_Farmers_Training.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            List<Training> model = _context.Trainings.ToList();

            return View(model);
        }
        public IActionResult Viewdetails(int? id)
        {
            Training model = _context.Trainings.Find(id);
            return View(model);
           
        }
        public IActionResult Trainings()

        {

            List<Training> model = _context.Trainings.ToList();

            return View(model);

        }
        public ActionResult Search(string option, string search, int? pageNumber, string sort)
        {

            //if the sort parameter is null or empty then we are initializing the value as descending name  
            ViewBag.sortByEventName = string.IsNullOrEmpty(sort) ? "descending Event" : "";
            //if the sort value is gender then we are initializing the value as descending gender  
            ViewBag.SortByTrainerName = sort == "TrainerName" ? "descending TrainerName" : "TrainerName";
            ViewBag.SortByEventName = sort == "EventName" ? "descending EventName" : "EventName";
            ViewBag.SortByCost = sort == "Cost" ? "descending Cost" : "Cost";
            ViewBag.SortByTrainingTime = sort == "TrainingTime" ? "descending TrainingTime" : "TrainingTime";
            //here we are converting the db.Students to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = _context.Trainings.AsQueryable();

            if (option == "TrainingTime")
            {
                records = records.Where(x => x.TrainingTime.ToString() == search || search == null);
            }
            else if (option == "TrainerName")
            {
                records = records.Where(x => x.TrainerName == search || search == null);
            }
            else
            {
                records = records.Where(x => x.EventName.StartsWith(search) || search == null);
            }

            switch (sort)
            {

                case "descending EventName":
                    records = records.OrderByDescending(x => x.EventName);
                    break;
                case "descending Cost":
                    records = records.OrderByDescending(x => x.Cost);
                    break;

                case "descending TrainerName":
                    records = records.OrderByDescending(x => x.TrainerName);
                    break;
                case "descending TrainingTime":
                    records = records.OrderByDescending(x => x.TrainingTime);
                    break;

                case "TrainerName":
                    records = records.OrderBy(x => x.TrainerName);
                    break;

                case "TrainingTime":
                    records = records.OrderBy(x => x.TrainingTime);
                    break;

                case "Cost":
                    records = records.OrderBy(x => x.Cost);
                    break;
                default:
                    records = records.OrderBy(x => x.EventName);
                    break;
            }

            return View(records.ToPagedList(pageNumber ?? 1, 20));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int? TrainingId, string TrainerName, string EventName, string Description, DateTime? TrainingTme,decimal? Cost)
        {

            Training trainings = new Training();
            if (ModelState.IsValid)
            {
                trainings.TrainerName = TrainerName;
                trainings.EventName = EventName;
                trainings.Description = Description;
                trainings.TrainingTime = (DateTime)TrainingTme;
                trainings.Cost = (decimal)Cost;

                _context.Trainings.Add(trainings);
           return RedirectToAction("Index");
            }

            return View(trainings);
        }
        public IActionResult add(int? TrainingId, string TrainerName, string EventName, string Description, DateTime? TrainingTme, decimal? Cost)
        {


            Training trainings = new Training();
           
           
            if (ModelState.IsValid)
            {
                TrainingTme = DateTime.Now;
                Cost =0;
                trainings.TrainerName = TrainerName;
                trainings.EventName = EventName;
                trainings.Description = Description;
                trainings.TrainingTime = (DateTime)TrainingTme;
               trainings.Cost = (decimal)Cost;

                
                    if (TrainerName!=null)        
                    {
                        _context.Trainings.Add(trainings);
                        _context.SaveChanges();
                        return RedirectToAction("Trainings");
                    }
              
            }
            return View(trainings);
        }
       
        public IActionResult Apply()
        {


           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(int? ApplicationId, string EventName, string FirstName, string LastName, string email)
        {


            TrainingApplication Apply = new TrainingApplication();


            if (ModelState.IsValid)
            {

                if (EventName != null)
                {

                    Apply.EventName = EventName;
                    Apply.FirstName = FirstName;
                    Apply.LastName = LastName;
                    Apply.email = email;



                    _context.TrainingApplications.Add(Apply);
                    _context.SaveChanges();
                    return RedirectToAction("Index","Application");
                }

            }
            return View(Apply);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
