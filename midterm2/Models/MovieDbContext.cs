using System;
using Microsoft.EntityFrameworkCore;

namespace midterm2.Models
{
    public class MovieDbContext : DbContext
    {
        // this constructor doesn't do anything yet, just brings options into the context file
        public MovieDbContext (DbContextOptions<MovieDbContext> options) : base (options)
        {

        }

        // pulling Movie model into asp.net as a Movie item
        public DbSet<Movie> Movies { get; set; }
    }
}
