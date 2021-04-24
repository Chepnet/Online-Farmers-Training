using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Online_Farmers_Training.Models;

namespace Online_Farmers_Training.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly ApplicationDbContext _context;
        public ApplicationController(ILogger<ApplicationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }
        public IActionResult Index()
        {
            List<TrainingApplication> model = _context.TrainingApplications.ToList();

            return View(model);
        }
        public IActionResult ApplicationDetails(int? id)
        {
            TrainingApplication model = _context.TrainingApplications.Find(id);
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(int? ApplicationId, string EventName, string FirstName, string LastName, string email )
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
                    return RedirectToAction("Index");
                }

            }
            return View(Apply);
        }
    }
}
