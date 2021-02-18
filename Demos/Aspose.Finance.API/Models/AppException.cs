using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspose.Finance.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public AppException(string message) : base(message)
        {
        }
    }
}