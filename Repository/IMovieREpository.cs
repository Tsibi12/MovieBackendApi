using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Repository
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();
        Task<List<Movie>> GetMoviesByRating();
        Task<Movie> GetMovieByID(int id);
        Task<Movie> AddEditMovie(Movie Movie);
        Task<Movie> DeleteMovie(int id);
    }
}