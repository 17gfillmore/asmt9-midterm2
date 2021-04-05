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
                    // passes the movie object so ViewBag can use it to display title
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

        [HttpGet]
                                        // this value comes from the name="movieId" in the form submit
        public IActionResult Update(int movieId)
        {
            Movie movie = context.Movies
                .Where(m => m.MovieId == movieId).FirstOrDefault();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Update(Movie movie, int movieId)
        {

            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Title = movie.Title;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Year = movie.Year;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Director = movie.Director;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Category = movie.Category;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Rating = movie.Rating;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Edited = movie.Edited;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().LentTo = movie.LentTo;
            context.Movies.Where(m => m.MovieId == movieId).FirstOrDefault().Notes = movie.Notes;

            context.SaveChanges();

            return RedirectToAction("MovieList");
        }

        public IActionResult Delete(int movieId)
        {
            // set a movie item equal to the movie in the context with the same movieId as passsed from the view
            Movie movie = context.Movies
                .Where(m => m.MovieId == movieId).FirstOrDefault();

            context.Movies.Remove(movie);
            context.SaveChanges();

            //return View("AddMovie");
            return RedirectToAction("MovieList");
            // ^Todo: redirect to MovieList instead when you can figure out nullreference exception
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
