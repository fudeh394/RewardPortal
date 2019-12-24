using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    public interface IReportService
    {

        List<UploadModel> ApprovedItems(ReportRequestModel model);
        List<UploadModel> AllItems(ReportRequestModel model);
        //List<UploadModel> SelectApprovedDudCheques(ReportRequestModel model);
        //List<UploadModel> SelectDeclinedDudCheques(ReportRequestModel model);
        byte[] ExportRequest(List<ReportModel> request);
        //byte[] ExportPreRequest(List<ReportModel> request);
    }
}
