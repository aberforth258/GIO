using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIO.Models;
using GIO.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace GIO.Services
{
    public class DriverService
    {

        private static GIOContext db = new GIOContext();

        //Driver calls
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverID"></param>
        /// <returns>Driver object based on provided Driver ID</returns>
        public static Driver GetDriver(long driverId)
        {
            Driver driver = db.Drivers.FirstOrDefault(d => d.DriverId == driverId);

            return driver;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all available drivers</returns>
        public static List<Driver> GetDrivers()
        {
            var drivers = new List<Driver>();

            drivers = (from d in db.Drivers
                       select d).ToList<Driver>();
            return drivers;
        }

        public static bool TryValidateDriver(DriverRecord driverRecord, out string[] feedback)
        {
            feedback = Array.Empty<string>();
            bool isDriverValid = false;

            List<ValidationResult> errors = new List<ValidationResult>();
            if (driverRecord != null && Validator.TryValidateObject(driverRecord, new ValidationContext(driverRecord), errors, true))
                isDriverValid = true;
            else
                feedback = errors.Select(e => e.ErrorMessage).ToArray();

            return isDriverValid;
        }

        public static T GetDriver<T>(Expression<Func<Driver, bool>> query, Expression<Func<Driver, T>> selector)
        {
            return db.Drivers.Where(query).Select(selector).FirstOrDefault();
        }
        public static IEnumerable<T> GetDrivers<T>(Expression<Func<Driver, bool>> query, Expression<Func<Driver, T>> selector)
        {
            return db.Drivers.Where(query).Select(selector).ToArray();
        }

        public static Driver CreateDriver(DriverRecord driverRecord,
                                                out string[] feedback
                                                )
        {
            feedback = Array.Empty<string>();
            List<ValidationResult> errors = new List<ValidationResult>();
            if (driverRecord.RequiresValidation)
            {
                
                if(Validator.TryValidateObject(driverRecord, new ValidationContext(driverRecord),errors, true))
                {
                    Driver driverOut = new Driver(driverRecord);

                    db.Drivers.Add(driverOut);
                    db.SaveChanges();
                    return driverOut;
                }
            }
            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;
        }

        public static string GetDriverName(long driverId) => db.Drivers.FirstOrDefault(d => d.DriverId == driverId).Name;
        #endregion
    }
}
