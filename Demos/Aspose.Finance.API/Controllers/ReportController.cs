using Aspose.Finance.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Tools.Foundation.Models;

namespace Aspose.Finance.API.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class ReportController : ApiController
    {
        [HttpPost]
        [ActionName("Error")]
        public async Task<ReportResult> Error([FromBody]ReportModel model)
        {
            try
            {
                return ReportService.Submit(model);
            }
            catch (Exception ex)
            {
                NLogger.LogError(ex, "Report Error Forum");
                return new ReportResult()
                {
                    StatusCode = 500,
                    Status = ex.Message
                };
            }
        }
    }
}