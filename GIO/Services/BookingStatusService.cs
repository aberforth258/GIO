using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIO.Models;

namespace GIO.Services
{
    public class BookingStatusService
    {
        private static GIOContext db = new GIOContext();

        //BookingStatus calls
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns>BookingStatusID for Created Status</returns>
        public static long GetCreatedStatusId()
        {
            long status = (db.BookingStatuses.FirstOrDefault(s => s.Name == "Created")).BookingStatusId;

            return status;
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns>Vehicle object based on provided Vehicle ID</returns>
        
    }
}
