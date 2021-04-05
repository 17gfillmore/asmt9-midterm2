using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using midterm2.Models;

namespace midterm2.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private MovieDbContext context { get; set; }

        // constructor - set when the home controller is built for the first time
        public HomeController(MovieDbContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        // overloaded methods - action with same name, but two different
        // attributes (get and post) that return different results
        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            // only update the database for correct Movie objects
            if(ModelState.IsValid)
            {
                context.Movies.Add(movie);
                context.SaveChanges();

                return View("Confirmation", movie);
            }
            else
            {
                return View();
            }
        }

        public IActionResult MovieList()
        {
            return View(context.Movies);
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
