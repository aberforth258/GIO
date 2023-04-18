using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIO.Models;
using GIO.Interfaces;
using System.Linq.Expressions;

namespace GIO.Services
{
    public class TrailerService
    {
        private static GIOContext db = new GIOContext();

        //Trailer Calls
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trailerID"></param>
        /// <returns>Trailer object based on provided Trailer ID</returns>
        public static Trailer GetTrailer(long trailerId)
        {
            Trailer trailer = db.Trailers.FirstOrDefault(d => d.TrailerId == trailerId);

            return trailer;

        }

        public static T GetTrailer<T>(Expression<Func<Trailer, bool>> query, Expression<Func<Trailer, T>> selector)
        {
            return db.Trailers.Where(query).Select(selector).FirstOrDefault();
        }

        public static bool TryValidateTrailer(TrailerRecord trailerRecord, out string[] feeedback)
        {
            feeedback = null;
            List<ValidationResult> errors = new List<ValidationResult>();
            bool isTrailerValid = false;
            if (trailerRecord != null && Validator.TryValidateObject(trailerRecord, new ValidationContext(trailerRecord), errors))
                isTrailerValid = true;
            else
                feeedback = errors.Select(e => e.ErrorMessage).ToArray();

            return isTrailerValid;
        }

        public static IEnumerable<T> GetTrailers<T>(Expression<Func<Trailer, bool>> query, Expression<Func<Trailer, T>> selector)
        {
            return db.Trailers.Where(query).Select(selector).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all available Trailers</returns>
        public static List<Trailer> GetTrailers()
        {
            var trailers = new List<Trailer>();

            trailers = (from t in db.Trailers
                        select t).ToList<Trailer>();
            return trailers;
        }

        /// <summary>
        /// Validates and creates a new Trailer and saves it into DB
        /// </summary>
        /// <param name="trailerRecord">TrailerRecord instance to create</param>
        /// <param name="feedback">output error list</param>
        /// <returns></returns>
        public static Trailer CreateTrailer(TrailerRecord trailerRecord, out string[] feedback)
        {
            feedback = null;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(trailerRecord, new ValidationContext(trailerRecord), errors,true))
            {
                Trailer trailer = new Trailer()
                {
                    Name = trailerRecord.TrailerName,
                    TrailerTypeId = trailerRecord.TrailerTypeId,
                    HaulierId = trailerRecord.HaulierId
                };

                db.Trailers.Add(trailer);
                db.SaveChanges();
                return trailer;
            }
            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;
        }

        public static string GetTrailerName(long trailerId) => db.Trailers.FirstOrDefault(t => t.TrailerId == trailerId).Name;
    }
}
