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
    public static class CountryService
    {
        private static GIOContext db = new GIOContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>Country object based on provided Country ID</returns>
        public static Country GetCountry(long countryId)
        {
            Country country = db.Countries.FirstOrDefault(d => d.CountryId == countryId);

            return country;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all available Countries</returns>
        public static List<Country> GetCountries()
        {
            var countries = new List<Country>();

            countries = (from t in db.Countries
                        select t).ToList<Country>();
            return countries;
        }

        public static T GetCountry<T>(Expression<Func<Country, bool>> query, Expression<Func<Country, T>> selector)
        {
            return db.Countries.Where(query).Select(selector).FirstOrDefault();
        }
        public static IEnumerable<T> GetCountries<T>(Expression<Func<Country, bool>> query, Expression<Func<Country, T>> selector)
        {
            return db.Countries.Where(query).Select(selector).ToArray();
        }

        public static bool TryValidateCountry(CountryRecord countryRecord, out string[] feedback)
        {
            feedback = null;
            bool isCountryValid = false;
            List<ValidationResult> errors = new List<ValidationResult>();
            
            if (countryRecord != null && Validator.TryValidateObject(countryRecord, new ValidationContext(countryRecord), errors, true))
                isCountryValid = true;
            else
                feedback = errors.Select(e => e.ErrorMessage).ToArray();


            return isCountryValid;
        }

        /// <summary>
        /// Validates and creates a new Country and saves it into DB
        /// </summary>
        /// <param name="countryRecord">CountryRecord instance to create</param>
        /// <param name="feedback">output error list</param>
        /// <returns></returns>
        public static Country CreateCountry(CountryRecord countryRecord, out string[] feedback)
        {
            feedback = null;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(countryRecord, new ValidationContext(countryRecord), errors, true))
            {
                Country country = new Country()
                {
                    Name = countryRecord.CountryName,
                    CountryCode2 = countryRecord.CountryCode2,
                    CountryCode3 = countryRecord.CountryCode3
                };

                db.Countries.Add(country);
                db.SaveChanges();
                return country;
            }
            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;
        }
    }
}
