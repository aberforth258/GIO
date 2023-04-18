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
    public class HaulierService
    {
        private static GIOContext db = new GIOContext();

        //Haulier Calls
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Haulier object based on provided Haulier ID</returns>
        public static Haulier GetHaulier(long haulierId)
        {
            Haulier haulier = db.Hauliers.FirstOrDefault(d => d.HaulierId == haulierId);

            return haulier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all available Hauliers</returns>
        public static List<Haulier> GetHauliers()
        {
            var hauliers = new List<Haulier>();

            hauliers = (from h in db.Hauliers
                        select h).ToList<Haulier>();
            return hauliers;
        }

        public static bool TryValidateHaulier(HaulierRecord haulierRecord, out string[] feedback)
        {
            feedback = null;
            bool isHaulierValid = false;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (haulierRecord != null && Validator.TryValidateObject(haulierRecord, new ValidationContext(haulierRecord), errors))
                isHaulierValid = true;
            else
                feedback = errors.Select(e => e.ErrorMessage).ToArray();

            return isHaulierValid;
        }

        public static T GetHaulier<T>(Expression<Func<Haulier, bool>> query, Expression<Func<Haulier, T>> selector)
        {
            return db.Hauliers.Where(query).Select(selector).FirstOrDefault();
        }
        public static IEnumerable<T> GetHauliers<T>(Expression<Func<Haulier, bool>> query, Expression<Func<Haulier, T>> selector)
        {
            return db.Hauliers.Where(query).Select(selector).ToArray();
        }

        public static Haulier CreateHaulier(HaulierRecord haulierRecord, out string[] feedback)
        {
            feedback = null;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(haulierRecord, new ValidationContext(haulierRecord), errors))
            {
                Haulier haulier = new Haulier()
                {
                    Name = haulierRecord.HaulierName,
                    AddressLine1 = haulierRecord.HaulierAddressLine1,
                    AddressLine2 = haulierRecord.HaulierAddressLine2,
                    City = haulierRecord.City,
                    PostCode = haulierRecord.PostCode,
                    CountryId = haulierRecord.CountryId.Value

                };

                db.Hauliers.Add(haulier);
                db.SaveChanges();
                return haulier;
            }

            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;

        }

        public static string GetHaulierName(long haulierId) => db.Hauliers.FirstOrDefault(h => h.HaulierId == haulierId).Name;
    }
}
