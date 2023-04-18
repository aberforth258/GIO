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
    public class VehicleService
    {
        private static GIOContext db = new GIOContext();

        //Vehicle Calls
        public static Vehicle GetVehicle(long vehicleID)
        {
            return db.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleID);
        }

        public static T GetVehicle<T>(Expression<Func<Vehicle, bool>> query, Expression<Func<Vehicle, T>> selector)
        {
            return db.Vehicles.Where(query).Select(selector).FirstOrDefault();
        }
        public static IEnumerable<T> GetVehicles<T>(Expression<Func<Vehicle, bool>> query, Expression<Func<Vehicle, T>> selector)
        {
            return db.Vehicles.Where(query).Select(selector).ToArray();
        }

        public static bool TryValidateVehicle(VehicleRecord vehicleRecord, out string[] feedback)
        {
            feedback = null;
            bool isVehicleValid = false;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (vehicleRecord != null && Validator.TryValidateObject(vehicleRecord, new ValidationContext(vehicleRecord), errors, true))
                isVehicleValid = true;
            else
                feedback = errors.Select(e => e.ErrorMessage).ToArray();

            return isVehicleValid;
        }   

        public static string GetVehicleRegPlate(long vehicleId) => db.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleId).RegPlate;

        public static Vehicle CreateVehicle(VehicleRecord vehicleRecord, out string[] feedback)
        {
            feedback = null;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(vehicleRecord, new ValidationContext(vehicleRecord), errors, true))
            {
                Vehicle vehicle = new Vehicle()
                {
                    RegPlate = vehicleRecord.RegPlate,
                    IsBanned = vehicleRecord.IsBanned,
                    VehicleTypeId = vehicleRecord.VehicleTypeId
                };
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return vehicle;
            }

            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;
        }


    }
}
