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
    public class TrailerTypeService
    {
        private static GIOContext db = new GIOContext();

        //Trailer Type calls

        public static List<TrailerType> GetTrailerTypes()
        {
            return (from tt in db.TrailerTypes
                    select tt).ToList<TrailerType>();
        }

        public static TrailerType GetTrailerType(long trailerType)
        {
            return db.TrailerTypes.FirstOrDefault(tt => tt.TrailerTypeId == trailerType);
        }

        public static string GetTrailerTypeName(long trailerTypeId)
        {
            return db.TrailerTypes.FirstOrDefault(tt => tt.TrailerTypeId == trailerTypeId).Name;
        }

        public static T GetTrailerType<T>(Expression<Func<TrailerType, bool>> query, Expression<Func<TrailerType, T>> selector)
        {
            return db.TrailerTypes.Where(query).Select(selector).FirstOrDefault();
        }

        public static bool TryValidateTrailerType(TrailerTypeRecord trailerTypeRecord, out string[] feedback)
        {
            feedback = null;
            bool isTrailerTypeValid = false;
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(trailerTypeRecord, new ValidationContext(trailerTypeRecord), errors, true))
                isTrailerTypeValid = true;
            else
                feedback = errors.Select(e => e.ErrorMessage).ToArray();

            return isTrailerTypeValid;
        }   

        public static IEnumerable<T> GetTrailerTypes<T>(Expression<Func<TrailerType, bool>> query, Expression<Func<TrailerType, T>> selector)
        {
            return db.TrailerTypes.Where(query).Select(selector).ToArray();
        }

        public static TrailerType CreateTrailerType(TrailerTypeRecord trailerTypeRecord, out string[] feedback)
        {
            feedback = null;
            //List<ValidationResult> errors = new List<ValidationResult>();
            //if (Validator.TryValidateObject(trailerTypeRecord, new ValidationContext(trailerTypeRecord), errors, true))
            if(TryValidateTrailerType(trailerTypeRecord, out feedback))
            {
                TrailerType trailer = new TrailerType()
                {
                    Name = trailerTypeRecord.TrailerTypeName,
                    Height = trailerTypeRecord.Height
                };

                db.TrailerTypes.Add(trailer);
                db.SaveChanges();
                return trailer;
            }

            return null;
        }
    }
}
