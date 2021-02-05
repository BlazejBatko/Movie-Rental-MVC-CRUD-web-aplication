using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WypozyczalniaFilmow.Models
{
    public class FilmContext : DbContext /*Dependency injection  wstrzykiwanie usługi do konstruktora*/
    {
        public FilmContext(DbContextOptions<FilmContext> options) : base(options)
        {

        }

        public DbSet<Film> Film { get; set; }
    }
}
