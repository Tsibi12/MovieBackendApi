using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Movies.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration _config;

        //Dependency Injection
        public MovieRepository(IConfiguration config)
        {
            _config = config;
        }

        // Getting db connection
        public IDbConnection connection {
            get {
                return new SqlConnection(_config.GetConnectionString("MovieSqlConnection"));
            }
        }

        // Get all movies
        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string Query = "USP_Movies";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ACTION", "A");
                    var result = await con.QueryAsync<Movie>(Query, param, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //Grouping movies by rating 
        public async Task<List<Movie>> GetMoviesByRating()
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    
                    var Query = @"SELECT COUNT(Id) as TotalRows, Rating
                                    FROM Movies
                                    GROUP By Rating;";

                    var result =  await con.QueryAsync<Movie>(Query);
                    return result.ToList();
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Deleting Movie
        public async Task<Movie> DeleteMovie(int id)
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string Query = "USP_Movies";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ACTION", "D");
                    param.Add("@ID", id);
                    var result = await con.QueryAsync<Movie>(Query, param, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //Adding or update movie
        public async Task<Movie> AddEditMovie(Movie movie)
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string sQuery = "USP_Movies";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ACTION", "E");
                    param.Add("@ID", movie.Id);
                    param.Add("@NAME", movie.Name);
                    param.Add("@CATEGORY", movie.Category);
                    param.Add("@RATING", movie.Rating);
            
                    var result = await con.QueryAsync<Movie>(sQuery, param, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get movie by Id
        public async Task<Movie> GetMovieByID(int id)
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string sQuery = "USP_Movies";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ACTION", "G");
                    param.Add("@Id", id);
                    var result = await con.QueryAsync<Movie>(sQuery, param, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
