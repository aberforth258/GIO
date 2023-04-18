using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GIO.Interfaces;
using GIO.Models;

namespace GIO.Services
{
    public class VehicleTypeService
    {
        private static GIOContext db = new GIOContext();

        //Vehicle Type calls

        /// <summary>
        /// 
        /// </summary>
        /// <returns>VehicleType object based on provided Vehicle Type ID</returns>
        public static VehicleType GetVehicleType(long vehType)
        {
            return db.VehicleTypes.FirstOrDefault(vt => vt.VehicleTypeId == vehType);
        }

        public static List<VehicleType> GetVehicleTypes()
        {
            return (from vt in db.VehicleTypes
                    select vt).ToList<VehicleType>();
        }

        public static T GetVehicleType<T>(Expression<Func<VehicleType, bool>> query, Expression<Func<VehicleType, T>> selector)
        {
            return db.VehicleTypes.Where(query).Select(selector).FirstOrDefault();
        }

        public static bool TryValidateVehicleType(VehicleTypeRecord vehicleTypeRecord, out string[] feedback)
        {
            feedback = null;
            bool isVehicleTypeValid = false;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(vehicleTypeRecord, new ValidationContext(vehicleTypeRecord), errors, true))
                isVehicleTypeValid = true;
            else
                feedback = errors.Select(e => e.ErrorMessage).ToArray();

            return isVehicleTypeValid;
        }   

        public static IEnumerable<T> GetVehicleTypes<T>(Expression<Func<VehicleType, bool>> query, Expression<Func<VehicleType, T>> selector)
        {
            return db.VehicleTypes.Where(query).Select(selector).ToArray();
        }
        public static string GetVehicleTypeName(long vehicleTypeId) => db.VehicleTypes.FirstOrDefault(v => v.VehicleTypeId == vehicleTypeId).Name;

        public static VehicleType CreateVehicleType(VehicleTypeRecord vehicleTypeRecord, out string[] feedback)
        {
            feedback = null;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(vehicleTypeRecord, new ValidationContext(vehicleTypeRecord), errors, true))
            {
                VehicleType vehicleType = new VehicleType()
                {
                    Name = vehicleTypeRecord.VehicleTypeName
                    
                };
                db.VehicleTypes.Add(vehicleType);
                db.SaveChanges();
                return vehicleType;
            }

            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;
        }


    }
}
